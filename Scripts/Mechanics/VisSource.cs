using Godot;

public partial class VisSource : Node2D
{
    private Node2D VisGroup;
    public VisShape VisShape;

    public float AimAngle = 0.0f;

    public string VisPath;

    public override void _Ready()
    {
        VisGroup = (Node2D)GetTree().GetFirstNodeInGroup("VisShapes");
    }

    public void Init(string visPath, Vector2 offset)
    {
        VisPath = visPath;
        VisShape = new VisShape();
        VisShape.Texture = ResourceLoader.Load<Texture2D>(VisPath);
        VisShape.Offset = offset;
        VisShape.VisibilityLayer = 1 << 1;
        VisGroup.AddChild(VisShape);
    }

    public void Remove()
    {
        VisShape.QueueFree();
        QueueFree();
    }
}