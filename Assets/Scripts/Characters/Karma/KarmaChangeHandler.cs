using UnityEngine;

public class KarmaChangeHandler : MonoBehaviour
{
    [SerializeField] 
    private Karma karma;

    private void Awake()
    {
        karma.OnKarmaChange += OnKarmaChange;
    }

    private void OnDestroy()
    {
        karma.OnKarmaChange -= OnKarmaChange;
    }

    private void OnKarmaChange(object caller, Karma.KarmaChangeArgs args)
    {
        var currentKarmaState = args.newKarmaState;
        Debug.Log("Karma: " + currentKarmaState);
     
        //TODO: Add logic when karma is chagned
    }
}