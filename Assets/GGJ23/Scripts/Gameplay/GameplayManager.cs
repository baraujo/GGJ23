using GGJ23.Input;
using UnityEngine;

namespace GGJ23.Gameplay
{
    public class GameplayManager : MonoBehaviour
    {
        //[SerializeField] private ControllerInput m_Input = null;
        [SerializeField] private GameplayManagerRef m_Ref = null;
        [SerializeField] private UIManager m_UI = null;
        [SerializeField] private EnemySpawner m_EnemySpawner = null;
        [SerializeField] private int m_SurvivingEnemyLimit = 5;
        private void Awake()
        {
            m_Ref.Ref = this;
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

        public void UpdatePlantReserves(int reserve)
        {
            m_UI.UpdatePlantReserves(reserve);
        }
    }

    // TODO:
    // Espaços fixos para plantar - 12 espaços
    // Plantas serão um botão com cooldown ilimitado - truque da UI
    // Cooldown radial ou de cima pra baixo com overlay preto e branco ou escurecido
    // Time to next wave como contagem regressiva
    // Remover available plants
    // Indicar quais são esses espaços
    // Clicar na planta faz ela sair da tela
}
