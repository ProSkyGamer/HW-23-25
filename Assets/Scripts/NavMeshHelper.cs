#region

using UnityEngine;
using UnityEngine.AI;

#endregion

public class NavMeshHelper
{
    public static Vector3 GetRandomLocation(Vector3 point, float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += point;
        Vector3 finalPosition = Vector3.zero;

        if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, radius, 1))
            finalPosition = hit.position;

        return finalPosition;
    }
}