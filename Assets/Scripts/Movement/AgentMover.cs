#region

using UnityEngine;
using UnityEngine.AI;

#endregion

namespace Movement
{
    public class AgentMover
    {
        private const float MinMovementVelocity = 0.05f;
        private readonly NavMeshAgent _agent;

        public Vector3 Direction => _agent.desiredVelocity;

        public bool IsRunning => _agent.desiredVelocity.magnitude >= MinMovementVelocity;

        public AgentMover(NavMeshAgent agent, float speed)
        {
            _agent = agent;
            agent.speed = speed;
            agent.angularSpeed = 999f;
            agent.acceleration = 999f;
        }

        public void SetDestination(Vector3 destination)
        {
            _agent.SetDestination(destination);
        }

        public void Stop()
        {
            _agent.isStopped = true;
        }

        public void Continue()
        {
            _agent.isStopped = false;
        }
    }
}