using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace Day24
{
    public class Game
    {

    }


    public class Graph
    {
        private readonly string[] data;
        private readonly int width;
        private readonly int height;

        private readonly Pos start;
        private readonly IList<Pos> destinations = new List<Pos>();

        public Graph(string[] data)
        {
            this.data = data;
            this.width = data[0].Length;
            this.height = data.Length;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (char.IsDigit(data[y][x]))
                    {
                        if (data[y][x] == '0')
                        {
                            start = new Pos(x, y);
                        }
                        else
                        {
                            destinations.Add(new Pos(x, y));
                        }
                    }
                }
            }
        }

        public Pos GetStart()
        {
            return start;
        }

        public IList<Pos> GetDestinations()
        {
            return destinations;
        }

        // Breadth First Search
        public Pos GetRoute(Pos startPos, Pos endPos)
        {
            var visited = new HashSet<Pos>
            {
                startPos
            };

            var frontier = new Queue<Pos>();
            frontier.Enqueue(startPos);

            while (frontier.Count > 0)
            {
                var current = frontier.Dequeue();

                // Early exit
                if (current == endPos)
                {
                    return current;
                }

                foreach (var next in GetNeighbors(current))
                {
                    if (!visited.Contains(next))
                    {
                        frontier.Enqueue(next);
                        visited.Add(next);
                    }
                }
            }

            throw new ArgumentOutOfRangeException(nameof(endPos), "Destination was not found");
        }

        private IList<Pos> GetNeighbors(Pos pos)
        {
            return new List<Pos>
                {
                    new Pos(pos.X + 1, pos.Y, pos),
                    new Pos(pos.X - 1, pos.Y, pos),
                    new Pos(pos.X, pos.Y + 1, pos),
                    new Pos(pos.X, pos.Y - 1, pos)
                }
                .Where(x => !IsWall(x))
                .Where(IsValid)
                .ToList();
        }

        private bool IsValid(Pos pos)
        {
            return pos.X < width && pos.Y < height;
        }

        private bool IsWall(Pos pos)
        {
            return data[pos.Y][pos.X] == '#';
        }
    }
}