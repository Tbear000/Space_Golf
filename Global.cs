using Godot;
using System;

public class Global : Node
{
    public static int CurrentHole = 1;
    public static Vector2 LastBallPos;
    public static int CurrentHits = 0;

    public static Ui UserInterface;

    public override void _Ready()
    {
        UserInterface = GetNode<Ui>("Ui");
    }

    public static void Respawn(RigidBody2D Ball)
    {
        Ball.Sleeping = true;
        CurrentHits++;
        Ball.GlobalPosition = LastBallPos;
    }
}
