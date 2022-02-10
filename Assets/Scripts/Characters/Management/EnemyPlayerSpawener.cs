using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPlayerSpawener : MonoBehaviour
{

    [SerializeField]
    private float radiusOffset;

    [SerializeField]
    private float sampleRadius = 5f;

    [SerializeField]
    private float camOffset = 0.5f;

    [SerializeField]
    private int pointsToCheck = 20;    
    private int currentCheck = 0;

    private Camera cam;
    private float camRadius = 0;

    private Queue<GameObject> spawnQueue = new Queue<GameObject>(); 

    public int QueueCount => spawnQueue.Count;

    private void Awake() {
        cam = Camera.main;
        Rect camRect = CreateCamRect();
        float camCornerDistance = Vector2.Distance(camRect.center, camRect.max);
        camRadius = camCornerDistance;

    }

    private void Update() {
        float angle = 360 / pointsToCheck * currentCheck;
        (bool pointFound, Vector3 point) result = TryFindLocationInNavmeshAroundPlayer(camRadius + radiusOffset, angle);
        
        Vector3 position = result.point;
        if (result.pointFound && !IsInView(position))
        {
            SpawnEnemy(position);
        }
        currentCheck++;
        currentCheck %= pointsToCheck;
    }

    private bool IsInView(Vector2 position){
        var camRect = CreateCamRect();
        return camRect.Contains(position);
    }

    private Rect CreateCamRect(){
        var rect = new Rect();
        float ratio = (float)Screen.width / Screen.height;
        float camHeight = cam.orthographicSize * 2;
        float camWidth = camHeight * ratio;

        rect.height = camHeight + camOffset;        
        rect.width = camWidth + camOffset;

        rect.center = cam.transform.position;
        
        return rect;
    }

    
     public (bool pointFound, Vector3 point) TryFindLocationInNavmeshAroundPlayer(float radius, float angle) {
         Vector3 direction = Vector2.up.Rotate(angle) * radius;
         direction += cam.transform.position;
         direction.z = transform.position.z;

         NavMeshHit hit;
         if (NavMesh.SamplePosition(direction, out hit, sampleRadius, ~0)) {
             return (true, hit.position);            
         } else {
            return (false, Vector3.zero);
         }
     }

     public void AddToSpawnQueue(GameObject enemy){
         enemy.SetActive(false);
         spawnQueue.Enqueue(enemy);
     }

     private void SpawnEnemy(Vector3 position){
        
        if(spawnQueue.Count == 0)
            return;
        
        var enemy = spawnQueue.Dequeue();
        enemy.transform.position = position;
        enemy.SetActive(true);
     }

}
