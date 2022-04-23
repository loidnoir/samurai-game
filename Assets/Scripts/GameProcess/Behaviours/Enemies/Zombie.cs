using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Zombie : IEnemy
{
    const string WalkState = "Walk";
    const string IdlingActionState = "IdlingAction";
    const string IdleRandomActionState = "IdlingActionNumber";
    const string AttackState = "Attack";
    const string AttackRandomState = "AttackNumber";

    const int IdleActionsCount = 1;
    const int AttackTypesCount = 2;

    public Animator animator;

    private float speed;


    private void Start()
    {
        speed = Random.Range(0.38f, 0.42f);
        detectionDistacnce = Random.Range(5.5f, 6.5f);
        attackDistance = Random.Range(1.2f, 1.4f);

    }

    protected override IEnumerator IdleNumerator()
    {
        yield return new WaitForSeconds(Random.Range(0, 5));

        while (true)
        {
            yield return new WaitForSeconds(Random.Range(4, 10));
            animator.SetTrigger(IdlingActionState);
            animator.SetTrigger($"{IdleRandomActionState}{Random.Range(0, IdleActionsCount) + 1}");
        }
    }

    protected override IEnumerator DetectionNumerator()
    {
        yield return new WaitForSeconds(Random.Range(0, 0.5f));

        Quaternion lookRotation;
        animator.SetBool(WalkState, true);

        while (true)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, speed * Time.deltaTime);
            lookRotation = Quaternion.LookRotation(transform.position - target.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, speed * 4 * Time.deltaTime);
            yield return null;
        }
    }
    protected override void OnDetectionEnded()
    {
        animator.SetBool(WalkState, false);
    }

    protected override IEnumerator AttackNumerator()
    {
        yield return new WaitForSeconds(Random.Range(0, 0.5f));

        Quaternion lookRotation;
        animator.SetBool(AttackState, true);
        animator.SetInteger(AttackRandomState, Random.Range(0, AttackTypesCount));


        while (true)
        {
            lookRotation = Quaternion.LookRotation(transform.position - target.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, speed * 4 * Time.deltaTime);
            yield return null;
        }
    }
    protected override void OnAttackEnded()
    {
        animator.SetBool(AttackState, false);
    }
}
