using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 4번
public class Coin : MonoBehaviour
{
    public int amount;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("PLAYER"))
        {
            GetComponent<MoveCoin>().MoveTo(ScoreManager.Instance.coinPos.position, 0.5f, () => {
                ScoreManager.Instance.IncreaseScore(amount);
                Destroy(gameObject);
            }); 
        }
    }
}
