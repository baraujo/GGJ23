using UnityEngine;
using GGJ23.Gameplay;

namespace GGJ23.Player
{
    public class ProjectileController : MonoBehaviour
    {
        [SerializeField] private float m_ProjectileSpeed = 10.0f;

        private FloatingMovement m_Movement;
        private float m_Elapsed = 0;

        private void Awake()
        {
            m_Movement = GetComponent<FloatingMovement>();
        }

        private void Update()
        {
            if(m_Elapsed > 20.0f)
            {
                Destroy(gameObject);
            }

            m_Elapsed += Time.deltaTime;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Destroy(gameObject);
        }

        public void SetDirection(Vector3 direction)
        {
            m_Movement.Velocity = direction * m_ProjectileSpeed;
        }
    }
}
