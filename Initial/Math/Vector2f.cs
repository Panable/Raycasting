namespace Math;

public class Vector2f
{
    private float _x;
    private float _y;

    public float X { get => _x; set => _x = value; }
    public float Y { get => _y; set => _y = value; }
    public float Magnitude { get => MathF.Sqrt((X * X) + (Y * Y));}
    public Vector2f Normalized { get => new Vector2f(X / Magnitude, Y / Magnitude); }

    public void Normalize()
    {
        Vector2f normalizedVector = Normalized;
        X = normalizedVector.X;
        Y = normalizedVector.X;
    }

    public static Vector2f operator+(Vector2f a, Vector2f b)
    {
        float x = a.X + b.X;
        float y = a.Y + b.Y;
        return new Vector2f(x, y);
    }

    public static Vector2f operator-(Vector2f a, Vector2f b)
    {
        float x = a.X - b.X;
        float y = a.Y - b.Y;

        return new Vector2f(x, y);
    }

    public Vector2f(float x, float y)
    {
        X = x;
        Y = y;
    }

}
