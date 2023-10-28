using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingAttack : MonoBehaviour
{


    public GameObject projectile;
   private Transform playerPosition;   
    public Transform RHFirePoint;  
    private Vector3 destination;

    [SerializeField] private float projectileSpeed = 50f;
  Vector3 direction;
  Vector3 hitPosition;

  private void OnEnable()
    {
   
   //     Camera cam = GameObject.Find("Main Camera").
   //GetComponent<Camera>();
   
       playerPosition = GameObject.FindWithTag("Player").transform;
         RaycastHit hit;
      direction = playerPosition.position - transform.position;

    //       Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f,0));
        //      RaycastHit hit;

        //      if(Physics.Raycast(ray, out hit))
        if (Physics.Raycast(transform.position, direction, out hit, 300))
        {

   
   //         if (hit.transform.CompareTag("Player"))
   //         {
                Debug.DrawRay(transform.position, direction, Color.white);
                hitPosition = hit.point;
                InstantiateProjectile(RHFirePoint);
   //         }
   //         else
   //         {
                //     destination = ray.GetPoint(1000);
   //         }
        }
    }


    void InstantiateProjectile(Transform firePoint)
    {

        var projectileObj = Instantiate(projectile, firePoint.position, Quaternion.identity);
        projectileObj.GetComponent<Rigidbody>().velocity = (hitPosition - firePoint.position).normalized * projectileSpeed;
    }





}
