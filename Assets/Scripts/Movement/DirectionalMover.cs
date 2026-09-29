#region

using UnityEngine;

#endregion

namespace Movement
{
    public abstract class DirectionalMover : IDirectionalMovable
    {
        private readonly float _speed;
        private Vector3 _direction;

        public Vector3 CurrentVelocity => _direction * _speed;

        public DirectionalMover(float speed)
        {
            _speed = speed;
        }

        public void MoveIn(Vector3 direction)
        {
            _direction = direction;
        }

        public abstract void Update(float deltaTime);
    }
}