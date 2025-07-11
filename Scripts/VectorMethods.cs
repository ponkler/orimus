using Godot;

public static partial class VectorMethods
{
    private static readonly RandomNumberGenerator rng = new();

    public static Vector2 GetRandomInRadius(this Vector2 point, float radius)
    {
        rng.Randomize();

        float x = point.X + rng.RandfRange(-radius, radius);
        float y = point.Y + rng.RandfRange(-radius, radius);

        return new Vector2(x, y);
    }
}
