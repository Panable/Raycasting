using SplashKitSDK;
using Math;
namespace Initial;

public class Doom
{
    /* Window Dimensions */
    public const float SCALE = 2.5f;
    public const int WIDTH = (int)(800 * SCALE);
    public const int HEIGHT = (int)(800 * SCALE);

    /* Grid */
    public const int GRID_COLS = 10;
    public const int GRID_ROWS = 10;

    public const float CELL_WIDTH = WIDTH / GRID_COLS;
    public const float CELL_HEIGHT = HEIGHT / GRID_ROWS;

    Player player = new Player();
    Map map = new Map();

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
            Update((float)deltaTime);
            Render();
            SplashKit.RefreshScreen(144);
        }
    }

    public void Update(float deltaTime)
    {
        player.Update(deltaTime);
    }

    public void RenderMinimap(float mapX, float mapY, float scale)
    {
        /* Draw Board */
        Color color = SplashKit.StringToColor("#505050");
        int lineWidth = 2;


        // draw vertical lines
        for (int x = 0; x <= GRID_COLS; ++x)
        {
            Vector2f start = new Vector2f(mapX + x, mapY) * CELL_WIDTH * scale;
            Vector2f end = new Vector2f(mapX + x, mapY + GRID_ROWS) * CELL_WIDTH * scale;
            DanRenderer.DrawLine(color, start, end, lineWidth);
        }

        // draw horizontal lines
        for (int y = 0; y <= GRID_ROWS; ++y)
        {
            Vector2f start = new Vector2f(mapX, mapY + y) * CELL_HEIGHT * scale;
            Vector2f end = new Vector2f(mapX + GRID_COLS, mapY + y) * CELL_HEIGHT * scale;
            DanRenderer.DrawLine(color, start, end, lineWidth);
        }

        // draw tiles
        for (int x = 0; x < GRID_ROWS; ++x)
        {
            for (int y = 0; y < GRID_COLS; ++y)
            {
                if (map.isWall(x, y))
                {
                    Vector2f pos = new Vector2f((mapX + x) * CELL_WIDTH, (mapY + y) * CELL_HEIGHT) * scale;
                    Vector2f size = new Vector2f(CELL_WIDTH, CELL_HEIGHT) * scale;
                    DanRenderer.DrawRectangle(Color.Black, pos, size);
                }
            }
        }

        DanRenderer.DrawCircle(Color.CornflowerBlue, DanRenderer.MapToWorld(player.Position) * scale, 25 * scale);
        Vector2f col = DDA();
        Vector2f line = col - player.Position;
        DanRenderer.DrawCircle(Color.Tan, DanRenderer.MapToWorld(col) * scale, 25 * scale);
        DanRenderer.DrawLine(Color.WhiteSmoke, DanRenderer.MapToWorld(player.Position + (player.Direction * scale)) * scale, DanRenderer.MapToWorld(col) * scale, lineWidth);
    }

    public void Render()
    {
        RenderMinimap(0.0f, 0.0f, 0.2f);
    }

    public Vector2f DDA()
    {
        Vector2f start = player.Position;
        Vector2f dir = player.Direction * 10f;
        float dirMagnitude = dir.Magnitude;

        Vector2f stepSize = new Vector2f(dirMagnitude / MathF.Abs(dir.X), dirMagnitude / MathF.Abs(dir.Y));
        Vector2i stepSign = new Vector2i();
        Vector2i mapCheck = new Vector2i((int)start.X, (int)start.Y);

        Vector2f len = new Vector2f();

        bool hit = false;

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
            //DanRenderer.DrawCircle(Color.Red, DanRenderer.MapToWorld(coord) * 0.2f, 6.5f);
            if (len.X < len.Y)
            {
                distance = len.X;
                len.X += stepSize.X;
                mapCheck.X += stepSign.X;
            }
            else
            {
                distance = len.Y;
                len.Y += stepSize.Y;
                mapCheck.Y += stepSign.Y;
            }

            bool outsideX = (int)coord.X > GRID_COLS;
            bool outsideY = (int)coord.Y > GRID_COLS;
            bool wallFound = map.isWall(mapCheck.X, mapCheck.Y);

            if (wallFound || outsideX || outsideY)
            {
                hit = true;
                coord = start + (dir.Normalized * distance);
                Console.WriteLine($"hit found at {coord.ToStr()} map coords {mapCheck.ToStr()}");
                return coord;
            }
        }
        return Vector2f.Zero;
    }
}
