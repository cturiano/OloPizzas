using System;
using NUnit.Framework;
using OloPizzas;

namespace OloPizzasTest
{
    [TestFixture]
    public class ToppingsTests
    {
        public enum Comparison
        {
            Less = -1,
            Same = 0,
            Greater = 1
        }

        [TestCase("", null)]
        [TestCase("", new string[] { })]
        [TestCase("", new[] {""})]
        [TestCase("apple", new[] {"apple"})]
        [TestCase("apple, apple", new[] {" Apple ", "apple"})]
        [TestCase("apple, banana", new[] {" apple ", "banana "})]
        [TestCase("apple, banana", new[] {" banana ", " Apple "})]
        [TestCase("apple, banana", new[] {"banana", "apple"})]
        [TestCase("apple, apple, banana", new[] {" Banana ", " apple ", "apple"})]
        [TestCase("apple, apple pie, banana", new[] {" banana ", " apple ", "apple pie"})]
        [TestCase("apple pie, banana, cranberry", new[] {"banana", "cranberry", "apple pie"})]
        public void ConstructorAndPropertiesTest(string expected, string[] toppings)
        {
            var t1 = new Toppings(toppings);
            Assert.AreEqual(expected, t1.ToppingString);
        }

        [TestCase(Comparison.Same, new string[] { }, null)]
        [TestCase(Comparison.Same, new string[] { }, new[] {""})]
        [TestCase(Comparison.Same, new[] {"apple"}, new[] {"apple"})]
        [TestCase(Comparison.Greater, new[] {"apple"}, null)]
        [TestCase(Comparison.Greater, new[] {"apple"}, new[] {""})]
        [TestCase(Comparison.Greater, new[] {"apple", "apple"}, new[] {"apple"})]
        [TestCase(Comparison.Greater, new[] {"apple", "banana"}, new[] {"apple"})]
        [TestCase(Comparison.Greater, new[] {"apple", "banana", "cranberry"}, new[] {"apple"})]
        [TestCase(Comparison.Less, null, new[] {"apple"})]
        [TestCase(Comparison.Less, new[] {""}, new[] {"apple"})]
        [TestCase(Comparison.Less, new[] {"apple"}, new[] {"apple", "apple"})]
        [TestCase(Comparison.Less, new[] {"apple"}, new[] {"apple", "banana"})]
        [TestCase(Comparison.Less, new[] {"apple"}, new[] {"apple", "banana", "cranberry"})]
        public void CompareToTest(Comparison expected, string[] toppings1, string[] toppings2)
        {
            var t1 = new Toppings(toppings1);
            var t2 = new Toppings(toppings2);

            switch (expected)
            {
                case Comparison.Less:
                    Assert.LessOrEqual((int)expected, t1.CompareTo(t2));
                    break;
                case Comparison.Same:
                    Assert.AreEqual((int)expected, t1.CompareTo(t2));
                    break;
                case Comparison.Greater:
                    Assert.GreaterOrEqual((int)expected, t1.CompareTo(t2));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(expected), expected, null);
            }
        }

        [TestCase(true, new string[] { }, null)]
        [TestCase(true, new string[] { }, new[] {""})]
        [TestCase(true, new[] {"apple"}, new[] {"apple"})]
        [TestCase(true, new[] {"apple", "banana"}, new[] {"apple", "banana"})]
        [TestCase(true, new[] {"apple", "banana", "cranberry"}, new[] {"apple", "banana", "cranberry"})]
        [TestCase(false, new[] {"apple"}, null)]
        [TestCase(false, new[] {"apple"}, new[] {""})]
        [TestCase(false, new[] {"apple", "apple"}, new[] {"apple"})]
        [TestCase(false, new[] {"apple", "banana"}, new[] {"apple"})]
        [TestCase(false, new[] {"apple", "banana", "cranberry"}, new[] {"apple"})]
        [TestCase(false, null, new[] {"apple"})]
        [TestCase(false, new[] {""}, new[] {"apple"})]
        [TestCase(false, new[] {"apple"}, new[] {"apple", "apple"})]
        [TestCase(false, new[] {"apple"}, new[] {"apple", "banana"})]
        [TestCase(false, new[] {"apple"}, new[] {"apple", "banana", "cranberry"})]
        public void EqualsAsToppingsTest(bool equal, string[] toppings1, string[] toppings2)
        {
            var t1 = new Toppings(toppings1);
            var t2 = new Toppings(toppings2);
            Assert.AreEqual(equal, t1.Equals(t2));
        }

