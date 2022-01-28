using UnityEngine;
using System;

public class TargetPicker : MonoBehaviour
{

    [SerializeField]
    private float radius = 10;

    [SerializeField]
    private LayerMask mask;

    [SerializeField]
    private Transform mainTarget;
    public Transform MainTarget => mainTarget;

    private Transform currentTarget;

    public event Action<Transform> onTargetChanged;

    private void Update() {
        CheckForTarget();
    }

    private void CheckForTarget() {
        var foundTargets = Physics2D.OverlapCircleAll(transform.position, radius, mask);
        var newTarget = ChooseTarget(foundTargets); 
        if(newTarget != null) {
            if(newTarget != currentTarget) {
                if(onTargetChanged != null)
                    onTargetChanged.Invoke(newTarget.transform);
                currentTarget = newTarget.transform;
            }
        }
        else if(currentTarget != mainTarget) currentTarget = mainTarget;
    }

    private Transform ChooseTarget(Collider2D[] targets) {
        if(targets.Length == 0) return null;

        Transform chosenTarget = null;
        foreach (var target in targets)
        {
            if(target.transform == MainTarget) return target.transform;
            chosenTarget = target.transform;
        }
        return chosenTarget;
    }
}
