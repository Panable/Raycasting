using SplashKitSDK;
using Math;
namespace Initial;

public class Doom
{
    /* Window Dimensions */
    public const float SCALE = 2.5f;
    public const int WIDTH  = (int)(800 * SCALE);
    public const int HEIGHT = (int)(800 * SCALE);

    /* Grid */
    public const int GRID_COLS = 10;
    public const int GRID_ROWS = 10;

    public const float CELL_WIDTH = WIDTH / GRID_COLS;
    public const float CELL_HEIGHT = HEIGHT / GRID_ROWS;

    public Doom()
    {
    }

    public void Run()
    {
        SplashKit.HideMouse();
        Color backgroundColor = SplashKit.StringToColor("#181818");
        Window window = new Window("Raycasting", WIDTH, HEIGHT);
        double _lastTime = SplashKit.CurrentTicks();
        while (!window.CloseRequested)
        {
            // Calculate deltaTime
            double currentTime = SplashKit.CurrentTicks();
            double deltaTime = (currentTime - _lastTime) / 1000.0;
            _lastTime = currentTime;
            SplashKit.ProcessEvents();
            SplashKit.ClearScreen(backgroundColor);
            Update(deltaTime);
            Render();
            SplashKit.RefreshScreen(144);
        }
    }

    Vector2f circleA = new Vector2f(5.25f, 5.5f);
    Vector2f circleB = new Vector2f(3.25f, 1.0f);


    public void Update(double deltaTime)
    {
        Vector2f pos = new Vector2f(SplashKit.MousePosition());
        pos.X /= CELL_WIDTH;
        pos.Y /= CELL_HEIGHT;
        circleB = pos;

        Vector2f keyInput = new Vector2f();
        if (SplashKit.KeyDown(KeyCode.WKey))
        {
            keyInput.Y = -1.0f;
        }
        if (SplashKit.KeyDown(KeyCode.AKey))
        {
            keyInput.X = -1.0f;
        }
        if (SplashKit.KeyDown(KeyCode.SKey))
        {
            keyInput.Y = 1.0f;
        }
        if (SplashKit.KeyDown(KeyCode.DKey))
        {
            keyInput.X = 1.0f;
        }
        float speed = 2.0f;
        keyInput.Normalize();
        // Console.WriteLine(keyInput.ToStr());
        keyInput *= (float)deltaTime * speed;
        circleA += keyInput;

        DDA();
    }

    public void Render()
    {
        /* Draw Board */
        Color color = SplashKit.StringToColor("#505050");
        int lineWidth = 2;

        for (int x = 0; x <= GRID_COLS; ++x)
        {
            Vector2f start = new Vector2f(x, 0);
            Vector2f end = new Vector2f(x, GRID_ROWS);
            DanRenderer.DrawLine(color, start, end, lineWidth);
        }

        for (int y = 0; y <= GRID_ROWS; ++y)
        {
            Vector2f start = new Vector2f(0, y);
            Vector2f end = new Vector2f(GRID_COLS, y);
            DanRenderer.DrawLine(color, start, end, lineWidth);
        }

        DanRenderer.DrawCircle(Color.CornflowerBlue, circleA, 25);
        DanRenderer.DrawCircle(Color.Plum, circleB, 25);
        DanRenderer.DrawLine(Color.Tan, circleA, circleB, 1);
    }

    public void DDA()
    {
        Vector2f start = circleA;
        Vector2f dir = circleB - circleA;
        float dirMagnitude = dir.Magnitude;

        Vector2f stepSize = new Vector2f(dirMagnitude / MathF.Abs(dir.X), dirMagnitude / MathF.Abs(dir.Y));
        Vector2i stepSign = new Vector2i();

        Vector2f len = new Vector2f();

        if (dir.X < 0) // going left
        {
            stepSign.X = -1;
            float mapX = MathF.Floor(start.X); //left side of grid
            len.X = (start.X - mapX) * stepSize.X;
        }
        else // going right
        {
            stepSign.X = 1;
            float mapX = MathF.Floor(start.X) + 1; //right side of grid
            len.X = (mapX - start.X) * stepSize.X;
        }

        if (dir.Y < 0) // going up
        {
            stepSign.Y = -1;
            float mapY = MathF.Floor(start.Y);     // top part of grid
            len.Y = (start.Y - mapY) * stepSize.Y;
        }
        else // going down
        {
            stepSign.Y = 1;
            float mapY = MathF.Floor(start.Y) + 1; // bottom part of grid
            len.Y = (mapY - start.Y) * stepSize.Y;
        }
        float distance = 0.0f;
        /*----------------------------------------*/
        while (distance < dirMagnitude)
        {
            Vector2f coord = start + (dir.Normalized * distance);
            DanRenderer.DrawCircle(Color.Red, coord, 8.5f);
            if (len.X < len.Y)
            {
                distance = len.X;
                len.X += stepSize.X;
            }
            else
            {
                distance = len.Y;
                len.Y += stepSize.Y;
            }
        }
    }
}
