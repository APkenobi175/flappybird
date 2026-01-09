using Godot;
using System;
using System.ComponentModel.DataAnnotations.Schema;

public partial class Pipes : Node2D
{

    float speed = 200f; // speed variable for the pipes
    public override void _Ready()
	{
		GD.Print("Player node is ready!");
	}

	public override void _Process(double delta)
	{


    float delta_movement = speed * (float)delta; // calculate movement based on delta time
    Position += new Vector2(-delta_movement, 0); // move pipes to the left

    if (Position.X < -750f) // if pipes go off screen to the left
    {
        QueueFree(); // remove pipes from scene
        GD.Print("pipe died");
    }
    }

	public override void _PhysicsProcess(double delta)
	{

    }

    public override void _Draw()
{
    float w = 90f;        // pipe width
    float gap = 180f;     // gap size (normal difficulty)
    float H = 2000f;      // make pipes absurdly tall so they always cover the screen

    // Top pipe: ends at -gap/2
    DrawRect(
        new Rect2(-w / 2f, -H, w, H - gap / 2f),
        Colors.Green,
        false,
        2
    );

    // Bottom pipe: starts at +gap/2
    DrawRect(
        new Rect2(-w / 2f, gap / 2f, w, H),
        Colors.Green,
        false,
        2
    );
}


}
