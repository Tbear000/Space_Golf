using Godot;
using System;

public class Black_Hole : Area2D
{
    private RigidBody2D ball;

    public override void _Ready()
    {
        ball = (RigidBody2D)GetTree().GetNodesInGroup("Ball")[0];
    }
    public override void _Process(float delta)
    {
        if(OverlapsBody(ball)){
            ball.Sleeping = true;
            ball.GlobalPosition = Global.LastBallPos;
        }
    }

    public void _on_Area2D_body_exited(object body)
    {
        ball.Sleeping = true;
        ball.GlobalPosition = Global.LastBallPos;
    }
}
