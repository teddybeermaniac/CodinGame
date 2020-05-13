// https://www.codingame.com/training/easy/ascii-art
// https://github.com/teddybeermaniac/CodinGame/tree/master/AsciiArt
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
#if OUTSIDE_CODINGAME
using CodinGame;
#endif

namespace AsciiArt
{
    /// <summary>
    /// The solution.
    /// </summary>
#if OUTSIDE_CODINGAME
    public class Solution : SolutionBase<Solution>

#else
    public class Solution
#endif
    {
        private static string CharacterSet { get; } = "ABCDEFGHIJKLMNOPQRSTUVWXYZ?";

        private static string UnknownCharacter { get; } = "?";

        private static Regex OutsideCharacterSetRegex { get; } = new Regex(
            $"[^{Regex.Escape(CharacterSet)}]",
            RegexOptions.Compiled
        );

        /// <summary>
        /// Runs the solution.
        /// </summary>
        public static void Main()
        {
            (string text, Dictionary<char, Glyph> font) = ReadInput();
            IEnumerable<string> lines = OutsideCharacterSetRegex.Replace(
                    text.ToUpper(),
                    UnknownCharacter
                ).Select(character => font[character].Text)
                .Transpose()
                .Select(glyphLine => string.Join(string.Empty, glyphLine));

            foreach (string line in lines)
            {
                Console.WriteLine(line);
            }
        }

        private static (string Text, Dictionary<char, Glyph> Font) ReadInput()
        {
            int width = int.Parse(Console.ReadLine());
            int height = int.Parse(Console.ReadLine());
            string text = Console.ReadLine();
            IEnumerable<string> glyphs = Enumerable.Range(0, height)
                .Select(line => Console.ReadLine());

            Dictionary<char, Glyph> font = glyphs.Select(line => line.Batch(width))
                .ZipMany()
                .Zip(CharacterSet, (glyph, character) => new Glyph(character, glyph))
                .ToDictionary(glyph => glyph.Character);

            return (text, font);
        }
    }

    /// <summary>
    /// An class providing extensions and additions for IEnumerable{T}.
    /// </summary>
    internal static class EnumerableExtensions
    {
        /// <summary>
        /// Batches the enumerable.
        /// </summary>
        /// <param name="enumerable">The enumerable to be batched.</param>
        /// <param name="size">The size of a batch.</param>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <returns>An enumerable of batches.</returns>
        public static IEnumerable<IEnumerable<T>> Batch<T>(this IEnumerable<T> enumerable, int size)
        {
            var batch = new List<T>(size);
            foreach (T item in enumerable)
            {
                batch.Add(item);
                if (batch.Count == size)
                {
                    yield return batch;

                    batch = new List<T>(size);
                }
            }

            if (batch.Count > 0)
            {
                yield return batch;
            }
        }

        /// <summary>
        /// Transposes (rotates) nested enumerables.
        /// </summary>
        /// <param name="enumerable">An enumerable to be transposed.</param>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <returns>A rotated enumerable.</returns>
        public static IEnumerable<IEnumerable<T>> Transpose<T>(
            this IEnumerable<IEnumerable<T>> enumerable
        )
        {
            return enumerable
                .SelectMany(inner => inner.Select((item, index) => (Item: item, Index: index)))
                .GroupBy(row => row.Index, i => i.Item);
        }

        /// <summary>
        /// Zips multiple enumerables, until the shortest one ends.
        /// </summary>
        /// <param name="enumerables">A list of enumerables.</param>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <returns>An array of items.</returns>
        public static IEnumerable<IEnumerable<T>> ZipMany<T>(
            this IEnumerable<IEnumerable<T>> enumerables
        )
        {
            IEnumerator<T>[] enumerators = enumerables.Select(
                enumerable => enumerable.GetEnumerator()
            ).ToArray();
            while (enumerators.Select(enumerator => enumerator.MoveNext()).All(result => result))
            {
                yield return enumerators.Select(enumerator => enumerator.Current);
            }
        }
    }

    /// <summary>
    /// A container for a single font glyph.
    /// </summary>
    internal class Glyph
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Glyph"/> class.
        /// </summary>
        /// <param name="character">The character represented by this glyph.</param>
        /// <param name="text">The array containing the glyph itself.</param>
        public Glyph(char character, IEnumerable<IEnumerable<char>> text)
        {
            Character = character;
            Text = text.Select(line => string.Join(string.Empty, line)).ToArray();
        }

        /// <summary>
        /// Gets a character represented by this glyph.
        /// </summary>
        public char Character { get; }

        /// <summary>
        /// Gets an array containing the glyph itself.
        /// </summary>
        public string[] Text { get; }
    }
}
