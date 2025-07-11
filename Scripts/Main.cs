using Godot;

public partial class Main : Node2D
{
	private Camera2D Camera;

	private Camera2D AsciiCamera;
	private TextureRect AsciiLayer;

	private Camera2D VisCamera;
	private ColorRect VisLayer;

	private Player Player;

	private GpuParticles2D Particles;

	public override void _Ready()
	{
		Camera = GetNode<Camera2D>("Camera");

		AsciiCamera = GetNode<Camera2D>("AsciiRenderer/Viewport/AsciiCamera");
		AsciiLayer = GetNode<TextureRect>("AsciiLayer");

		VisCamera = GetNode<Camera2D>("VisRenderer/Viewport/VisCamera");
		VisLayer = GetNode<ColorRect>("VisLayer");

		Player = GetNode<Player>("World/Player");

		Particles = GetNode<GpuParticles2D>("WorldParticles");
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 camTarget = Player.PivotPoint.Lerp(GetGlobalMousePosition(), 0.2f);
		Camera.GlobalPosition = Camera.GlobalPosition.Lerp(camTarget, 0.1f);

		AsciiCamera.GlobalPosition = Camera.GlobalPosition;
		AsciiLayer.GlobalPosition = Camera.GlobalPosition - AsciiLayer.PivotOffset;

		VisCamera.GlobalPosition = Camera.GlobalPosition;
        VisLayer.GlobalPosition = Camera.GlobalPosition - AsciiLayer.PivotOffset;

        Particles.GlobalPosition = Camera.GlobalPosition;
	}
}
