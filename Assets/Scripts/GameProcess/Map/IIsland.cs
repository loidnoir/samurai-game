using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class IIsland : MonoBehaviour
{
    public UnityEvent OnIslandExit;

    public List<IIslandPoint> islandPoints;
    private IIslandPoint currentPoint;

    public virtual void EnterIsland() 
    {
        if (islandPoints.Count > 0)
        {
            currentPoint = islandPoints[0];
            currentPoint.OnEndingAction.AddListener(CurrentPoint_OnEndingAction);
            currentPoint.StartAction();
        }
    }
    public virtual void ExitIsland() { if (OnIslandExit != null) OnIslandExit.Invoke(); }
    public virtual void NextPoint()
    {
        int curPointIndex = islandPoints.IndexOf(currentPoint);

        if (curPointIndex + 1 < islandPoints.Count)
        {
            currentPoint = islandPoints[curPointIndex + 1];
            currentPoint.OnEndingAction.AddListener(CurrentPoint_OnEndingAction);
            currentPoint.StartAction();
        }
        else
        {
            ExitIsland();
        }
    }

    private void CurrentPoint_OnEndingAction()
    {
        NextPoint();     
    }
}
