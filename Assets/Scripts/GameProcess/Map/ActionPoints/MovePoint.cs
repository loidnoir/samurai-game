using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePoint : IIslandPoint
{
    private Player player;

    public override void StartAction()
    {
        if(player == null)
            player = FindObjectOfType<Player>();

        player.MoveTo(transform.position, EndAction);
    }
}
