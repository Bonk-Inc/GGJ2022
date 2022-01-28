using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseState : PdaState
{

    [SerializeField]
    private InputAction unpauseInput;

    [SerializeField]
    private Canvas pauseMenuUI;

    private float timeScaleAtEnter = 1;

    private void Start() {
        pauseMenuUI.enabled = false;
        unpauseInput.performed += (c) => UnPause();
    }

    public override void Pause()
    {
        base.Pause();
        unpauseInput.Disable();
    }

    public override void Resume()
    {
        base.Resume();
        unpauseInput.Enable();
    }

    public override void Enter()
    {
        base.Enter();
        unpauseInput.Enable();
        timeScaleAtEnter = Time.timeScale;
        Time.timeScale = 0;
        pauseMenuUI.enabled = true;
    }

    public override void Leave()
    {
        base.Leave();
        unpauseInput.Disable();
        Time.timeScale = timeScaleAtEnter;
        pauseMenuUI.enabled = false;
    }



    public void UnPause()
    {
        if (Machine == null || Machine.CurrentState as Object != this)
            return;

        Machine.PopState();
    }
}
