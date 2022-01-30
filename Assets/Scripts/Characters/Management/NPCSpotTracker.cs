using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NPCSpotTracker : MonoBehaviour
{
    [SerializeField]
    private List<NPCPlace> npcPlaces;
    
    public event Action<GameObject> onNPCExtracted; //TODO

    [ContextMenu("SpawnNPC")]
    public void SpawnNPC() {
        var spot = FindPlace();
        if(spot == null) return;

        // TODO Randomize or give as parameter
        spot.FillSpots(5);
    }

    private NPCPlace FindPlace() {
        List<NPCPlace> places = new List<NPCPlace>();
        foreach (var spot in npcPlaces)
        {
            if(spot.GetAmountNPCLeft() == 0) places.Add(spot);
        }
        if(places.Count == 0) return null;
        var position = UnityEngine.Random.Range(0, places.Count -1);

        return places[position];
    }

    public List<NPCPlace> GetActivePlaces() {
        List<NPCPlace> places = new List<NPCPlace>();
        foreach (var spot in npcPlaces)
        {
            if(spot.GetAmountNPCLeft() > 0) places.Add(spot);
        }
        return places;
    }
}
