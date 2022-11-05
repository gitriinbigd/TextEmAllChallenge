namespace TextEmAll.Tests
{
    using NUnit.Framework;
    using TextEmAll;

    public class Challenge2Tests
    {
        [Test]
        [TestCase("gbcjbdha", 7)]
        [TestCase("ab", 0)]
        [TestCase("a      C", 1)]
        [TestCase("abcz", 24)]
        [TestCase("zwrga", IChallenge2Service.NO_RESULT)]
        [TestCase(null, IChallenge2Service.NO_RESULT)]
        [TestCase("", IChallenge2Service.NO_RESULT)]
        [TestCase("d", IChallenge2Service.NO_RESULT)]
        [TestCase("123456789", IChallenge2Service.NO_RESULT)]
        public void VariousInputs_ReturnExpectedDistance(string input, int expectedResult)
        {
            // arrange 
            IChallenge2Service challenge = new Challenge2Service();

            // act 
            int distance = challenge.MaxDistance(input);

            // assert 
            Assert.AreEqual(expectedResult, distance);
        }
    }
}