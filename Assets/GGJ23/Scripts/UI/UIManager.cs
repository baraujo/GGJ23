using GGJ23.Gameplay;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GGJ23
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private Image m_MeleeOverlay;
        [SerializeField] private Image m_RangedOverlay;
        [SerializeField] private TextMeshProUGUI m_WaveText;
        [SerializeField] private GameObject m_OverlayCanvas;
        [SerializeField] private GameObject m_TitleCanvas;
        [SerializeField] private GameObject m_GameOverCanvas;
        [SerializeField] private GameObject m_GameWonCanvas;
        [SerializeField] private GameplayManagerRef m_GameplayManager;

        public void UpdatePlantCooldowns(float meleeFactor, float rangedFactor)
        {
            m_MeleeOverlay.fillAmount = 1 - meleeFactor;
            m_RangedOverlay.fillAmount = 1 - rangedFactor;
        }

        internal void UpdateNextWaveText(string text)
        {
            m_WaveText.text = text;
        }

        public void StartGame()
        {
            m_TitleCanvas.gameObject.SetActive(false);
            m_OverlayCanvas.SetActive(true);
            m_GameplayManager.Ref.StartGame();
        }

        public void GameOver()
        {
            m_GameOverCanvas.SetActive(true);
        }

        public void GameWon()
        {
            m_GameWonCanvas.SetActive(true);
        }

        public void ReloadGame()
        {
            SceneManager.LoadScene("SCN_Main");
        }
    }
}
