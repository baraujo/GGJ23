using UnityEngine;

public class LimitedTimeSpan : MonoBehaviour
{
    [SerializeField] private float m_TimeSpan = 10.0f;

    private float m_Elapsed = 0;

    // Update is called once per frame
    void Update()
    {
        if(m_Elapsed > m_TimeSpan)
        {
            gameObject.SetActive(false);
        }

        m_Elapsed += Time.deltaTime;
    }
}
