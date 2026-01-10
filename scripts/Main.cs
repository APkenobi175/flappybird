using Godot;
using System;

public partial class Main : Node2D
{
    public int score = 0; // score variable to keep track of the player's score
    
    
    // References to other nodes:
    [Export] public PackedScene PipePairScene { get; set; } // Reference to the PipePair scene a PackedScene is used to instance scenes at runtime
    private Node2D pipesContainer; // reference to the pipes container node
    private ScoreLabel scoreLabel; // reference to the score label node

    public override void _Ready()
    {
        // Create a field to hold the pipes container 
        // To create a node2d reference use the following syntax:
        pipesContainer = GetNode<Node2D>("pipes"); // in the scene tree in main this is the pipes 2d node, now that we have access to it we can modify it at runtime
        // call randomize once so that each time we run the game the randonm numbers are different
        GD.Randomize();
        GD.Print("Main node is ready!");
        
        scoreLabel = GetNode<ScoreLabel>("Score/score_label"); // get reference to score label node in scene tree
        GD.Print("ScoreLabel node found: " + (scoreLabel != null));
        scoreLabel.setScore(score); 
        GD.Print("Initial Score: " + score);

    }

    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("flap"))
        {
            score = score + 1; // increment score by 1
            scoreLabel.setScore(score); // update score label
            GD.Print("Score incremented to: " + score);
        }
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
        // hard coding between 95 and 550 so that the gap is ALWAYS shown on the screen with a little buffer
        float spawnY = (float)GD.RandRange(95, 475); // random y position in between 95 and 550 for the pipe gap center
        pipesContainer.AddChild(pipe);
        pipe.GlobalPosition = new Vector2(spawnX, spawnY); // set the position of the pipe to the spawn coordinates
         // add the pipe as a child of the pipes container so it appears in the scene (which remember is called "pipes" in the main scene tree)
        GD.Print("Spawned a new pipe pair at y: " + spawnY);
    }

    public override void _UnhandledInput(InputEvent e)
{
    if (e is InputEventMouseButton mb && mb.Pressed)
        GD.Print(GetGlobalMousePosition());
}

}
