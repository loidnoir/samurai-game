using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class IMovable : MonoBehaviour
{
    public virtual bool Enabled 
    {
        get => _enabled;
        set
        {
            if(value != _enabled)
                _enabled = value;
        }
    }
    protected bool _enabled;

    public virtual float Speed
    {
        get => _speed;
        set
        {
            if (value != _speed)
                _speed = value;
        }
    }
    protected float _speed;

    //[HideInInspector] public UnityEvent OnStartingMovement;
    //[HideInInspector] public UnityEvent OnMovement;
    //[HideInInspector] public UnityEvent OnEndingMovement;

    //[HideInInspector] public UnityEvent OnStartingFloating;
    //[HideInInspector] public UnityEvent OnFloating;
    //[HideInInspector] public UnityEvent OnEndingFloating;


    private void Awake()
    {
        Initialize();
    }
    protected virtual void Initialize() { }

    public abstract void MoveTo(Vector3 point);
    public abstract void MoveTo(Vector3 point, UnityAction endAction);
    public abstract void MoveTo(List<Vector3> path, UnityAction endAction);
    public abstract void StopMovement();
}
