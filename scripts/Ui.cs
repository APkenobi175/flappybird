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


    public override void _Ready()
    {
        GD.Print("UI node is ready!");
        StartButton = GetNode<Button>("Root/StartPanel/StartButton");
        RetryButton = GetNode<Button>("Root/GameOverPanel/Control/RetryButton");
        FinalScoreLabel = GetNode<Label>("Root/GameOverPanel/Control/ColorRect2/FinalScore");
        startPanel = GetNode<Control>("Root/StartPanel");
        gameOverPanel = GetNode<CanvasLayer>("Root/GameOverPanel");
        // Get reference to animaton player
        Anim = GetNode<AnimationPlayer>("Root/GameOverPanel/Control/AnimationPlayer");
        ShowStart();
    }

    public void ShowStart()
    {
        GetNode<Control>("Root/StartPanel").Visible = true;
        GetNode<CanvasLayer>("Root/GameOverPanel").Visible = false;
        StartButton.Visible = true;
        RetryButton.Visible = false;
        FinalScoreLabel.Visible = false;
    }

    public void ShowGameOver(int finalScore, bool isGameOver)
    {
        startPanel.Visible = false;
        gameOverPanel.Visible = true;
        StartButton.Visible = false;
        RetryButton.Visible = true;
        FinalScoreLabel.Visible = true;
        FinalScoreLabel.Text = "Final Score: " + finalScore.ToString();
        if (isGameOver)
        {
            PlayDeathAnimation();
        }
    }

    public void ShowPlaying()
    {
        startPanel.Visible = false;
        gameOverPanel.Visible = false;
        StartButton.Visible = false;
        RetryButton.Visible = false;
        FinalScoreLabel.Visible = false;
    }

    public void PlayDeathAnimation()
    {
        Anim.Play("you_died_animation");
    }






}
