using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomScreen : MonoBehaviour
{
    public static List<Vector3> ScreenToWorld(List<Vector2> touches, Vector3 startPosition)
    {
        List<Vector3> returnValue = new List<Vector3>();

        float distance = Vector3.Distance(Camera.main.transform.position, startPosition);
        float angle = (90 - Camera.main.transform.eulerAngles.x) * Mathf.Deg2Rad;

        returnValue.Add(Camera.main.ScreenToWorldPoint(new Vector3(touches[0].x, touches[0].y, distance)));
        Vector3 delta = startPosition - returnValue[0];

        returnValue[0] += delta;
        returnValue[0] = new Vector3(returnValue[0].x, startPosition.y, returnValue[0].z + (returnValue[0].y - startPosition.y) * Mathf.Tan(angle));

        for (int i = 1; i < touches.Count; i++)
        {
            returnValue.Add(Camera.main.ScreenToWorldPoint(new Vector3(touches[i].x, touches[i].y, distance)));

            returnValue[i] += delta;
            returnValue[i] = new Vector3(returnValue[i].x + (returnValue[i].x - startPosition.x) * 0.2f, startPosition.y, returnValue[i].z + (returnValue[i].y - startPosition.y) * Mathf.Tan(angle));
        }

        return returnValue;
    }
}
