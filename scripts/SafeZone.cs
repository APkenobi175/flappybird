using Godot;
using System;

public partial class SafeZone : Area2D
{
    
    [Signal] public delegate void ScoredEventHandler(); // signal to notify when bird enters safe zone for main to increment score
    private bool _scored = false;



    public override void _Ready()
    {
        GD.Print("SafeZone node is ready!");
        BodyEntered += OnBodyEntered; // connect the BodyEntered signal to the OnBodyEntered methodq

    }

    private void OnBodyEntered(Node body)
    {
        if (body is Bird && !_scored) // check if the body that entered is the bird and if it hasn't scored yet
        {
            GD.Print("Bird entered SafeZone!");
            _scored = true; // set scored to true to prevent multiple score increments
            EmitSignal(SignalName.Scored); // emit the signal to notify main to increment score
        }
    }






}
