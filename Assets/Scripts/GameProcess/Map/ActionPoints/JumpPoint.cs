using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPoint : IIslandPoint
{
    private Player player;

    public override void StartAction()
    {
        if (player == null)
            player = FindObjectOfType<Player>();

        player.JumpTo(transform.position);

        StartCoroutine(CheckIfEnded());
    }

    private IEnumerator CheckIfEnded()
    {
        while (Vector3.Distance(player.transform.position, transform.position) > 0.5f)
        {
            yield return new WaitForSeconds(0.1f);
        }

        EndAction();
    }
}
