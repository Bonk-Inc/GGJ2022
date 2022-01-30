using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NPCPlace : MonoBehaviour
{
    [SerializeField]
    private ExtractionTimer npcPrefab;

    [SerializeField]
    private GameObject enemyPrefab;

    [SerializeField]
    private List<Transform> npcSpots;

    [SerializeField]
    private List<Transform> enemySpots;

    [SerializeField]
    private Transform target;

    private int npcSaved = 0;

    public event Action<int> OnSpotsCleared;

    private void ResetPlace() {
        npcSaved = 0;
    }

    private bool SetNPC(Transform npc) {
        var stepDad = SelectSpot();
        if(stepDad == null) return false;

        npc.transform.SetParent(stepDad);
        npc.localPosition = Vector2.zero;
        return true;
    }

    private Transform SelectSpot() {
        foreach (var spot in npcSpots)
        {
            if(spot.childCount == 0) return spot; 
        }
        return null;
    }


    private void ExtractNPC(GameObject npc) {
        Destroy(npc.gameObject);
        CheckSpots();
    }

    private void CheckSpots() {
        if(GetAmountNPCLeft() == 0 && OnSpotsCleared != null) OnSpotsCleared.Invoke(npcSaved);
    }

    public List<Transform> GetNPCs() {
        var npcs = new List<Transform>();
        foreach (var spot in npcSpots)
        {
            if(transform == null) continue;
            if(transform.childCount != 0) npcs.Add(spot.transform.GetChild(0));
        }
        return npcs;
    }

    public void FillSpots(int spots) {
        ResetPlace();
        spots = Math.Min(npcSpots.Count, spots);
        for (var i = 0; i < spots; i++)
        {
            ExtractionTimer npc = GameObject.Instantiate(npcPrefab);
            SetNPC(npc.transform);
            npc.onExtract += ExtractNPC;
            npc.GetComponent<Health>().OnHealthChange += (caller, args) => {
                if(args.IsDead) CheckSpots();
            };
        }

        for (var i = 0; i < spots; i++)
        {
            GameObject enemy = GameObject.Instantiate(enemyPrefab);
            enemy.GetComponent<TargetPicker>().SetMainTarget(target);
            enemy.transform.position = enemySpots[i].position;
        }
    }

    public int GetAmountNPCLeft() {
        var filled = 0;
        foreach (var spot in npcSpots)
        {
            if(spot.childCount != 0) filled++;
        }
        return filled;
    }
}
