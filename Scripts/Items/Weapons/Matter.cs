using Godot;
using System.Collections.Generic;

public partial class Matter : Weapon
{
    private int _baseDamage = 14;
    private float _fireDelay = 0.5f;
    private float _fireCost = 27.0f;
    private float _knockBack = 30.0f;
    private float _cameraZoom = 1.0f;
    private string _visPath = "res://Sprites/VisMasks/MatterMask.png";
    private Vector2 _visOffset = new Vector2(96.0f, 0.0f);

    public override int BaseDamage { get => _baseDamage; set => _baseDamage = value; }
    public override float FireDelay { get => _fireDelay; set => _fireDelay = value; }
    public override float FireCost { get => _fireCost; set => _fireCost = value; }
    public override float Knockback { get => _knockBack; set => _knockBack = value; }
    public override float CameraZoom { get => _cameraZoom; set => _cameraZoom = value; }
    public override string VisPath { get => _visPath; set => _visPath = value; }
    public override Vector2 VisOffset { get => _visOffset; set => _visOffset = value; }

    private Area2D HurtBox;
    private CpuParticles2D Spray;

    public override void _Ready()
    {
        HurtBox = GetNode<Area2D>("Hurtbox");
        Spray = GetNode<CpuParticles2D>("Spray");
    }

    public override void ClearEffects()
    {
        return;
    }

    public override void Fire()
    {
        foreach (var body in HurtBox.GetOverlappingBodies())
        {
            if (body is Enemy)
            {
                ((Enemy)body).TakeDamage(_baseDamage);
                ((Enemy)body).TakeKnockback(Spray.GlobalPosition.DirectionTo(body.GlobalPosition), _knockBack);
            }
        }

        Spray.Emitting = true;
    }
}