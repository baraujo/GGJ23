using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GGJ23
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private Image m_MeleeOverlay;
        [SerializeField] private Image m_RangedOverlay;
        [SerializeField] private TextMeshProUGUI m_WaveText;

        public void UpdatePlantCooldowns(float meleeFactor, float rangedFactor)
        {
            m_MeleeOverlay.fillAmount = 1 - meleeFactor;
            m_RangedOverlay.fillAmount = 1 - rangedFactor;
        }

        internal void UpdateNextWaveText(string text)
        {
            m_WaveText.text = text;
        }
    }
}
