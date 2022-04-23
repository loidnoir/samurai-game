using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "RandomMovement", menuName = "States/RandomMovement")]
public class RandomMovement : State
{
    public float minIdleTime;
    public float maxIdleTime;

    [Space] public float areaRange;

    private Vector3 startPosition;
    private IMovable movable;

    protected override void StartAction()
    {
        movable = characterController.movable;
        startPosition = movable.transform.position;
        movable.Enabled = true;
        NextPoint();
    }
    protected override void Finish()
    {
        characterController.StopCoroutine(IdleEnumerator());
        characterController.animator.SetBool(Keys.EnemyMoveBool, false);
        movable.StopMovement();
        movable.Enabled = false;
    }

    private void NextPoint()
    {
        characterController.animator.SetBool(Keys.EnemyMoveBool, false);
        characterController.StartCoroutine(IdleEnumerator());
    }
    private Vector3 GetPoint()
    {
        return startPosition + new Vector3(Random.Range(-areaRange, areaRange), 0, Random.Range(-areaRange, areaRange));
    }

    IEnumerator IdleEnumerator()
    {
        yield return new WaitForSeconds(Random.Range(minIdleTime, maxIdleTime));
        movable.MoveTo(GetPoint(), NextPoint);
        characterController.animator.SetBool(Keys.EnemyMoveBool, true);
    }
}
