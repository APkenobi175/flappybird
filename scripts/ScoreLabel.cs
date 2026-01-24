using Godot;
using System;

public partial class ScoreLabel : Label
{
    int score;
    public override void _Ready()
    {
        Text = "Score: 0";
    }

    public void setScore(int newScore)
    {
        score = newScore;
        GD.Print("Score updated to: " + score);
        Text = "Score: " + score.ToString();
    }

    
}
