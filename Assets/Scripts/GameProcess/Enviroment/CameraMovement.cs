using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform player;
    public Transform island;

    public Vector3 offset;
    public float offsetFromIsland;

    private void Start()
    {
        StartCoroutine(MoveToTarget());
    }

    private IEnumerator MoveToTarget()
    {
        Vector3 position;
        Vector3 islandPos;
        Vector3 islandScale;

        while (true)
        {
            position = player.position + offset;
            islandPos = island.position;
            islandPos.z += offset.z * 1.5f;
            islandScale = island.localScale * GlobalConsts.isalndScaleToWorld / 2;

            if (position.x > islandPos.x + islandScale.x + offsetFromIsland)
                position.x = islandPos.x + islandScale.x + offsetFromIsland;
            if(position.x < islandPos.x - islandScale.x - offsetFromIsland)
                position.x = islandPos.x - islandScale.x - offsetFromIsland;

            if (position.z > islandPos.z + islandScale.z + offsetFromIsland)
                position.z = islandPos.z + islandScale.z + offsetFromIsland;
            if (position.z < islandPos.z - islandScale.z - offsetFromIsland)
                position.z = islandPos.z - islandScale.z - offsetFromIsland;

            transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime * 3);
            yield return null;
        }
    }
}
