using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    
    [SerializeField]
    private EnemyPlayerSpawener playerSpawener;

    [SerializeField]
    private GameObject enemyPrefab;

    [SerializeField]
    private FloatNumberRange spawnDelay;

    [SerializeField]
    private IntNumberRange groupSize;

    [SerializeField]
    private Transform target;

    private void Awake() {
        StartCoroutine(PlayerEnemySpawnRoutine());
    }

    private IEnumerator PlayerEnemySpawnRoutine(){
        while (true)
        {
            yield return new WaitForSeconds( spawnDelay.RandomInRange());

            if (playerSpawener.QueueCount == 0)
            {
                var enemySpawnCount = groupSize.RandomInRange();
                for (var i = 0; i < enemySpawnCount; i++)
                {
                    var enemy = Instantiate(enemyPrefab);
                    enemy.GetComponent<TargetPicker>().SetMainTarget(target);
                    playerSpawener.AddToSpawnQueue(enemy);   
                }
            }
        }
    }

}
