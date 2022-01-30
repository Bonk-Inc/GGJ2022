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

    [SerializeField]
    private float minDegreesApart;

    [SerializeField]
    private float arrowImageRotationOffset;

    private Camera cam;

    private void Start() {
        cam = Camera.main;
    }

    private void Update()
    {
        var npcs = GetAllNpcs();
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
        
        var angles = GetSortedAngleList(npcs);
        var finalAngles = CalculateFinalAngles(angles);
        CreateArrowsOnAngles(finalAngles);
    }


    private List<float> GetSortedAngleList(List<Transform> npcs){
        var centerPosition = GetCenterScreenWorldPosition();
        var angles = new List<float>(); 
        for (var i = 0; i < npcs.Count; i++)
        {
            var directionToNpc = ((Vector2)npcs[i].position - centerPosition).normalized;
            var angle = Vector2.SignedAngle(Vector2.up, directionToNpc);
            angles.Add(angle);
        }
        angles.Sort();
        return angles;
    }

    private List<float> CalculateFinalAngles(List<float> angles){
        float currentAngleCount = 0;
        float currentTotalPackedAngle = 0;
        List<float> finalAngles = new List<float>(); 
        for (var i = 0; i < angles.Count; i++)
        {
            float currentAngle = angles[i];
            currentAngleCount += 1;
            currentTotalPackedAngle += currentAngle;
            if (i >= angles.Count-1 || Mathf.Abs(angles[i+1] - currentAngle) > minDegreesApart){
                finalAngles.Add(currentTotalPackedAngle / currentAngleCount);
                currentAngleCount = 0;
                currentTotalPackedAngle = 0;
            }
        }
        return finalAngles;
    }

    private void CreateArrowsOnAngles(List<float> finalAngles){
        for (var i = 0; i < finalAngles.Count; i++)
        {
            var arrow = GetArrow(i);
            var angle = finalAngles[i];
            var directionToNpc = Vector2.up.Rotate(angle);
            arrow.SetParent(transform);
            arrow.gameObject.SetActive(true);
            arrow.rotation = Quaternion.Euler(0, 0, angle + arrowImageRotationOffset);
            arrow.position = GetScreenCenter() + directionToNpc * circleSize;
        }
    }

    private Vector2 GetCenterScreenWorldPosition(){
        var screenCenter = GetScreenCenter();
        var worldposition = cam.ScreenToWorldPoint(screenCenter.ToVector3(npcTracker.transform.position.z));
        return (Vector2)worldposition;
    }

    private Vector2 GetScreenCenter(){
        return new Vector2(Screen.width/2, Screen.height/2);
    }

    private Transform GetArrow(int n){
        if(transform.childCount > n){
            return transform.GetChild(n);
        }else {
            return Instantiate(arrowPrefab).transform;
        }
    }

}
