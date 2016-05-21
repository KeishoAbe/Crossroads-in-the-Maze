using UnityEngine;
using System.Collections;

public class SwicthToEraseObject : MonoBehaviour
{

    public GameObject delateToObject;

    [SerializeField]
    private string hitToObjectLayerName;

    void Start()
    {

    }

    void Update()
    {

    }

    void OnTriggerEnter(Collider col)
    {
        string layerName = LayerMask.LayerToName(col.gameObject.layer);
        
        if (layerName == hitToObjectLayerName)
        {
            Destroy(delateToObject);
        }

    }

}
