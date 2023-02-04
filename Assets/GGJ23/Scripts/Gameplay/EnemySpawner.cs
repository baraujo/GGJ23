using System;
using System.Collections;
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
            public float m_NormalEnemySpeed;
            public GameObject m_NormalEnemyPrefab;
            public int m_NormalEnemyHealth;
            public float m_DelayBetweenEnemies;
            public int m_BossQuantity;
            public float m_BossSpeed;
            public int m_BossHealth;
            public float m_DelayBetweenBosses;
        }

        [SerializeField] private GameObject m_BossEnemyPrefab;
        [SerializeField] private WaveParameters[] m_Waves;
        [SerializeField] private float m_DelayBetweenWaves;
        [SerializeField] private float m_InitialWaveDelay;
        [SerializeField] private SplineContainer m_SplineRef;
        [SerializeField] private GameplayManagerRef m_GameplayManagerRef;

        private float m_Elapsed = 0;
        private bool m_WaveActive = false;
        private int m_WaveIndex = -1;
        private bool m_IsRunning = false;
        private WaveParameters m_CurrentWave = default;

        public WaveParameters CurrentWave => m_CurrentWave;

        void Start()
        {
            m_IsRunning = true;
            StartCoroutine(TriggerNextWaveRoutine(m_InitialWaveDelay));
        }

        void Update()
        {
            if (!m_IsRunning) return;

            if(m_WaveActive)
            {
                if (m_CurrentWave.m_NormalQuantity > 0)
                {
                    if (m_Elapsed > m_CurrentWave.m_DelayBetweenEnemies)
                    {
                        EnemyController enemy = Instantiate(
                            m_CurrentWave.m_NormalEnemyPrefab,
                            transform.position,
                            Quaternion.Euler(1, 1, 1),  
                            transform).GetComponent<EnemyController>();
                        enemy.SplineAnimateRef.splineContainer = m_SplineRef;
                        enemy.SplineAnimateRef.maxSpeed = m_CurrentWave.m_NormalEnemySpeed;
                        enemy.SplineAnimateRef.Play();
                        var health = enemy.GetComponent<HealthManager>();
                        health.InitialHealth = m_CurrentWave.m_NormalEnemyHealth;
                        health.Init();
                        m_CurrentWave.m_NormalQuantity--;
                        m_Elapsed = 0;
                    }
                }
                else if (m_CurrentWave.m_BossQuantity > 0)
                {
                    if (m_Elapsed > m_CurrentWave.m_DelayBetweenBosses)
                    {
                        EnemyController enemy = Instantiate(m_BossEnemyPrefab, transform.position, Quaternion.Euler(1, 1, 1), transform).GetComponent<EnemyController>();
                        enemy.SplineAnimateRef.splineContainer = m_SplineRef;
                        enemy.SplineAnimateRef.maxSpeed = m_CurrentWave.m_BossSpeed;
                        enemy.SplineAnimateRef.Play();
                        enemy.GetComponent<HealthManager>().Init();
                        m_CurrentWave.m_BossQuantity--;
                        m_Elapsed = 0;
                    }
                }
                else
                {
                    m_WaveActive = false;
                    Debug.Log($"Ending wave {m_WaveIndex}");
                    m_Elapsed = 0;
                    StartCoroutine(TriggerNextWaveRoutine(m_DelayBetweenWaves));
                }
            }

            m_Elapsed += Time.deltaTime;
        }

        private IEnumerator TriggerNextWaveRoutine(float delay)
        {
            float elapsed = 0;
            float step;
            while(elapsed < delay)
            {
                step = delay - elapsed;
                m_GameplayManagerRef.Ref.UpdateNextWaveText($"Time to next wave: {step:0.00}");
                elapsed += Time.deltaTime;
                yield return null;
            }
            NextWave();
        }

        public void NextWave()
        {
            m_WaveIndex++;
            if (m_WaveIndex >= m_Waves.Length)
            {
                m_GameplayManagerRef.Ref.UpdateNextWaveText("Game won!");
                m_GameplayManagerRef.Ref.GameWon();
                m_IsRunning = false;
            }
            else
            {
                m_WaveActive = true;
                m_CurrentWave = m_Waves[m_WaveIndex];
                m_Elapsed = 0;
                Debug.Log($"Starting wave {m_WaveIndex}");
                m_GameplayManagerRef.Ref.UpdateNextWaveText($"Wave {m_WaveIndex + 1}");
            }
        }

        public void StopWaves()
        {
            m_IsRunning = false;
        }
    }
}