using Godot;
using System;

public partial class Ui : CanvasLayer
{

    [Export]
    public AnimationPlayer Anim;
    public Button StartButton {get; private set; }
    public Button RetryButton {get; private set; }
    public Label FinalScoreLabel {get; private set; }

    private Control startPanel;
    private CanvasLayer gameOverPanel;

    private Label clickToStart;

    private Label highScoreLabel;

    private Label newHighScoreLabel;


    public override void _Ready()
    {
        clickToStart = GetNode<Label>("Root/StartPanel/Label2");
        RetryButton = GetNode<Button>("Root/GameOverPanel/Control/RetryButton");
        FinalScoreLabel = GetNode<Label>("Root/GameOverPanel/Control/ColorRect2/FinalScore");
        startPanel = GetNode<Control>("Root/StartPanel");
        gameOverPanel = GetNode<CanvasLayer>("Root/GameOverPanel");
        highScoreLabel = GetNode<Label>("Root/StartPanel/HighScore");
        newHighScoreLabel = GetNode<Label>("Root/GameOverPanel/NEWHIGHSCORE");
        // Get reference to animaton player
        Anim = GetNode<AnimationPlayer>("Root/GameOverPanel/Control/AnimationPlayer");
        ShowStart();
    }

    public void ShowStart()
    {
        GetNode<Control>("Root/StartPanel").Visible = true;
        GetNode<CanvasLayer>("Root/GameOverPanel").Visible = false;
        clickToStart.Visible = true;
        RetryButton.Visible = false;
        FinalScoreLabel.Visible = false;
        highScoreLabel.Visible = true;
        newHighScoreLabel.Visible = false;
    }

    public void ShowGameOver(int finalScore, bool isGameOver, bool newHighScore)
    {
        startPanel.Visible = false;
        gameOverPanel.Visible = true;
        clickToStart.Visible = false;
        RetryButton.Visible = true;
        FinalScoreLabel.Visible = true;
        FinalScoreLabel.Text = "Final Score: " + finalScore.ToString();
        if (isGameOver)
        {
            PlayDeathAnimation();
        }
        if (newHighScore == true){
            newHighScoreLabel.Visible = true;
        }
    }

    public void ShowPlaying()
    {
        startPanel.Visible = false;
        gameOverPanel.Visible = false;
        clickToStart.Visible = false;
        RetryButton.Visible = false;
        FinalScoreLabel.Visible = false;
        highScoreLabel.Visible = false;
        newHighScoreLabel.Visible = false;
    }

    public void PlayDeathAnimation()
    {
        Anim.Play("you_died_animation");
    }






}
