using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCoin : MonoBehaviour
{
    public void MoveTo(Vector2 pos, float time, Action callback = null)
    {
        StartCoroutine(Move(pos, time, callback));
    }

    IEnumerator Move(Vector2 pos, float time, Action callback)
    {
        float elapsed= 0.0f;

        Vector3 step = ((Vector3)pos - transform.position) / time; // 1 초당 움직여야 함

        while(elapsed < time) {
            elapsed += Time.deltaTime;
            transform.position += step * Time.deltaTime;
            yield return null;
        }

        callback();
    }

    IEnumerator MoveSin(Vector2 pos, float time, Action callback)
    {
        Vector2 basePos = transform.position;
        float degree = 0.0f;
        float halfPI = Mathf.PI / 2.0f;
        float addAmount = 1.0f / (halfPI / time); // = (90 / time) by one

        while (degree <= 1.0f)
        {
            degree += addAmount * Time.deltaTime;
            transform.position = Vector2.Lerp(basePos, pos, Mathf.Sin(degree));
            // transform.position = Vector2.Lerp()
            // elapsed += Time.deltaTime;
            // transform.position += step * Time.deltaTime;
            yield return null;
        }

        callback();
    }
}
