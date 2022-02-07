using System;
using UnityEngine;

public class Karma : MonoBehaviour
{
    [SerializeField] 
    private int maxKarma = 100;
    
    public int MaxKarma => maxKarma;

    public event EventHandler<KarmaStateChangeArgs> OnKarmaStateChange;

    public event EventHandler<KarmaChangeArgs> OnKarmaChange;

    private KarmaState karmaState;

    private int currentKarma;
    public int CurrentKarma => currentKarma;

    private void Awake()
    {
        currentKarma = maxKarma / 2;
        karmaState = KarmaState.Human;
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

        var previousKarma = karmaState;
        var karmaPercentage = (100f / maxKarma) * currentKarma;
        karmaState = DetermineKarmaState(karmaPercentage);

        if (karmaState != previousKarma)
            CallStateEvent(previousKarma);
        
        CallKarmaEvent(currentKarma);
    }

    private void CallStateEvent(KarmaState previousKarmaState)
    {
        var eventArgs = new KarmaStateChangeArgs
        {
            previousKarmaState = previousKarmaState,
            newKarmaState = karmaState,
        };
        
        OnKarmaStateChange?.Invoke(this, eventArgs);
    }

    private void CallKarmaEvent(int karma)
    {
        var eventArgs = new KarmaChangeArgs { karma = karma };
        
        OnKarmaChange?.Invoke(this, eventArgs);
    }
    
    public class KarmaStateChangeArgs
    {
        public KarmaState previousKarmaState, newKarmaState;
    }
    
    public class KarmaChangeArgs
    {
        public int karma;
    }
}