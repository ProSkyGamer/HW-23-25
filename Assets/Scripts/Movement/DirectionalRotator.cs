#region

using UnityEngine;

#endregion

namespace Movement
{
    public abstract class DirectionalRotator : IDirectionalRotatable
    {
        private readonly float _speed;
        private Vector3 _targetDirection;

        public abstract Quaternion Rotation { get; }

        protected DirectionalRotator(float speed)
        {
            _speed = speed;
        }

        public void SetDirection(Vector3 direction)
        {
            _targetDirection = direction;
        }

        public void Update(float deltaTime)
        {
            if (_targetDirection == Vector3.zero)
                return;

            Quaternion lookRotation = Quaternion.LookRotation(_targetDirection);

            float step = deltaTime * _speed;

            ApplyRotation(Quaternion.RotateTowards(Rotation, lookRotation, step));
        }

        protected abstract void ApplyRotation(Quaternion rotation);
    }
}