        [TestCase(true, new string[] { }, null)]
        [TestCase(true, new string[] { }, new[] {""})]
        [TestCase(true, new[] {"apple"}, new[] {"apple"})]
        [TestCase(true, new[] {"apple", "banana"}, new[] {"apple", "banana"})]
        [TestCase(true, new[] {"apple", "banana", "cranberry"}, new[] {"apple", "banana", "cranberry"})]
        [TestCase(false, new[] {"apple"}, null)]
        [TestCase(false, new[] {"apple"}, new[] {""})]
        [TestCase(false, new[] {"apple", "apple"}, new[] {"apple"})]
        [TestCase(false, new[] {"apple", "banana"}, new[] {"apple"})]
        [TestCase(false, new[] {"apple", "banana", "cranberry"}, new[] {"apple"})]
        [TestCase(false, null, new[] {"apple"})]
        [TestCase(false, new[] {""}, new[] {"apple"})]
        [TestCase(false, new[] {"apple"}, new[] {"apple", "apple"})]
        [TestCase(false, new[] {"apple"}, new[] {"apple", "banana"})]
        [TestCase(false, new[] {"apple"}, new[] {"apple", "banana", "cranberry"})]
        public void EqualsAsObjectTest(bool equal, string[] toppings1, string[] toppings2)
        {
            var t1 = new Toppings(toppings1);
            var t2 = new Toppings(toppings2);
            Assert.AreEqual(equal, t1.Equals((object)t2));
        }

        [TestCase(new[] {"apple"}, null)]
        [TestCase(new[] {"apple"}, new[] {""})]
        [TestCase(new[] {"apple", "apple"}, new[] {"apple"})]
        [TestCase(new[] {"apple", "banana"}, new[] {"apple"})]
        [TestCase(new[] {"apple", "banana", "cranberry"}, new[] {"apple"})]
        [TestCase(null, new[] {"apple"})]
        [TestCase(new[] {""}, new[] {"apple"})]
        [TestCase(new[] {"apple"}, new[] {"apple", "apple"})]
        [TestCase(new[] {"apple"}, new[] {"apple", "banana"})]
        [TestCase(new[] {"apple"}, new[] {"apple", "banana", "cranberry"})]
        public void NotEqualsOperatorTest(string[] toppings1, string[] toppings2)
        {
            var t1 = new Toppings(toppings1);
            var t2 = new Toppings(toppings2);
            Assert.IsTrue(t1 != t2);
        }

        [TestCase(true, null)]
        [TestCase(true, new string[] { })]
        [TestCase(true, new[] {""})]
        [TestCase(true, new[] {"apple"})]
        [TestCase(true, new[] {"apple", "banana"})]
        [TestCase(true, new[] {"apple", "banana", "cranberry"})]
        [TestCase(true, new[] {"apple", "apple"})]
        public void GetHashCodeTest(bool junk, string[] toppings1)
        {
            var t1 = new Toppings(toppings1);
            var hashCode = (toppings1 != null ? string.Join(", ", toppings1) : string.Empty).GetHashCode();
            Assert.AreEqual(hashCode, t1.GetHashCode());
        }

        [TestCase("", null)]
        [TestCase("", new string[] { })]
        [TestCase("", new[] {""})]
        [TestCase("apple", new[] {"apple"})]
        [TestCase("apple, apple", new[] {" Apple ", "apple"})]
        [TestCase("apple, banana", new[] {" apple ", "banana "})]
        [TestCase("apple, banana", new[] {" banana ", " Apple "})]
        [TestCase("apple, banana", new[] {"banana", "apple"})]
        [TestCase("apple, apple, banana", new[] {" Banana ", " apple ", "apple"})]
        [TestCase("apple, apple pie, banana", new[] {" banana ", " apple ", "apple pie"})]
        [TestCase("apple pie, banana, cranberry", new[] {"banana", "cranberry", "apple pie"})]
        public void ToStringTest(string expected, string[] toppings)
        {
            var t1 = new Toppings(toppings);
            Assert.AreEqual(expected, t1.ToString());
        }
    }
}