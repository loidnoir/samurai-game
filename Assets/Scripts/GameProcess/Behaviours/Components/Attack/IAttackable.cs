using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IAttackable : MonoBehaviour
{
    public virtual bool Enabled
    {
        get => _enabled;
        set
        {
            if (value != _enabled)
                _enabled = value;
        }
    }
    protected bool _enabled;

    [Header("Characteristics")]
    public float damage;

    public abstract void SetTarget(Damagable target);
}
