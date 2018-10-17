using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;

namespace RtgFacility.Test
{
    [TestFixture]
    public class ComponentTests
    {
        [Test]
        public void ComponentIsEqualTest()
        {
            var comp1 = new Component { Name = "a", Type = ComponentType.Chip };
            var comp2 = new Component { Name = "a", Type = ComponentType.Chip };

            Assert.That(comp1 == comp2, Is.True);
            Assert.That(comp1.Equals(comp2), Is.True);
            Assert.That(comp1.GetHashCode() == comp2.GetHashCode(), Is.True);
        }

        [Test]
        public void ComponentIsNotEqualTest()
        {
            var comp1 = new Component { Name = "a", Type = ComponentType.Chip };
            var comp2 = new Component { Name = "b", Type = ComponentType.Chip };

            Assert.That(comp1 == comp2, Is.False);
            Assert.That(comp1.Equals(comp2), Is.False);
            Assert.That(comp1.GetHashCode() == comp2.GetHashCode(), Is.False);
        }
    }
}