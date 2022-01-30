using UnityEngine;

public class KarmaStateChangeHandler : MonoBehaviour
{
    [SerializeField] 
    private Karma karma;
    
    private void Awake()
    {
        karma.OnKarmaStateChange += OnKarmaStateChange;
    }

    private void OnDestroy()
    {
        karma.OnKarmaStateChange -= OnKarmaStateChange;
    }

    private void OnKarmaStateChange(object caller, Karma.KarmaStateChangeArgs args)
    {
        var currentKarmaState = args.newKarmaState;
        Debug.Log("Karma: " + currentKarmaState);
     
        //TODO: Add logic when karma is chagned
    }
}