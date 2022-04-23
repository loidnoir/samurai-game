using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IEnemy : MonoBehaviour
{
    public enum StateType
    {
        None,
        Idle,
        detection,
        Attack
    }

    public bool ActionsEnabled
    {
        get => _actionsEnabled;
        set
        {
            if(value != _actionsEnabled)
            {
                _actionsEnabled = value;

                if (_actionsEnabled)
                    SetActive();
                else
                    UnActive();
            }
        }
    }
    private bool _actionsEnabled;

    private StateType State
    {
        get => _state;
        set
        {
            if(value != _state)
            {
                if (actionCoroutine != null)
                {
                    StopCoroutine(actionCoroutine);

                    switch (State)
                    {
                        case StateType.Idle:
                            OnIdleEnded();
                            break;

                        case StateType.detection:
                            OnDetectionEnded();
                            break;

                        case StateType.Attack:
                            OnAttackEnded();
                            break;
                    }
                }
                _state = value;

                switch (value)
                {
                    case StateType.Idle:
                        actionCoroutine = StartCoroutine(IdleNumerator());
                        break;

                    case StateType.detection:
                        actionCoroutine = StartCoroutine(DetectionNumerator());
                        break;

                    case StateType.Attack:
                        actionCoroutine = StartCoroutine(AttackNumerator());
                        break;
                }
            }
        }
    }
    private StateType _state;


    protected Transform target;

    protected float detectionDistacnce;
    protected float attackDistance;
    protected float stateCheckTime;

    private Coroutine actionCoroutine;


    protected virtual void SetActive()
    {
        target = FindObjectOfType<Player>().transform;

        if (!gameObject.activeSelf)
            return;

        StartCoroutine(StateSettingEnumerator());
    }
    protected virtual void UnActive()
    {
        StopCoroutine(StateSettingEnumerator());
    }

    protected virtual void OnIdleEnded() { }
    protected virtual void OnDetectionEnded() { }
    protected virtual void OnAttackEnded() { }


    protected virtual IEnumerator IdleNumerator() { yield return null; }
    protected virtual IEnumerator DetectionNumerator() { yield return null; }
    protected virtual IEnumerator AttackNumerator()  {  yield return null; }

    private IEnumerator StateSettingEnumerator()
    {
        float distance;

        while (true)
        {
            distance = Vector3.Distance(transform.position, target.position);

            if (distance < attackDistance)
                State = StateType.Attack;
            else if (distance < detectionDistacnce)
                State = StateType.detection;
            else
                State = StateType.Idle;

            yield return new WaitForSeconds(stateCheckTime);
        }
    }

}