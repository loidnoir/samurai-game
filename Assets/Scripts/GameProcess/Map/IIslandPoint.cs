using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class IIslandPoint : MonoBehaviour
{
    public UnityEvent OnEndingAction;

    public virtual void StartAction() { }
    public virtual void EndAction() { OnEndingAction.Invoke(); }
}
