using Godot;

public partial class EnemyVisual : Node2D
{
    private CpuParticles2D Trail;
    private GradientTexture2D TrailTexture;

    private TextureRect BodyTexture;
    private GradientTexture2D BodyGradient;

    public float OriginalRadius;

    public float Radius = 0.0f;
    public Color Color = Colors.Cyan;

    public override void _Ready()
    {
        Trail = GetNode<CpuParticles2D>("Trail");
        TrailTexture = (GradientTexture2D)Trail.Texture;

        BodyTexture = GetNode<TextureRect>("Gradient");
        BodyGradient = (GradientTexture2D)BodyTexture.Texture;

        UpdateProperties();
    }

    public void UpdateProperties()
    {
        BodyGradient.Width = (int)Radius * 2;
        BodyGradient.Height = (int)Radius * 2;

        BodyTexture.SetDeferred("size", new Vector2(Radius * 2, Radius * 2));
        BodyTexture.Modulate = Color;

        TrailTexture.Width = (int)Radius;
        TrailTexture.Height = (int)Radius;
    }

    public void SetPosition()
    {
        BodyTexture.Position = new Vector2(-Radius, -Radius);
    }
}
