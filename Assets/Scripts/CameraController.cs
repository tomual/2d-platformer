using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    private bool disabled = false;

    void Start()
    {

    }

    void LateUpdate()
    {
        if (!disabled)
        {
            transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
        }
    }

    public void setCameraStop(bool disabled)
    {
        this.disabled = disabled;
    }
}