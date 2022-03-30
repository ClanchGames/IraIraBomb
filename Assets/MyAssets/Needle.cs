using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Needle : MonoBehaviour
{


    float speed = -2f;
    bool isOver;

    // Start is called before the first frame update
    void Start()
    {
        transform.DOMoveY(-1800, 25);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, speed, 0);
        if (MainManager.main.isOver)
        {
            isOver = true;
        }

        if (isOver)
        {
            if (MainManager.main.isStart)
            {
                Destroy(gameObject);
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bomb")
        {
            Destroy(collision.gameObject);
        }
    }
}
