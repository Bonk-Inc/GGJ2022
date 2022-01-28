using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState<MachineType>
{

    MachineType Machine { get; set; }

    void Enter();
    void Reason();
    void UpdateState();
    void Leave();

}
