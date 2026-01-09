using Godot;
using System;

public partial class Player : Node2D
{

	// Use these methods they way they are intended to be used
	
	public override void _Ready()
	{
		// this is called when the node is added to the scene
		// Called when the node enters the scene tree for the first time.
		// also happens every time the node is added to the scene.
		GD.Print("Player node is ready!");

	}

	public override void _Process(double delta)
	{
		// this is called every frame
		// This is the primary game loop function.
		// Every time we iterate through the loop we need to update things
		// this method will be called hundreds of times per second
		// you want it to be called as much as possible
		// You should NOT do expensive operations here
		// This process is basically the FPS counter of your game
		GD.Print("Player is processing");
		Position += new Vector2(200 * (float)delta, 0);
	}

	public override void _PhysicsProcess(double delta)
	{
		// this is called every physics frame
		// This is similar to _process, but its capped at a fixed rate
		// This is where you handle physics-related updates or simulations
		// Godot defaults this to 60 frames per second
		// Using Godots built in physics engine in here
		GD.Print("Player physics is processing");
	}

	public override void _Draw()
	{
		// this is called when the node is drawn to the screen
		// You can use this to do custom drawing on the node
		// For example drawing a sprite. You give it the image and it will draw the node
		base._Draw();
		GD.Print("Player is being drawn");
		// The rendering is not dependant on the physics
		// Your game should be entirely playable through its inputs
		// without any rendering at all
		// This is useful for debugging or visual effects
		// You should never change the behavior of your game based on rendering

		DrawCircle(new Vector2(Position.X, Position.Y), 10, Colors.Red);


	}

}
