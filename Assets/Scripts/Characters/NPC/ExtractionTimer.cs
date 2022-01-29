using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ExtractionTimer : MonoBehaviour
{
    [SerializeField]
    private float timeUntilExtraction = 60;

    public event Action<GameObject> onExtract;

    private void Start() {
        StartCoroutine("WaitForExtraction");
    }

    private IEnumerator WaitForExtraction() {
        while(true) {
            yield return new WaitForSeconds(timeUntilExtraction);

            Extract();
        }
    }

    private void Extract() {
        if(onExtract != null) onExtract.Invoke(gameObject);
    }
}
