using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Video;

[ExecuteInEditMode]
public class PlaceLights : MonoBehaviour
{
    [SerializeField] private bool placeLights;
    [SerializeField] private Transform streetLampParentObject;
    [SerializeField] private GameObject lightRigPrefab;
    [SerializeField] private Vector3 offset;
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
                    t.position = c.position + new Vector3(c.forward.x * offset.x, c.forward.y * offset.y, c.forward.z * offset.z);
                    t.rotation = c.rotation;
                }
            }

            placeLights = false;
        }
    }
}
