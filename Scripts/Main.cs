using Godot;
using GodotPlugins.Game;

public partial class Main : Node2D
{
	public float Zoom = 1.0f;

	private Camera2D Camera;

	private Camera2D AsciiCamera;
	private TextureRect AsciiLayer;

	private Camera2D VisCamera;
	private ColorRect LightLayer;
	private ColorRect VisLayer;

	private Player Player;

	private GpuParticles2D Particles;

	public override void _Ready()
	{
		Camera = GetNode<Camera2D>("Camera");

		AsciiCamera = GetNode<Camera2D>("AsciiRenderer/Viewport/AsciiCamera");
		AsciiLayer = GetNode<TextureRect>("AsciiLayer");

		VisCamera = GetNode<Camera2D>("VisRenderer/Viewport/VisCamera");
        LightLayer = GetNode<ColorRect>("VisRenderer/Viewport/LightMask");
		VisLayer = GetNode<ColorRect>("VisLayer");

		Player = GetNode<Player>("World/Player");

		Particles = GetNode<GpuParticles2D>("WorldParticles");

		Player.PlayerChangedWeapon += OnPlayerWeaponChanged;
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 camTarget = Player.PivotPoint.Lerp(GetGlobalMousePosition(), 0.2f);
		Camera.GlobalPosition = Camera.GlobalPosition.Lerp(camTarget, 0.1f);

		AsciiCamera.GlobalPosition = Camera.GlobalPosition;
		AsciiLayer.GlobalPosition = Camera.GlobalPosition - AsciiLayer.Size / 2;

		VisCamera.GlobalPosition = Camera.GlobalPosition;
        LightLayer.GlobalPosition = Camera.GlobalPosition - LightLayer.Size / 2;
        VisLayer.GlobalPosition = Camera.GlobalPosition - VisLayer.Size / 2;

        Particles.GlobalPosition = Camera.GlobalPosition;

		Camera.Zoom = Camera.Zoom.Lerp(new Vector2(Zoom, Zoom), 0.1f);
        AsciiCamera.Zoom = AsciiCamera.Zoom.Lerp(new Vector2(Zoom, Zoom), 0.1f);
        VisCamera.Zoom = VisCamera.Zoom.Lerp(new Vector2(Zoom, Zoom), 0.1f);
        AsciiLayer.Size = AsciiLayer.Size.Lerp(new Vector2(1152, 648) / Zoom, 0.1f);
		((ShaderMaterial)AsciiLayer.Material).SetShaderParameter("zoom", Zoom);
    }

	public void SetZoom(float zoom)
	{
		Zoom = zoom;
	}

	public void OnPlayerWeaponChanged(Weapon weapon)
	{
		Tween zoomTween = GetTree().CreateTween();

		zoomTween.TweenMethod(new Callable(this, nameof(SetZoom)), Zoom, weapon.CameraZoom, 0.5f);
	}
}
