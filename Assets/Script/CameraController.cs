using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    CharacterController controller;
    private Vector3 moveDirection;
    private Vector3 cameraDirection = new Vector3(0,1,0);

    float moveSpeed = 3.0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        CameraMoveDirection();

        float rotationY = Input.GetAxis("Horizontal2");
        //float rotationX = Input.GetAxis("Vertical2");
        cameraDirection = new Vector3(0, rotationY, 0);
        transform.Rotate(cameraDirection.x, cameraDirection.y, cameraDirection.z);
        controller.Move(moveDirection * Time.deltaTime);
    }

    //カメラの向きによってオブジェクトを移動させる
    private void CameraMoveDirection()
    {
        Vector3 forward = Camera.main.transform.TransformDirection(Vector3.forward);
        Vector3 right = Camera.main.transform.TransformDirection(Vector3.right);
        moveDirection = Input.GetAxis("Horizontal") * right + Input.GetAxis("Vertical") * forward;
        moveDirection *= moveSpeed;
    }
}
