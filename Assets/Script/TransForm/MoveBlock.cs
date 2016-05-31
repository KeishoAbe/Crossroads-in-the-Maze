using UnityEngine;
using System.Collections;

public class MoveBlock : MonoBehaviour
{

    [SerializeField]
    Vector3 m_MoveVelocity;

    [SerializeField]
    private bool m_Move = false,
                 m_MoveRepeat = false;

    private float m_RepeatPos;

    public float m_RepeatSpeed;

    void Start()
    {

    }

    void Update()
    {
        m_RepeatPos = Mathf.Sin(Time.frameCount * m_RepeatSpeed);

        if (m_Move == true) { Move(); }
        if (m_MoveRepeat == true) { MoveRepeat(); }
    }

    void Move()
    {
        this.transform.position += new Vector3(m_MoveVelocity.x,
                                               m_MoveVelocity.y,
                                               m_MoveVelocity.z);
    }

    void MoveRepeat()
    {
        this.transform.position += new Vector3(m_MoveVelocity.x * m_RepeatPos,
                                               m_MoveVelocity.y * m_RepeatPos,
                                               m_MoveVelocity.z * m_RepeatPos);
    }

}
