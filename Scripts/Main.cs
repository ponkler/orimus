using Godot;
using System.Collections.Generic;
using System.Linq;

public partial class Main : Node2D
{
	private Camera2D Camera;

	private Camera2D AsciiCamera;
	private TextureRect AsciiLayer;

	private Player Player;
	private List<Node2D> visSources;

	private GpuParticles2D Particles;

	public override void _Ready()
	{
		Camera = GetNode<Camera2D>("Camera");

		AsciiCamera = GetNode<Camera2D>("AsciiRenderer/Viewport/AsciiCamera");
		AsciiLayer = GetNode<TextureRect>("AsciiLayer");

		Player = GetNode<Player>("World/Player");

		Particles = GetNode<GpuParticles2D>("WorldParticles");

        visSources = GetTree().GetNodesInGroup("VisSource").Select(node => (Node2D)node).ToList();
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 camTarget = Player.PivotPoint.Lerp(GetGlobalMousePosition(), 0.3f);
		Camera.GlobalPosition = Camera.GlobalPosition.Lerp(camTarget, 0.1f);

		AsciiCamera.GlobalPosition = Camera.GlobalPosition;
		AsciiLayer.GlobalPosition = Camera.GlobalPosition - AsciiLayer.PivotOffset;

        Particles.GlobalPosition = Camera.GlobalPosition;
	}
}
