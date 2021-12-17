using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GoBackButton : MonoBehaviour
{
   private void Start() {
       GetComponent<Button>().onClick.AddListener(() => {
           SceneManager.LoadScene("SampleScene");
       });

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

   private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
   {
        FirebaseManager.instance.LoginButton();
        SceneManager.sceneLoaded -= OnSceneLoaded;
   }
}
