using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 2번
public class GoalObject : MonoBehaviour
{
    float time = 0.0f;


    private void Update()
    {
        time += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("PLAYER"))
        {
            FirebaseManager.instance.SaveTimeToDB(time);
            Destroy(gameObject);
        }
    }

}
