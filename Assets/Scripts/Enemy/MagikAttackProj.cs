using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagikAttackProj : MonoBehaviour
{

     [SerializeField] private Collider myCollider;
   [SerializeField] private int damage =10;
   [SerializeField] private float knockback =5;
    [SerializeField] private float stunTime = 0.2f;

    private List<Collider> alreadyCollidedWith = new List<Collider>();

    private void OnEnable()
    {
        alreadyCollidedWith.Clear();
        StartCoroutine(AttackSequence());        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == myCollider) { return; }

        if (alreadyCollidedWith.Contains(other)) { return; }
        alreadyCollidedWith.Add(other);

        if (other.TryGetComponent<Health>(out Health health))
        {            
            health.DealDamage(damage, stunTime);
            Destroy(gameObject);
        }

        if (other.TryGetComponent<ForceReceiver>(out ForceReceiver forceReceiver))
        {
            Vector3 direction = (other.transform.position - myCollider.transform.position).normalized;
            forceReceiver.AddForce(direction * knockback);
        }
                    Destroy(gameObject,1f);

    }

   IEnumerator AttackSequence()
    {
        yield return new WaitForSeconds(0.1f);
       myCollider.enabled = true;
         yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
    }


}
