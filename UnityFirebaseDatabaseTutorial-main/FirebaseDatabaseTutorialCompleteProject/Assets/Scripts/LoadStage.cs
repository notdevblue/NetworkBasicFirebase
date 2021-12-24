using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 4번, 5번
public class LoadStage : MonoBehaviour
{
    [SerializeField] private Button stage1;
    [SerializeField] private Button stage2;
    [SerializeField] private Button stage3;
    [SerializeField] private Button stage4;


    void Start()
    {
        stage1.onClick.AddListener(() => {
            FirebaseManager.instance.StageButton(1);
        });
        stage2.onClick.AddListener(() => {
            FirebaseManager.instance.StageButton(2);
        });
        stage3.onClick.AddListener(() => {
            FirebaseManager.instance.StageButton(3);
        });
        stage3.onClick.AddListener(() => {
            FirebaseManager.instance.StageButton(4);
        });
    }


}
