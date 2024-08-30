using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScript : MonoBehaviour
{
    public string nextScene;

    public GameObject hitKey;
    private int timer = 0;

    void Start()
    {
        
    }

    void Update()
    {
        //timerの増減で文字を点滅させる処理
        timer++;
        if (timer % 100 > 50)
        {
            hitKey.SetActive(false);
        }
        else
        {
            hitKey.SetActive(true);
        }

        //エンターキーを押したらゲームシーンへ
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(nextScene);
        }
    }
}
