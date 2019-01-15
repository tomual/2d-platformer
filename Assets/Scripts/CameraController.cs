using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    private bool onEdge = false;

    void Start()
    {

    }

    void LateUpdate()
    {
        if (!onEdge)
        {
            transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
        }
    }

    public void setOnEdge(bool onEdge)
    {
        this.onEdge = onEdge;
    }
}