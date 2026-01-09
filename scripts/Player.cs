using Godot;
using System;

public partial class Player : Node2D
{
		float vertical_velocity;
		float gravity = 980f; // pixels per second squared
		int flap_strength = -400; // pixels per second
	
	public override void _Ready()
	{
		GD.Print("Player node is ready!");
	}

	public override void _Process(double delta)
	{

	}

	public override void _PhysicsProcess(double delta)
	{

		//GD.Print("Player is in physics process");
		// Find the delta gravity
		float delta_gravity = (float)delta * gravity;

	
		vertical_velocity += delta_gravity; // vertical velocity increases due to gravity, we apply the delta time to make it independent of frame rate. so every physics frame, the vertical velocity increases by gravity
		if (Input.IsActionJustPressed("flap")) // If a player provides the flap input, apply the flap strength to the vertical velocity
		{
			vertical_velocity = flap_strength; // set the vertical velocity to the flap strength
			//GD.Print("Flap!");
			
		}
		float delta_velocity = vertical_velocity * (float)delta; // calculate the change in vertical position based on the vertical velocity and delta time
		Position += new Vector2(0, delta_velocity); // update the players position based on its delta_vertical_velocity, so it will still apply to the bird, even if the flap button is not pressed


		// clamp players position to not go above the sky
		float min_y_pos = -600f; // minimum y position the player can go to
		if (Position.Y < min_y_pos) // if the player is above the minimum y position
		{
			Position = new Vector2(Position.X, min_y_pos); // set the player's y position to the minimum y position
			vertical_velocity = 0; // reset the vertical velocity
		}
		//GD.Print("Vertical Velocity: " + delta_velocity);



		
	}

	public override void _Draw()
	{
		base._Draw();
		//GD.Print("Player is being drawn");
		DrawCircle(Vector2.Zero, 10, Colors.Blue);
	}

}
