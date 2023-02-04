using System;
using UnityEngine;

namespace GGJ23.Gameplay
{
    public class GameplayManager : MonoBehaviour
    {
        //[SerializeField] private ControllerInput m_Input = null;
        [Header("Parameters")]
        [SerializeField] private GameplayManagerRef m_Ref = null;
        [SerializeField] private UIManager m_UI = null;
        [SerializeField] private EnemySpawner m_EnemySpawner = null;
        [SerializeField] private int m_SurvivingEnemyLimit = 5;

        [Header("Cooldowns")]
        [SerializeField] private float m_MeleeCooldown = 5;
        [SerializeField] private float m_RangedCooldown = 5;

        private float m_MeleeElapsed;
        private float m_RangedElapsed;

        private void Awake()
        {
            m_Ref.Ref = this;
        }

        private void Start()
        {
            m_MeleeElapsed = m_MeleeCooldown;
            m_RangedElapsed = m_RangedCooldown;
        }

        private void Update()
        {
            if(m_MeleeElapsed < m_MeleeCooldown)
            {
                m_MeleeElapsed += Time.deltaTime;
            }

            if (m_RangedElapsed < m_RangedCooldown)
            {
                m_RangedElapsed += Time.deltaTime;
            }

            m_UI.UpdatePlantCooldowns(
                Mathf.Clamp01(m_MeleeElapsed / m_MeleeCooldown),
                Mathf.Clamp01(m_RangedElapsed / m_RangedCooldown)
            );
        }

        public void EnemySurvived()
        {
            m_SurvivingEnemyLimit--;
            if (m_SurvivingEnemyLimit > 0)
            {
                Debug.Log($"Enemy survived! Only {m_SurvivingEnemyLimit} can survive now!");
            }
            else
            {
                Debug.Log("The farm is destroyed!");
                m_EnemySpawner.StopWaves();
                // Game over!
            }
        }

        public void GameWon()
        {
            Debug.Log("All enemies defeated!!!!!");
            m_EnemySpawner.StopWaves();
            // Game won!
        }

        internal bool CanPlaceMelee()
        {
            return m_MeleeElapsed >= m_MeleeCooldown;
        }

        internal bool CanPlaceRanged()
        {
            return m_RangedElapsed >= m_RangedCooldown;
        }

        internal void TriggerMeleeCooldown()
        {
            m_MeleeElapsed = 0;
        }

        internal void TriggerRangedCooldown()
        {
            m_RangedElapsed = 0;
        }
    }

    // TODO:
    // Time to next wave como contagem regressiva
    // Clicar na planta faz ela sair da tela
}
