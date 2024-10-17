using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSword : MonoBehaviour
{
    public float damage;
    private void OnTriggerEnter(Collider other)
    {
        var enemy = other.GetComponent<SkeletonController>();

        if (enemy != null)
        {
            enemy.LooseHealth(damage);
        }
    }
}
