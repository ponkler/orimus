using Godot;

public partial class Siphon : Weapon
{
    [Signal]
    public delegate void EnergySiphonedEventHandler();

    private int _baseDamage = 1;
    private float _fireDelay = 0.1f;
    private float _fireCost = 2.0f;
    private float _knockBack = 0.0f;
    private float _cameraZoom = 0.8f;
    private string _visPath = "res://Sprites/VisMasks/SiphonMask.png";
    private Vector2 _visOffset = Vector2.Zero;

    public override int BaseDamage { get => _baseDamage; set => _baseDamage = value; }
    public override float FireDelay { get => _fireDelay; set => _fireDelay = value; }
    public override float FireCost { get => _fireCost; set => _fireCost = value; }
    public override float Knockback { get => _knockBack; set => _knockBack = value; }
    public override float CameraZoom { get => _cameraZoom; set => _cameraZoom = value; }
    public override string VisPath { get => _visPath; set => _visPath = value; }
    public override Vector2 VisOffset { get => _visOffset; set => _visOffset = value; }

    private Area2D HurtBox;
    private CpuParticles2D Vortex;

    public override void _Ready()
    {
        HurtBox = GetNode<Area2D>("Hurtbox");
        Vortex = GetNode<CpuParticles2D>("Vortex");
    }

    public override void ClearEffects()
    {
        Vortex.Emitting = false;
    }

    public override void Fire()
    {
        Vortex.Emitting = true;

        foreach (var body in HurtBox.GetOverlappingBodies())
        {
            if (body is Enemy)
            {
                ((Enemy)body).TakeDamage(_baseDamage);
                EmitSignal(SignalName.EnergySiphoned);
                //Player.CurrentEnergy += _baseDamage * 0.5f;
            }
        }
    }
}
