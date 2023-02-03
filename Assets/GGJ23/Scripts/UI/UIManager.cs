using GGJ23.Player;
using TMPro;
using UnityEngine;

namespace GGJ23
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private PlantSpawner m_PlantSpawner;
        [SerializeField] TextMeshProUGUI m_CurrentTypeText;

        public void SelectRanged()
        {
            m_PlantSpawner.CurrentType = PlantSpawner.PlantType.Ranged;
            m_CurrentTypeText.text = "Current plant: Ranged";
        }

        public void SelectMelee()
        {
            m_PlantSpawner.CurrentType = PlantSpawner.PlantType.Melee;
            m_CurrentTypeText.text = "Current plant: Melee";
        }
    }
}
