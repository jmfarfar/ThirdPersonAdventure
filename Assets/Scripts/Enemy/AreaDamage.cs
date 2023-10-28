using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDamage : MonoBehaviour
{

         [SerializeField] private Collider myCollider;
    private List<Collider> alreadyCollidedWith = new List<Collider>();
    [SerializeField] private int damage =15;
   [SerializeField] private float knockback =40;   

    void OnEnable()
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
            health.DealDamage(damage,knockback);
        }

        if (other.TryGetComponent<ForceReceiver>(out ForceReceiver forceReceiver))
        {
            Vector3 direction = (other.transform.position - myCollider.transform.position).normalized;
            forceReceiver.AddForce(direction * knockback);
        }
    }

   IEnumerator AttackSequence()
    {
        yield return new WaitForSeconds(2f);
       myCollider.enabled = true;
         yield return new WaitForSeconds(3f);

        Destroy(this.gameObject);
    }




   
    

}
