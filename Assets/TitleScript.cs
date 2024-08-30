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
        //timer�̑����ŕ�����_�ł����鏈��
        timer++;
        if (timer % 100 > 50)
        {
            hitKey.SetActive(false);
        }
        else
        {
            hitKey.SetActive(true);
        }

        //�G���^�[�L�[����������Q�[���V�[����
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(nextScene);
        }
    }
}
