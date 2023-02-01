using System;
using UnityEngine;
using UnityEngine.Splines;

namespace GGJ23.Gameplay
{
    public class EnemySpawner : MonoBehaviour
    {
        [Serializable]
        public struct WaveParameters
        {
            public int m_NormalQuantity;
            public float m_DelayBetweenEnemies;
            public int m_BossQuantity;
            public float m_DelayBetweenBosses;
        }

        [SerializeField] private GameObject m_NormalEnemyPrefab;
        [SerializeField] private GameObject m_BossEnemyPrefab;
        [SerializeField] private WaveParameters[] m_Waves;
        [SerializeField] private float m_DelayBetweenWaves;
        [SerializeField] private float m_Elapsed;
        [SerializeField] private SplineContainer m_SplineRef;

        private bool m_WaveActive = false;
        private int m_WaveIndex = -1;
        private bool m_IsRunning = false;
        private WaveParameters m_CurrentWave = default;

        void Start()
        {
            m_Elapsed = 0;
            m_IsRunning = true;
        }

        void Update()
        {
            if (!m_IsRunning) return;

            if (!m_WaveActive)
            {
                if (m_Elapsed > m_DelayBetweenWaves)
                {
                    m_WaveIndex++;
                    if (m_WaveIndex >= m_Waves.Length)
                    {
                        // Game won!
                        Debug.Log("Game won!");
                    }
                    else
                    {
                        m_WaveActive = true;
                        m_CurrentWave = m_Waves[m_WaveIndex];
                        m_Elapsed = 0;
                        Debug.Log($"Starting wave {m_WaveIndex}");
                    }
                }
            }
            else
            {
                if (m_CurrentWave.m_NormalQuantity > 0)
                {
                    if (m_Elapsed > m_CurrentWave.m_DelayBetweenEnemies)
                    {
                        EnemyController enemy = Instantiate(m_NormalEnemyPrefab, transform).GetComponent<EnemyController>();
                        enemy.SplineRef.splineContainer = m_SplineRef;
                        enemy.SplineRef.Play();
                        m_CurrentWave.m_NormalQuantity--;
                        m_Elapsed = 0;
                    }
                }
                else if (m_CurrentWave.m_BossQuantity > 0)
                {
                    if (m_Elapsed > m_CurrentWave.m_DelayBetweenBosses)
                    {
                        EnemyController enemy = Instantiate(m_BossEnemyPrefab, transform).GetComponent<EnemyController>();
                        enemy.SplineRef.splineContainer = m_SplineRef;
                        enemy.SplineRef.Play();
                        m_CurrentWave.m_BossQuantity--;
                        m_Elapsed = 0;
                    }
                }
                else
                {
                    m_WaveActive = false;
                    Debug.Log($"Ending wave {m_WaveIndex}");
                    m_Elapsed = 0;
                }
            }

            m_Elapsed += Time.deltaTime;
        }
    }
}