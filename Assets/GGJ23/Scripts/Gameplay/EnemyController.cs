using UnityEngine;
using UnityEngine.Splines;

namespace GGJ23.Gameplay
{
    public class EnemyController : MonoBehaviour
    {
        private SplineAnimate m_SplineAnimate;

        public SplineAnimate SplineAnimateRef
        {
            get { return m_SplineAnimate; }
            set { m_SplineAnimate = value; }               
        }

        private void Awake()
        {
            m_SplineAnimate = GetComponent<SplineAnimate>();
        }

        public void TakeDamage()
        {
            //Debug.Log($"{name} says: AAUGH");
        }

        public void Die()
        {
            //Debug.Log($"{name} died");
            Destroy(gameObject);
        }

        // TODO:
        // Melee attacks - use some variation of Physics.Overlap to apply damage on a frame
        // Projectiles - collect code from Bright Light Escape
        // Gameplay: add gameplay states: running, game over, game won
        // Title screen: just make a separated scene and go with it
    }
}