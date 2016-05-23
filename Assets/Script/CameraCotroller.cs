using UnityEngine;
using System.Collections;

public class CameraCotroller : MonoBehaviour
{
    [SerializeField]
    private float m_Speed = 1;
    public GameObject m_CameraPos;
    private Vector3 m_Rotate;

    void Start()
    {
        m_Rotate = new Vector3(0, 0, 0);
    }

    void Update()
    {
        this.transform.position = m_CameraPos.transform.position;
        int horizontal = (int)Input.GetAxis("Horizontal2");
        m_Rotate = new Vector3(0, horizontal, 0);

        this.transform.Rotate(0, horizontal * m_Speed, 0);
    }
}
