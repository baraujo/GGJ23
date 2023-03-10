using GGJ23.Utils;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.UI;

namespace GGJ23.Gameplay
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private Image m_HealthBarImage = null;
        [SerializeField] private GameplayManagerRef m_GameplayManagerRef = null;
        [SerializeField] private SpriteRenderer m_Sprite;

        private SplineAnimate m_SplineAnimate;
        private HealthManager m_HealthManager;
        private LayerMask m_ObjectiveLayer;

        public SplineAnimate SplineAnimateRef
        {
            get { return m_SplineAnimate; }
            set { m_SplineAnimate = value; }               
        }

        private void Awake()
        {
            m_SplineAnimate = GetComponent<SplineAnimate>();
            m_HealthManager = GetComponent<HealthManager>();
            m_ObjectiveLayer = LayerMask.GetMask("EnemyObjective");
            m_Sprite = GetComponentInChildren<SpriteRenderer>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(LayerUtils.MatchesLayerMask(collision.gameObject, m_ObjectiveLayer))
            {
                Debug.Log("Enemy survived!!!!");
                m_GameplayManagerRef.Ref.EnemySurvived();
                m_SplineAnimate.enabled = false;
            }
        }

        public void TakeDamage()
        {
            m_HealthBarImage.fillAmount = (float)m_HealthManager.CurrentHealth / m_HealthManager.InitialHealth;
            StartCoroutine(DamageFlashRoutine());
        }

        private IEnumerator DamageFlashRoutine()
        {
            Color c = m_Sprite.color;
            m_Sprite.color = Color.red;
            yield return new WaitForSeconds(0.05f);
            m_Sprite.color = c;
            yield return new WaitForSeconds(0.05f);
            m_Sprite.color = Color.red;
            yield return new WaitForSeconds(0.05f);
            m_Sprite.color = c;
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