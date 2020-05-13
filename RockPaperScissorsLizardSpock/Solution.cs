// https://www.codingame.com/training/easy/rock-paper-scissors-lizard-spock
// https://github.com/teddybeermaniac/CodinGame/tree/master/RockPaperScissorsLizardSpock
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
#if OUTSIDE_CODINGAME
using CodinGame;
#endif

namespace RockPaperScissorsLizardSpock
{
    /// <summary>
    /// A sign selected by player.
    /// </summary>
    internal enum Sign
    {
        /// <summary>
        /// A rock sign, beats lizard and scissors.
        /// </summary>
        Rock = 82,

        /// <summary>
        /// A paper sign, beats rock and spock.
        /// </summary>
        Paper = 80,

        /// <summary>
        /// A scissors sign, beats paper and lizard.
        /// </summary>
        Scissors = 67,

        /// <summary>
        /// A lizard sign, beats spock and paper.
        /// </summary>
        Lizard = 76,

        /// <summary>
        /// A spock sign, beats scissors and rock.
        /// </summary>
        Spock = 83,
    }

    /// <summary>
    /// The solution.
    /// </summary>
#if OUTSIDE_CODINGAME
    public class Solution : SolutionBase<Solution>

#else
    public class Solution
#endif
    {
        /// <summary>
        /// Runs the solution.
        /// </summary>
        public static void Main()
        {
            List<Player> players = ReadInput().ToList();
            while (players.Count > 1)
            {
                var winners = new List<Player>();
                for (int player = 0; player < players.Count; player += 2)
                {
                    winners.Add(Player.Fight(players[player], players[player + 1]));
                }

                players = winners;
            }

            Console.WriteLine(
                $"{players[0].PlayerNumber}\n{string.Join(" ", players[0].PlayersFought)}"
            );
        }

        private static IEnumerable<Player> ReadInput()
        {
            int playerCount = int.Parse(Console.ReadLine());

            return Enumerable.Range(0, playerCount)
                .Select(player => new Player(Console.ReadLine()));
        }
    }

    /// <summary>
    /// A container for a player.
    /// </summary>
    internal class Player
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        /// <param name="input">A line from input which describes the player.</param>
        public Player(string input)
        {
            Match match = InputRegex.Match(input);

            PlayerNumber = int.Parse(match.Groups["number"].Value);
            PlayerSign = (Sign)match.Groups["sign"].Value.Single();
        }

        /// <summary>
        /// Gets a number assigned to the player.
        /// </summary>
        public int PlayerNumber { get; }

        /// <summary>
        /// Gets a sign selected by the player.
        /// </summary>
        public Sign PlayerSign { get; }

        /// <summary>
        /// Gets a list of players that this player fought.
        /// </summary>
        public List<int> PlayersFought { get; } = new List<int>();

        private static Regex InputRegex { get; } = new Regex(
            @"^(?<number>\d+) (?<sign>[A-Z])$",
            RegexOptions.Compiled
        );

        private static Dictionary<Sign, Sign[]> SignPrecedence { get; }
            = new Dictionary<Sign, Sign[]>
            {
                [Sign.Rock] = new[] { Sign.Lizard, Sign.Scissors },
                [Sign.Paper] = new[] { Sign.Rock, Sign.Spock },
                [Sign.Scissors] = new[] { Sign.Paper, Sign.Lizard },
                [Sign.Lizard] = new[] { Sign.Spock, Sign.Paper },
                [Sign.Spock] = new[] { Sign.Scissors, Sign.Rock },
            };

        /// <summary>
        /// Calculates a result of a fight between players.
        /// </summary>
        /// <param name="playerA">The first player.</param>
        /// <param name="playerB">The second player.</param>
        /// <returns>The winning player.</returns>
        public static Player Fight(Player playerA, Player playerB)
        {
            if (playerA.PlayerSign == playerB.PlayerSign)
            {
                if (playerA.PlayerNumber < playerB.PlayerNumber)
                {
                    return playerA.Won(playerB);
                }
                else
                {
                    return playerB.Won(playerA);
                }
            }
            else if (SignPrecedence[playerA.PlayerSign].Contains(playerB.PlayerSign))
            {
                return playerA.Won(playerB);
            }
            else
            {
                return playerB.Won(playerA);
            }
        }

        private Player Won(Player loser)
        {
            PlayersFought.Add(loser.PlayerNumber);

            return this;
        }
    }
}
