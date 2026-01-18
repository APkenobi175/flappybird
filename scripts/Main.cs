using Godot;
using System;

public partial class Main : Node2D
{


    public int score = 0; // score variable to keep track of the player's score
    private Ui ui; // reference to the UI node
    private Bird bird; // reference to the bird node
    
    
    // References to other nodes:
    [Export] public PackedScene PipePairScene { get; set; } // Reference to the PipePair scene a PackedScene is used to instance scenes at runtime
    private Node2D pipesContainer; // reference to the pipes container node
    private ScoreLabel scoreLabel; // reference to the score label node

    private SafeZone safeZone; // reference to the safe zone node
    private Parallax2D groundParallax;
    private Ground ground;

    public override void _Ready()
    {
        ground = GetNode<Ground>("Ground");

        GetNode<Timer>("pipe_spawner").Stop(); // stop the pipe spawner timer initially

        bird = GetNode<Bird>("bird"); // get reference to the bird node in the scene tree
        bird.canMove = false; // initially the bird can't move until the game starts
        GD.Print("Bird canMove set to" + bird.canMove.ToString());

        ui = GetNode<Ui>("Ui"); // get reference to the Ui node in the scene tree
        ui.StartButton.Pressed += StartGame; // connect the StartButton's Pressed signal to the StartGame method
        ui.RetryButton.Pressed += RestartGame; // connect the RetryButton's Pressed signal to the RestartGame method

        ui.ShowStart(); // show the start screen



        // Create a field to hold the pipes container 
        // To create a node2d reference use the following syntax:
        pipesContainer = GetNode<Node2D>("pipes"); // in the scene tree in main this is the pipes 2d node, now that we have access to it we can modify it at runtime


        // call randomize once so that each time we run the game the randonm numbers are different
        GD.Randomize();
        GD.Print("Main node is ready!");
        
        scoreLabel = GetNode<ScoreLabel>("Score/score_label"); // get reference to score label node in scene tree
        scoreLabel.setScore(score); 

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
        var safeZone = pipe.GetNode<Area2D>("safe_zone"); // get reference to the safe zone node in the pipe pair
        safeZone.BodyEntered += OnScored; // connect the BodyEntered signal to the OnScored method
    }

    public override void _UnhandledInput(InputEvent e)
    {
        if (e is InputEventMouseButton mb && mb.Pressed)
            GD.Print(GetGlobalMousePosition());
    }

    private void OnScored(Node body)
    {

        if (body is Bird){
        score += 1; // increment score by 1
        scoreLabel.setScore(score); // update score label
        GD.Print("Score incremented to: " + score);
        }
    }


    private void StartGame()
    {
        bird.Reset();
        bird.canMove = true; // allow the bird to move
        score = 0; // reset score
        scoreLabel.setScore(score); // update score label
        ui.ShowPlaying(); // show playing UI
        GetNode<Timer>("pipe_spawner").Start(); // start the pipe spawner timer
        GD.Print("Bird canMove set to" + bird.canMove.ToString());
    }

    private void RestartGame()
    {
        score = 0;
        scoreLabel.setScore(score);
        ui.ShowPlaying();

        foreach (Node pipe in pipesContainer.GetChildren())
            pipe.QueueFree();

        bird.canMove = true;
        bird.Reset();

        GetNode<Timer>("pipe_spawner").Start();
    }

    public void GameOver()
    {
        ui.ShowGameOver(score);
        GetNode<Timer>("pipe_spawner").Stop();

        bird.canMove = false;
    }

        public void OnBirdDied()
    {
        // stop spawner
        GetNode<Timer>("pipe_spawner").Stop();

        // kill/freeze all existing pipes immediately
        foreach (Node pipe in pipesContainer.GetChildren())
            pipe.QueueFree();

        // stop ground scroll
        ground.StopScrolling();
    }




}
