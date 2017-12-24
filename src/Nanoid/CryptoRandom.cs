using System;
using System.Security.Cryptography;
namespace Nanoid
{
    /// <inheritdoc />
    /// <summary>
    /// </summary>
    public class CryptoRandom : Random
    {
        private static RandomNumberGenerator _r;
        private readonly byte[] _uint32Buffer = new byte[4];
        /// <inheritdoc />
        /// <summary>
        /// </summary>
        public CryptoRandom()
        {
            _r = RandomNumberGenerator.Create();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public override void NextBytes(byte[] buffer)
        {
            if (buffer == null) throw new ArgumentNullException(nameof(buffer));
            _r.GetBytes(buffer);
        }
        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <returns></returns>
        public override double NextDouble()
        {
            _r.GetBytes(_uint32Buffer);
            return BitConverter.ToUInt32(_uint32Buffer, 0) / (1.0 + UInt32.MaxValue);
        }
        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException"></exception>
        public override int Next(int minValue, int maxValue)
        {
            if (minValue > maxValue) throw new ArgumentOutOfRangeException(nameof(minValue));
            if (minValue == maxValue) return minValue;
            var range = (long)maxValue - minValue;
            return (int)((long)Math.Floor(NextDouble() * range) + minValue);
        }
        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <returns></returns>
        public override int Next()
        {
            return Next(0, int.MaxValue);
        }
        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException"></exception>
        public override int Next(int maxValue)
        {
            if (maxValue < 0) throw new ArgumentOutOfRangeException(nameof(maxValue));
            return Next(0, maxValue);
        }
    }
}