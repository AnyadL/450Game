using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    public class Camera2DFollow : MonoBehaviour
    {
        public List<Transform> targets;
        public float damping = 1;
        public float lookAheadFactor = 3;
        public float lookAheadReturnSpeed = 0.5f;
        public float lookAheadMoveThreshold = 0.1f;

        private float m_OffsetZ;
        private Vector3 m_LastTargetPosition;
        private Vector3 m_CurrentVelocity;
        private Vector3 m_LookAheadPos;
        private Vector3 target;

        // Use this for initialization
        private void Start()
        {
            float x = 0f;
            float y = 0f;
            for (int i = 0; i < targets.Count; i++)
            {
                x += targets[i].position.x;
                y += targets[i].position.y;
            }
            target = new Vector3(x/targets.Count, y/targets.Count);
            m_LastTargetPosition = target;
            m_OffsetZ = (transform.position - target).z;
            transform.parent = null;
        }


        // Update is called once per frame
        private void Update()
        {
            float x = 0f;
            float y = 0f;
            int deadCount = 0;
            for (int i = 0; i < targets.Count; i++)
            {
                if (targets[i] == null)
                {
                    ++deadCount;
                    continue;
                }
                x += targets[i].position.x;
                y += targets[i].position.y;
            }
            if (deadCount == targets.Count)
            {
                return;
            }
            target.x = x / targets.Count;
            target.y = y / targets.Count;

            // only update lookahead pos if accelerating or changed direction
            float xMoveDelta = (target - m_LastTargetPosition).x;

            bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

            if (updateLookAheadTarget)
            {
                m_LookAheadPos = lookAheadFactor*Vector3.right*Mathf.Sign(xMoveDelta);
            }
            else
            {
                m_LookAheadPos = Vector3.MoveTowards(m_LookAheadPos, Vector3.zero, Time.deltaTime*lookAheadReturnSpeed);
            }

            Vector3 aheadTargetPos = target + m_LookAheadPos + Vector3.forward*m_OffsetZ;
            Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref m_CurrentVelocity, damping);

            transform.position = newPos;

            m_LastTargetPosition = target;
        }
    }
}
