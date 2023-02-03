using GGJ23.Player;
using System;
using UnityEngine;

namespace GGJ23.Gameplay
{
    public class RangedPlant : MonoBehaviour
    {
        [SerializeField] private Transform m_Pivot = null;
        [SerializeField] private float m_AttackRadius = 5.0f;
        [SerializeField] private float m_AttackDelay = 5.0f;
        [SerializeField] private GameObject m_ProjectilePrefab = null;
        [SerializeField] private Transform m_ProjectileExitPoint = null;

        private LayerMask m_Mask;
        private float m_AimAngle;
        private Vector3 m_NearestTargetDirection;
        private float m_NextShotReady = 0;

        private void Start()
        {
            m_Mask = LayerMask.GetMask("Enemy");
        }

        private void Update()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, m_AttackRadius, m_Mask);
            m_NearestTargetDirection = Vector3.zero;
            float distance = Mathf.Infinity;
    
            foreach(Collider2D collider in colliders)            
            {
                var currentDistance = Vector2.Distance(transform.position, collider.transform.position);
                if (currentDistance < distance)
                {
                    distance = currentDistance;
                    m_NearestTargetDirection = collider.transform.position - transform.position;
                    m_NearestTargetDirection.Normalize();
                }
            }

            if(m_NearestTargetDirection != Vector3.zero)
            {
                m_AimAngle = Mathf.RoundToInt(Mathf.Atan2(m_NearestTargetDirection.y, m_NearestTargetDirection.x) * Mathf.Rad2Deg);
                m_Pivot.rotation = Quaternion.Lerp(m_Pivot.rotation, Quaternion.Euler(0, 0, m_AimAngle), 0.3f);
                m_NextShotReady += Time.deltaTime;
                if(m_NextShotReady > m_AttackDelay)
                {
                    FireProjectile(m_Pivot.right);
                    m_NextShotReady = 0;
                }
            }

            // World position to plant things
            //var mouseDirection = Camera.main.ScreenToWorldPoint(m_Input.m_MousePosition) - transform.position;
        }

        private void FireProjectile(Vector3 right)
        {
            var projectile = Instantiate(m_ProjectilePrefab, m_ProjectileExitPoint.position, Quaternion.identity, transform).GetComponent<ProjectileController>();
            projectile.SetDirection(right);
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, m_AttackRadius);

            if(m_NearestTargetDirection != null)
            {
                Gizmos.DrawRay(transform.position, m_NearestTargetDirection * m_AttackRadius);
            }

        }
#endif

    }
}
