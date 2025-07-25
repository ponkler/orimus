using Godot;
using System.Linq;

public partial class Anchor : StaticBody2D
{
    private Sprite2D Generator;
    private Area2D ChargeRegion;

    private Line2D ChargeLineA;
    private Line2D ChargeLineB;

    private VisSource VisSource;

    public override void _Ready()
    {
        Generator = GetNode<Sprite2D>("Generator");
        ChargeRegion = GetNode<Area2D>("ChargeRegion");

        ChargeLineA = GetNode<Line2D>("ChargeLineA");
        ChargeLineB = GetNode<Line2D>("ChargeLineB");

        VisSource = GetNode<VisSource>("VisSource");
        VisSource.Init("res://Sprites/VisMasks/AnchorMask.png", Vector2.Zero);
    }

    public override void _PhysicsProcess(double delta)
    {
        Generator.Rotation += (float)delta * 2.0f;
        Generator.Rotation %= 360.0f;

        foreach (var body in ChargeRegion.GetOverlappingBodies())
        {
            if (body is Player)
            {
                Player player = (Player)body;

                ChargeLineA.SetPointPosition(1, ToLocal(player.PivotPoint));
                ChargeLineB.SetPointPosition(1, ToLocal(player.PivotPoint));

                if (player.CurrentEnergy + 50.0f * (float)delta < 1000.0f)
                {
                    player.CurrentEnergy += 50.0f * (float)delta;
                }
                else
                {
                    player.CurrentEnergy = 1000.0f;
                }
            }
        }

        if (!ChargeRegion.GetOverlappingBodies().Any(body => body is Player))
        {
            ChargeLineA.SetPointPosition(1, Vector2.Zero);
            ChargeLineB.SetPointPosition(1, Vector2.Zero);
        }

        VisSource.VisShape.GlobalPosition = GlobalPosition;
    }
}
