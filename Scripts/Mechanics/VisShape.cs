using Godot;

public partial class VisShape : PointLight2D
{
    public float AimAngle = 0.0f;

    public override void _Ready()
    {
        ShadowEnabled = true;
    }

    public override void _PhysicsProcess(double delta)
    {
        Rotation = AimAngle;
    }
}