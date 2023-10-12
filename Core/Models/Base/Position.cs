

namespace Core.Models.Base
{
    public struct Position
    {
        public double X { get; set; } = 0;
        public double Y { get; set; } = 0;

        public Position(double x = 0, double y = 0)
        {
            X = x;
            Y = y;
        }

        public double GetDist(Position position)
        {
            return Math.Sqrt(Math.Pow(X - position.X, 2) + Math.Pow(Y - position.Y, 2));
        }

        public static Position CreateRandom(double a, double b)
        {
            var rnd = new Random();
            return new Position(
                rnd.NextDouble() * a,
                rnd.NextDouble() * b
            );
        }

        public static Position operator +(Position a, Position b) 
        {
            return new Position(a.X + b.X, a.Y + b.Y);
        }

        public static Position operator -(Position a, Position b)
        {
            return new Position(a.X - b.X, a.Y - b.Y);
        }
    }
}
