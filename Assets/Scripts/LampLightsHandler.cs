using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampLightsHandler : MonoBehaviour
{
    [SerializeField] private float activationRange;
    [SerializeField] private float fadeTime;
    private GameObject player;
    private Light lightComponent;
    private float ogLightIntensity;
    // Start is called before the first frame update
    void Awake()
    {
        player = FindObjectOfType<PlayerStateMachine>().gameObject;
        lightComponent = GetComponentInChildren<Light>();
        this.transform.GetChild(0).gameObject.SetActive(false);
        ogLightIntensity = lightComponent.intensity;
        lightComponent.intensity = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            Vector3 vectorToPlayer = player.transform.position - this.transform.position;

            if (vectorToPlayer.magnitude < activationRange)
            {
                this.transform.GetChild(0).gameObject.SetActive(true);
                lightComponent.intensity += Time.deltaTime * ogLightIntensity / fadeTime;
            }
            else
            {
                lightComponent.intensity -= Time.deltaTime * ogLightIntensity / fadeTime;
            }
            lightComponent.intensity = Mathf.Clamp(lightComponent.intensity, 0f, ogLightIntensity);

            if (lightComponent.intensity == 0f)
            {
                this.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }
}
