using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassicIsland : IIsland
{
    public List<CharacterController> zombies;

    public override void EnterIsland()
    {
        base.EnterIsland();

        foreach (var zombie in zombies)
            zombie.Enabled = true;
    }

    public override void ExitIsland()
    {
        base.ExitIsland();

        foreach (var zombie in zombies)
            zombie.Enabled = false;
    }
}
