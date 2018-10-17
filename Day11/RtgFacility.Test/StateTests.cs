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
        [TestCase("F4 E  .  .  .  .  \nF3 .  HG .  LG .  \nF2 .  .  HM .  .  \nF1 .  .  .  .  LM ")]
        public void ElevatorOnEmptyFloorTest(string input)
        {
            var state = input.GetState();

            var result = StateValidator.IsValidElevatorMove(state);

            Assert.That(result, Is.False);
        }

        [TestCase("F4 .  .  .  .  .  \nF3 E  HG .  LG .  \nF2 .  .  HM .  .  \nF1 .  .  .  .  LM ")]
        public void ElevatorOnNonEmptyFloorTest(string input)
        {
            var state = input.GetState();

            var result = StateValidator.IsValidElevatorMove(state);

            Assert.That(result, Is.True);
        }

        [Test]
        public void ChipsWillNotBeFriedTest()
        {
            var state = new State
            {
                Elevator = 1,
                Components = new Dictionary<int, List<Component>>
                {
                    { 1, new List<Component>
                        {
                            new Component { Type = ComponentType.Chip, Name = "helium" },
                            new Component { Type = ComponentType.Generator, Name = "helium" },
                            new Component { Type = ComponentType.Generator, Name = "hydrogen" },
                        }
                    }
                }
            };

            var result = StateValidator.ChipsAreNotFried(state);
            Assert.That(result, Is.True);
        }

        [Test]
        public void ChipsWillBeFriedTest()
        {
            var state = new State
            {
                Elevator = 1,
                Components = new Dictionary<int, List<Component>>
                {
                    { 1, new List<Component>
                        {
                            new Component { Type = ComponentType.Chip, Name = "helium" },
                            new Component { Type = ComponentType.Chip, Name = "hydrogen" },
                            new Component { Type = ComponentType.Generator, Name = "helium" },
                        }
                    }
                }
            };

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

        [Test]
        public void StateIsEqualTest()
        {
            var state1 = "F2 E  HG HM LG LM \nF1 .  .  .  .  .  ".GetState();
            state1.Moves = 2;
            
            var state2 = "F2 E  HG HM LG LM \nF1 .  .  .  .  .  ".GetState();
            state2.Moves = 3;

            Assert.That(state1 == state2, Is.True);
            Assert.That(state1.Equals(state2), Is.True);
            Assert.That(state1.GetHashCode() == state2.GetHashCode(), Is.True);
        }

        [Test]
        public void StateIsNotEqualTest()
        {
            var state1 = "F4 .  .  .  .  .  \nF3 .  .  .  LG .  \nF2 .  HG .  .  .  \nF1 E  .  HM .  LM ".GetState();
            state1.Moves = 2;

            var state2 = "F4 .  .  .  .  .  \nF3 .  HG .  LG .  \nF2 .  .  .  .  .  \nF1 E  .  HM .  LM ".GetState();
            state2.Moves = 3;

            Assert.That(state1 == state2, Is.False);
            Assert.That(state1.Equals(state2), Is.False);
            Assert.That(state1.GetHashCode() == state2.GetHashCode(), Is.False);
        }
    }
}

