using Godot;

public partial class Player : CharacterBody2D
{
	private Node2D LowerPivot;
	private AnimatedSprite2D LeftTrack;
	private AnimatedSprite2D RightTrack;

	private TextureRect Body;
	private TextureRect LeftHand;
	private TextureRect RightHand;

	private RayCast2D GunRaycast;
	private Area2D TargetBubble;

	private Vector2 newVel = Vector2.Zero;

	public float LookAngle;
	public Vector2 PivotPoint;

	public override void _Ready()
	{
		LowerPivot = GetNode<Node2D>("Lower");
		LeftTrack = LowerPivot.GetNode<AnimatedSprite2D>("LeftTrack");
		RightTrack = LowerPivot.GetNode<AnimatedSprite2D>("RightTrack");

		Body = GetNode<TextureRect>("Body");
		LeftHand = Body.GetNode<TextureRect>("Left");
		RightHand = Body.GetNode<TextureRect>("Right");

		GunRaycast = LeftHand.GetNode<RayCast2D>("GunRaycast");
		TargetBubble = LeftHand.GetNode<Area2D>("TargetBubble");
	}

	public override void _PhysicsProcess(double delta)
	{
		float turnAxis = Input.GetAxis("left", "right");
		float moveAxis = Input.GetAxis("backward", "forward");

		PivotPoint = GlobalPosition + Body.PivotOffset + Body.Position;

		Vector2 moveTurnVector = new Vector2(turnAxis, moveAxis);

		SetVelocity(moveAxis, delta);
		RotateLower(turnAxis, delta);
		RotateUpper();
		Animate(moveTurnVector);
		MoveAndSlide();
		Aim();
	}

	// There has GOT to be a better way to handle this
	public void Animate(Vector2 vec)
	{
		// back -1 forward 1
		// left -1 right 1
		LeftTrack.Play();
		RightTrack.Play();

		if (vec.Y == -1.0f)
		{
			LeftTrack.Animation = "backward";
			RightTrack.Animation = "backward";

			if (vec.X == -1.0f)
			{
				LeftTrack.SpeedScale = 1.0f;
				RightTrack.SpeedScale = 0.25f;
			}
			else if (vec.X == 1.0f)
			{
				LeftTrack.SpeedScale = 0.25f;
				RightTrack.SpeedScale = 1.0f;
			}
			else
			{
				LeftTrack.SpeedScale = 1.0f;
				RightTrack.SpeedScale = 1.0f;
			}
		}
		else if (vec.Y == 1.0f)
		{
			LeftTrack.Animation = "forward";
			RightTrack.Animation = "forward";

			if (vec.X == -1.0f)
			{
				LeftTrack.SpeedScale = 0.25f;
				RightTrack.SpeedScale = 1.0f;
			}
			else if (vec.X == 1.0f)
			{
				LeftTrack.SpeedScale = 1.0f;
				RightTrack.SpeedScale = 0.25f;
			}
			else
			{
				LeftTrack.SpeedScale = 1.0f;
				RightTrack.SpeedScale = 1.0f;
			}
		}
		else
		{
			if (vec.X == -1.0f)
			{
				LeftTrack.SpeedScale = -1.0f;
				RightTrack.SpeedScale = 1.0f;
			}
			else if (vec.X == 1.0f)
			{
				LeftTrack.SpeedScale = 1.0f;
				RightTrack.SpeedScale = -1.0f;
			}
			else
			{
				LeftTrack.Pause();
				RightTrack.Pause();
			}
		}
	}

	public void SetVelocity(float axis, double delta)
	{
		newVel = newVel.Lerp(Vector2.Down * 100.0f, 10.0f * (float)delta);
		Velocity = Vector2.Down.Rotated(LowerPivot.Rotation) * newVel.Length() * axis;
	}

	public void RotateLower(float axis, double delta)
	{
		LowerPivot.Rotation = (float)Mathf.Lerp(LowerPivot.Rotation, LowerPivot.Rotation + delta * axis, 120.0 * delta);
		LowerPivot.Rotation %= 2 * float.Pi;
	}

	public void RotateUpper()
	{
		Vector2 mousePos = GetGlobalMousePosition();

		if (PivotPoint.DistanceTo(mousePos) > 5.0f)
		{
			float angle = PivotPoint.AngleToPoint(mousePos);
			Body.Rotation = angle - (float.Pi / 2);
			LookAngle = Body.Rotation;
		}
	}

	public void Aim()
	{
		Vector2 mousePos = GetGlobalMousePosition();

		GunRaycast.TargetPosition = GunRaycast.ToLocal(mousePos);

		TargetBubble.GlobalPosition = mousePos;
	}
}
