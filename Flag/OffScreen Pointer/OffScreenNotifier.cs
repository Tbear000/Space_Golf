using Godot;
using System;

public class OffScreenNotifier : Node2D
{
    public Sprite sprite;
    [Export]
    public Vector2 TopLeftMargin = new Vector2(-84,-80);
    [Export]
    public Vector2 SizeMargin = new Vector2(170,155);
    public override void _Ready()
    {
        sprite = GetNode<Sprite>("Sprite");
        sprite.GlobalPosition = GlobalPosition;
    }

    public override void _Process(float delta)
    {
        var Canvas = GetCanvasTransform();
        var TopLeft = (-Canvas.origin - TopLeftMargin)/ Canvas.Scale;
        var Size = (GetViewportRect().Size - SizeMargin) / Canvas.Scale;
        var Rect = new Rect2(TopLeft, Size);
        SetMarkerPosition(Rect);
        SetMarkerRotation();
    }

    public void SetMarkerPosition(Rect2 bounds)
    {
        var NewPos = GlobalPosition;
        NewPos.x = Mathf.Clamp(NewPos.x, bounds.Position.x, bounds.End.x);
        NewPos.y = Mathf.Clamp(NewPos.y, bounds.Position.y, bounds.End.y);
        sprite.GlobalPosition = NewPos;
        if(bounds.HasPoint(GlobalPosition)){
            Hide();
        } else {
            Show();
        }
    }

    public void SetMarkerRotation()
    {
        var Angle = (GlobalPosition-sprite.GlobalPosition).Angle() - 80;
        sprite.GlobalRotation = Angle;
    }
}
