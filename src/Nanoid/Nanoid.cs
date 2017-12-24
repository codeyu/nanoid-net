using System;
using System.Text;

namespace Nanoid
{
    public static class Nanoid
    {
        private static readonly Random random = new Random();
        public static String Generate(string alphabet="_~0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ", int size=21)
        {
            return Generate(random, alphabet, size);
        }
        public static String Generate(Random random, string alphabet="_~0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ", int size=21)
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

            int mask = (2 << (int)Math.Floor(Math.Log(alphabet.Length - 1) / Math.Log(2))) - 1;
            int step = (int)Math.Ceiling(1.6 * mask * size / alphabet.Length);

            StringBuilder idBuilder = new StringBuilder();

            while (true)
            {

                byte[] bytes = new byte[step];
                random.NextBytes(bytes);

                for (int i = 0; i < step; i++)
                {

                    int alphabetIndex = bytes[i] & mask;

                    if (alphabetIndex < alphabet.Length)
                    {
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
}
