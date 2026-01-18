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

    if (Position.X < -1200f) // if pipes go off screen to the left
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
        // float w = 90f; // pipe width
        // float gap = 180f; // gap between top and bottom pipes

        // float H = 2000f;   // VERY tall so pipes always extend off-screen
        // The gap should always be at y = 0 + gap/2 for bottom pipe and y = 0 - gap/2 for top pipe so that the gap is always on screen

        // Top pipe: ends at -gap/2, extends upward far off-screen, its bottom part should always be at at least y=0 
        // DrawRect(
        //     new Rect2(-w / 2f, -H - gap / 2f, w, H),
        //     Colors.Green,
        //     false,
        //     2
        // );

        // Bottom pipe: starts at +gap/2, extends downward far off-screen, its top part should always be at least at y=649
        // DrawRect(
        //     new Rect2(-w / 2f, gap / 2f, w, H),
        //     Colors.Green,
        //     false,
        //     2
        // );

        // DrawRect( // Draw a white pipe outline around the GAP area for debugging
        //     new Rect2(-w / 2f, -gap / 2f, w, gap),
        //     Colors.White,
        //     true,
        //     0
        // );


        // DrawLine(new Vector2(-30, 0), new Vector2(30, 0), Colors.Red, 2);
        // DrawLine(new Vector2(0, -30), new Vector2(0, 30), Colors.Red, 2);

    }



}
