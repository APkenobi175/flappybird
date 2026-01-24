using Godot;
using System;

public partial class HighScore : Label
{
    int highScore;
    public override void _Ready()
    {

    }

    public void setHighScore(int newScore)
    {
        highScore = newScore;
        Text = "High Score: " + highScore.ToString();
    }
}
