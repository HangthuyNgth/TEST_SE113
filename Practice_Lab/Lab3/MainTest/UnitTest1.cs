
using Group13_Lab3;
using NUnit.Framework;

namespace MainTest
{
    [TestFixture]
    public class MainTests
    {
        [Test]
        [TestCase(1, 2000, 31)]
        [TestCase(4, 2000, 30)]
        [TestCase(2, 2000, 29)]
        [TestCase(2, 2003, 28)]
        [TestCase(2, 2100, 28)]
        [TestCase(2, 2024, 29)]
        [TestCase(2, 2025, 28)]
        [TestCase(5, 2024, 31)]
        [TestCase(9, 2024, 30)]
        [TestCase(0, 2024, 0)]
        [TestCase(13, 2024, 0)]
        public void TestCheckDateInMonth(int month, int year, int expectedDays)
        {
            int actualDays = Main.CheckDateInMonth(month, year);
            Assert.That(actualDays, Is.EqualTo(expectedDays));
        }

        [Test]
        [TestCase(28, 2, 2000, true)]
        [TestCase(29, 2, 2000, true)]
        [TestCase(29, 2, 2003, false)]
        [TestCase(29, 2, 2004, true)]
        [TestCase(29, 2, 1900, false)]
        [TestCase(30, 2, 2000, false)]
        [TestCase(30, 4, 2000, true)]
        [TestCase(31, 1, 2000, true)]
        [TestCase(31, 4, 2000, false)]
        [TestCase(31, 6, 2000, false)]
        [TestCase(31, 12, 2000, true)]
        [TestCase(0, 1, 2000, false)]
        [TestCase(32, 1, 2000, false)]
        [TestCase(15, 0, 2000, false)]
        [TestCase(15, 13, 2000, false)]
        public void TestCheckDate(int day, int month, int year, bool expected)
        {
            bool actual = Main.CheckDate(day, month, year);
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}