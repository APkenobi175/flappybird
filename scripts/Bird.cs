using Godot;
using System;

public partial class Bird : CharacterBody2D
{
    float vertical_velocity;
    float gravity = 980f; // pixels per second squared
    int flap_strength = -400; // pixels per second
	public bool canMove = false; // If you haven't started the game yet, the bird can't move
	private Vector2 startPosition;

	private bool isDead = false;

	private bool gameOverSent = false;

	private Audio audio;


    public override void _Ready()
    {
		startPosition = GlobalPosition;
        audio = GetNode<Audio>("../Audio");
        GD.Print("Bird node is ready!");
    }

    public override void _Process(double delta)


    {

		if (!canMove && !isDead)
		{
			float bob = Mathf.Sin(Time.GetTicksMsec() * 0.005f) * 4f;
			GlobalPosition = new Vector2(GlobalPosition.X, startPosition.Y + bob);
		}

    }

	public override void _PhysicsProcess(double delta)
	{
		// Before start: freeze in place
		if (!canMove && !isDead)
		{
			Velocity = Vector2.Zero;
			return;
		}

		// Gravity applies during gameplay and after death (so bird falls)
		Velocity += Vector2.Down * gravity * (float)delta;

		// Only flap if alive + allowed
		if (!isDead && canMove && Input.IsActionJustPressed("flap"))
		{
			Velocity = new Vector2(Velocity.X, flap_strength);
			audio?.PlayFlap();
		}
		float angle = Mathf.Clamp(Velocity.Y / 400f, -1f, 1f) * Mathf.DegToRad(45f);
		Rotation = angle;

		
			
			

		MoveAndSlide();

		// Ceiling clamp
		if (GlobalPosition.Y < 10f)
		{
			GlobalPosition = new Vector2(GlobalPosition.X, 10f);
			Velocity = Vector2.Zero;
		}
		// Death on collision with obstacles
		if (!isDead && GetSlideCollisionCount() > 0)
		{
			die();
			return;
		}

		// After death: when we hit the ground, and then show game over
		// GameOverSent ensures we only play the death animation once
		if (isDead && IsOnFloor() && !gameOverSent)
		{
			gameOverSent = true;
			GD.Print("Bird has hit the ground after death.");
			GetNode<Main>("/root/Main").GameOver();
		}
	}


	public override void _Draw()
	{
		base._Draw();
		//GD.Print("Player is being drawn");
		DrawCircle(Vector2.Zero, 10, Colors.Blue);
	}

    public void die()
    {
        if (isDead) return;
		
		isDead = true;
		canMove = false;
		Velocity = Vector2.Zero;
		GD.Print("Bird has died.");
		GetNode<Main>("/root/Main").OnBirdDied();
		}

    internal void Reset()
    {
        GlobalPosition = startPosition;
		Velocity = Vector2.Zero;
		vertical_velocity = 0f;
		isDead = false;
		// set the ground to start scrolling again
		GD.Print("Bird has been reset.");
		GetNode<Main>("/root/Main").ResetGround();
		gameOverSent = false;

	}

}
