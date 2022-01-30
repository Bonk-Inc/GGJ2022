using System.Collections;
using UnityEngine;

public class KarmaGenerator : MonoBehaviour
{
    [SerializeField] 
    private Karma karma;
    
    private Coroutine generatorCoroutine;

    private void Start()
    {
        generatorCoroutine = StartCoroutine(KarmaValueGenerator());
    }

    private void OnDestroy()
    {
        if(generatorCoroutine != null) StopCoroutine(generatorCoroutine);
    }

    private IEnumerator KarmaValueGenerator()
    {
        while (true)
        {
            var value = Random.Range(-100, 100);
            if (value >= 0)
                karma.Increase(value);
            else
                karma.Decrease(value);

            yield return new WaitForSeconds(2f);
        }
    }
}