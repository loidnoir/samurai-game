using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingIsland : IIsland
{
    public const string FloatKey = "Float";

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(StartFloating());
    }

    public override void EnterIsland()
    {
        base.EnterIsland();
        animator.SetBool(FloatKey, false);
    }
    public override void ExitIsland()
    {
        base.ExitIsland();
        animator.SetBool(FloatKey, true);
    }

    IEnumerator StartFloating()
    {
        yield return new WaitForSeconds(Random.Range(0, 1.5f));

        animator.SetBool(FloatKey, true);
    }
}
