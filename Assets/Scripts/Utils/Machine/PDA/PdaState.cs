using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PdaState : MonoBehaviour, IPdaState
{
    public PushDownAutomaton Machine { get; set; }

    public bool IsActiveState {get; private set;}

    public virtual void Enter() {
        IsActiveState = true;
    }

    public virtual void Leave() {
        IsActiveState = false;
    }

    public virtual void Pause() {
        IsActiveState = false;
    }
    
    public virtual void Resume() {
        IsActiveState = true;
    }
    
    public virtual void Reason() {}
    public virtual void UpdateState() {}

}
