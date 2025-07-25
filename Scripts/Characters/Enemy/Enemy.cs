using Godot;
using System.Threading.Tasks;

public enum BehaviorMode
{
    APPROACH,
    ATTACK,
    STRAFE,
    FLEE
}

public enum Size
{
    SMALL = 32,
    MEDIUM = 64,
    LARGE = 128
}

public partial class Enemy : CharacterBody2D
{
    const float MAX_SPEED = 200.0f;
    private float CurrentSpeed => (1 - ((3 * Radius) / 1024)) * MAX_SPEED;

    private float Radius = 32.0f;

    private CollisionShape2D Collider;

    private VisSource VisSource;
    private Node2D VisualsGroup;
    private PackedScene VisualScene = ResourceLoader.Load<PackedScene>("res://Scenes/enemy_visual.tscn");
    public EnemyVisual Visual;
    private Area2D Sight;

    public int MaxHealth;
    public int CurrentHealth;

    private CharacterBody2D Target;

    public override void _Ready()
    {
        Collider = GetNode<CollisionShape2D>("Collider");
        ((CircleShape2D)Collider.Shape).Radius = Radius * 0.8f;

        VisSource = GetNode<VisSource>("VisSource");

        VisualsGroup = (Node2D)GetTree().GetFirstNodeInGroup("EnemyVisuals");

        Visual = VisualScene.Instantiate<EnemyVisual>();

        Visual.Radius = Radius;
        Visual.OriginalRadius = Radius;

        VisualsGroup.AddChild(Visual);

        Sight = GetNode<Area2D>("Sight");
    }

    public override void _PhysicsProcess(double delta)
    {
        Visual.GlobalPosition = GlobalPosition;

        foreach (var body in Sight.GetOverlappingBodies())
        {
            if (body is Player)
            {
                Target = (CharacterBody2D)body;
            }
        }

        if (Target != null) 
        {
            Velocity = GlobalPosition.DirectionTo(Target.GlobalPosition) * CurrentSpeed;
        }

        MoveAndSlide();
    }

    public void TakeDamage(int damage)
    {
        if (Radius - damage < 8)
        {
            Visual.QueueFree();
            VisSource.Remove();
            QueueFree();
            return;
        }

        Radius -= damage;

        Visual.Radius = Radius;
        Visual.UpdateProperties();
        Visual.SetPosition();

        ((CircleShape2D)Collider.Shape).Radius = Radius * 0.8f;
    }

    public void TakeKnockback(Vector2 direction, float power)
    {
        GlobalPosition += -direction * power;
    }
}