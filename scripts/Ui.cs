using Godot;
using System;

public partial class Ui : CanvasLayer
{
    public Button StartButton {get; private set; }
    public Button RetryButton {get; private set; }
    public Label FinalScoreLabel {get; private set; }

    private Control startPanel;
    private Control gameOverPanel;


    public override void _Ready()
    {
        GD.Print("UI node is ready!");
        StartButton = GetNode<Button>("Root/StartPanel/StartButton");
        RetryButton = GetNode<Button>("Root/GameOverPanel/RetryButton");
        FinalScoreLabel = GetNode<Label>("Root/GameOverPanel/FinalScore");
        startPanel = GetNode<Control>("Root/StartPanel");
        gameOverPanel = GetNode<Control>("Root/GameOverPanel");
        ShowStart();
    }

    public void ShowStart()
    {
        GetNode<Control>("Root/StartPanel").Visible = true;
        GetNode<Control>("Root/GameOverPanel").Visible = false;
        StartButton.Visible = true;
        RetryButton.Visible = false;
        FinalScoreLabel.Visible = false;
    }

    public void ShowGameOver(int finalScore)
    {
        startPanel.Visible = false;
        gameOverPanel.Visible = true;
        StartButton.Visible = false;
        RetryButton.Visible = true;
        FinalScoreLabel.Visible = true;
        FinalScoreLabel.Text = "Final Score: " + finalScore.ToString();
    }

    public void ShowPlaying()
    {
        startPanel.Visible = false;
        gameOverPanel.Visible = false;
        StartButton.Visible = false;
        RetryButton.Visible = false;
        FinalScoreLabel.Visible = false;
    }






}
