using System;
using System.Collections.Generic;
using System.Linq;

namespace NanoidDotNet.Test
{
    public class PredefinedRandomSequence : Random
    {
        private readonly byte[] _sequence;

        public PredefinedRandomSequence(byte[] sequence)
        {
            _sequence = sequence;
        }

        public override void NextBytes(byte[] buffer)
        {
            var seq = GetSequence(buffer.Length).GetEnumerator();
            for (int i = 0; i < buffer.Length; i++)
            {
                seq.MoveNext();
                buffer[i] = seq.Current;
            }
        }

#if NETCOREAPP3_0
        public override void NextBytes(Span<byte> buffer)
        {
            var bytes = new byte[buffer.Length];
            NextBytes(bytes);
            bytes.CopyTo(buffer);
        }
#endif

        private IEnumerable<byte> GetSequence(int size)
        {
            var result = _sequence.AsEnumerable();
            // Update the sequence to match nanoid.js tests and implementation
            // which takes random bytes in reverse order (as of 3489e1e3b0dd7678b72c30f5fb00b806c8ce4fef).
            for (var i = 0; i < (size / _sequence.Length); i++)
            {
                result = result.Concat(result);
            }

            return result.Take(size).Reverse();
        }
    }
}