using Godot;

public enum BehaviorMode
{
    ATTACK,
    STRAFE,
    FLEE
}

public partial class Enemy : CharacterBody2D
{
    const float MAX_SPEED = 50.0f;

    private Node2D VisualsGroup;
    private PackedScene VisualScene = ResourceLoader.Load<PackedScene>("res://Scenes/enemy_visual.tscn");
    public Node2D Visual;

    public override void _Ready()
    {
        VisualsGroup = (Node2D)GetTree().GetFirstNodeInGroup("EnemyVisuals");
        Visual = VisualScene.Instantiate<Node2D>();
        VisualsGroup.AddChild(Visual);
    }

    public override void _PhysicsProcess(double delta)
    {
        Visual.GlobalPosition = GlobalPosition;
    }
}
