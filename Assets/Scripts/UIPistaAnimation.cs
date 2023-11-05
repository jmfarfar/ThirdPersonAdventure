using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPistaAnimation : MonoBehaviour
{
    private Animator animator;
    private Button button;
    private bool wasPressed;

    void Start()
    {
        animator = GetComponent<Animator>();
        button = GetComponent<Button>();

        button.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        Debug.Log("button pressed");
        if (!wasPressed)
        {
            animator.Play("Pressed");
            wasPressed = true;
        }
        else
        {
            animator.Play("PressedReverse");
            wasPressed = false;
        }
    }

    void Update()
    {
        
    }
}
