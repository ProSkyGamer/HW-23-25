#region

using System.Collections;
using UnityEngine;
using UnityEngine.AI;

#endregion

namespace Movement
{
    public class AgentJumper
    {
        private readonly AnimationCurve _jumpCurve;
        private readonly NavMeshAgent _agent;

        private readonly float _jumpSpeed;
        private readonly float _jumpHeight;

        private readonly MonoBehaviour _coroutineRunner;
        private Coroutine _jumpCoroutine;

        public bool IsJumpInProgress => _jumpCoroutine != null;

        public AgentJumper(NavMeshAgent agent, MonoBehaviour coroutineRunner, float jumpSpeed, AnimationCurve jumpCurve, float jumpHeight)
        {
            _agent = agent;
            _jumpCurve = jumpCurve;
            _jumpSpeed = jumpSpeed;
            _coroutineRunner = coroutineRunner;
            _jumpHeight = jumpHeight;
        }

        public void Jump(OffMeshLinkData offMeshLinkData)
        {
            _jumpCoroutine = _coroutineRunner.StartCoroutine(ProcessJump(offMeshLinkData));
        }

        private IEnumerator ProcessJump(OffMeshLinkData offMeshLinkData)
        {
            Vector3 startPos = _agent.transform.position;
            Vector3 endPos = offMeshLinkData.endPos;

            float totalJumpTime = GetTotalJumpTime(startPos, endPos);
            float currentJumpTime = 0;

            while (currentJumpTime < totalJumpTime)
            {
                float jumpProgress = currentJumpTime / totalJumpTime;
                Vector3 newAgentPosition = Vector3.Lerp(startPos, endPos, jumpProgress);
                newAgentPosition.y += _jumpCurve.Evaluate(jumpProgress) * _jumpHeight;

                _agent.transform.position = newAgentPosition;

                currentJumpTime += Time.deltaTime;

                yield return null;
            }

            _jumpCoroutine = null;
            _agent.CompleteOffMeshLink();
        }

        private float GetTotalJumpTime(Vector3 startPos, Vector3 endPos)
        {
            float horizonalDistance = GetHorizonalDistance(startPos, endPos);

            return horizonalDistance / _jumpSpeed;
        }

        private float GetHorizonalDistance(Vector3 startPos, Vector3 endPos)
        {
            startPos.y = endPos.y = 0f;

            return Vector3.Distance(startPos, endPos);
        }
    }
}