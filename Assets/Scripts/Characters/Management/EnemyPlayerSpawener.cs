using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPlayerSpawener : MonoBehaviour
{

    [SerializeField]
    private float radiusOffset;

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
        Vector3 position = CircledNavmeshLocation(camRadius + radiusOffset, angle);
        if (!IsInView(position))
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

    
     public Vector3 CircledNavmeshLocation(float radius, float angle) {
         Vector3 direction = Vector2.up.Rotate(angle) * radius;
         direction += cam.transform.position;
         NavMeshHit hit;
         Vector3 finalPosition = Vector3.zero;
         if (NavMesh.SamplePosition(direction, out hit, radius, 1)) {
             finalPosition = hit.position;            
         }
         return finalPosition;
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
