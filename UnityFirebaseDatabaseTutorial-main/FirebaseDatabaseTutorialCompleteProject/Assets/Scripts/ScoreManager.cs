using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 4번
public class ScoreManager : MonoBehaviour
{
    static public ScoreManager Instance { get; private set; }
    public Transform coinPos;
    public Text scoreText;

    private void Awake()
    {
        Instance = this;
    }

    public int Score { get; private set; } = 0;

    public void IncreaseScore(int amount)
    {
        Score += amount;
        scoreText.text = Score.ToString();
    }
}
