using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace RtgFacility.Test
{
    public class StateComparerTest
    {

        [TestCase("F4 E  HG HM LG LM \nF3 .  .  .  .  .  \nF2 .  .  .  .  .  \nF1 .  .  .  .  .  ")]
        public void ContainsTest(string inp)
        {
            var state = inp.GetState();

            var sw = new HashSet<State>(new StateComparer());

            sw.Add(state);

            Assert.That(sw.Contains(state), Is.True);
        }

        [Test]
        public void NoCollisionsTest()
        {
            var states = new[]
            {
                "F4 .  .  .  .  .  \nF3 .  .  .  LG .  \nF2 .  HG .  .  .  \nF1 E  .  HM .  LM ",
                "F4 .  .  .  .  .  \nF3 .  .  .  LG .  \nF2 E  HG HM .  .  \nF1 .  .  .  .  LM ",
                "F4 .  .  .  .  .  \nF3 E  HG HM LG .  \nF2 .  .  .  .  .  \nF1 .  .  .  .  LM ",
                "F4 .  .  .  .  .  \nF3 .  HG .  LG .  \nF2 E  .  HM .  .  \nF1 .  .  .  .  LM ",
                "F4 .  .  .  .  .  \nF3 .  HG .  LG .  \nF2 .  .  .  .  .  \nF1 E  .  HM .  LM ",
                "F4 .  .  .  .  .  \nF3 .  HG .  LG .  \nF2 E  .  HM .  LM \nF1 .  .  .  .  .  ",
                "F4 .  .  .  .  .  \nF3 E  HG HM LG LM \nF2 .  .  .  .  .  \nF1 .  .  .  .  .  ",
                "F4 E  .  HM .  LM \nF3 .  HG .  LG .  \nF2 .  .  .  .  .  \nF1 .  .  .  .  .  ",
                "F4 .  .  .  .  LM \nF3 E  HG HM LG .  \nF2 .  .  .  .  .  \nF1 .  .  .  .  .  ",
                "F4 E  HG .  LG LM \nF3 .  .  HM .  .  \nF2 .  .  .  .  .  \nF1 .  .  .  .  .  ",
                "F4 .  HG .  LG .  \nF3 E  .  HM .  LM \nF2 .  .  .  .  .  \nF1 .  .  .  .  .  ",
                "F4 E  HG HM LG LM \nF3 .  .  .  .  .  \nF2 .  .  .  .  .  \nF1 .  .  .  .  .  ",
            }
            .Select(x => x.GetState());

            var sw = new HashSet<State>(new StateComparer());

            foreach(var state in states)
            {
                Assert.That(sw.Contains(state), Is.False);
                sw.Add(state);
            }

            foreach(var state in states)
            {
                Assert.That(sw.Contains(state), Is.True);
            }
        }

        [TestCase("F4 E  HG HM LG LM \nF3 .  .  .  .  .  \nF2 .  .  .  .  .  \nF1 .  .  .  .  .  ",
                  "F4 E  HG HM LG LM \nF3 .  .  .  .  .  \nF2 .  .  .  .  .  \nF1 .  .  .  .  .  ")]
        [TestCase("F4 E  HM HG LM LG \nF3 .  .  .  .  .  \nF2 .  .  .  .  .  \nF1 .  .  .  .  .  ",
                  "F4 E  HG HM LG LM \nF3 .  .  .  .  .  \nF2 .  .  .  .  .  \nF1 .  .  .  .  .  ")]
        public void EqualsTest(string str1, string str2)
        {
            var s1 = str1.GetState();
            var s2 = str2.GetState();

            var sw = new StateComparer();

            var result = sw.Equals(s1, s2);
            
            Assert.That(result, Is.True);
        }

        [TestCase("F4 E  .  HM LG LM \nF3 .  HG .  .  .  \nF2 .  .  .  .  .  \nF1 .  .  .  .  .  ",
                  "F4 E  HG HM LG LM \nF3 .  .  .  .  .  \nF2 .  .  .  .  .  \nF1 .  .  .  .  .  ")]
        [TestCase("F4 E  HM HG LM LG \nF3 .  .  .  .  .  \nF2 .  .  .  .  .  \nF1 .  .  .  .  .  ",
                  "F4 .  HG HM LG LM \nF3 E  .  .  .  .  \nF2 .  .  .  .  .  \nF1 .  .  .  .  .  ")]
        public void EqualsNotTest(string str1, string str2)
        {
            var s1 = str1.GetState();
            var s2 = str2.GetState();

            var sw = new StateComparer();

            var result = sw.Equals(s1, s2);

            Assert.That(result, Is.False);
        }

        [TestCase("F4 E  HG HM LG LM \nF3 .  .  .  .  .  \nF2 .  .  .  .  .  \nF1 .  .  .  .  .  ",
                  "F4 E  HG HM LG LM \nF3 .  .  .  .  .  \nF2 .  .  .  .  .  \nF1 .  .  .  .  .  ")]
        [TestCase("F4 E  HM HG LM LG \nF3 .  .  .  .  .  \nF2 .  .  .  .  .  \nF1 .  .  .  .  .  ",
                  "F4 E  HG HM LG LM \nF3 .  .  .  .  .  \nF2 .  .  .  .  .  \nF1 .  .  .  .  .  ")]
        public void GetHashCodeTest(string str1, string str2)
        {
            var s1 = str1.GetState();
            var s2 = str2.GetState();

            var sw = new StateComparer();

            var h1 = sw.GetHashCode(s1);
            var h2 = sw.GetHashCode(s2);

            Assert.That(h1, Is.EqualTo(h2));
        }

        [TestCase("F4 E  .  HM LG LM \nF3 .  HG .  .  .  \nF2 .  .  .  .  .  \nF1 .  .  .  .  .  ",
                  "F4 E  HG HM LG LM \nF3 .  .  .  .  .  \nF2 .  .  .  .  .  \nF1 .  .  .  .  .  ")]
        [TestCase("F4 E  HM HG LM LG \nF3 .  .  .  .  .  \nF2 .  .  .  .  .  \nF1 .  .  .  .  .  ",
                  "F4 .  HG HM LG LM \nF3 E  .  .  .  .  \nF2 .  .  .  .  .  \nF1 .  .  .  .  .  ")]
        public void GetHashCodeNotEqualTest(string str1, string str2)
        {
            var s1 = str1.GetState();
            var s2 = str2.GetState();

            var sw = new StateComparer();

            var h1 = sw.GetHashCode(s1);
            var h2 = sw.GetHashCode(s2);

            Assert.That(h1, Is.Not.EqualTo(h2));
        }
    }
}