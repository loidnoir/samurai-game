using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(NavMeshAgent))]
public class NavMeshMovement : IMovable
{
    public override float Speed
    {
        get => _speed;
        set
        {
            if (value != _speed)
            {
                _speed = value;
                navMesh.speed = _speed;
            }
        }
    }
    private NavMeshAgent navMesh;

    protected override void Initialize()
    {
        base.Initialize();
        navMesh = GetComponent<NavMeshAgent>();
    }

    public override void MoveTo(Vector3 point)
    {
        if(Enabled)
            navMesh.SetDestination(point);
    }
    public override void MoveTo(Vector3 point, UnityAction endAction)
    {
        if (Enabled)
        {
            point = ConvertPointToSelfArea(point);
            MoveTo(point);

            StartCoroutine(EndActionEnumerator(StartCoroutine(WaitForReachingPoint(point)), endAction));
        }
    }
    public override void MoveTo(List<Vector3> path, UnityAction endAction)
    {
        if(Enabled)
            StartCoroutine(EndActionEnumerator());

        IEnumerator EndActionEnumerator()
        {
            foreach (var point in path)
            {
                MoveTo(ConvertPointToSelfArea(point));
                yield return StartCoroutine(WaitForReachingPoint(point));
            }

            if (endAction != null) endAction.Invoke(); 
        }
    }
    public override void StopMovement()
    {
        StopAllCoroutines();
        navMesh.destination = transform.position;
    }

    private Vector3 ConvertPointToSelfArea(Vector3 point)
    {
        if(NavMesh.SamplePosition(point, out NavMeshHit hit, navMesh.radius * 50, NavMesh.AllAreas))
            return hit.position;

        return point;
    }

    IEnumerator WaitForReachingPoint(Vector3 point)
    {
        while (Vector3.Distance(transform.position, point) > 0.2f)
            yield return null;
    }
    IEnumerator EndActionEnumerator(Coroutine coroutine, UnityAction endAction)
    {
        yield return coroutine;
        if (endAction != null) endAction.Invoke();
    }
}
