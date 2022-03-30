using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MainManager.main.moveObject = gameObject;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        /* if (collision.gameObject.tag == "Needle")
         {
             Destroy(gameObject);
         }*/
    }
    private void OnDestroy()
    {
        SE.se.PlayGameOverSE();
        MainManager.main.GameOver();
    }
}
