using SplashKitSDK;
using Math;

namespace Initial
{
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

        /* Map Array - 0: empty space, 1: wall */
        public int[,] map = new int[GRID_ROWS, GRID_COLS]
        {
            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1}, // 10 elements
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 1}, // 10 elements
            {1, 0, 0, 0, 1, 1, 0, 0, 0, 1}, // 10 elements
            {1, 0, 0, 0, 0, 1, 0, 0, 0, 1}, // 10 elements
            {1, 0, 1, 1, 0, 0, 0, 1, 0, 1}, // 10 elements
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 1}, // 10 elements
            {1, 1, 0, 0, 0, 1, 1, 1, 0, 1},  // 10 elements
            {1, 1, 0, 0, 0, 1, 1, 1, 0, 1},  // 10 elements
            {1, 1, 0, 0, 0, 1, 1, 1, 0, 1},  // 10 elements
            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1}  // 10 elements
        };

        Vector2f playerPos = new Vector2f(2.5f, 2.5f);
        Vector2f playerDir = new Vector2f(-1.0f, 0.0f);
        Vector2f plane = new Vector2f(0.0f, 0.66f); // FOV

        public Doom() { }

        public void Run()
        {
            SplashKit.HideMouse();
            Color backgroundColor = SplashKit.StringToColor("#181818");
            Window window = new Window("Raycasting", WIDTH, HEIGHT);
            double _lastTime = SplashKit.CurrentTicks();
            while (!window.CloseRequested)
            {
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

        public void Update(double deltaTime)
        {
            // Handle player movement (WASD)
            Vector2f moveDir = new Vector2f();
            float speed = 2.0f;

            if (SplashKit.KeyDown(KeyCode.WKey))
            {
                moveDir += playerDir;
            }
            if (SplashKit.KeyDown(KeyCode.SKey))
            {
                moveDir -= playerDir;
            }
            if (SplashKit.KeyDown(KeyCode.AKey))
            {
                Vector2f leftDir = new Vector2f(playerDir.Y, -playerDir.X); // rotate 90°
                moveDir += leftDir;
            }
            if (SplashKit.KeyDown(KeyCode.DKey))
            {
                Vector2f rightDir = new Vector2f(-playerDir.Y, playerDir.X); // rotate -90°
                moveDir += rightDir;
            }

            moveDir.Normalize();
            moveDir *= speed * (float)deltaTime;
            
            // Check for collision before updating position
            if (map[(int)(playerPos.Y + moveDir.Y), (int)playerPos.X] == 0) playerPos.Y += moveDir.Y;
            if (map[(int)playerPos.Y, (int)(playerPos.X + moveDir.X)] == 0) playerPos.X += moveDir.X;
        }

        public void Render()
        {
            // Raycasting
            for (int x = 0; x < WIDTH; x++)
            {
                float cameraX = 2 * x / (float)WIDTH - 1; // x-coordinate on the camera plane
                Vector2f rayDir = playerDir + (plane * cameraX);

                Vector2i mapPos = new Vector2i((int)playerPos.X, (int)playerPos.Y);

                Vector2f sideDist = new Vector2f();
                Vector2f deltaDist = new Vector2f(
                    MathF.Abs(1 / rayDir.X),
                    MathF.Abs(1 / rayDir.Y)
                );
                float perpWallDist;

                Vector2i step = new Vector2i();
                bool hit = false;
                int side = 0; // 0: X side, 1: Y side

                // Calculate step and initial sideDist
                if (rayDir.X < 0)
                {
                    step.X = -1;
                    sideDist.X = (playerPos.X - mapPos.X) * deltaDist.X;
                }
                else
                {
                    step.X = 1;
                    sideDist.X = (mapPos.X + 1.0f - playerPos.X) * deltaDist.X;
                }
                if (rayDir.Y < 0)
                {
                    step.Y = -1;
                    sideDist.Y = (playerPos.Y - mapPos.Y) * deltaDist.Y;
                }
                else
                {
                    step.Y = 1;
                    sideDist.Y = (mapPos.Y + 1.0f - playerPos.Y) * deltaDist.Y;
                }

                // Perform DDA
                while (!hit)
                {
                    if (sideDist.X < sideDist.Y)
                    {
                        sideDist.X += deltaDist.X;
                        mapPos.X += step.X;
                        side = 0;
                    }
                    else
                    {
                        sideDist.Y += deltaDist.Y;
                        mapPos.Y += step.Y;
                        side = 1;
                    }

                    if (map[mapPos.Y, mapPos.X] > 0) hit = true; // hit a wall
                }

                // Calculate distance projected on camera plane
                if (side == 0)
                    perpWallDist = (mapPos.X - playerPos.X + (1 - step.X) / 2) / rayDir.X;
                else
                    perpWallDist = (mapPos.Y - playerPos.Y + (1 - step.Y) / 2) / rayDir.Y;

                // Calculate height of the wall line
                int lineHeight = (int)(HEIGHT / perpWallDist);

                // Calculate start and end points of the wall line
                int drawStart = -lineHeight / 2 + HEIGHT / 2;
                if (drawStart < 0) drawStart = 0;
                int drawEnd = lineHeight / 2 + HEIGHT / 2;
                if (drawEnd >= HEIGHT) drawEnd = HEIGHT - 1;

                // Choose wall color (make it darker for Y side)
                Color wallColor = (side == 1) ? Color.Gray : Color.LightGray;

                // Draw the wall as a vertical line
                SplashKit.DrawLine(wallColor, x, drawStart, x, drawEnd);
            }
        }
    }
}
