using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public enum StateType
    {
        Idle,
        Walking,
        Falling,
        Attacking
    }

    public const string WalkKey = "Walk";
    public const string JumpKey = "Jump";
    public const string Fall = "Fall";

    [SerializeField] private Animator animator;
    private new Rigidbody rigidbody;

    public StateType State { get => _state; 
        set 
        { 
            if(value != _state)
            {
                switch (_state)
                {
                    case StateType.Idle:
                        break;
                        
                    case StateType.Walking:
                        animator.SetBool("Walk", false);
                        break;

                    case StateType.Falling:
                        animator.SetBool(Fall, false);
                        break;

                    case StateType.Attacking:
                        break;
                }

                switch (value)
                {
                    case StateType.Idle:
                        break;

                    case StateType.Walking:
                        break;

                    case StateType.Falling:
                        animator.SetBool(Fall, true);
                        break;

                    case StateType.Attacking:
                        break;
                }

                _state = value;

            }
        }
    }
    private StateType _state;

    private Coroutine moveCoroutine;
    private float speed;


    private void Awake()
    {
        rigidbody = GetComponentInChildren<Rigidbody>();
        animator = GetComponentInChildren<Animator>();

        speed = 1;
    }

    public void Attack(List<Vector3> path)
    {
        foreach (var point in path)
        {
            MoveTo(point);
        }
    }

    public void MoveTo(Vector3 position, UnityAction endAction = null)
    {
        State = StateType.Walking;

        if (moveCoroutine != null)
            StopCoroutine(moveCoroutine);

        moveCoroutine = StartCoroutine(MoveEnumerator());

        IEnumerator MoveEnumerator()
        {
            Quaternion lookRotation;

            while (Vector3.Distance(transform.position, position) > 0.5f)
            {
                transform.Translate(Vector3.forward * Time.deltaTime);
                lookRotation = Quaternion.LookRotation(position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, speed * 1 * Time.deltaTime);
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
                yield return null;
            }

            if (endAction != null)
                endAction.Invoke();
        }
    }

    public void JumpTo(Vector3 position)
    {
        if (moveCoroutine != null)
            StopCoroutine(moveCoroutine);

        moveCoroutine = StartCoroutine(JumpEnumerator(position));
    }
    IEnumerator JumpEnumerator(Vector3 position)
    {
        yield return new WaitForSeconds(0.5f);

        Quaternion lookRotation;

        animator.SetBool(WalkKey, false);
        animator.SetTrigger("Jump");

        yield return new WaitForSeconds(0.5f);

        rigidbody.AddForce(Vector3.up * Vector3.Distance(transform.position, position), ForceMode.Impulse);

        while (Vector3.Distance(transform.position, position) > 0.5f)
        {
            transform.Translate(Vector3.forward * speed * 6.5f * Time.deltaTime);
            lookRotation = Quaternion.LookRotation(position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, speed * 12 * Time.deltaTime);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
            yield return null;
        }
    }

    private void FixedUpdate()
    {
        if(rigidbody.velocity.y < -0.1f || rigidbody.velocity.y > 0.1f)
        {
            State = StateType.Falling;
        }
        else if (State == StateType.Falling)
        {
            State = StateType.Walking;
        }
    }
}
