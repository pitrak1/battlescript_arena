using System.Collections.Generic;

public class MapGenerator
{
    public TileTypes[,] Generate(MapParameters parameters)
    {
        TileTypes[,] result = new TileTypes[parameters.Size, parameters.Size];

        int horizontalFreeSpaces = Math.Ceiling(parameters.Size / 2) - 1;
        int verticalFreeSpaces = horizontalFreeSpaces - 1;

        List<(int x, int y)> emptySpaces = new List<(int x, int y)>();

        for (int i = 0; i < parameters.Size; i++)
        {
            for (int j = 0; j < parameters.Size; j++)
            {
                if (i + j < horizontalFreeSpaces)
                {
                    emptySpaces.Add((i, j));
                }

                if (i + j > 2 * parameters.Size - 1 - horizontalFreeSpaces)
                {
                    emptySpaces.Add((i, j));
                }

                if (Math.Abs(i - j) > parameters.Size - 1 - verticalFreeSpaces)
                {
                    emptySpaces.Add((i, j));
                }
            }
        }
    }
}
