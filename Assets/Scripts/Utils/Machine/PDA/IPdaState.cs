using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPdaState : IState<PushDownAutomaton>
{
    void Pause();
    void Resume();
}
