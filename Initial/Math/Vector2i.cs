namespace Math;

public class Vector2i
{
    private int _x;
    private int _y;
    public int X { get => _x; set => _x = value; }
    public int Y { get => _y; set => _y = value; }
    public float Magnitude { get => MathF.Sqrt((X * X) + (Y * Y));}
    public Vector2f Normalized { get => new Vector2f(X / Magnitude, Y / Magnitude); }

    public static Vector2i operator+(Vector2i a, Vector2i b)
    {
        int x = a.X + b.X;
        int y = a.Y + b.Y;
        return new Vector2i(x, y);
    }

    public static Vector2i operator-(Vector2i a, Vector2i b)
    {
        int x = a.X - b.X;
        int y = a.Y - b.Y;

        return new Vector2i(x, y);
    }

    public Vector2i(int x, int y)
    {
        X = x;
        Y = y;
    }

    public Vector2i(SplashKitSDK.Point2D pt)
    {
        X = (int)pt.X;
        Y = (int)pt.Y;
    }

    public Vector2i() : this(0, 0)
    {
    }

    public string ToStr() => $"{X}, {Y}";
}
