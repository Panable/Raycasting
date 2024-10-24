using Math;
using SplashKitSDK;

namespace Initial;

public class Minimap
{

    public const float CELL_WIDTH = Doom.WIDTH / Map.GRID_COLS;
    public const float CELL_HEIGHT = Doom.HEIGHT / Map.GRID_ROWS;

    private Player _player;
    private Map _map;
    private Raycaster _raycaster;
    private float _x, _y, _scale;

    public Minimap(Player player, Map map, Raycaster raycaster, float x, float y, float scale)
    {
        _player = player;
        _map = map;
        _raycaster = raycaster;
        _x = x;
        _y = y;
        _scale = scale;
    }


    public static Vector2f MapToWorld(Vector2f map)
    {
        return new Vector2f(map.X * CELL_WIDTH, map.Y * CELL_HEIGHT);
    }

    public void Render()
    {
        /* Draw Board */
        Color color = SplashKit.StringToColor("#505050");
        int lineWidth = 2;

        // draw vertical lines
        for (int x = 0; x <= Map.GRID_COLS; ++x)
        {
            Vector2f start = new Vector2f(_x + x, _y) * CELL_WIDTH * _scale;
            Vector2f end = new Vector2f(_x + x, _y + Map.GRID_ROWS) * CELL_WIDTH * _scale;
            DanRenderer.DrawLine(color, start, end, lineWidth);
        }

        // draw horizontal lines
        for (int y = 0; y <= Map.GRID_ROWS; ++y)
        {
            Vector2f start = new Vector2f(_x, _y + y) * CELL_HEIGHT * _scale;
            Vector2f end = new Vector2f(_x + Map.GRID_COLS, _y + y) * CELL_HEIGHT * _scale;
            DanRenderer.DrawLine(color, start, end, lineWidth);
        }

        // draw tiles
        for (int x = 0; x < Map.GRID_ROWS; ++x)
        {
            for (int y = 0; y < Map.GRID_COLS; ++y)
            {
                if (_map.isWall(x, y))
                {
                    Vector2f pos = new Vector2f((_x + x) * CELL_WIDTH, (_y + y) * CELL_HEIGHT) * _scale;
                    Vector2f size = new Vector2f(CELL_WIDTH, CELL_HEIGHT) * _scale;
                    DanRenderer.DrawRectangle(Color.Black, pos, size);
                }
            }
        }

        DanRenderer.DrawCircle(Color.CornflowerBlue, MapToWorld(_player.Position) * _scale, 25 * _scale);
        Raycaster.RaycastDetails col = _raycaster.CastRay(_player.Direction * 10.0f);
        Vector2f line = col.HitCoordinate - _player.Position;
        DanRenderer.DrawCircle(Color.Tan, MapToWorld(col.HitCoordinate) * _scale, 25 * _scale);
        DanRenderer.DrawLine(Color.WhiteSmoke, MapToWorld(_player.Position + (_player.Direction * _scale)) * _scale, MapToWorld(col.HitCoordinate) * _scale, lineWidth);
    }

}
