using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    public virtual bool Enabled
    {
        get => _enabled;
        set
        {
            if (value != _enabled)
            {
                _enabled = value;

                if (_enabled) Appear();
                else Hide();
            }
        }
    }
    protected bool _enabled;

   [HideInInspector] public float damage;

    private void Awake()
    {
        Initialize();
    }
    protected virtual void Initialize() { }

    protected virtual void Appear() { }
    protected virtual void Hide() { }
    protected abstract void OnHitting(Damagable damagable);
}
