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
using System.Reflection;
using System.Threading;
using Xunit.Runners;

namespace CodinGame
{
    /// <summary>
    /// A test runner which runs tests from current assembly programmatically (based on
    /// https://github.com/xunit/samples.xunit/blob/master/TestRunner/Program.cs).
    /// </summary>
    internal static class Program
    {
        private static int Main()
        {
            object consoleLock = new object();
            int result = 0;

            using (AssemblyRunner runner = AssemblyRunner.WithoutAppDomain(
                Assembly.GetExecutingAssembly().Location
            ))
            using (ManualResetEvent finished = new ManualResetEvent(false))
            {
                runner.OnDiscoveryComplete = info =>
                {
                    lock (consoleLock)
                    {
                        Console.WriteLine(
                            $"Running {info.TestCasesToRun} of {info.TestCasesDiscovered} tests..."
                        );
                    }
                };
                runner.OnExecutionComplete = info =>
                {
                    lock (consoleLock)
                    {
                        Console.WriteLine(
                            "Finished: {0} tests in {1}s ({2} failed, {3} skipped)",
                            info.TotalTests,
                            Math.Round(info.ExecutionTime, 3),
                            info.TestsFailed,
                            info.TestsSkipped
                        );
                    }

                    finished.Set();
                };
                runner.OnTestFailed = info =>
                {
                    lock (consoleLock)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;

                        Console.WriteLine(
                            "[FAIL] {0}: {1}",
                            info.TestDisplayName,
                            info.ExceptionMessage
                        );
                        if (info.ExceptionStackTrace != null)
                        {
                            Console.WriteLine(info.ExceptionStackTrace);
                        }

                        Console.ResetColor();
                    }

                    result = 1;
                };
                runner.OnTestSkipped = info =>
                {
                    lock (consoleLock)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;

                        Console.WriteLine("[SKIP] {0}: {1}", info.TestDisplayName, info.SkipReason);

                        Console.ResetColor();
                    }
                };

                Console.WriteLine("Discovering...");
                runner.Start(null);
                finished.WaitOne();

                return result;
            }
        }
    }
}
