using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace RtgFacility.Test
{
    [TestFixture]
    public class ComponentListTests
    {
        HashSet<Component> a;
        HashSet<Component> b;

        [SetUp]
        public void Setup()
        {
            var comp1 = new Component { Name = "a", Type = ComponentType.Chip };
            var comp2 = new Component { Name = "b", Type = ComponentType.Generator };
            a = new HashSet<Component> { comp1, comp2 };
            b = new HashSet<Component> { comp1, comp2 };
        }

        [Test]
        public void PrintHashCodes()
        {
            foreach (var x in a)
            {
                Console.WriteLine(x.GetHashCode());
            }

            foreach (var x in b)
            {
                Console.WriteLine(x.GetHashCode());
            }
        }

        [Test]
        public void EqualityOperatorTest()
        {
            Assert.That(a == b, Is.True);
        }

        [Test]
        public void EqualsMethodTest()
        {
            Assert.That(a.Equals(b), Is.True);
        }

        [Test]
        public void GetHashCodeTest()
        {
            Assert.That(a.GetHashCode() == b.GetHashCode(), Is.True);
        }
    }
}