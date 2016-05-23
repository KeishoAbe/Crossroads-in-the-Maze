using UnityEngine;
using System.Collections;

public class TPSCamera : MonoBehaviour
{
    [SerializeField]
    private float m_Smooth = 3.0f;
    
    [SerializeField]
    GameObject m_Player;

    Transform m_StandardPos;
    Transform m_FrontPos;
    Transform m_JumpPos;
    Transform m_TopVewPos;

    private bool m_QuickSwitch = false;

    void Start()
    {
        m_StandardPos = GameObject.Find("CamPos").transform;
       
        transform.position = m_StandardPos.position;
        transform.forward = m_StandardPos.forward;
    }

    void FixedUpdate()
    {
        setPositionViewCamera();

        if (Input.GetButton("Fire1") || m_Player.GetComponent<CharacterControl>().m_CameraType == 0)
        {
           // standardPos = GameObject.Find("CamPos").transform;
        }
        else if (m_Player.GetComponent<CharacterControl>().m_CameraType == 1)
        {
            m_StandardPos = GameObject.Find("TopViewPos").transform;
        }
      
    }

    void setPositionViewCamera()
    {
        if (m_QuickSwitch == false)
        {
            transform.position = Vector3.Lerp(transform.position, m_StandardPos.position, Time.fixedDeltaTime * m_Smooth);
            transform.forward = Vector3.Lerp(transform.forward, m_StandardPos.forward, Time.fixedDeltaTime * m_Smooth);
        }
        else
        {
            transform.position = m_StandardPos.position;
            transform.forward = m_StandardPos.forward;
            m_QuickSwitch = false;
        }
    }

    void setPositionTopViewCamera()
    {
        if (m_QuickSwitch == false)
        {
            transform.position = Vector3.Lerp(transform.position, m_TopVewPos.position, Time.fixedDeltaTime * m_Smooth);
            transform.forward = Vector3.Lerp(transform.forward, m_TopVewPos.forward, Time.fixedDeltaTime * m_Smooth);
        }
        else
        {
            transform.position = m_TopVewPos.position;
            transform.forward = m_TopVewPos.forward;
            m_QuickSwitch = false;
        }
    }
    
  
}
