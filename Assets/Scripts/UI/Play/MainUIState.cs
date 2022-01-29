using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainUIState : PdaState
{

    [SerializeField]
    private InputAction pauseInput;

    [SerializeField]
    private PdaState pauseState;

    private void Awake() {
        pauseInput.Enable();
        pauseInput.performed += (c) => PauseGame();
    }

    public override void Pause()
    {
        base.Pause();
        pauseInput.Disable();
    }

    public override void Resume()
    {
        base.Resume();
        pauseInput.Enable();
    }

    public void PauseGame(){
        if(!IsActiveState)
            return;

        Machine.PushState(pauseState);
    }

}
