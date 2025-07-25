using Godot;

public partial class Haven : StaticBody2D
{
    public string VisTexture = "res://Sprites/VisMasks/HavenMask.png";

    private VisSource VisSource;
    private Area2D ChargeRegion;

    public override void _Ready()
    {
        VisSource = GetNode<VisSource>("VisSource");
        VisSource.Init(VisTexture, Vector2.Zero);
        VisSource.VisShape.GlobalPosition = GlobalPosition;

        ChargeRegion = GetNode<Area2D>("ChargeRegion");
    }

    public override void _PhysicsProcess(double delta)
    {
        foreach (var body in ChargeRegion.GetOverlappingBodies())
        {
            if (body is Player)
            {
                Player player = (Player)body;

                if (player.CurrentEnergy + 120.0f * (float)delta < 1000.0f)
                {
                    player.CurrentEnergy += 120.0f * (float)delta;
                }
                else
                {
                    player.CurrentEnergy = 1000.0f;
                }
            }
        }
    }
}
