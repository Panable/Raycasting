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
}
