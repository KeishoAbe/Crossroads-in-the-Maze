using UnityEngine;
using System.Collections;

public class OnSwitch : MonoBehaviour
{
    public bool canEnter,
                canOnSwitch = true,
                isOnSwitch = false;

    [SerializeField]
    private float notInputReceptionTime;

    float notInputTime;

    void Start()
    {
        Setup();
    }

    private void Setup()
    {
        notInputTime = notInputReceptionTime * 60;
    }

    void Update()
    {
        if (canEnter)
        {
            if (Input.GetKeyDown(KeyCode.C) && canOnSwitch)
            {
                canOnSwitch = false;
                isOnSwitch = true;
            }
        }
        if (!canOnSwitch)
        {
            notInputTime--;
        }
        if (notInputTime == 0)
        {
            canOnSwitch = true;
            Setup();
        }
    }

    void OnTriggerEnter(Collider col)
    {
        string layerName = LayerMask.LayerToName(col.gameObject.layer);
        if (layerName == "Player") { canEnter = true; }
    }

    void OnTriggerExit(Collider col)
    {
        string layerName = LayerMask.LayerToName(col.gameObject.layer);
        if (layerName == "Player") { canEnter = false; }
    }
}