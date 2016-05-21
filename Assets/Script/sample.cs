using UnityEngine;
using System.Collections;

public class sample : MonoBehaviour
{

    private float vertical = 0;
    private float horizontal = 0;

    [SerializeField]
    private float speed = 3.0f;

    private Vector3 velocity,
                    direction;

    Transform playerPos;

    void Update()
    {
        CharacterMoveController();
    }

    private void CharacterMoveController()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");
        playerPos = gameObject.transform;

        velocity = new Vector3(horizontal, 0, vertical);
        //velocity = transform.TransformDirection(Vector3.forward);

        direction = new Vector3(horizontal, 0, vertical);

        playerPos.localPosition += velocity;
        transform.LookAt(transform.position + direction);
    }

}
