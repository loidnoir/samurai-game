using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : ScriptableObject
{
    public virtual bool Active
    {
        get => _active;
        set
        {
            if (value != _active)
            {
                _active = value;

                if (_active)
                {
                    StartAction();
                    characterController.StartCoroutine(ActionUpdate());
                }
                else
                {
                    Finish();
                }
            }
        }
    }
    protected bool _active;
    protected float deltaTime;

    [HideInInspector] public CharacterController characterController;

    protected virtual void StartAction() { }
    protected virtual void Action() { }
    protected virtual void Finish() { }

    protected IEnumerator ActionUpdate()
    {
        while (_active)
        {
            Action();

            if (deltaTime <= 0)
                yield return null;
            else
                yield return new WaitForSeconds(deltaTime);
        }
    }
}
