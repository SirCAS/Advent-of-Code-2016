using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;

namespace RtgFacility.Test
{
    [TestFixture]
    public class StateValidatorTests
    {
        [TestCase("F1 E  .  .  .  .  ")]
        public void ElevatorOnEmptyFloorTest(string input)
        {
            var state = input.GetState();

            var result = StateValidator.IsValidElevatorMove(state);

            Assert.That(result, Is.False);
        }

        [TestCase("F1 E  HG .  LG .  ")]
        public void ElevatorOnNonEmptyFloorTest(string input)
        {
            var state = input.GetState();

            var result = StateValidator.IsValidElevatorMove(state);

            Assert.That(result, Is.True);
        }

        [TestCase("F1 E  HM HG LG .  ")]
        public void ChipsWillNotBeFriedTest(string input)
        {
            var state = input.GetState();

            var result = StateValidator.ChipsAreNotFried(state);

            Assert.That(result, Is.True);
        }

        [TestCase("F1 E  HM LM LG .  ")]
        public void ChipsWillBeFriedTest(string input)
        {
            var state = input.GetState();

            var result = StateValidator.ChipsAreNotFried(state);

            Assert.That(result, Is.False);
        }

        [TestCase("F4 .  HG .  LG .  \nF3 E  .  HM .  LM \nF2 .  .  .  .  .  \nF1 .  .  .  .  .  ", false)]
        [TestCase("F4 E  HG HM LG LM \nF3 .  .  .  .  .  \nF2 .  .  .  .  .  \nF1 .  .  .  .  .  ", true)]
        public void IsFinalStateTest(string input, bool isFinal)
        {
            var state = input.GetState();

            var result = StateValidator.IsFinalState(state);

            Assert.That(result, Is.EqualTo(isFinal));
        }

        [TestCase("F4 .  .  .  .  .  \nF3 .  .  .  LG .  \nF2 .  HG .  .  .  \nF1 E  .  HM .  LM ")]
        [TestCase("F4 .  .  .  .  .  \nF3 .  .  .  LG .  \nF2 E  HG HM .  .  \nF1 .  .  .  .  LM ")]
        [TestCase("F4 .  .  .  .  .  \nF3 E  HG HM LG .  \nF2 .  .  .  .  .  \nF1 .  .  .  .  LM ")]
        [TestCase("F4 .  .  .  .  .  \nF3 .  HG .  LG .  \nF2 E  .  HM .  .  \nF1 .  .  .  .  LM ")]
        [TestCase("F4 .  .  .  .  .  \nF3 .  HG .  LG .  \nF2 .  .  .  .  .  \nF1 E  .  HM .  LM ")]
        [TestCase("F4 .  .  .  .  .  \nF3 .  HG .  LG .  \nF2 E  .  HM .  LM \nF1 .  .  .  .  .  ")]
        [TestCase("F4 .  .  .  .  .  \nF3 E  HG HM LG LM \nF2 .  .  .  .  .  \nF1 .  .  .  .  .  ")]
        [TestCase("F4 E  .  HM .  LM \nF3 .  HG .  LG .  \nF2 .  .  .  .  .  \nF1 .  .  .  .  .  ")]
        [TestCase("F4 .  .  .  .  LM \nF3 E  HG HM LG .  \nF2 .  .  .  .  .  \nF1 .  .  .  .  .  ")]
        [TestCase("F4 E  HG .  LG LM \nF3 .  .  HM .  .  \nF2 .  .  .  .  .  \nF1 .  .  .  .  .  ")]
        [TestCase("F4 .  HG .  LG .  \nF3 E  .  HM .  LM \nF2 .  .  .  .  .  \nF1 .  .  .  .  .  ")]
        [TestCase("F4 E  HG HM LG LM \nF3 .  .  .  .  .  \nF2 .  .  .  .  .  \nF1 .  .  .  .  .  ")]
        public void ValidStatesFromExampleTest(string input)
        {
            var state = input.GetState();

            var result = StateValidator.ChipsAreNotFried(state) && StateValidator.IsValidElevatorMove(state);

            Assert.That(result, Is.True);
        }
    }
}

