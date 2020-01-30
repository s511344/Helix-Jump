using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public enum State { 
    Big,
    Small,
    General,
}
public class PlayerState : MonoBehaviour, INotifyPropertyChanged
{
    public PlayerEvent playerEvent;
    public GameObject player;
    public float Speed { get; set; } = 1;

    public event PropertyChangedEventHandler PropertyChanged;
    void Start()
    {
        PropertyChanged += PlayerState_PropertyChanged;
    }

    private void PlayerState_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        var state = (State)sender;
        if (state == State.Big)
        {
            ToBig();
        }
        else if (state == State.Small)
        {
            ToSmall();
        }
        else
        {
            ToGeneral();
        }
    }
    private State currentState = State.General;
    public State CurrentState
    {
        get => currentState; set
        {
            if (value == currentState) return;
            else
            {
                currentState = value;
                PropertyChanged?.Invoke(value, new PropertyChangedEventArgs(nameof(CurrentState)));
            }
        }
    }

    void ToBig()
    {
        playerEvent.ToBig(player);
    }
    void ToSmall()
    {
        playerEvent.ToSmall(player);
    }

    void ToGeneral()
    {
        playerEvent.ToGeneral(player);
    }
}
