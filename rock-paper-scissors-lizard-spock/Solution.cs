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
using System.Linq;
using System.Text.RegularExpressions;

namespace rock_paper_scissors_lizard_spock
{
    internal enum Sign
    {
        Rock,
        Paper,
        Scissors,
        Lizard,
        Spock
    }

    internal static class Solution
    {
        private static Dictionary<string, Sign> SignMapping { get; } = new Dictionary<string, Sign>
        {
            { "R", Sign.Rock },
            { "P", Sign.Paper },
            { "C", Sign.Scissors },
            { "L", Sign.Lizard },
            { "S", Sign.Spock }
        };

        private static Dictionary<Sign, Sign[]> SignPrecedence { get; }
            = new Dictionary<Sign, Sign[]>
            {
                { Sign.Scissors, new[] { Sign.Paper, Sign.Lizard } },
                { Sign.Paper, new[] { Sign.Rock, Sign.Spock } },
                { Sign.Rock, new[] { Sign.Lizard, Sign.Scissors } },
                { Sign.Lizard, new[] { Sign.Spock, Sign.Paper } },
                { Sign.Spock, new[] { Sign.Scissors, Sign.Rock } }
            };

        private static Regex ParticipantRegex { get; }
            = new Regex($@"^(?<number>\d+) (?<sign>[{string.Join("", SignMapping.Keys)}])$");

        public static void Main()
        {
            List<(int Number, Sign Sign, List<int> Fought)> participants = GetInput();
            while (participants.Count > 1)
            {
                var winners = new List<(int Number, Sign Sign, List<int> Fought)>();
                for (int i = 0; i < participants.Count; i += 2)
                {
                    (int Number, Sign Sign, List<int> Fought)
                        participantA = participants[i],
                        participantB = participants[i + 1],
                        winner,
                        loser;

                    if (participantA.Sign == participantB.Sign)
                    {
                        if (participantA.Number < participantB.Number)
                        {
                            winner = participantA;
                            loser = participantB;
                        }
                        else
                        {
                            winner = participantB;
                            loser = participantA;
                        }
                    }
                    else if (SignPrecedence[participantA.Sign].Contains(participantB.Sign))
                    {
                        winner = participantA;
                        loser = participantB;
                    }
                    else
                    {
                        winner = participantB;
                        loser = participantA;
                    }

                    winner.Fought.Add(loser.Number);
                    winners.Add(winner);
                }

                participants = winners;
            }

            Console.WriteLine(participants[0].Number);
            Console.WriteLine(string.Join(" ", participants[0].Fought));
        }

        private static List<(int Number, Sign Sign, List<int> Fought)> GetInput()
        {
            int participantCount = int.Parse(Console.ReadLine());

            var participants = new List<(int Number, Sign Sign, List<int> Fought)>();
            for (int p = 0; p < participantCount; p++)
            {
                Match participantMatch = ParticipantRegex.Match(Console.ReadLine());
                participants.Add((
                    int.Parse(participantMatch.Groups["number"].Value),
                    SignMapping[participantMatch.Groups["sign"].Value],
                    new List<int>()
                ));
            }

            return participants;
        }
    }
}
