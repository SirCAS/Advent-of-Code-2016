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
        [Test]
        public void ElevatorOnEmptyFloorTest()
        {
            var state = new State
            {
                Elevator = 1,
                Floors = new Dictionary<int, Floor>
                {
                    { 1, new Floor { Components = new List<Component>() } }
                }
            };

            var result = StateValidator.IsValidElevatorMove(state);
            Assert.That(result, Is.False);
        }

        [Test]
        public void ElevatorOnNonEmptyFloorTest()
        {
            var state = new State
            {
                Elevator = 1,
                Floors = new Dictionary<int, Floor>
                {
                    { 1, new Floor
                        {
                            Components = new List<Component>
                            {
                                new Component { Type = ComponentType.Chip, Name = "helium" }
                            }
                        }
                    }
                }
            };

            var result = StateValidator.IsValidElevatorMove(state);
            Assert.That(result, Is.True);
        }

        [Test]
        public void ChipsWillNotBeFriedTest()
        {
            var state = new State
            {
                Elevator = 1,
                Floors = new Dictionary<int, Floor>
                {
                    { 1, new Floor
                        {
                            Components = new List<Component>
                            {
                                new Component { Type = ComponentType.Chip, Name = "helium" },
                                new Component { Type = ComponentType.Generator, Name = "helium" },
                                new Component { Type = ComponentType.Generator, Name = "hydrogen" },
                            }
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
                Floors = new Dictionary<int, Floor>
                {
                    { 1, new Floor
                        {
                            Components = new List<Component>
                            {
                                new Component { Type = ComponentType.Chip, Name = "helium" },
                                new Component { Type = ComponentType.Chip, Name = "hydrogen" },
                                new Component { Type = ComponentType.Generator, Name = "helium" },
                            }
                        }
                    }
                }
            };

            var result = StateValidator.ChipsAreNotFried(state);
            Assert.That(result, Is.False);
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
            var state = new State
            {
                Elevator = input.Find('E', -2).ToInt(),
                Floors = input.Split('\n')
                              .ToDictionary(
                                f => f.Find('F', 1).ToInt(),
                                f => new Floor
                                {
                                    Components = f.FindAll('G', -1)
                                                .Select(x => new Component { Type = ComponentType.Generator, Name = x })
                                                .Concat(f.FindAll('M', -1)
                                                            .Select(x => new Component { Type = ComponentType.Chip, Name = x }))
                                                    .ToList(),
                                })
            };

            var result = StateValidator.ChipsAreNotFried(state) && StateValidator.IsValidElevatorMove(state);

            Assert.That(result, Is.True);
        }
    }
}

