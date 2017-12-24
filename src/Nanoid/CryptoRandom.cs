using System;
using System.Text;
using System.Security.Cryptography;
namespace Nanoid
{
    public class CryptoRandom : Random
    {
        private static RandomNumberGenerator r;
        private byte[] uint32Buffer = new byte[4];
        public CryptoRandom()
        {
            r = RandomNumberGenerator.Create();
        }
        public CryptoRandom(Int32 seedIgnored) { }
        
        public override void NextBytes(byte[] buffer)
        {
            if (buffer == null) throw new ArgumentNullException(nameof(buffer));
            r.GetBytes(buffer);
        }
        public override double NextDouble()
        {
            r.GetBytes(uint32Buffer);
            return BitConverter.ToUInt32(uint32Buffer, 0) / (1.0 + UInt32.MaxValue);
        }
        public override int Next(int minValue, int maxValue)
        {
            if (minValue > maxValue) throw new ArgumentOutOfRangeException(nameof(minValue));
            if (minValue == maxValue) return minValue;
            var range = (long)maxValue - minValue;
            return (int)((long)Math.Floor(NextDouble() * range) + minValue);
        }
        public override int Next()
        {
            return Next(0, Int32.MaxValue);
        }
        public override int Next(int maxValue)
        {
            if (maxValue < 0) throw new ArgumentOutOfRangeException(nameof(maxValue));
            return Next(0, maxValue);
        }
    }
}