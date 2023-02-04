using GGJ23.Utils;
using UnityEngine;
using UnityEngine.Events;

namespace GGJ23.Gameplay
{
    // This component manages an entity health, firing events when 
    // damage and death occurs
    public class HealthManager : MonoBehaviour
    {
        public UnityEvent m_OnTakeDamage = default;
        public UnityEvent m_OnDeath = default;
        public UnityEvent m_OnRecoverHealth = default;

        [SerializeField] private int m_InitialHealth = default;
        [SerializeField] private int m_CurrentHealth = default;
        [SerializeField] private bool m_IsInvincible = default;
        [SerializeField] private LayerMask m_CollisionMask = default;

        public int CurrentHealth => m_CurrentHealth;
        public int InitialHealth
        {
            get => m_InitialHealth;
            set => m_InitialHealth = value;
        }

        public bool IsInvincible
        {
            get => m_IsInvincible;
            set => m_IsInvincible = value;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (m_CurrentHealth <= 0 || m_IsInvincible) return;

            if (LayerUtils.MatchesLayerMask(collision.gameObject, m_CollisionMask))
            {
                TakeHit();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (m_CurrentHealth <= 0 || m_IsInvincible) return;

            if (LayerUtils.MatchesLayerMask(collision.gameObject, m_CollisionMask))
            {
                TakeHit();
            }
        }

        public void Init()
        {
            m_CurrentHealth = m_InitialHealth;
        }

        public void TakeHit()
        {
            m_CurrentHealth--;
            if (m_CurrentHealth <= 0)
            {
                m_OnDeath.Invoke();
            }
            else
            {
                m_OnTakeDamage.Invoke();
            }
        }

        public bool RecoverHealth(int amount)
        {
            if (m_CurrentHealth == m_InitialHealth)
            {
                return false;
            }

            m_CurrentHealth = Mathf.Min(m_InitialHealth, m_CurrentHealth + amount);
            m_OnRecoverHealth.Invoke();
            return true;
        }
    }
}
