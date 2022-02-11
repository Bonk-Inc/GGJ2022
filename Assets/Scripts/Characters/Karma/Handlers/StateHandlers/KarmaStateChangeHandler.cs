using UnityEngine;

public abstract class KarmaStateChangeHandler : MonoBehaviour
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

    protected virtual void OnKarmaStateChange(object caller, Karma.KarmaStateChangeArgs args) {
        CheckState(args);
    }

    private void CheckState(Karma.KarmaStateChangeArgs args) {
        switch (args.newKarmaState)
        {
            case KarmaState.Angel:
                OnAngelState(args.previousKarmaState);
                break;
            case KarmaState.Demon:
                OnDemonState(args.previousKarmaState);
                break;
            default:
                OnHumanState(args.previousKarmaState);
                break;
        }        
    }

    protected virtual void OnAngelState(KarmaState prevState) {}
    protected virtual void OnDemonState(KarmaState prevState) {}
    protected virtual void OnHumanState(KarmaState prevState) {}
}