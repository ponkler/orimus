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

    private Node2D VisGroup;
    private VisShape VisShape;

    public override void _Ready()
    {
        VisGroup = (Node2D)GetTree().GetFirstNodeInGroup("VisShapes");
        VisShape = new VisShape(VisRadius, HasArc, ArcAngle, ArcWidth, ArcRadius);
        VisShape.VisibilityLayer = 1 << 1;
        VisGroup.AddChild(VisShape);
    }

    public override void _Process(double delta)
    {
        Vector2 localAimPos = GetLocalMousePosition();
        float localAimAngle = Position.AngleTo(localAimPos) + (float.Pi / 4.0f);
        Vector2 startPos = -localAimPos.Normalized() * (VisRadius - 1.0f);

        VisShape.aimAngle = localAimAngle;
        VisShape.startPos = startPos;
        VisShape.GlobalPosition = GlobalPosition;
    }
}
