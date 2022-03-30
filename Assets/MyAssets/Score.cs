using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Score : MonoBehaviour
{

    TextMeshProUGUI text;

    public int time { get; set; }

    bool isStart;
    Coroutine coroutine;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        text.text = MyMethod.TimeDisplay(time);
    }

    // Update is called once per frame
    void Update()
    {
        if (MainManager.main.isStart)
        {
            if (!isStart)
            {
                isStart = true;
                coroutine = StartCoroutine(CountTime());
            }
        }

        if (MainManager.main.isOver)
        {
            isStart = false;
            StopCoroutine(coroutine);

            int bestScore = ES3.Load("best_score", defaultValue: 0);

            //new record
            if (time > bestScore)
            {
                ES3.Save<int>("best_score", time);
            }

        }
    }

    IEnumerator CountTime()
    {
        time = 0;
        while (true)
        {
            text.text = MyMethod.TimeDisplay(time);
            yield return new WaitForSeconds(1);
            time++;

        }
    }
}
