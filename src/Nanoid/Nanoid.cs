using System;
using System.Text;

namespace Nanoid
{
    /// <summary>
    /// 
    /// </summary>
    public static class Nanoid
    {
        private static readonly CryptoRandom Random = new CryptoRandom();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="alphabet"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static string Generate(string alphabet="_~0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ", int size=21)
        {
            return Generate(Random, alphabet, size);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="random"></param>
        /// <param name="alphabet"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static string Generate(Random random, string alphabet="_~0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ", int size=21)
        {

            if (random == null)
            {
                throw new ArgumentNullException("random cannot be null.");
            }

            if (alphabet == null)
            {
                throw new ArgumentNullException("alphabet cannot be null.");
            }

            if (alphabet.Length == 0 || alphabet.Length >= 256)
            {
                throw new ArgumentOutOfRangeException("alphabet must contain between 1 and 255 symbols.");
            }

            if (size <= 0)
            {
                throw new ArgumentOutOfRangeException("size must be greater than zero.");
            }

            var mask = (2 << (int)Math.Floor(Math.Log(alphabet.Length - 1) / Math.Log(2))) - 1;
            var step = (int)Math.Ceiling(1.6 * mask * size / alphabet.Length);

            var idBuilder = new StringBuilder();

            while (true)
            {

                var bytes = new byte[step];
                random.NextBytes(bytes);

                for (var i = 0; i < step; i++)
                {

                    var alphabetIndex = bytes[i] & mask;

                    if (alphabetIndex >= alphabet.Length) continue;
                    idBuilder.Append(alphabet[alphabetIndex]);
                    if (idBuilder.Length == size)
                    {
                        return idBuilder.ToString();
                    }

                }

            }

        }
    }
}
