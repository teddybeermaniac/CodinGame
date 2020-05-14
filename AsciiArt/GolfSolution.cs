// https://www.codingame.com/training/easy/ascii-art
// https://github.com/teddybeermaniac/CodinGame/tree/master/AsciiArt
using System;
#if OUTSIDE_CODINGAME
using CodinGame;
#endif

namespace AsciiArt
{
    /// <summary>
    /// The solution, as short as possible.
    /// </summary>
#if OUTSIDE_CODINGAME
    public class GolfSolution : SolutionBase<GolfSolution>
#else
    public class GolfSolution
#endif
    {
        /// <summary>
        /// Runs the solution.
        /// </summary>
        public static void Main()
        {
            string characterSet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ?";
            int width = int.Parse(Console.ReadLine());
            Console.ReadLine();
            string text = Console.ReadLine().ToUpper();
            for (string line = Console.ReadLine(); !string.IsNullOrEmpty(line); line = Console.ReadLine())
            {
                foreach (char character in text)
                {
                    Console.Write(line.Substring(characterSet.IndexOf(characterSet.Contains(character) ? character : '?') * width, width));
                }

                Console.WriteLine();
            }
        }
    }
}
