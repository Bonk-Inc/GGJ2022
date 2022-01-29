using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NPCPlace : MonoBehaviour
{
    [SerializeField]
    private ExtractionTimer npcPrefab;

    [SerializeField]
    private List<Transform> npcSpots;

    private int npcSaved = 0;

    public event Action<int> OnSpotsCleared;

    private void ResetPlace() {

    }

    private bool SetNPC(Transform npc) {
        // if(GetNPC() != null) return false;

        npc.transform.SetParent(SelectSpot());
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

    public Transform GetNPC() {
        if(transform.childCount == 0) return null;
        return transform.GetChild(0);
    }

    private void ExtractNPC(GameObject npc) {
        Destroy(npc.gameObject);
        CheckSpots();
    }

    private void CheckSpots() {
        if(GetAmountNPCLeft() == 0 && OnSpotsCleared != null) OnSpotsCleared.Invoke(npcSaved);
    }

    public void FillSpots(int spots) {
        ExtractionTimer npc = GameObject.Instantiate(npcPrefab);
        npc.onExtract += ExtractNPC;
        npc.GetComponent<Health>().OnHealthChange += (caller, args) => {
            if(args.IsDead) CheckSpots();
        };
        
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
