using UnityEngine;
using System.Collections;

public class SwicthToEraseObject : MonoBehaviour
{
    public GameObject m_DelateToObject;

    [SerializeField]
    private string m_HitToObjectLayerName;

    void OnTriggerEnter(Collider col)
    {
        string layerName = LayerMask.LayerToName(col.gameObject.layer);
        
        if (layerName == m_HitToObjectLayerName)
        {
            Destroy(m_DelateToObject);
        }
    }
}
