using GGJ23.Gameplay;
using UnityEngine;

namespace GGJ23.Player
{
    public class PlantSpaceSpawner : MonoBehaviour
    {
        [Header("UI elements")]
        [SerializeField] private GameObject m_MeleePlantPrefab;
        [SerializeField] private GameObject m_RangedPlantPrefab;
        [SerializeField] private GameObject m_UICanvas;
        [SerializeField] private GameObject m_MeleeButton;
        [SerializeField] private GameObject m_RangedButton;
        [SerializeField] private GameObject m_RemovePlantButton;
        [SerializeField] private SpriteRenderer m_PlacementSprite;

        [Header("Parameters")]
        [SerializeField] private GameplayManagerRef m_GameplayManagerRef;

        private GameObject m_CurrentPlant = null;

        public void PlantMelee()
        {
            if (m_CurrentPlant == null && m_GameplayManagerRef.Ref.CanPlaceMelee())
            {
                m_CurrentPlant = Instantiate(m_MeleePlantPrefab, transform.position, Quaternion.identity, transform);
                ToggleUI();
                m_PlacementSprite.enabled = false;
                m_GameplayManagerRef.Ref.TriggerMeleeCooldown();
            }
        }

        public void PlantRanged()
        {
            if (m_CurrentPlant == null && m_GameplayManagerRef.Ref.CanPlaceRanged())
            {                
                m_CurrentPlant = Instantiate(m_RangedPlantPrefab, transform.position, Quaternion.identity, transform);
                ToggleUI();
                m_PlacementSprite.enabled = false;
                m_GameplayManagerRef.Ref.TriggerRangedCooldown();
            }
        }
        
        public void RemovePlant()
        {
            if (m_CurrentPlant != null)
            {
                Destroy(m_CurrentPlant);
                m_CurrentPlant = null;
                m_PlacementSprite.enabled = true;
            }
        }
        
        public void ToggleUI()
        {
            m_RemovePlantButton.SetActive(!m_RemovePlantButton.activeSelf);
            m_RangedButton.SetActive(!m_RangedButton.activeSelf);
            m_MeleeButton.SetActive(!m_MeleeButton.activeSelf);            
        }
    }
}