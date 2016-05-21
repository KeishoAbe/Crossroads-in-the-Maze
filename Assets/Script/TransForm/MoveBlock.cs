using UnityEngine;
using System.Collections;

public class MoveBlock : MonoBehaviour
{

    [SerializeField]
    Vector3 moveVelocity;

    [SerializeField]
    private bool move = false,
                 moveRepeat = false;

    float repeatPos;

    public float repeatSpeed;

    void Start()
    {
        
    }

    void Update()
    {
        repeatPos = Mathf.Sin(Time.frameCount * repeatSpeed);

        if (move == true) { Move(); }
        if(moveRepeat == true) { MoveRepeat(); }
    }

    void Move()
    {
        this.transform.position += new Vector3(moveVelocity.x,
                                               moveVelocity.y,
                                               moveVelocity.z);
    }

    void MoveRepeat()
    {
        this.transform.position += new Vector3(moveVelocity.x * repeatPos,
                                               moveVelocity.y * repeatPos,
                                               moveVelocity.z * repeatPos);
    }

}
