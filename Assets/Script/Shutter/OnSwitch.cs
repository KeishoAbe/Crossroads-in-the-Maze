using UnityEngine;
using System.Collections;

public class OnSwitch : MonoBehaviour
{
    public bool m_CanEnter,
                m_CanOnSwitch = true,
                m_IsOnSwitch = false;

    [SerializeField]
    private float m_NotInputReceptionTime;

    private float m_NotInputTime;

    void Start()
    {
        Setup();
    }

    private void Setup()
    {
         m_NotInputTime = m_NotInputReceptionTime * 60;
    }

    void Update()
    {
        if (m_CanEnter)
        {
            if (Input.GetKeyDown(KeyCode.C) && m_CanOnSwitch)
            {
                m_CanOnSwitch = false;
                m_IsOnSwitch = true;
            }
        }
        if (!m_CanOnSwitch)
        {
            m_NotInputTime--;
        }
        if (m_NotInputTime == 0)
        {
            m_CanOnSwitch = true;
            Setup();
        }
    }

    void OnTriggerEnter(Collider col)
    {
        string layerName = LayerMask.LayerToName(col.gameObject.layer);
        if (layerName == "Player") { m_CanEnter = true; }
    }

    void OnTriggerExit(Collider col)
    {
        string layerName = LayerMask.LayerToName(col.gameObject.layer);
        if (layerName == "Player") { m_CanEnter = false; }
    }
}