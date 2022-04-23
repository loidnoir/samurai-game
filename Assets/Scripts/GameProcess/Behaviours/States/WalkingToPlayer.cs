using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WalkingToPlayer", menuName = "States/WalkingToPlayer")]
public class WalkingToPlayer : State
{
    private Transform target;
    private IMovable movable;

    protected override void StartAction()
    {
        target = FindObjectOfType<PlayerController>().transform;

        characterController.animator.SetBool(Keys.EnemyMoveBool, true);
        movable = characterController.movable;
        movable.Enabled = true;
        deltaTime = 0.05f;
    }
    protected override void Action()
    {
        movable.MoveTo(target.position);
    }
    protected override void Finish()
    {
        characterController.animator.SetBool(Keys.EnemyMoveBool, false);
        movable.StopMovement();
        movable.Enabled = false;
    }

}
