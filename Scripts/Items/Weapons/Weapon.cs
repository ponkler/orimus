using Godot;
using System.Collections.Generic;

public abstract partial class Weapon : Node2D
{
    public abstract int BaseDamage { get; set; }
    public abstract float FireDelay { get; set; }
    public abstract float FireCost { get; set; }
    public abstract float Knockback { get; set; }
    public abstract float CameraZoom { get; set; }
    public abstract string VisPath { get; set; }
    public abstract Vector2 VisOffset { get; set; }
    public abstract void ClearEffects();
    public abstract void Fire();
}