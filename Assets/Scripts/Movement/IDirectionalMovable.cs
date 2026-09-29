#region

using UnityEngine;

#endregion

namespace Movement
{
    public interface IDirectionalMovable
    {
        public Vector3 CurrentVelocity { get; }

        public void MoveIn(Vector3 direction);
    }
}