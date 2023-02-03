using GGJ23.Input;
using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace GGJ23.Player
{
    public class PlantSpawner : MonoBehaviour
    {
        public enum PlantType
        {
            Melee,
            Ranged
        }

        [SerializeField] private PlantType m_CurrentType;
        [SerializeField] private GameObject m_MeleePlantPrefab;
        [SerializeField] private GameObject m_RangedPlantPrefab;
        [SerializeField] private ControllerInput m_Input;
        [SerializeField] private BoxCollider2D m_ClickableArea;

        private Camera m_MainCamera = null;

        public PlantType CurrentType
        {
            get { return m_CurrentType; }
            set { m_CurrentType = value; }
        }

        private void Start()
        {
            m_MainCamera = Camera.main;
        }

        private void Update()
        {
            if (m_Input.m_MouseClick.m_Pressed)
            {
                Vector3 plantPosition = m_MainCamera.ScreenToWorldPoint(m_Input.m_MousePosition);
                if (IsInsideClickableArea(plantPosition))
                {
                    plantPosition.z = 2;
                    GameObject prefab = m_CurrentType == PlantType.Melee ? m_MeleePlantPrefab : m_RangedPlantPrefab;
                    var plant = Instantiate(prefab, plantPosition, Quaternion.identity, null);
                }
            }
        }

        private bool IsInsideClickableArea(Vector3 plantPosition)        
        {
            plantPosition.z = 5;
            return m_ClickableArea.bounds.Contains(plantPosition);
        }
    }
}
