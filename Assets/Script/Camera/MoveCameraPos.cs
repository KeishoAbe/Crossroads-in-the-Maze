using UnityEngine;
using System.Collections;

public class MoveCameraPos : MonoBehaviour
{
    
    private Vector3 m_MoveDirection;
    private Vector3 m_CameraDirection = new Vector3(0, 1, 0);
    
    private float m_MoveSpeed = 3.0f;

    void Update()
    {
#if true
        CameraMoveDirection();
#endif

#if false
        CameraKeyControl();
#endif
        float rotationY = Input.GetAxis("Horizontal2");
        m_CameraDirection = new Vector3(0, rotationY, 0);
        transform.Rotate(m_CameraDirection.x, m_CameraDirection.y, m_CameraDirection.z);
    }

    private void CameraMoveDirection()
    {
        Vector3 forward = Camera.main.transform.TransformDirection(Vector3.forward);
        Vector3 right = Camera.main.transform.TransformDirection(Vector3.right);
        m_MoveDirection = Input.GetAxis("Horizontal") * right + Input.GetAxis("Vertical") * forward;
        m_MoveDirection *= m_MoveSpeed;
    }

    private void CameraKeyControl()
    {
        if (Input.GetKey(KeyCode.B))
        {
            m_CameraDirection.y = 1 * -1.0f;
        }
        else if (Input.GetKey(KeyCode.N))
        {
            m_CameraDirection.y = 1 * 1.0f;
        }
        else
        {
            m_CameraDirection.y = 0;
        }
    }

}
