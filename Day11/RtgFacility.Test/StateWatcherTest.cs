using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace RtgFacility.Test
{
    public class StateWatcherTest
    {

        [TestCase("F4 E  HG HM LG LM \nF3 .  .  .  .  .  \nF2 .  .  .  .  .  \nF1 .  .  .  .  .  ")]
        public void Test(string inp)
        {
            var state = inp.GetState();

            var sw = new StateWatcher();

            sw.Add(state);

            Assert.That(sw.Contains(state), Is.True);
        }

        [Test]
        public void NoCollisions()
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

            var sw = new StateWatcher();

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
    }
}