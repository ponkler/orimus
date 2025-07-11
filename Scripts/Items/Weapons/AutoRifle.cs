using Godot;

public partial class AutoRifle : Weapon
{
    private int _baseDamage = 15;
    private float _fireDelay = 0.15f;
    public override int BaseDamage { get => _baseDamage; set => _baseDamage = value; }

    public override float FireDelay { get => _fireDelay; set => _fireDelay = value; }

    public Line2D Bullet = null;
    public Texture2D Texture = ResourceLoader.Load<Texture2D>("res://Sprites/rifle.png");

    public override void ClearEffects()
    {
        if (Bullet != null)
        {
            Bullet.QueueFree();
            Bullet = null;
        }
    }

    public override void Fire(Vector2 origin, Vector2 target)
    {
        if (Bullet != null) 
        {
            Bullet.QueueFree();
            Bullet = null;
        }


        Bullet = new Line2D();

        Bullet.Width = 1.0f;
        Bullet.DefaultColor = Colors.Cyan;

        Bullet.AddPoint(origin);
        Bullet.AddPoint(target.GetRandomInRadius(2.0f));
        AddChild(Bullet);
    }
}