using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleTargetAttackHelper : MonoBehaviour
{
    private SingleTargetAttack singleTargetAttack;

    private void Awake()
    {
        singleTargetAttack = GetComponentInParent<SingleTargetAttack>();
    }

    public void ActivateProjectile()
    {
        singleTargetAttack.ActivateProjectile();
    }
    public void DisActivateProjectile()
    {
        singleTargetAttack.DisActivateProjectile();
    }
}