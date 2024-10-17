using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class DamageBlast : MonoBehaviour
{
    public float damage;
    private void OnTriggerEnter(Collider other)
    {
        var enemy = other.GetComponent<SkeletonController>();

        if (enemy != null)
        {
            enemy.LooseHealth(damage);
            Destroy(this.gameObject);
        }
    }
}
