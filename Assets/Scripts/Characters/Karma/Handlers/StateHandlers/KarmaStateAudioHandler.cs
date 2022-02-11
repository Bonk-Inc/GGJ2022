using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KarmaStateAudioHandler : KarmaStateChangeHandler
{
    [SerializeField]
    private AudioSource source;
    
    [SerializeField]
    private AudioClip demonSound, angelSound, humanSound;

    protected override void OnAngelState(KarmaState prevState){
        ChangeSound(angelSound);
    }

    protected override void OnDemonState(KarmaState prevState){
        ChangeSound(demonSound);
    }

    protected override void OnHumanState(KarmaState prevState){
        ChangeSound(humanSound);
    }

    private void ChangeSound(AudioClip clip) {
        source.clip = clip;
        source.Play();
    }
}
