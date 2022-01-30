using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class WaveSpawning : MonoBehaviour
{
    [SerializeField]
    private NPCSpotTracker spotTracker;

    [SerializeField]
    private List<WaveData> waveData;

    private const string recentWinPlayerPref = "RecentWin";
    private const string humanWinPlayerPref = "HumanWin";
    private const string winScene = "WinScreen";

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
        PlayerPrefs.SetInt(humanWinPlayerPref, 1);
        PlayerPrefs.SetString(recentWinPlayerPref, humanWinPlayerPref);
        SceneManager.LoadScene(winScene);
    }

    [Serializable]
    public struct WaveData
    {
        public float time;
        public int npcPlaces;
    }
}
