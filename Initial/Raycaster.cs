﻿using Math;

namespace Initial;

public class Raycaster
{
    private Map _map;
    private Player _player;

    public Raycaster(Map map, Player player)
    {
        _map = map;
        _player = player;
    }

    // The way DDA works is that it basically iterates through each map unit.
    // It checks if the distance to the map edge your moving towards is shorter on the X or Y
    // depending on which axis it's shorter on it will generate a collision in the cell bounding the axis.
    // it will continue this checking against the old axis.
    public void CastRay()
    {
        Vector2f start = _player.Position;
        Vector2f dir = _player.Direction * 10f;
        float dirMagnitude = dir.Magnitude;

        // step size is how far the ray will travel when moving one unit in x or y.
        // we calculate this by computing the magnitude in proportion to the x or the y.
        Vector2f stepSize = new Vector2f(dirMagnitude / MathF.Abs(dir.X), dirMagnitude / MathF.Abs(dir.Y));
        Vector2i stepSign = new Vector2i();
        Vector2i mapCheck = new Vector2i((int)start.X, (int)start.Y);

        // len keeps track of how far the ray has traveled when moving in X or Y.
        Vector2f len = new Vector2f();

        // find distance to edge depending on direction
        if (dir.X < 0) // going left
        {
            stepSign.X = -1;
            len.X = (start.X - mapCheck.X) * stepSize.X;
        }
        else // going right
        {
            stepSign.X = 1;
            len.X = (mapCheck.X - start.X) * stepSize.X;
        }

        if (dir.Y < 0) // going up
        {
            stepSign.Y = -1;
            len.Y = (start.Y - mapCheck.X) * stepSize.Y;
        }
        else // going down
        {
            stepSign.Y = 1;
            len.Y = (mapCheck.Y - start.Y) * stepSize.Y;
        }
        float distance = 0.0f;
        /*----------------------------------------*/
        while (distance < dirMagnitude)
        {
            Vector2f coord = start + (dir.Normalized * distance);
            if (len.X < len.Y) // move in X
            {
                distance = len.X;         // set distance to the X value of the selected length
                len.X += stepSize.X;      // step the length one unit on X
                mapCheck.X += stepSign.X; // update the map location
            }
            else // move in Y
            {
                distance = len.Y;         // set distance to the Y value of the selected length
                len.Y += stepSize.Y;      // step the length one unit on Y
                mapCheck.Y += stepSign.Y; // update the map location
            }

            bool outsideX = (int)coord.X > Map.GRID_COLS;
            bool outsideY = (int)coord.Y > Map.GRID_ROWS;
            bool wallFound = _map.isWall(mapCheck.X, mapCheck.Y);

            if (wallFound || outsideX || outsideY)
            {
                coord = start + (dir.Normalized * distance);
                Console.WriteLine($"hit found at {coord.ToStr()} map coords {mapCheck.ToStr()}");
            }
        }
    }
}