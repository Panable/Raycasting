using Math;
using SplashKitSDK;
namespace Initial;

public class DanRenderer
{
    public static void DrawCircle(Color color, Vector2f position, float radius)
    {
        SplashKit.FillCircle(color, position.X * Doom.CELL_WIDTH, position.Y * Doom.CELL_HEIGHT, radius);
    }

    public static void DrawLine(Color color, Vector2f start, Vector2f end, int width)
    {
        SplashKit.DrawLine(color, start.X * Doom.CELL_WIDTH, start.Y * Doom.CELL_HEIGHT, end.X * Doom.CELL_WIDTH, end.Y * Doom.CELL_HEIGHT, SplashKit.OptionLineWidth(width));
    }
}
