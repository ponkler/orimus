using Godot;

public abstract partial class Weapon : Node2D
{
    public abstract int BaseDamage { get; set; }
    public abstract float FireDelay { get; set; }
    public abstract void ClearEffects();
    public abstract void Fire(Vector2 origin, Vector2 target);
}