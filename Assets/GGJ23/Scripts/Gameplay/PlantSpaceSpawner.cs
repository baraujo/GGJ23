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
        [SerializeField] private Image m_MeleeOverlayImage; 
        [SerializeField] private Image m_RangedOverlayImage;
        [SerializeField] private GameObject m_MeleeButton;
        [SerializeField] private GameObject m_RangedButton;
        [SerializeField] private SpriteRenderer m_PlacementSprite;

        [Header("Parameters")]
        [SerializeField] private float m_NewMeleeTimer = 5.0f;
        [SerializeField] private float m_NewRangedTimer = 5.0f;
        [SerializeField] private GameplayManagerRef m_GameplayManagerRef;

        private GameObject m_CurrentPlant = null;
        private float m_MeleeTimer = 0;
        private float m_RangedTimer = 0;

        private void Start()
        {
            m_MeleeTimer = m_NewMeleeTimer;
            m_RangedTimer = m_NewRangedTimer;
        }

        private void Update()
        {
            m_MeleeTimer += Time.deltaTime;
            m_RangedTimer += Time.deltaTime;

            m_MeleeOverlayImage.fillAmount = 1 - Mathf.Clamp01(m_MeleeTimer / m_NewMeleeTimer);
            m_RangedOverlayImage.fillAmount = 1 - Mathf.Clamp01(m_RangedTimer / m_NewRangedTimer);
        }

        public void PlantMelee()
        {
            Debug.Log("Placing melee!");
            if (m_CurrentPlant == null)
            {
                m_CurrentPlant = Instantiate(m_MeleePlantPrefab, transform.position, Quaternion.identity, transform);
                ToggleUI();
                m_PlacementSprite.enabled = false;
            }
        }

        public void PlantRanged()
        {
            Debug.Log("Placing ranged!");
            if (m_CurrentPlant == null)
            {                
                m_CurrentPlant = Instantiate(m_RangedPlantPrefab, transform.position, Quaternion.identity, transform);
                ToggleUI();
                m_PlacementSprite.enabled = false;
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