using System;
using System.Threading.Tasks;
using Xunit;
using Nanoid;
namespace Nanoid.Test
{
    public class NanoidTest
    {
        [Fact]
        public void TestDefault()
        {
            var result = Nanoid.Generate();
            Assert.Equal(21, result.Length);
        }

        [Fact]
        public void TestCustomSize()
        {
            var result = Nanoid.Generate(size:10);
            Assert.Equal(10, result.Length);
        }
        [Fact]
        public void TestCustomAlphabet()
        {
            var result = Nanoid.Generate("1234abcd");
            Assert.Equal(21, result.Length);
        }

        [Fact]
        public void TestCustomAlphabetAndSize()
        {
            var result = Nanoid.Generate("1234abcd", 7);
            Assert.Equal(7, result.Length);
        }

        [Fact]
        public void TestCustomRandom()
        {
            var random = new Random(10);
            var result = Nanoid.Generate(random);
            Assert.Equal(21, result.Length);
        }

        [Fact]
        public async Task TestAsyncGenerate()
        {
        
            var result = await Nanoid.GenerateAsync();
        
            Assert.Equal(21, result.Length);
        }
    }
}
