namespace Day13
{
    public class Pos
    {
        public int X { get; set;}
        public int Y { get; set;}

        public override bool Equals(System.Object obj)
        {
            if (obj == null) return false;

            Pos p = obj as Pos;
            if ((System.Object) p == null) return false;

            return (X == p.X) && (Y == p.Y);
        }

        public bool Equals(Pos p)
        {
            if ((object)p == null) return false;

            return (X == p.X) && (Y == p.Y);
        }

        public override int GetHashCode()
        {
            return X ^ Y;
        }

        public static bool operator ==(Pos a, Pos b)
        {
            if (System.Object.ReferenceEquals(a, b)) return true;
            if (((object)a == null) || ((object)b == null)) return false;

            return a.X == b.X && a.Y == b.Y;
        }

        public static bool operator !=(Pos a, Pos b)
        {
            return !(a == b);
        }
    }
}

