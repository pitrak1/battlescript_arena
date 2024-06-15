public class MapParameters
{
    public int Size
    {
        get => Size;
        set => Size = Math.Clamp(value, 3, 15);
    }

    public void MapParameters(int size)
    {
        Size = size;
    }
}