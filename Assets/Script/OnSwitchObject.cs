using UnityEngine;
using System.Collections;

public class OnSwitchObject : MonoBehaviour
{
    [SerializeField]
    private GameObject m_DerateObject;

    private bool m_ObjectState = true;

    void Start()
    {
        
    }

    void Update()
    {
        SetObject();
    }

    private void SetObject()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            m_ObjectState = !m_ObjectState;
        }
        m_DerateObject.SetActive(m_ObjectState);
    }
}
