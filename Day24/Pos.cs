using System;

namespace Day24
{
    public class Pos
    {
        public Pos(int x, int y, Pos from = null)
        {
            this.X = x;
            this.Y = y;
            this.From = from;
        }

        public int X { get; }
        public int Y { get; }
        public Pos From { get; }

        public uint GetLength()
        {
            var current = this.From;
            uint length = 0;
            while (current != null)
            {
                length++;
                current = current.From;
            }

            return length;
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as Pos);
        }

        public bool Equals(Pos p)
        {
            // If parameter is null, return false.
            if (Object.ReferenceEquals(p, null))
            {
                return false;
            }

            // Optimization for a common success case.
            if (Object.ReferenceEquals(this, p))
            {
                return true;
            }

            // If run-time types are not exactly the same, return false.
            if (this.GetType() != p.GetType())
            {
                return false;
            }

            // Return true if the fields match.
            // Note that the base class is not invoked because it is
            // System.Object, which defines Equals as reference equality.
            return (X == p.X) && (Y == p.Y);
        }

        public override int GetHashCode()
        {
            return X * 0x00010000 + Y;
        }

        public static bool operator ==(Pos lhs, Pos rhs)
        {
            // Check for null on left side.
            if (Object.ReferenceEquals(lhs, null))
            {
                if (Object.ReferenceEquals(rhs, null))
                {
                    // null == null = true.
                    return true;
                }

                // Only the left side is null.
                return false;
            }
            // Equals handles case of null on right side.
            return lhs.Equals(rhs);
        }

        public static bool operator !=(Pos lhs, Pos rhs)
        {
            return !(lhs == rhs);
        }
    }
}