using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackArea : MonoBehaviour
{

    [SerializeField] private GameObject areaParticles;
    private Camera cam;
    public Transform RHFirePoint;
    

    void OnEnable()
    {
        Camera cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        InstantiateProjectile(RHFirePoint);
    }

    void InstantiateProjectile(Transform firePoint)
    {
        var projectileObj = Instantiate(areaParticles, firePoint.position, Quaternion.identity);
       // projectileObj.transform.parent = BaseInstantiate.transform;
    }



}
