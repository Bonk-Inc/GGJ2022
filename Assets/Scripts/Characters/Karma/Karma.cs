using System;
using UnityEngine;

public class Karma : MonoBehaviour
{
    [SerializeField] 
    private int maxKarma = 100;

    public event EventHandler<KarmaChangeArgs> OnKarmaChange; 
    
    public KarmaState KarmaState { get; private set; }

    private int currentKarma;

    private void Awake()
    {
        currentKarma = maxKarma / 2;
        KarmaState = KarmaState.Human;
    }

    private KarmaState DetermineKarmaState(float karmaPercentage) => karmaPercentage switch
    {
        <= 40f => KarmaState.Demon,
        >= 60f => KarmaState.Angel,
        _ => KarmaState.Human
    };

    public void Increase(int amount) => SetKarma(currentKarma + amount);

    public void Decrease(int amount) => SetKarma(currentKarma - amount);
    
    private void SetKarma(int karma)
    {
        currentKarma = (karma >= maxKarma) ? maxKarma : (karma <= 0) ? 0 : karma;

        var previousKarma = KarmaState;
        var karmaPercentage = (100f / maxKarma) * currentKarma;
        KarmaState = DetermineKarmaState(karmaPercentage);

        if (KarmaState != previousKarma)
            CallEvent(previousKarma);
    }

    private void CallEvent(KarmaState previousKarmaState)
    {
        var eventArgs = new KarmaChangeArgs
        {
            previousKarmaState = previousKarmaState,
            newKarmaState = KarmaState,
            karma = currentKarma
        };
        
        OnKarmaChange?.Invoke(this, eventArgs);
    }

    public class KarmaChangeArgs
    {
        public KarmaState previousKarmaState, newKarmaState;
        public int karma;
    }
}