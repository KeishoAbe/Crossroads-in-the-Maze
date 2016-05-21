using UnityEngine;
using System.Collections;

public class MoveCameraPos : MonoBehaviour
{
    
    private Vector3 moveDirection;
    private Vector3 cameraDirection = new Vector3(0, 1, 0);

    float moveSpeed = 3.0f;

    void Start()
    {

    }

    void Update()
    {

#if true
        CameraMoveDirection();
#endif

#if false
        CameraKeyControl();
#endif

        float rotationY = Input.GetAxis("Horizontal2");
        cameraDirection = new Vector3(0, rotationY, 0);

        transform.Rotate(cameraDirection.x, cameraDirection.y, cameraDirection.z);
    }
    private void CameraMoveDirection()
    {
        Vector3 forward = Camera.main.transform.TransformDirection(Vector3.forward);
        Vector3 right = Camera.main.transform.TransformDirection(Vector3.right);
        moveDirection = Input.GetAxis("Horizontal") * right + Input.GetAxis("Vertical") * forward;
        moveDirection *= moveSpeed;
    }

    private void CameraKeyControl()
    {
        if (Input.GetKey(KeyCode.B))
        {
            cameraDirection.y = 1 * -1.0f;
        }
        else if (Input.GetKey(KeyCode.N))
        {
            cameraDirection.y = 1 * 1.0f;
        }
        else
        {
            cameraDirection.y = 0;
        }
    }

}
