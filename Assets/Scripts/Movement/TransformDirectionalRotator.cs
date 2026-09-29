#region

using UnityEngine;

#endregion

namespace Movement
{
    public class TransformDirectionalRotator : DirectionalRotator
    {
        private readonly Transform _transform;

        public override Quaternion Rotation => _transform.rotation;

        public TransformDirectionalRotator(Transform transform, float speed) : base(speed)
        {
            _transform = transform;
        }

        protected override void ApplyRotation(Quaternion rotation)
        {
            _transform.rotation = rotation;
        }
    }
}