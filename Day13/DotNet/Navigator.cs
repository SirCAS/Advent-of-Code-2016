using System;
using System.Collections.Generic;
using System.Linq;

namespace Day13
{
    public class Navigator
    {
        private int favoriteNumber;
        public Navigator(int favoriteNumber)
        {
            this.favoriteNumber = favoriteNumber;
        }

        private bool IsSpace(Pos pos)
        {
            var sum = pos.X * pos.X + 3 * pos.X + 2 * pos.X * pos.Y + pos.Y + pos.Y * pos.Y + favoriteNumber;
            return Convert.ToString(sum, 2).Count(z => z == '1') % 2 == 0;
        }

        private IEnumerable<IEnumerable<Pos>> Next(IEnumerable<Pos> steps, Pos target)
        {
            if(steps.Count() > 100) // Stop iterating if we exceed 100 steps (this value might needs adjustments in accordance with input)
                return new Pos[0][];

            var copy = steps.ToList();
            var last = copy.Last();
            if(last == target)
                return new [] { copy };

            return
                new [] {
                    new Pos { Y = last.Y - 1, X = last.X },
                    new Pos { Y = last.Y + 1, X = last.X },
                    new Pos { Y = last.Y, X = last.X - 1 },
                    new Pos { Y = last.Y, X = last.X + 1 }
                }
                .Where(
                    pos =>
                        !copy.Contains(pos) &&
                        IsSpace(pos) &&
                        pos.Y >= 0 &&
                        pos.X >= 0
                )
                .SelectMany(
                    pos =>
                        Next(copy.Append(pos), target)
                );

        }

        private IEnumerable<IEnumerable<Pos>> Next(IEnumerable<Pos> steps, int target)
        {
            var copy = steps.ToList();
            if(copy.Count == target)
                return new []{ copy };

            var last = copy.Last();
            var newPositions =  new [] {
                new Pos { Y = last.Y - 1, X = last.X },
                new Pos { Y = last.Y + 1, X = last.X },
                new Pos { Y = last.Y, X = last.X - 1 },
                new Pos { Y = last.Y, X = last.X + 1 }
            };
            
            var validNewPositions = newPositions.Where(
                pos => !copy.Contains(pos) &&
                IsSpace(pos) &&
                pos.Y >= 0 &&
                pos.X >= 0);
            
            if(validNewPositions.Any())
                return validNewPositions.SelectMany(pos => Next(copy.Append(pos), target));

            return new []{ copy };
        }

        public int FindDistinctLocations(Pos start, int steps)
        {
            var solutions = Next(new List<Pos>{start}, steps + 1)
                .SelectMany(x => x)
                .Distinct();
            return solutions.Count();
        }

        public int FindLeastSteps(Pos start, Pos target)
        {
            var solutions = Next(new List<Pos>{start}, target);
            var solution = solutions.OrderBy(x => x.Count()).First();
            return solution.Count() - 1;
        }
    }
}

