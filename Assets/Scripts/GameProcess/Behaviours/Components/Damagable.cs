using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damagable : MonoBehaviour
{
    public UnityEvent OnGettingDamage;
    public UnityEvent OnHealhEnding;

    [SerializeField] private float health;

    public void ChangeHealth(float size)
    {
        health += size;
        if(OnGettingDamage != null) OnGettingDamage.Invoke();

        if (health <= 0) OnHealhEnding.Invoke();
    }
}
