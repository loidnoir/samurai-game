using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchCanvas : MonoBehaviour
{
    public Animator animator;

    public void Show()
    {
        animator.SetTrigger("FadeIn");
    }

    public void Hide()
    {
        animator.SetTrigger("FadeOut");
    }
}
