using GGJ23.Gameplay;
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
        [SerializeField] private Transform m_ClickableAreas;
        [SerializeField] private int m_InitialPlantReserve = 3;
        [SerializeField] private float m_NewPlantTimer = 5.0f;
        [SerializeField] private GameplayManagerRef m_GameplayManagerRef;

        private int m_CurrentPlantReserve;
        private float m_PlantTimerElapsed = 0;
        private Camera m_MainCamera = null;

        public PlantType CurrentType
        {
            get { return m_CurrentType; }
            set { m_CurrentType = value; }
        }

        private void Start()
        {
            m_MainCamera = Camera.main;
            m_CurrentPlantReserve = m_InitialPlantReserve;
            m_GameplayManagerRef.Ref.UpdatePlantReserves(m_CurrentPlantReserve);
        }

        private void Update()
        {
            if (m_Input.m_MouseClick.m_Pressed && m_CurrentPlantReserve > 0)
            {
                Vector3 plantPosition = m_MainCamera.ScreenToWorldPoint(m_Input.m_MousePosition);
                if (IsInsideAnyClickableArea(plantPosition))
                {
                    plantPosition.z = 2;
                    GameObject prefab = m_CurrentType == PlantType.Melee ? m_MeleePlantPrefab : m_RangedPlantPrefab;
                    var plant = Instantiate(prefab, plantPosition, Quaternion.identity, null);
                    m_CurrentPlantReserve--;
                    m_GameplayManagerRef.Ref.UpdatePlantReserves(m_CurrentPlantReserve);
                }
            }

            m_PlantTimerElapsed += Time.deltaTime;
            if(m_PlantTimerElapsed > m_NewPlantTimer)
            {
                m_CurrentPlantReserve++;
                m_PlantTimerElapsed = 0;
                m_GameplayManagerRef.Ref.UpdatePlantReserves(m_CurrentPlantReserve);
            }
        }

        private bool IsInsideAnyClickableArea(Vector3 plantPosition)
        {
            plantPosition.z = 5;
            bool found = false;
            foreach (Transform t in m_ClickableAreas)
            {
                if (t.gameObject.GetComponent<BoxCollider2D>().bounds.Contains(plantPosition))
                {
                    found = true;
                    break;
                }
            }
            return found;
        }
    }
}
