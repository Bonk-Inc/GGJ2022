using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WaveSpawning : MonoBehaviour
{
    [SerializeField]
    private NPCSpotTracker spotTracker;

    [SerializeField]
    private List<WaveData> waveData;

    private int currentWave = 0;

    private Coroutine routine;
    private void Awake() {
        routine = StartCoroutine("SpawnWaves");
    }

    private IEnumerator SpawnWaves() {
        while(true) {
            if(currentWave == waveData.Count) {
                WinGame();
                StopCoroutine(routine);
            }

            yield return new WaitForSeconds(waveData[currentWave].time);
            for (int i = 0; i < waveData[currentWave].npcPlaces; i++)
            {
                spotTracker.SpawnNPC();
            }
            currentWave++;
        }
    }

    private void WinGame() {

    }

    [Serializable]
    public struct WaveData
    {
        public float time;
        public int npcPlaces;
    }
}
