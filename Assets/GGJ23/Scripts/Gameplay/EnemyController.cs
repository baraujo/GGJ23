using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.UI;

namespace GGJ23.Gameplay
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private Image m_HealthBarImage;

        private SplineAnimate m_SplineAnimate;
        private HealthManager m_HealthManager;

        public SplineAnimate SplineAnimateRef
        {
            get { return m_SplineAnimate; }
            set { m_SplineAnimate = value; }               
        }

        private void Awake()
        {
            m_SplineAnimate = GetComponent<SplineAnimate>();
            m_HealthManager = GetComponent<HealthManager>();
        }

        public void TakeDamage()
        {
            m_HealthBarImage.fillAmount = (float)m_HealthManager.CurrentHealth / m_HealthManager.InitialHealth;
        }

        public void Die()
        {
            //Debug.Log($"{name} died");
            Destroy(gameObject);
        }

        // TODO:
        // Gameplay: add gameplay states: running, game over, game won
        // Title screen: just make a separated scene and go with it
    }
}