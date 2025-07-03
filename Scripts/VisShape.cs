using Godot;

public partial class VisShape : Node2D
{
    private float VisRadius = -1.0f;
    private bool HasArc = false;
    private float ArcAngle = -1.0f;
    private float ArcWidth = -1.0f;
    private float ArcRadius = -1.0f;

    public Vector2 startPos = Vector2.Zero;
    public float aimAngle = -1.0f;

    public VisShape(float visRadius, bool hasArc, float arcAngle, float arcWidth, float arcRadius)
    {
        VisRadius = visRadius;
        HasArc = hasArc;
        ArcAngle = arcAngle;
        ArcWidth = arcWidth;
        ArcRadius = arcRadius;
    }

    public override void _Draw()
    {
        if (HasArc)
        {
            float arcWidthRad = Mathf.DegToRad(ArcWidth);
            float startAngle = aimAngle - (arcWidthRad / 2.0f);

            DrawArc(startPos, ArcRadius, startAngle, startAngle + arcWidthRad, 32, new Color(1.0f, 1.0f, 1.0f, 1.0f), ArcRadius * 2);
        }
        DrawCircle(Vector2.Zero, VisRadius, new Color(1.0f, 1.0f, 1.0f, 1.0f));
    }

    public override void _Process(double delta)
    {
        QueueRedraw();
    }
}