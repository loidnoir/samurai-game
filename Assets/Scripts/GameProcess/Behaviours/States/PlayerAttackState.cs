using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerAttack", menuName = "States/PlayerAttack")]
public class PlayerAttackState : State
{
    private IAttackable attackable;

    protected override void StartAction()
    {
        attackable = characterController.attackable;
        attackable.SetTarget(FindObjectOfType<PlayerController>().damagable);
        attackable.Enabled = true;

        characterController.animator.SetInteger(Keys.EnemyAttackIndex, Random.Range(0, Keys.EnemyAttackVariantsCount));
        characterController.animator.SetBool(Keys.EnemyAttackBool, true);
    }
    protected override void Finish()
    {
        characterController.animator.SetBool(Keys.EnemyAttackBool, false);
        attackable.Enabled = false;
    }
}
