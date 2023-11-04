using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;

public class SceneManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> mDirectors;
    private static SceneManager instance;
    public static SceneManager Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        //mDirectors = GetComponentsInChildren<PlayableDirector>().ToList();

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);

        }
    }    

    public void StartTimeline(int index)
    {
        mDirectors[index].SetActive(true);
        var director = mDirectors[index].GetComponent<PlayableDirector>();
        director.Play();
    }
}
