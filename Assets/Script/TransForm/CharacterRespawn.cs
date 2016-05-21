using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class CharacterRespawn : MonoBehaviour
{
    [SerializeField]
    Vector3 respawnPos;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            transform.position = new Vector3(respawnPos.x, respawnPos.y, respawnPos.z);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        string layerName = LayerMask.LayerToName(col.gameObject.layer);
        if (layerName == "DestroyArea")
        {
            transform.position = new Vector3(respawnPos.x, respawnPos.y, respawnPos.z);
        }
    }
}