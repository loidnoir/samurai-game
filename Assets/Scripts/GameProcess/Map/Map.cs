using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField]
    private List<IIsland> islands;
    private IIsland currentIsland;

    void Start()
    {
        //NextIsland();
    }

    private void NextIsland()
    {
        int index = currentIsland != null ? islands.IndexOf(currentIsland) : -1;

        if (index < islands.Count - 1)
        {
            currentIsland = islands[index + 1];
            currentIsland.OnIslandExit.AddListener(CurrentIsland_OnIslandExit);
            currentIsland.EnterIsland();
        }
    }
    private void CurrentIsland_OnIslandExit()
    {
        NextIsland();
    }
}
