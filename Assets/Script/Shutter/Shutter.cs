using UnityEngine;
using System.Collections;

public class Shutter : MonoBehaviour
{
    [SerializeField]
    GameObject switchObj;       //switchObjの参照

    [SerializeField]
    Vector3 openVelocity,       //開閉スピード
            closeVelocity;
    [SerializeField]
    private float openLength,   //開閉する大きさ
                  closeLength;

    [SerializeField]
    private float waiteTime;

    private bool isOpen = false,
                 isClose = false;

    private float openlength, closelength, openWaiteTime;

    void Start()
    {
        openVelocity = new Vector3(openVelocity.x / 60, openVelocity.y / 60, openVelocity.z / 60);
        closeVelocity = new Vector3(closeVelocity.x / 60, closeVelocity.y / 60, openVelocity.z / 60);

        SetValue(out openlength, out closelength, out openWaiteTime);
    }

    void Update()
    {
        OpenAndClosed();
    }

    private void OpenAndClosed()
    {
        if (switchObj.GetComponent<OnSwitch>().isOnSwitch && isOpen == false) { isOpen = true; }

        if (isOpen)
        {
            openlength--;
            transform.position += openVelocity;
        }
        if (openlength <= 0)
        {
            switchObj.GetComponent<OnSwitch>().isOnSwitch = false;
            isOpen = false;
            openWaiteTime--;
            if (openWaiteTime == 0) { isClose = true; }
        }
        if (isClose)
        {
            closelength--;
            transform.position -= closeVelocity;
        }
        if (closelength <= 0)
        {
            isClose = false;
            SetValue(out openlength, out closelength, out openWaiteTime);
            isOpen = false;
        }
    }

    private void SetValue(out float openlength, out float closelength, out float openWaiteTime)
    {
        openlength = openLength * 60;
        closelength = closeLength * 60;
        openWaiteTime = waiteTime * 60;
    }
}
