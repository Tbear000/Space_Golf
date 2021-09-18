using Godot;
using System;

public class Flag : Area2D
{
    [Export]
    public int FlagNumber;
    [Export]
    public AudioStream HoleSound;
    private Label FlagNumberLabel;
    private OffScreenNotifier offScreenNotifier;

    public override void _Ready()
    {
        offScreenNotifier = GetNode<OffScreenNotifier>("OffScreenNotifier");
        FlagNumberLabel = GetNode<Label>("Label");
        FlagNumberLabel.Text = FlagNumber.ToString();
    }
    public override void _Process(float delta)
    {
        if(Global.CurrentHole == FlagNumber){
            offScreenNotifier.sprite.Visible = true;
            if(OverlapsBody((Node)GetTree().GetNodesInGroup("Ball")[0])){
                Global.CurrentHole++;
                AudioManager.Play(HoleSound);
                QueueFree();
            }
        } else {
            offScreenNotifier.sprite.Visible = false;
        }
    }
}
