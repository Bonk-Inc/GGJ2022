using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcFinder : MonoBehaviour
{

    [SerializeField]
    private NPCSpotTracker npcTracker;

    [SerializeField]
    private GameObject arrowPrefab;

    [SerializeField]
    private float circleSize;

    private Camera cam;

    private void Start() {
        cam = Camera.main;
    }

    private void Update()
    {
        var npcs = GetAllNpcs();
        Debug.Log(npcs.Count);
        DisableArrows();
        CreateArrows(npcs);
    }

    private List<Transform> GetAllNpcs(){
        var spots = npcTracker.GetActivePlaces();
        var npcs = new List<Transform>();
        foreach (var spot in spots)
        {
            npcs.AddRange(spot.GetNPCs());
        }
        return npcs;
    }

    private void DisableArrows(){
        for (var i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);   
        }
    }

    private void CreateArrows(List<Transform> npcs){
        var centerPosition = GetCenterScreenWorldPosition();
        for (var i = 0; i < npcs.Count; i++)
        {
            var arrow = GetArrow(i);
            arrow.SetParent(transform);
            
            var directionToNpc = ((Vector2)npcs[i].position - centerPosition).normalized;
            var angle = Vector2.Angle(Vector2.up, directionToNpc);

            arrow.rotation = Quaternion.Euler(0, 0, angle);
            arrow.position = centerPosition + directionToNpc * circleSize;
        }
    }

    private Vector2 GetCenterScreenWorldPosition(){
        var screenCenter = new Vector2(Screen.width/2, Screen.height/2);
        var worldposition = cam.ScreenToWorldPoint(screenCenter.ToVector3(npcTracker.transform.position.z));
        return (Vector2)worldposition;
    }

    private Transform GetArrow(int n){
        if(transform.childCount < n){
            return transform.GetChild(n);
        }else {
            return Instantiate(arrowPrefab).transform;
        }
    }

}
