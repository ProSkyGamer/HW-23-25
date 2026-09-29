#region

using UnityEngine;

#endregion

public class LookAtRotator : MonoBehaviour
{
    [SerializeField] private Transform _lookTarget;

    private void LateUpdate()
    {
        transform.LookAt(_lookTarget);
    }
}