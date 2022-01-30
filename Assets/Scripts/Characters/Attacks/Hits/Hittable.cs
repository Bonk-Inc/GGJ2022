using UnityEngine;

public abstract class Hittable : MonoBehaviour
{
    public abstract void Hit(HitData data);
}
