using Godot;
using System;

public partial class Ground : Node2D
{
    
    private Parallax2D parrallax;

    public override void _Ready()
    {
        parrallax = GetNode<Parallax2D>("Parallax2D");
        GD.Print("Ground node is ready!");
        
    }

    public void StopScrolling()
    {
        parrallax.Autoscroll = Vector2.Zero;
    }

    public void StartScrolling(float speed)
    {
        parrallax.Autoscroll = new Vector2(-speed, 0);
    }
}
