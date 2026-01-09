using Godot;
using System;

public partial class Main : Node2D
{
    private Node2D pipesContainer; // Rereference to the pipes container node which is called "pipes" in the scene tree

    [Export] public PackedScene PipePairScene { get; set; } // Reference to the PipePair scene a PackedScene is used to instance scenes at runtime
    public override void _Ready()
    {
        // Create a field to hold the pipes container 
        // To create a node2d reference use the following syntax:
        pipesContainer = GetNode<Node2D>("pipes"); // in the scene tree in main this is the pipes 2d node, now that we have access to it we can modify it at runtime
        // call randomize once so that each time we run the game the randonm numbers are different
        GD.Randomize();
        GD.Print("Main node is ready!");
    }

    public override void _Process(double delta)
    {

    }

    public override void _PhysicsProcess(double delta)
    {

    }

    public override void _Draw()
    {

    }

    private void _on_PipeSpawner_timeout()
    {
        GD.Print("Pipe spawner timeout signal received");
        // Instantiate the scene this means create an instance of one of the PipePair scenes
        var pipe = PipePairScene.Instantiate<Node2D>(); // we create variable pipe and assign it the pipe pair scene instantiated as a Node2D
        // var means the type is inferred by the compiler
        float spawnX = 1250f;
        float spawnY = (float)GD.RandRange(-650,0); // random y position
        pipe.Position = new Vector2(spawnX, spawnY); // set the position of the pipe to the spawn coordinates
        pipesContainer.AddChild(pipe); // add the pipe as a child of the pipes container so it appears in the scene (which remember is called "pipes" in the main scene tree)
        GD.Print("Spawned a new pipe pair at y: " + spawnY);
    }
}
