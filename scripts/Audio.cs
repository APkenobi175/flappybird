using Godot;
using System;

public partial class Audio : Node2D
{
    
    private AudioStreamPlayer2D flap, score, die, fall;


    public override void _Ready()
    {
        flap = GetNode<AudioStreamPlayer2D>("Flap");
        score = GetNode<AudioStreamPlayer2D>("Score");
        die = GetNode<AudioStreamPlayer2D>("Die");
        fall = GetNode<AudioStreamPlayer2D>("Falling");
    }

    public void PlayFlap()
    {
        flap.Play();
    }

    public void PlayScore()
    {
        score.Play();
    }

    public void PlayDie()
    {
        die.Play();
    }

    public void PlayFall()
    {
        fall.Play();
    }
}