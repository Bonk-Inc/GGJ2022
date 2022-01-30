using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] 
    public Karma karma;

    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }
}