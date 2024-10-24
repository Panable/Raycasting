namespace Initial;

public class Map
{
    public const int GRID_COLS = 10;
    public const int GRID_ROWS = 10;
    public int[] _map = new int[GRID_COLS * GRID_ROWS] {
            1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
            1, 0, 0, 0, 0, 0, 0, 0, 0, 1,
            1, 0, 0, 0, 1, 1, 0, 0, 0, 1,
            1, 0, 0, 0, 0, 1, 0, 0, 0, 1,
            1, 0, 1, 1, 0, 0, 0, 1, 0, 1,
            1, 0, 0, 0, 0, 0, 0, 0, 0, 1,
            1, 1, 0, 0, 0, 1, 1, 1, 0, 1,
            1, 1, 0, 0, 0, 1, 1, 1, 0, 1,
            1, 1, 0, 0, 0, 1, 1, 1, 0, 1,
            1, 1, 1, 1, 1, 1, 1, 1, 1, 1 
    };

    private int CoordToIndex(int x, int y) => x + y * GRID_COLS;
    public bool isWall(int x, int y) => _map[CoordToIndex(x, y)] > 0;
}
