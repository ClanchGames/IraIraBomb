using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Linq;
using System;
using System.Runtime.InteropServices;

public static class MyMethod
{
    public static GameObject GetChildWithName(GameObject obj, string name)
    {
        Transform trans = obj.transform;
        Transform childTrans = trans.Find(name);
        if (childTrans != null)
        {
            return childTrans.gameObject;
        }
        else
        {
            return null;
        }
    }
    public static GameObject GetChildWithTag(GameObject obj, string tag)
    {
        Transform trans = obj.transform;
        return obj;
    }
    public static GameObject GetParent(GameObject obj)
    {
        return obj.transform.parent.gameObject;
    }




    public static void FadeOut(Image image)
    {
        image.DOFade(0.1f, 0);
    }

    public static void FadeIn(Image image)
    {
        image.DOFade(1, 0);
    }
    public static Sequence UpDown(Transform transform)
    {
        Sequence seq;
        seq = DOTween.Sequence();
        seq.Append(transform.DOLocalMoveY(-1f, 0.5f).SetEase(Ease.Linear).SetRelative());
        seq.Append(transform.DOLocalMoveY(1f, 0.5f).SetEase(Ease.Linear).SetRelative());
        seq.SetLoops(-1);
        return seq;
    }

    public static void KillSequence(Sequence seq)
    {
        seq.Kill();
    }
    public static GameObject FindObject(string objname)
    {
        GameObject obj = GameObject.Find(objname);
        return obj;
    }
    public static T GetComponentInObject<T>(string objectName = "")
    {
        T c = default(T);
        if (objectName != "")
        {
            GameObject gameObject = GameObject.Find(objectName);
            if (gameObject == null)
            {
                Debug.LogError(objectName + " is not found");
            }
            else
            {
                c = gameObject.GetComponent<T>();
                if (c == null)
                {
                    Debug.LogError(nameof(T) + " is not found");
                }
            }
        }

        return c;
    }
    public static T MyGetComponent<T>(GameObject obj)
    {
        T c = default;

        c = obj.GetComponent<T>();
        if (c == null)
        {
            obj.AddComponent(typeof(T));
            c = obj.GetComponent<T>();
        }
        return c;
    }
    public static int[,] ReverseMap(int[,] map)
    {
        //int[,] reversemap = map;

        for (int i = 0; i <= map.GetLength(0) - 1; i++)
        {
            for (int j = 0; j <= map.GetLength(1) - 1; j++)
            {
                int row = map.GetLength(0) - 1;
                int column = map.GetLength(1) - 1;

                if (!(i > (row / 2)))
                {


                    int s1 = map[i, j];
                    int s2 = map[column - i, j];
                    //SetTile(mymain.UnitPrefabs[UnitPosition[i, j]], i, j);
                    map[i, j] = s2;
                    map[column - i, j] = s1;
                }
            }
        }

        return map;
    }

    public static string SetTag(string tagname, List<string> taglist)
    {
        if (taglist.Contains(tagname))
        {
            return tagname;
        }
        else
        {
            return null;
        }
    }


    public static T[] ToOneDimensional<T>(T[,] src)
    {
        int ymax = src.GetLength(0);
        int xmax = src.GetLength(1);
        int len = xmax * ymax;
        var dest = new T[len];

        for (int y = 0, i = 0; y < ymax; y++)
        {
            for (int x = 0; x < xmax; x++, i++)
            {
                dest[i] = src[y, x];
            }
        }
        return dest;
    }

    public static T[] ToOneDimensionalPrimitives<T>(T[,] src)
    {
        int ymax = src.GetLength(0);
        int xmax = src.GetLength(1);
        int len = xmax * ymax;
        var dest = new T[len];

        var size = Marshal.SizeOf(typeof(T));
        Buffer.BlockCopy(src, 0, dest, 0, len * size);
        return dest;
    }


    public static T[,] ToTowDimensional<T>(T[] src, int width, int heigth)
    {
        var dest = new T[heigth, width];
        int len = width * heigth;
        len = src.Length < len ? src.Length : len;
        for (int y = 0, i = 0; y < heigth; y++)
        {
            for (int x = 0; x < width; x++, i++)
            {
                if (i >= len)
                {
                    return dest;
                }
                dest[y, x] = src[i];
            }
        }

        return dest;
    }


    public static T[,] ToTowDimensionalPrimitives<T>(T[] src, int width, int heigth)
    {
        var dest = new T[heigth, width];
        int len = width * heigth;
        len = src.Length < len ? src.Length : len;

        var size = Marshal.SizeOf(typeof(T));
        Buffer.BlockCopy(src, 0, dest, 0, len * size);
        return dest;
    }

    public static float GetAngle(Vector2 start, Vector2 target)
    {
        Vector2 dt = target - start;
        float rad = Mathf.Atan2(dt.y, dt.x);
        float degree = rad * Mathf.Rad2Deg;
        return degree;
    }

    //リストの要素消去メソッド　もし要素がなければ実行しない。
    public static List<T> RemoveListElement<T>(List<T> list, T element)
    {
        if (list.Contains(element))
        {
            list.Remove(element);
        }
        return list;
    }

    /// <summary>
    /// サイコロ関数 1d100=>Dice(1,1,100)
    /// </summary>
    /// <param name="dice_num">振るサイコロの数</param>
    /// <param name="maxValue">サイコロの最大値</param>
    /// <returns></returns>
    public static int Dice(int dice_num, int minValue, int maxValue)
    {
        int value = 0;
        for (int i = 0; i < dice_num; i++)
        {
            int roll = UnityEngine.Random.Range(minValue, maxValue + 1);
            value += roll;
        }
        return value;
    }

    /// <summary>
    /// 文字から数字を抽出するメソッド(intのみ)
    /// </summary>
    /// <returns></returns>
    public static List<int> ExtractNumberInString(string text)
    {
        List<int> extractedNumList = new List<int>();
        string extractedString = null;
        int textLength = text.Length;
        for (int i = 0; i < textLength; i++)
        {
            if (char.IsNumber(text[i]))
            {
                extractedString += text[i];
                if (i == textLength - 1)
                {
                    extractedNumList.Add(int.Parse(extractedString));
                }
            }
            else
            {
                extractedNumList.Add(int.Parse(extractedString));
                extractedString = null;
            }
        }
        return extractedNumList;
    }

    public static void ResetRect(GameObject ui)
    {
        RectTransform rect = ui.GetComponent<RectTransform>();
        rect.localPosition = new Vector2(0, 0);
    }


    /// <summary>
    /// 秒を分、時に直して返すメソッド
    /// </summary>
    /// <param name="second">timeは秒で入力</param>
    public static string TimeDisplay(int second)
    {
        int s;
        int m;
        int h;
        h = second / 3600;
        second -= h * 3600;
        m = second / 60;
        second -= m * 60;
        s = second;

        string ht = "";
        string mt = "";
        string st = "";
        if (h == 0)
        {
            ht = "";
        }
        else
        {
            ht = h.ToString() + ":";
        }

        if (h == 0 && m == 0)
        {
            mt = "";
        }
        else
        {
            if (h != 0 && m < 10)
            {
                mt = "0" + m.ToString() + ":";
            }
            else
            {
                mt = m.ToString() + ":";
            }
        }

        if (m != 0 && s < 10)
        {
            st = "0" + s.ToString();
        }
        else
        {
            st = s.ToString();
        }
        return ht + mt + st;
    }
}

