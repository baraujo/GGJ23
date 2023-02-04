using GGJ23.Player;
using System.Collections;
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
        [SerializeField] private SpriteRenderer m_MainSprite;
        [SerializeField] private SpriteRenderer m_PivotSprite;

        private LayerMask m_Mask;
        private float m_AimAngle;
        private Vector3 m_NearestTargetDirection;
        private float m_NextShotReady = 0;

        private void Start()
        {
            m_Mask = LayerMask.GetMask("Enemy");
            StartCoroutine(FadeIn());    
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

        private IEnumerator FadeIn()
        {
            Color startMain = m_MainSprite.color;
            Color startPivot = m_PivotSprite.color;
            Color endMain = startMain;
            Color endPivot = startPivot;

            startMain.a = 0;
            startPivot.a = 0;
            m_MainSprite.color = startMain;
            m_PivotSprite.color = startPivot;

            float elapsed = 0;
            float duration = 0.25f;
            while(elapsed < duration)
            {
                var step = elapsed / duration;
                m_MainSprite.color = Color.Lerp(startMain, endMain, step);
                m_PivotSprite.color = Color.Lerp(startPivot, endPivot, step);

                elapsed += Time.deltaTime;
                yield return null;
            }
            m_MainSprite.color = endMain;
            m_PivotSprite.color = endPivot;
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
