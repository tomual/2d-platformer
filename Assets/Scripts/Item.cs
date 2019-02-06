using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    float floatSpan = 2.0f;
    float speed = 1.0f;
    float startY;

    private GameObject itemScreen;
    private bool startFadeIn = false;
    private float fadeInTime;
    private bool fadeInInProgress = false;
    private bool fadeOutInProgress = false;
    private float itemScreenAlpha = 0;

    public GameObject player;

    void Start()
    {
        itemScreen = GameObject.FindGameObjectWithTag("ItemScreen");
        startY = transform.position.y;
    }

    void Update()
    {
        var pos = transform.position;
        pos.y = startY + Mathf.Sin(Time.time * speed) * floatSpan / 15f;
        transform.position = pos;

        if (fadeInInProgress)
        {
            Time.timeScale = 0;
            itemScreenAlpha += 0.05f;
            itemScreen.GetComponent<CanvasGroup>().alpha = itemScreenAlpha;
            if (itemScreenAlpha >= 1)
            {
                fadeInInProgress = false;
            }
        }

        if (fadeOutInProgress)
        {
            Time.timeScale = 1;
            itemScreenAlpha -= 0.1f;
            itemScreen.GetComponent<CanvasGroup>().alpha = itemScreenAlpha;
            if (itemScreenAlpha <= 0)
            {
                fadeOutInProgress = false;
                Destroy(gameObject);
                Time.timeScale = 1;
            }
        }

        if (Input.GetButtonDown("Fire1"))
        {
            if (System.Math.Floor(itemScreenAlpha) == 1)
            {
                Time.timeScale = 1;
                fadeOutInProgress = true;
                PlayerControl playerControl = player.GetComponent<PlayerControl>();
                player.GetComponent<Animator>().runtimeAnimatorController = playerControl.animatorWithPants;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            Debug.Log("Pick up that item");
            fadeInTime = Time.time;
            fadeInInProgress = true;
        }
    }
}
