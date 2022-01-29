using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainUIState : PdaState
{

    [Header("Pause")]
    [SerializeField]
    private InputAction pauseInput;

    [SerializeField]
    private PdaState pauseState;

    [Header("Game Over")]
    [SerializeField]
    private Health playerHealth;

    [SerializeField]
    private PdaState gameOverState;

    private void Awake() {
        pauseInput.Enable();
        pauseInput.performed += (c) => PauseGame();
        playerHealth.OnHealthChange += (caller, args) => { 
            if(args.IsDead)
                Machine.PushState(gameOverState);
        };
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
