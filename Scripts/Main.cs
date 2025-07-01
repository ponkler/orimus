
using Godot;
using System.Collections.Generic;
using System.Linq;

public partial class Main : Node2D
{
	private Camera2D Camera;
	private Player Player;
	private List<Node2D> visSources;

	private CpuParticles2D Particles;

	public override void _Ready()
	{
		Camera = GetNode<Camera2D>("Camera");
		Player = GetNode<Player>("Player");

		Particles = GetNode<CpuParticles2D>("WorldParticles");

		visSources = GetTree().GetNodesInGroup("VisSource").Select(node => (Node2D)node).ToList();
	}

	public override void _PhysicsProcess(double delta)
	{
		Camera.GlobalPosition = Player.PivotPoint.Lerp(GetGlobalMousePosition(), 0.3f);

		Particles.GlobalPosition = Camera.GlobalPosition;
	}
}
