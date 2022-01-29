using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverState : PdaState
{
    
    [SerializeField]
    private GameObject gameOverUI;

    public override void Enter(){
        gameOverUI.SetActive(true);
    }


}
