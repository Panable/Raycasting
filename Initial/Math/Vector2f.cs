namespace Math;

public class Vector2f
{
    private float _x;
    private float _y;

    public float X { get => _x; set => _x = value; }
    public float Y { get => _y; set => _y = value; }
    public float Magnitude { get => MathF.Sqrt((X * X) + (Y * Y)); }
    public static Vector2f Zero
    {
        get
        {
            return new Vector2f(0.0f, 0.0f);
        }
    }

    public Vector2f Normalized
    {
        get
        {
            if (Magnitude == 0) return Vector2f.Zero;
            return new Vector2f(X / Magnitude, Y / Magnitude);
        }
    }

    public static Vector2f Up
    {
        get
        {
            return new Vector2f(0.0f, 1.0f);
        }
    }

    public static Vector2f Down
    {
        get
        {
            return new Vector2f(0.0f, -1.0f);
        }
    }

    public static Vector2f Left
    {
        get
        {
            return new Vector2f(-1.0f, 0.0f);
        }
    }

    public static Vector2f Right
    {
        get
        {
            return new Vector2f(1.0f, 0.0f);
        }
    }

    public static Vector2f operator +(Vector2f a, Vector2f b)
    {
        float x = a.X + b.X;
        float y = a.Y + b.Y;
        return new Vector2f(x, y);
    }

    public static Vector2f operator -(Vector2f a, Vector2f b)
    {
        float x = a.X - b.X;
        float y = a.Y - b.Y;

        return new Vector2f(x, y);
    }

    public static Vector2f operator *(Vector2f a, float b)
    {
        float x = a.X * b;
        float y = a.Y * b;

        return new Vector2f(x, y);
    }

    public static Vector2f operator *(float a, Vector2f b)
    {
        float x = b.X * a;
        float y = b.Y * a;

        return new Vector2f(x, y);
    }

    public Vector2f(float x, float y)
    {
        X = x;
        Y = y;
    }

    public Vector2f(SplashKitSDK.Point2D pt)
    {
        X = (float)pt.X;
        Y = (float)pt.Y;
    }

    public Vector2f() : this(0.0f, 0.0f)
    {
    }

    public void Normalize()
    {
        Vector2f normalizedVector = Normalized;
        X = normalizedVector.X;
        Y = normalizedVector.Y;
    }

    public static float Distance(Vector2f a, Vector2f b)
    {
        Vector2f dir = b - a;
        return dir.Magnitude;
    }

    // https://matthew-brett.github.io/teaching/rotation_2d.html
    public void Rotate(float angle)
    {
        // x = x*cosθ - y * sinθ
        // y = x*sinθ + y * cosθ

        float cos = MathF.Cos(angle);
        float sin = MathF.Sin(angle);

        X = X * cos - Y * sin;
        Y = X * sin + Y * cos;
    }

    public string ToStr() => $"{X}, {Y}";
}
