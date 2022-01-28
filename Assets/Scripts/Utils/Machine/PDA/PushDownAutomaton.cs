using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushDownAutomaton : MonoBehaviour
{

    [SerializeField]
    private List<PdaState> initialStateStack;

    private Stack<IPdaState> states = new Stack<IPdaState>();

    public IPdaState CurrentState => states.Count > 0 ? states.Peek() : null;

    private void Awake() {
        foreach (var state in initialStateStack)
            PushState(state);
    }

    public void PushState(IPdaState state){

        if(state == null) return;

        state.Machine = this;
        CurrentState?.Pause();
        states.Push(state);
        state.Enter();
    }

    public void PushState(PdaState state){
        PushState((IPdaState)state);
    }

    public void SetState(IPdaState state){
        PopStateSilent();
        PushState(state);
    }

    public void PopState(){
        PopStateSilent();
        CurrentState?.Resume();
    }

    public void PopStateSilent(){
        if(states.Count == 0){
            return;
        }
        IPdaState oldState = states.Pop();
        oldState.Leave();
    }

    private void Update() {
        IPdaState current = CurrentState;
        if(current != null){
            CurrentState.Reason();
            CurrentState.UpdateState();
        }
    }
}
