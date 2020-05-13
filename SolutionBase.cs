/*
 * Copyright © 2020 Michał Przybyś <michal@przybys.eu>
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy of
 * this software and associated documentation files (the "Software"), to deal in
 * the Software without restriction, including without limitation the rights to
 * use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
 * of the Software, and to permit persons to whom the Software is furnished to do
 * so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 *
 * Distributions of all or part of the Software intended to be used by the
 * recipients as they would use the unmodified Software, containing modifications
 * that substantially alter, remove, or disable functionality of the Software,
 * outside of the documented configuration mechanisms provided by the Software,
 * shall be modified such that the Original Author's bug reporting email addresses
 * and urls are either replaced with the contact information of the parties
 * responsible for the changes, or removed entirely.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Xunit;

namespace CodinGame
{
    /// <summary>
    /// Base class for solution a solution, which runs its tests.
    /// </summary>
    /// <typeparam name="TSolution">The type of the solution class.</typeparam>
    public abstract class SolutionBase<TSolution>
    {
        /// <summary>
        /// Loads test data from files and returns it.
        /// </summary>
        /// <returns>Test data.</returns>
        public static IEnumerable<object[]> TestData()
        {
            Assembly assembly = typeof(TSolution).Assembly;
            string testsNamespace = $"{typeof(TSolution).Namespace}.Tests.";
            Regex testNameRegex = new Regex(
                $@"^{Regex.Escape(testsNamespace)}(?<number>\d+)\.(input|output).txt$"
            );

            int testsCount = assembly.GetManifestResourceNames()
                .Where(name => testNameRegex.IsMatch(name))
                .Select(name => int.Parse(testNameRegex.Match(name).Groups["number"].Value))
                .Max();

            return Enumerable.Range(1, testsCount)
                .Select(i =>
                {
                    using (Stream inputStream = assembly.GetManifestResourceStream(
                        $"{testsNamespace}{i}.input.txt"
                    ))
                    using (StreamReader inputReader = new StreamReader(inputStream))
                    using (Stream outputStream = assembly.GetManifestResourceStream(
                        $"{testsNamespace}{i}.output.txt"
                    ))
                    using (StreamReader outputReader = new StreamReader(outputStream))
                    {
                        string input = inputReader.ReadToEnd().TrimEnd('\n');
                        string output = outputReader.ReadToEnd().TrimEnd('\n');

                        return new object[] { input, output };
                    }
                });
        }

        /// <summary>
        /// A wrapper which replaces stdin and stdout when running the solution.
        /// </summary>
        /// <param name="input">The input data.</param>
        /// <param name="output">The expected output.</param>
        [Theory]
        [MemberData(nameof(TestData))]
        public void Test(string input, string output)
        {
            using (var inReader = new StringReader(input))
            using (var outWriter = new StringWriter())
            {
                TextReader standardInput = Console.In;
                TextWriter standardOutput = Console.Out;

                MethodInfo method = typeof(TSolution).GetMethod(
                    "Main",
                    BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public
                );

                Console.SetIn(inReader);
                Console.SetOut(outWriter);
                try
                {
                    method.Invoke(null, null);
                }
                finally
                {
                    Console.SetOut(standardOutput);
                    Console.SetIn(standardInput);
                }

                Assert.Equal(output, outWriter.ToString().TrimEnd('\n'));
            }
        }
    }
}
