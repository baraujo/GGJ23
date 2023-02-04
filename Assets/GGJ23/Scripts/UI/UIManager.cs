using UnityEngine;
using UnityEngine.UI;

namespace GGJ23
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private Image m_MeleeOverlay;
        [SerializeField] private Image m_RangedOverlay;

        public void UpdatePlantCooldowns(float meleeFactor, float rangedFactor)
        {
            m_MeleeOverlay.fillAmount = 1 - meleeFactor;
            m_RangedOverlay.fillAmount = 1 - rangedFactor;
        }
    }
}
