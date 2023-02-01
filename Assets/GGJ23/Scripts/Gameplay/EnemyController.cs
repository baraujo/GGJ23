using UnityEngine;
using UnityEngine.Splines;

namespace GGJ23.Gameplay
{
    public class EnemyController : MonoBehaviour
    {
        private SplineAnimate m_SplineAnimate;

        public SplineAnimate SplineRef
        {
            get { return m_SplineAnimate; }
            set { m_SplineAnimate = value; }               
        }

        private void Awake()
        {
            m_SplineAnimate = GetComponent<SplineAnimate>();
        }
    }
}