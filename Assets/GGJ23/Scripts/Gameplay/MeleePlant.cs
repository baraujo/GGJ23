using GGJ23.Gameplay;
using System.Collections;
using UnityEngine;

public class MeleePlant : MonoBehaviour
{
    [SerializeField] private float m_AttackInterval = 1f;
    [SerializeField] private Transform m_AreaCenter;
    [SerializeField] private Vector2 m_AreaSize = new Vector2(1f, 1f);
    [SerializeField] private SpriteRenderer m_MainSprite;
    [SerializeField] private GameObject m_Overlay;

    private bool m_IsRunning = true;
    private bool m_IsAttacking = false;
    private LayerMask m_Mask;
    public bool IsRunning
    {
        get { return m_IsRunning; } set { m_IsRunning = value; }
    }
   
    void Start()
    {
        m_Mask = LayerMask.GetMask("Enemy");
        StartCoroutine(Attack());
        StartCoroutine(FadeIn());
    }

    private IEnumerator Attack()
    {
        while (m_IsRunning)
        {
            yield return new WaitForSeconds(m_AttackInterval - 0.25f);
            m_IsAttacking = true;
            DoDamage();
            m_Overlay.SetActive(true);
            yield return new WaitForSeconds(0.25f);
            m_Overlay.SetActive(false);
            m_IsAttacking = false;
        }
    }

    private void DoDamage()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(m_AreaCenter.position, m_AreaSize, 0, m_Mask);
        foreach(Collider2D c in colliders)
        {
            c.GetComponent<HealthManager>().TakeHit();
        }
    }

    private IEnumerator FadeIn()
    {
        Color startMain = m_MainSprite.color;
        Color endMain = startMain;

        startMain.a = 0;
        m_MainSprite.color = startMain;

        float elapsed = 0;
        float duration = 0.25f;
        while (elapsed < duration)
        {
            var step = elapsed / duration;
            m_MainSprite.color = Color.Lerp(startMain, endMain, step);

            elapsed += Time.deltaTime;
            yield return null;
        }

        m_MainSprite.color = endMain;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (m_AreaCenter != null)
        {
            Gizmos.color = m_IsAttacking ? Color.red : Color.white;
            Gizmos.DrawWireCube(m_AreaCenter.position, m_AreaSize);
        }
    }
#endif
}
