namespace Day17
{
    public struct Solutions
    {
        public string Longest { get; set;}
        public string Shortest { get; set;}
    }

    public class Solution
    {
        public Solution()
        {
            this.Path = string.Empty;
            this.Position = new Point(0, 0);
        }

        public Solution(string path, int x, int y)
        {
            Path = path;
            Position = new Point(x, y);
        }

        public string Path { get; set; }
        public Point Position { get; set; }
    }

    public struct Point 
    {
        public Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
        public int X { get; set;}
        public int Y { get; set;}
    }
}