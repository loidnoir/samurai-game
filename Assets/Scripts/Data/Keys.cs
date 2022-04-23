using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Keys
{
#region Enemy
    public static readonly int EnemyMoveBool = Animator.StringToHash("Move");

    public static readonly int EnemyAttackBool = Animator.StringToHash("Attack");
    public static readonly int EnemyAttackIndex = Animator.StringToHash("AttackIndex");
    public static readonly int EnemyAttackVariantsCount = 3;

    public static readonly int EnemyIdlingAction = Animator.StringToHash("IdlingAction");
    public static readonly int EnemyIdlingActionIndex = Animator.StringToHash("IdlingActionNumber");
    public static readonly int EnemyIdlingActionVariantsCount = 3;
#endregion
}
