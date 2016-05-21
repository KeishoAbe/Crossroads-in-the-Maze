using UnityEngine;
using System.Collections;

public class TPSCamera : MonoBehaviour
{
    public float smooth = 3.0f;
    Transform player;

    [SerializeField]
    GameObject _player;

    Transform standardPos;
    Transform frontPos;
    Transform jumpPos;
    Transform topVewPos;

    bool quickSwitch = false;

    void Start()
    {
        standardPos = GameObject.Find("CamPos").transform;
       
        transform.position = standardPos.position;
        transform.forward = standardPos.forward;
    }

    void FixedUpdate()
    {
        setPositionViewCamera();

        if (Input.GetButton("Fire1") || _player.GetComponent<CharacterControl>()._cameratype == 0)
        {
           // standardPos = GameObject.Find("CamPos").transform;
        }
        else if (_player.GetComponent<CharacterControl>()._cameratype == 1)
        {
            standardPos = GameObject.Find("TopViewPos").transform;
        }
      
    }

    void setPositionViewCamera()
    {
        if (quickSwitch == false)
        {
            transform.position = Vector3.Lerp(transform.position, standardPos.position, Time.fixedDeltaTime * smooth);
            transform.forward = Vector3.Lerp(transform.forward, standardPos.forward, Time.fixedDeltaTime * smooth);
        }
        else
        {
            transform.position = standardPos.position;
            transform.forward = standardPos.forward;
            quickSwitch = false;
        }
    }

    void setPositionTopViewCamera()
    {
        if (quickSwitch == false)
        {
            transform.position = Vector3.Lerp(transform.position, topVewPos.position, Time.fixedDeltaTime * smooth);
            transform.forward = Vector3.Lerp(transform.forward, topVewPos.forward, Time.fixedDeltaTime * smooth);
        }
        else
        {
            transform.position = topVewPos.position;
            transform.forward = topVewPos.forward;
            quickSwitch = false;
        }
    }
    
  
}
