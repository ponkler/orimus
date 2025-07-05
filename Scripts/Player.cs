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

	public float MaxEnergy = 1000.0f;
	public float CurrentEnergy = 1000.0f;

	private Node2D EnergyMeters;

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

		EnergyMeters = GetNode<Node2D>("EnergyMeters");
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
		UpdateEnergyMeters();
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

		foreach (Sprite2D meter in EnergyMeters.GetChildren())
		{
			meter.Visible = false;
		}

		switch (Body.Rotation)
		{
			case var _ when (Body.Rotation > float.Pi / 4 || Body.Rotation <= -5 * float.Pi / 4):
				// FACING LEFT
				EnergyMeters.GetNode<Sprite2D>("Right").Visible = true;
				break;
            case var _ when (Body.Rotation < -5 * float.Pi / 4 || Body.Rotation <= -3 * float.Pi / 4):
                // FACING UP
                EnergyMeters.GetNode<Sprite2D>("Bottom").Visible = true;
                break;
            case var _ when (Body.Rotation < -3 * float.Pi / 4 || Body.Rotation <= -float.Pi / 4):
                // FACING RIGHT
                EnergyMeters.GetNode<Sprite2D>("Left").Visible = true;
                break;
            case var _ when (Body.Rotation < -float.Pi / 4 || Body.Rotation <= float.Pi / 4):
                // FACING DOWN
                EnergyMeters.GetNode<Sprite2D>("Top").Visible = true;
                break;
        }
	}

	public void UpdateEnergyMeters()
	{
		GD.Print(Mathf.Ceil(CurrentEnergy / (MaxEnergy / 10.0f)));
		foreach (Sprite2D meter in EnergyMeters.GetChildren())
		{
			if (CurrentEnergy == MaxEnergy) 
			{ 
				meter.Frame = 11;
				continue;
			}

			meter.Frame = (int)Mathf.Ceil( CurrentEnergy / (MaxEnergy / 10.0f) );
		}
	}

	public void Aim()
	{
		Vector2 mousePos = GetGlobalMousePosition();

		GunRaycast.TargetPosition = GunRaycast.ToLocal(mousePos);

		TargetBubble.GlobalPosition = mousePos;
	}
}
