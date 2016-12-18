using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Day17
{
    public class Maze
    {
        private MD5 md5;
        private Point destination;
        private int width = 3;
        private int height = 3;
        private Queue<Solution> queue = new Queue<Solution>();

        public Maze()
        {
            this.md5 = MD5.Create();
            this.destination = new Point(3, 3);
        }

        private byte[] GetPasscodeHash(string passcode)
        {
            return md5.ComputeHash(Encoding.ASCII.GetBytes(passcode));
        }

        public Solutions GetSolutions(string passcode)
        {
            string shortestRoute = null;
            string longestRoute = string.Empty;
            
            queue.Enqueue(new Solution(string.Empty, 0, 0));

            do
            {
                var i = queue.Dequeue();
                if(i.Position.X == destination.X && i.Position.Y == destination.Y)
                {
                    shortestRoute = shortestRoute ?? i.Path; // Using breadth-first search thus if value exsits it's better

                    if(longestRoute.Length < i.Path.Length)
                        longestRoute = i.Path;
                }
                else
                {
                    var hash = GetPasscodeHash(passcode + i.Path);
                    if (i.Position.Y > 0 && (hash[0] >> 4) > 10)
                        queue.Enqueue(new Solution(i.Path + "U", i.Position.X, i.Position.Y - 1));

                    if (i.Position.Y < height && (hash[0] & 15) > 10)
                        queue.Enqueue(new Solution(i.Path + "D", i.Position.X, i.Position.Y + 1));
            
                    if (i.Position.X > 0 && (hash[1] >> 4) > 10)
                        queue.Enqueue(new Solution(i.Path + "L", i.Position.X - 1, i.Position.Y));
                    
                    if (i.Position.X < width && (hash[1] & 15) > 10)
                        queue.Enqueue(new Solution(i.Path + "R", i.Position.X + 1, i.Position.Y));
                }
            } while(queue.Count > 0);

            return new Solutions {
                Longest = longestRoute,
                Shortest = shortestRoute
            };
        }
    }
}