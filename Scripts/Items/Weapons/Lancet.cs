using Godot;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public partial class Lancet : Weapon
{
    private int _baseDamage = 20;
    private float _fireDelay = 2.0f;
    private float _fireCost = 30.0f;
    private float _knockBack = 10.0f;
    private float _cameraZoom = 0.6f;
    private string _visPath = "res://Sprites/VisMasks/LancetMask.png";
    private Vector2 _visOffset = new Vector2(480.0f, 0.0f);

    public override int BaseDamage { get => _baseDamage; set => _baseDamage = value; }

    public override float FireDelay { get => _fireDelay; set => _fireDelay = value; }

    public override float FireCost { get => _fireCost; set => _fireCost = value; }
    public override float Knockback { get => _knockBack; set => _knockBack = value; }
    public override float CameraZoom { get => _cameraZoom; set => _cameraZoom = value; }
    public override string VisPath { get => _visPath; set => _visPath = value; }
    public override Vector2 VisOffset { get => _visOffset; set => _visOffset = value; }

    private RayCast2D Hitcast;
    private Node2D BulletOrigin;

    public Line2D Bullet = null;

    public override void _Ready()
    {
        Hitcast = GetNode<RayCast2D>("GunRaycast");
        BulletOrigin = GetNode<Node2D>("BulletOrigin");
    }

    public override void ClearEffects()
    {
        if (Bullet != null)
        {
            Tween bulletTween = GetTree().CreateTween();

            bulletTween.TweenProperty(Bullet, "modulate", new Color(1.0f, 1.0f, 1.0f, 0.0f), 0.2f);
        }
    }

    public override void Fire()
    {
        if (Bullet != null)
        {
            Bullet.QueueFree();
            Bullet = null;
        }

        Bullet = new Line2D();

        Bullet.Width = 2.0f;
        Bullet.DefaultColor = Colors.Cyan;

        Bullet.AddPoint(BulletOrigin.GlobalPosition);

        if (Hitcast.IsColliding())
        {
            var target = Hitcast.GetCollider();

            Bullet.AddPoint(Hitcast.GetCollisionPoint());

            if (target is Enemy)
            {
                ((Enemy)target).TakeDamage(_baseDamage);
                ((Enemy)target).TakeKnockback(Hitcast.GetCollisionNormal(), _knockBack);
            }
        }
        else
        {
            Bullet.AddPoint(GetGlobalMousePosition());
        }

        Bullet.TopLevel = true;

        AddChild(Bullet);
    }
}