using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterController : MonoBehaviour
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

    [HideInInspector] public IMovable movable;
    [HideInInspector] public IAttackable attackable;
    [HideInInspector] public Damagable damagable;

    [HideInInspector] public Animator animator;

    private void Awake()
    {
        Initialize();
    }
    public virtual void Initialize()
    {
        movable = GetComponentInChildren<IMovable>();
        if (movable == null) throw new NullReferenceException($"Missing {name}'s movable component");

        attackable = GetComponentInChildren<IAttackable>();
        if (attackable == null) throw new NullReferenceException($"Missing {name}'s attackable component");

        damagable = GetComponentInChildren<Damagable>();
        if (damagable == null) throw new NullReferenceException($"Missing {name}'s damagable component");

        animator = GetComponentInChildren<Animator>();
        if (animator == null) throw new NullReferenceException($"Missing {name}'s animator component");
    }
}
