using Godot;
using System;
using System.ComponentModel.DataAnnotations.Schema;

public partial class Pipes : Node2D
{

    float speed = 200f; // speed variable for the pipes

	public override void _Process(double delta)
	{

        float delta_movement = speed * (float)delta; // calculate movement based on delta time
        Position += new Vector2(-delta_movement, 0); // move pipes to the left

        if (Position.X < -1200f) // if pipes go off screen to the left
        {
            QueueFree(); // remove pipes from scene, important for no memory leaks
            GD.Print("pipe died");
        }
    }

	public override void _PhysicsProcess(double delta)
	{

    }
}
