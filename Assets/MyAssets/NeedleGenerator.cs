using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class NeedleGenerator : MonoBehaviour
{
    [SerializeField] GameObject NeedlePrefab;
    [SerializeField] GameObject NeedleArea;

    [SerializeField] GameObject BombPrefab;
    [SerializeField] GameObject BombArea;

    bool isfirst = true;
    float interval = 1.75f;

    float posY = 850;
    float pos1 = -440;
    float pos2 = -220;
    float pos3 = 0;
    float pos4 = 220;
    float pos5 = 440;

    //ãÛÇØÇÈå¬êî
    int emptySlot = 1;

    List<int> SlotNumber = new List<int>();

    Coroutine generator;

    bool isStart;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (MainManager.main.isStart)
        {
            if (!isStart)
            {
                isStart = true;
                GenerateBomb();
                generator = StartCoroutine(Generator());
            }
        }
        if (MainManager.main.isOver)
        {
            isStart = false;
            StopCoroutine(generator);
        }
    }


    IEnumerator Generator()
    {
        while (true)
        {
            GenerateNeedle();
            if (isfirst)
            {
                isfirst = false;
                yield return new WaitForSeconds(interval);
            }
            else
            {
                yield return new WaitForSeconds(interval);
            }
        }

    }

    void GenerateBomb()
    {
        GameObject bomb = Instantiate(BombPrefab);
        bomb.name = "Bomb";
        bomb.transform.SetParent(BombArea.transform);
        RectTransform bombRect = bomb.GetComponent<RectTransform>();
        bombRect.anchoredPosition = new Vector2(0, -600);
    }

    //5å¬ÉZÉbÉgÇ≈ê∂ê¨Ç∑ÇÈ
    void GenerateNeedle()
    {
        SlotNumber.Clear();
        for (int i = 1; i <= 5; i++)
        {
            SlotNumber.Add(i);
        }
        ChooseSlot();
        for (int i = 0; i < SlotNumber.Count; i++)
        {

            GameObject needle = Instantiate(NeedlePrefab);

            needle.name = "Needle";
            needle.transform.SetParent(NeedleArea.transform);

            RectTransform needleRect = needle.GetComponent<RectTransform>();
            switch (SlotNumber[i])
            {
                case 1:
                    needleRect.anchoredPosition = new Vector2(pos1, posY);
                    break;
                case 2:
                    needleRect.anchoredPosition = new Vector2(pos2, posY);
                    break;
                case 3:
                    needleRect.anchoredPosition = new Vector2(pos3, posY);
                    break;
                case 4:
                    needleRect.anchoredPosition = new Vector2(pos4, posY);
                    break;
                case 5:
                    needleRect.anchoredPosition = new Vector2(pos5, posY);
                    break;
            }


        }

    }

    void ChooseSlot()
    {
        for (int i = 0; i < emptySlot; i++)
        {
            int n = Random.Range(0, SlotNumber.Count);
            SlotNumber.RemoveAt(n);
        }


    }
}