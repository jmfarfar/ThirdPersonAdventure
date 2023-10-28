using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class PlaceLights : MonoBehaviour
{
    [SerializeField] private bool placeLights;
    [SerializeField] private Transform streetLampParentObject;
    [SerializeField] private GameObject lightRigPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (placeLights)
        {
            foreach (Transform c in streetLampParentObject.GetComponentsInChildren<Transform>())
            {
                var instance = PrefabUtility.InstantiatePrefab(lightRigPrefab,
                    this.transform);
                GameObject instanceGameObject = instance as GameObject;

                if (instanceGameObject.TryGetComponent<Transform>(out Transform t))
                {
                    t.position = c.position;
                    t.rotation = Quaternion.identity;
                }
            }

            placeLights = false;
        }
    }
}
