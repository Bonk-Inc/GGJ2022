using UnityEngine;

public class KarmaStateChangeHandler : MonoBehaviour
{
    [SerializeField] 
    private Karma karma;

    [SerializeField]
    private AudioSource source;
    
    [SerializeField]
    private AudioClip demonSound, angelSound, humanSound;
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
        CheckSoundUpdate(args);
    }

    private void CheckSoundUpdate(Karma.KarmaStateChangeArgs args) {
        switch (args.newKarmaState)
        {
            case KarmaState.Angel:
                ChangeSound(angelSound);
                break;
            case KarmaState.Demon:
                ChangeSound(demonSound);
                break;
            default:
                ChangeSound(humanSound);
                break;
        }        
    }

    private void ChangeSound(AudioClip clip) {
        source.clip = clip;
        source.Play();
    }
}