
using Godot;
using System.Collections.Generic;
using System.Linq;

public partial class Main : Node2D
{
    private TextureRect VisLayer;
    private NoiseTexture2D NoiseTexture;
    private FastNoiseLite Noise;

    private List<Node2D> visSources;
    private RandomNumberGenerator rng = new RandomNumberGenerator();
    public override void _Ready()
    {
        VisLayer = GetNode<TextureRect>("VisibilityLayer");

        NoiseTexture = (NoiseTexture2D)VisLayer.Texture;
        Noise = (FastNoiseLite)NoiseTexture.Noise;

        visSources = GetTree().GetNodesInGroup("VisSource").Select(node => (Node2D)node).ToList();
    }

    public override void _Process(double delta)
    {
        Noise.Offset += new Vector3(0.0f, 0.0f, (float)delta * 5.0f);
    }
}
