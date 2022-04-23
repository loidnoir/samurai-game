using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScaler : MonoBehaviour
{
    public static TimeScaler timeScaler;

    private Coroutine slow;
    private Coroutine normalize;

    private void Awake()
    {
        timeScaler = this;
    }

    public void Slow(float speed)
    {
        if (normalize != null)
            StopCoroutine(normalize);
        slow = StartCoroutine(enumerator());

        IEnumerator enumerator()
        {
            while (Time.timeScale > 0.1f)
            {
                Time.timeScale -= Time.deltaTime * speed;
                yield return null;
            }

            Time.timeScale = 0.1f;
        }
    }
    public void Normalize(float speed)
    {
        if (slow != null)
            StopCoroutine(slow);
        normalize = StartCoroutine(enumerator());

        IEnumerator enumerator()
        {
            while (Time.timeScale < 1f)
            {
                Time.timeScale += Time.deltaTime * speed;
                yield return null;
            }

            Time.timeScale = 1;
        }
    }
}
