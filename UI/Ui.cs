using Godot;
using System;

public class Ui : CanvasLayer
{
    private NinePatchRect PausePanel;
    private Label HoleCounter;
    private TextureRect PauseButton;
    public override void _Ready()
    {
        PauseButton = GetNode<TextureRect>("PauseButton/TextureRect");
        HoleCounter = GetNode<Label>("NinePatchRect/HBoxContainer/HoleNumber");
        PausePanel = GetNode<NinePatchRect>("Panel");
        PausePanel.Visible = false;
    }

    public override void _Process(float delta)
    {
        HoleCounter.Text = Global.CurrentHits.ToString();
        if(!GetTree().Paused){
            PauseButton.Texture = (Texture)ResourceLoader.Load("res://UI/UI_Art/pauseBtn.png");
        } else {
            PauseButton.Texture = (Texture)ResourceLoader.Load("res://UI/UI_Art/playBtn.png");
        }
    }

    public void _on_PauseButton_pressed()
    {
        PausePanel.Visible = !PausePanel.Visible;
        GetTree().Paused = !GetTree().Paused;
    }

    public void _on_RestartButton_pressed()
    {
        GetTree().Paused = false;
        Global.CurrentHole = 1;
        Global.CurrentHits = 0;
        GetTree().ReloadCurrentScene();
    }

    public void _on_MusicSwitch_toggled(bool Pressed)
    {
        if(Pressed){
            AudioManager.MusicVolume = -100.0f;
        } else {
            AudioManager.MusicVolume = -15.0f; //Set back to Whatever value is set in Options menu;
        }
    }
}
