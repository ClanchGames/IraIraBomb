using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static MainManager;

/// <summary>
/// 使い方
/// TouchAreaを作り、これをアタッチ
/// moveObjectに動かしたいオブジェクトを入れる
/// 
/// 移動制限を設定する
/// </summary>
public class DragMove : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    GameObject moveObject;

    Vector3 epos;
    Vector3 Pos;
    float distance;

    //移動制限
    float maxLeft = -440;
    float maxRight = 440;
    float maxTop = 850;
    float maxBottom = -850;

    private void Update()
    {
        moveObject = main.moveObject;
    }

    public void OnPointerDown(PointerEventData data)
    {
        if (moveObject == null) return;
        //Debug.Log("touch");
        if (Mathf.Approximately(Time.timeScale, 0f))
        {
            return;
        }
        Pos = moveObject.transform.position;
        epos = data.position;
    }
    public void OnDrag(PointerEventData data)
    {
        if (moveObject == null) return;
        // Debug.Log("drag");
        if (Mathf.Approximately(Time.timeScale, 0f))
        {
            return;
        }

        Vector3 prePos = moveObject.transform.position;
        Vector3 epos2 = data.position;
        Vector3 diff = epos2 - epos;
        diff.z = 0.0f;

        moveObject.transform.position = Pos + diff;
        moveObject.transform.position = new Vector3(moveObject.transform.position.x, moveObject.transform.position.y, prePos.z);
        var Recttransform = moveObject.GetComponent<RectTransform>();
        Vector2 localPosition = Recttransform.anchoredPosition;

        //移動制限をかける
        localPosition.x = Mathf.Clamp(localPosition.x, maxLeft, maxRight);
        localPosition.y = Mathf.Clamp(localPosition.y, maxBottom, maxTop);
        Recttransform.anchoredPosition = localPosition;

    }

}

