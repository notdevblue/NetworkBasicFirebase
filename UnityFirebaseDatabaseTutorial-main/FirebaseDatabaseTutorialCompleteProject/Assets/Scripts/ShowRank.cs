using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 3번
public class ShowRank : MonoBehaviour
{
    public Button btnRank;
    public Button btnRankClose;
    public GameObject rankPanel;
    public GameObject stagePanel;
    public Text rankText;

    private void Start()
    {
        btnRank.onClick.AddListener(() => {
            stagePanel.SetActive(false);
            rankPanel.SetActive(true);
            FirebaseManager.instance.LoadAllUser();
            foreach(string txt  in RankVO.Instance.GetRank())
            {
                rankText.text += txt + "\r\n";
            }
        });

        btnRankClose.onClick.AddListener(() => {
            rankPanel.SetActive(false);
            stagePanel.SetActive(true);
        });
    }
}
