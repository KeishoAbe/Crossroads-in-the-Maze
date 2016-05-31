using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class CharacterRespawn : MonoBehaviour
{
    [SerializeField]
    Vector3 m_RespawnPos;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            transform.position = new Vector3(m_RespawnPos.x, m_RespawnPos.y, m_RespawnPos.z);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        string layerName = LayerMask.LayerToName(col.gameObject.layer);
        if (layerName == "DestroyArea")
        {
            transform.position = new Vector3(m_RespawnPos.x, m_RespawnPos.y, m_RespawnPos.z);
        }
    }
}