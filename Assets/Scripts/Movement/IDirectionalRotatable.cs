#region

using UnityEngine;

#endregion

namespace Movement
{
    public interface IDirectionalRotatable
    {
        public Quaternion Rotation { get; }

        public void SetDirection(Vector3 direction);
    }
}