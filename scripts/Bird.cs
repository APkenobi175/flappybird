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


    public override void _Ready()
    {
		startPosition = GlobalPosition;
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
		// Before start: freeze in place (no gravity, no collisions)
		if (!canMove && !isDead)
		{
			Velocity = Vector2.Zero;
			return;
		}

		// Gravity applies during gameplay and after death (so bird falls)
		Velocity += Vector2.Down * gravity * (float)delta;

		// Only flap if alive + allowed
		if (!isDead && canMove && Input.IsActionJustPressed("flap"))
			Velocity = new Vector2(Velocity.X, flap_strength);

		MoveAndSlide();

		// Ceiling clamp
		if (GlobalPosition.Y < 10f)
		{
			GlobalPosition = new Vector2(GlobalPosition.X, 10f);
			Velocity = Vector2.Zero;
		}

		// ✅ THIS is what you're missing: die on collision while alive
		if (!isDead && GetSlideCollisionCount() > 0)
		{
			die();
			return;
		}

		// After death: when we hit the ground, show game over
		if (isDead && IsOnFloor())
		{
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

	}

}
