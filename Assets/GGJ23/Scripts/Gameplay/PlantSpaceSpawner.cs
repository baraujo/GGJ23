using GGJ23.Gameplay;
using GGJ23.Input;
using UnityEngine;
using UnityEngine.UI;

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
        [SerializeField] private SpriteRenderer m_PlacementSprite;

        [Header("Parameters")]
        [SerializeField] private GameplayManagerRef m_GameplayManagerRef;

        private GameObject m_CurrentPlant = null;
        private float m_MeleeTimer = 0;
        private float m_RangedTimer = 0;

        private void Update()
        {
            m_MeleeTimer += Time.deltaTime;
            m_RangedTimer += Time.deltaTime;
        }

        public void PlantMelee()
        {
            if (m_CurrentPlant == null && m_GameplayManagerRef.Ref.CanPlaceMelee())
            {
                m_CurrentPlant = Instantiate(m_MeleePlantPrefab, transform.position, Quaternion.identity, transform);
                m_MeleeTimer = 0;
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
                m_RangedTimer = 0;
                m_PlacementSprite.enabled = false;
                m_GameplayManagerRef.Ref.TriggerRangedCooldown();
            }
        }
        
        public void RemovePlant()
        {
            Destroy(m_CurrentPlant);
            m_CurrentPlant = null;
            m_PlacementSprite.enabled = true;
            Debug.Log("Removing plant!");
        }

        public void ToggleUI()
        {
            m_RangedButton.SetActive(!m_RangedButton.activeSelf);
            m_MeleeButton.SetActive(!m_MeleeButton.activeSelf);
        }
    }
}