using Godot;

public partial class VisSource : Node2D
{
    [Export]
    private float VisRadius = 0.0f;
    [Export]
    private bool HasArc;

    [Export]
    private float ArcAngle;
    [Export]
    private float ArcWidth;
    [Export]
    private float ArcRadius;

    public override void _Draw()
    {
        if (HasArc)
        {
            float arcWidthRad = Mathf.DegToRad(ArcWidth);
            Vector2 localAimPos = ToLocal(GetGlobalMousePosition());
            float localAimAngle = Position.AngleTo(localAimPos) + (float.Pi / 4.0f);

            float startAngle = localAimAngle - (arcWidthRad / 2.0f);

            Vector2 startPos = -localAimPos.Normalized() * (VisRadius - 1.0f);

            DrawArc(startPos, ArcRadius, startAngle, startAngle + arcWidthRad, 32, new Color(1.0f, 1.0f, 1.0f, 1.0f), ArcRadius * 2);
        }
        DrawCircle(Vector2.Zero, VisRadius, new Color(1.0f, 1.0f, 1.0f, 1.0f));
    }

    public override void _Process(double delta)
    {
        QueueRedraw();
    }
}
