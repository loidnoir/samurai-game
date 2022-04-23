using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : CharacterController
{
    private const float stateCheckingTime = 0.2f;

    public override bool Enabled 
    { 
        get => _enabled; 
        set
        {
            if(value != _enabled)
            {
                _enabled = value;

                if (_enabled && statesCheckingCoroutine == null) 
                    statesCheckingCoroutine = StartCoroutine(UpdateStatesEnumerator());
            }
        } 
    }

    [SerializeField] private State idlingState;
    [SerializeField] private State targetDetectionState;
    [SerializeField] private State targetAttackingState;

    [Space]
    [SerializeField] private float detectionDistacnce;
    [SerializeField] private float attackDistance;

    private State currentState;
    private Transform target;

    private Coroutine statesCheckingCoroutine;

    private void Start()
    {
        movable.Speed = 1f;

        Enabled = true;
    }

    private void SetState(State state)
    {
        if(currentState == null || state.GetType() != currentState.GetType())
        {
            if(currentState != null) currentState.Active = false;

            currentState = Instantiate(state);
            currentState.characterController = this;
            currentState.Active = true;
        }
    }
    private IEnumerator UpdateStatesEnumerator()
    {
        if (target == null)
        {
            PlayerController player = FindObjectOfType<PlayerController>();

            if (player != null)
                target = player.transform;
            else
                throw new NullReferenceException("None player found in the scene");
        }

        float distance;

        while (Enabled)
        {
            distance = Vector3.Distance(transform.position, target.position);
            
            if(distance > detectionDistacnce)
            {
                SetState(idlingState);
            }
            else if(distance > attackDistance)
            {
                SetState(targetDetectionState);
            }
            else
            {
                SetState(targetAttackingState);
            }

            yield return new WaitForSeconds(stateCheckingTime);
        }
    }
}
