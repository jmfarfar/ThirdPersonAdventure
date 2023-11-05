using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagingZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (other.TryGetComponent<Health>(out Health healthComponent))
        {
            healthComponent.DealDamage(10, 0f);
        }
    }
}
