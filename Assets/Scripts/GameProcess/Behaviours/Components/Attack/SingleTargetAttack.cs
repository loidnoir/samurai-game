using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleTargetAttack : IAttackable
{
    public override bool Enabled 
    { 
        get => _enabled;
        set 
        {
            if(value != _enabled)
            {
                _enabled = value;

                if (_enabled)
                {
                    if (projectile == null) SpawnProjectile();
                }
                else
                    DisActivateProjectile();
            }
        } 
    }

    private Damagable target;

    [Header("ProjectileInfo")]
    public Projectile projectilePrefab;
    public Transform projectileHandler;

    private Projectile projectile;
    private Quaternion lookRotation;


    private void Update()
    {
        if (Enabled)
        {
            lookRotation = Quaternion.LookRotation(target.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        }
    }
    private void SpawnProjectile()
    {
        projectile = Instantiate(projectilePrefab.gameObject, projectileHandler).GetComponent<Projectile>();
        projectile.transform.localPosition = new Vector3();
    }

    public override void SetTarget(Damagable target)
    {
        this.target = target;
    }

    public void ActivateProjectile()
    {
        projectile.damage = damage;
        projectile.Enabled = true;
    }
    public void DisActivateProjectile()
    {
        if (projectile != null) projectile.Enabled = false;
    }
}
