using Math;
using SplashKitSDK;
namespace Initial;

public class DanRenderer
{

    public DanRenderer()
    {
    }



    public static void DrawCircle(Color color, Vector2f position, float radius)
    {
        SplashKit.FillCircle(color, position.X, position.Y, radius);
    }

    public static void DrawRectangle(Color color, Vector2f position, Vector2f size)
    {
        SplashKit.FillRectangle(Color.Black, position.X, position.Y, size.X, size.Y);
    }

    public static void DrawLine(Color color, Vector2f start, Vector2f end, int width)
    {
        SplashKit.DrawLine(color, start.X, start.Y, end.X, end.Y, SplashKit.OptionLineWidth(width));
    }

    public static Vector2f MapToWorld(Vector2f map)
    {
        return new Vector2f(map.X * Doom.CELL_WIDTH, map.Y * Doom.CELL_HEIGHT);
    }

    public static Vector2f WorldToMap(Vector2f world)
    {
        return new Vector2f(world.X / Doom.CELL_WIDTH, world.Y / Doom.CELL_HEIGHT);
    }
}
