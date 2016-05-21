using UnityEngine;
using System.Collections;

public class OnSwitchObject : MonoBehaviour
{
    [SerializeField]
    private GameObject derateObject;

    private bool objectState = true;

    void Start()
    {
        
    }

    void Update()
    {
        SetObject();
    }

    private void SetObject()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            objectState = !objectState;
        }
        derateObject.SetActive(objectState);
    }
}
