using Godot;
using System;

public class Ball : RigidBody2D
{
    public Timer timer;
    public Tween tween;
    public Line2D ShotMeter;
    private Vector2 OriginPoint;

    [Export]
    public AudioStream HitSound;
    public override void _Ready()
    {
        timer = GetNode<Timer>("Timer");
        ShotMeter = GetNode<Line2D>("Line2D");
        ShotMeter.SetAsToplevel(true);
    }

    public override void _Process(float delta)
    {
        if(Sleeping && !GetTree().Paused){
            Global.LastBallPos = this.GlobalPosition;
            Engine.TimeScale = 1;
            if(timer.TimeLeft > 0){
                timer.Stop();
            }
            if(Input.IsActionJustPressed("Mouse")){
                ShotMeter.GlobalPosition = GlobalPosition;
                OriginPoint = GetGlobalMousePosition();
                ShotMeter.AddPoint(Vector2.Zero);
                ShotMeter.AddPoint(Vector2.Zero);
            }
            if(Input.IsActionPressed("Mouse") && ShotMeter.GetPointCount() > 0){
                var CurrentPoint = GetGlobalMousePosition();
                var TempDirection = CurrentPoint - OriginPoint;
                ShotMeter.SetPointPosition(1, Vector2.Zero - TempDirection);
            }
            if(Input.IsActionJustReleased("Mouse") && ShotMeter.GetPointCount() > 0){
                var Direction = (OriginPoint - GetGlobalMousePosition()) * 2;
                Direction = Direction.Clamped(500);
                ShotMeter.ClearPoints();
                if(Direction.Abs() > new Vector2(10,10)){
                    Sleeping = false;
                    timer.Start(5);
                    AudioManager.Play(HitSound);
                    Global.CurrentHits++;
                    ApplyCentralImpulse(Direction);
                }
            }
        }
    }

    public void _on_Timer_timeout()
    {
        if(Engine.TimeScale <= 10){
            Engine.TimeScale = Engine.TimeScale + 0.5f;
        }
    }
}
