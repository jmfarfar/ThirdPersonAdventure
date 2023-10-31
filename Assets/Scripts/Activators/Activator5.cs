using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator5 : MonoBehaviour
{
    [SerializeField] private CutsceneCatalog cutscene;
    [SerializeField] private GameObject cutsceneDeborah;
    [SerializeField] private GameObject cutsceneDeborah2;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (cutsceneDeborah != null)
            {
                InputReader inputReader = cutsceneDeborah.GetComponent<InputReader>();
                inputReader.enabled = false;

                if (cutsceneDeborah2 != null)
                {
                    InputReader inputReader2 = cutsceneDeborah2.GetComponent<InputReader>();
                    inputReader2.enabled = false;
                }
            }
            SceneManager.Instance.StartTimeline((int)cutscene);
            Destroy(this);
        }
    }
}
