#region

using UnityEngine;

#endregion

namespace Controllers
{
    public interface IPositionMover
    {
        public bool IsMoving { get; }
        public Vector3 Position { get; }

        public void Continue();
        public void Stop();
        public void SetDestination(Vector3 destination);
    }
}