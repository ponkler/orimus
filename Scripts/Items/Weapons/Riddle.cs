using Godot;
using System.Collections.Generic;

public partial class Riddle : Weapon
{
    private int _baseDamage = 3;
    private float _fireDelay = 0.15f;
    private float _fireCost = 10.0f;
    private float _knockBack = 10.0f;
    private float _cameraZoom = 1.0f;
    private string _visPath = "res://Sprites/VisMasks/RiddleMask.png";
    private Vector2 _visOffset = new Vector2(112.0f, 0.0f);

    public override int BaseDamage { get => _baseDamage; set => _baseDamage = value; }
    public override float FireDelay { get => _fireDelay; set => _fireDelay = value; }
    public override float FireCost { get => _fireCost; set => _fireCost = value; }
    public override float Knockback { get => _knockBack; set => _knockBack = value; }
    public override float CameraZoom { get => _cameraZoom; set => _cameraZoom = value; }
    public override string VisPath { get => _visPath; set => _visPath = value; }
    public override Vector2 VisOffset { get => _visOffset; set => _visOffset = value; }

    private RayCast2D Hitcast;
    private Node2D BulletOrigin;
    private Line2D Bullet = null;
    
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

            bulletTween.Connect(Tween.SignalName.Finished, Callable.From(FreeBullet));

            bulletTween.TweenProperty(Bullet, "modulate", new Color(1.0f, 1.0f, 1.0f, 0.0f), 0.05f);
        }
    }

    public void FreeBullet()
    {
        if (Bullet != null)
        {
            Bullet.QueueFree();
            Bullet = null;
        }
    }

    public override void Fire()
    {
        Bullet = new Line2D();

        Bullet.Width = 1.0f;
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