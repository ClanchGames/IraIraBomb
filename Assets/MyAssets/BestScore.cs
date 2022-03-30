using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BestScore : MonoBehaviour
{
    TextMeshProUGUI text;
    int bestScore;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        bestScore = ES3.Load("best_score", defaultValue: 0);
    }

    // Update is called once per frame
    void Update()
    {
        text.text = MyMethod.TimeDisplay(bestScore);
    }
}
