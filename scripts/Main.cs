using Godot;
using System;

public partial class Main : Node2D
{
    public int score = 0; // score variable to keep track of the player's score
    private Ui ui; 
    private Bird bird; 
    [Export] public PackedScene PipePairScene { get; set; } 
    private Node2D pipesContainer; 
    private ScoreLabel scoreLabel; 
    private SafeZone safeZone; 
    private Parallax2D groundParallax;
    private Ground ground;
    private bool gameStarted = false;
    public int highScore;
    private HighScore highScoreLabel;
    public bool newHighScore = false;
    private Sky sky;
    private Audio audio;
    public override void _Ready()
    {
        ground = GetNode<Ground>("Ground");
        sky = GetNode<Sky>("Sky");
        audio = GetNode<Audio>("Audio");
        sky.StartScrolling(50f);
        highScoreLabel = GetNode<HighScore>("Ui/Root/StartPanel/HighScore");

        GetNode<Timer>("pipe_spawner").Stop(); // stop the pipe spawner timer initially

        bird = GetNode<Bird>("bird"); // get reference to the bird node in the scene tree
        bird.canMove = false; // initially the bird can't move until the game starts
        ui = GetNode<Ui>("Ui"); // get reference to the Ui node in the scene tree
        // Press the flap action to start the game
        ui.ShowStart(); // show the start screen
        ui.RetryButton.Pressed += RestartGame; // connect the RetryButton's Pressed signal to the RestartGame method
        pipesContainer = GetNode<Node2D>("pipes"); // in the scene tree in main this is the pipes 2d node, now that we have access to it we can modify it at runtime
        // call randomize once so that each time we run the game the randonm numbers are different
        GD.Randomize();
        
        scoreLabel = GetNode<ScoreLabel>("Score/score_label"); // get reference to score label node in scene tree
        scoreLabel.setScore(score); 

    }

    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("flap") && !gameStarted) // if flap is pressed and game hasn't started yet, start the game
        {
            StartGame();
            gameStarted = true;
        }

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
        var safeZone = pipe.GetNode<Area2D>("safe_zone"); // get reference to the safe zone node in the pipe pair
        safeZone.BodyEntered += OnScored; // connect the BodyEntered signal to the OnScored method this will have to only apply to the bird, which we will handle in the method
    }
    private void OnScored(Node body)
    {

        if (body is Bird){ // body that entered must be bird
        score += 1; // increment score by 1
        scoreLabel.setScore(score); // update score label
        audio.PlayScore();
        GD.Print("Scored!");
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
    }

    private void RestartGame()
    {
        gameStarted = false;
        score = 0;
        ui.ShowStart(); // show the start screen
        scoreLabel.setScore(score);
        // Delete all existing pipes
        foreach (Node pipe in pipesContainer.GetChildren())
        {
            pipe.QueueFree();
        }
            
        bird.canMove = false;
        bird.Reset();
        newHighScore = false;


        
    }

    public void GameOver()
    {
        if (score > highScore)
        {
            highScore = score;
            GD.Print("New high score: " + highScore);
            highScoreLabel.setHighScore(highScore);
            newHighScore = true;

        }
        ui.ShowGameOver(score, true, newHighScore);
        GetNode<Timer>("pipe_spawner").Stop();

        bird.canMove = false;
        
        // Play the game over animation
    }

        public async void OnBirdDied()
    {
        audio.PlayDie();
        GD.Print("Bird died!");
        // stop spawner
        GetNode<Timer>("pipe_spawner").Stop();

        // kill/freeze all existing pipes immediately
        foreach (Node pipe in pipesContainer.GetChildren())
            pipe.QueueFree();

        // stop ground scroll
        ground.StopScrolling();
        sky.StopScrolling();
        await ToSignal(GetTree().CreateTimer(0.5f), "timeout"); // tiny delay before playing fall sound
        audio.PlayFall();

    }

    internal void ResetGround()
    {
        // This will reset the ground scrolling, when the game is reset
        ground.StartScrolling(200f);
        sky.StartScrolling(50f);

    }

}
