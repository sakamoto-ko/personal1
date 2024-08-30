using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManagerScript : MonoBehaviour
{
    static public float mapMax = 23.0f;
    static public float mapMin = -23.0f;
    void Start()
    {
    }

    void FixedUpdate()
    {
    }

    void Update()
    {
        if (PlayerScript.Hp <= 0)
        {
            SceneManager.LoadScene("Title Scene");
        }
    }

    public void GameOverStart()
    {
        //gameOverText.SetActive(true);
        //gameOverFlag = true;
    }

    //public bool IsGameOver()
    //{
    //    return gameOverFlag;
    //}

    public void Hit(Vector3 position)
    {
        //hitAudioSource.Play();
        //Instantiate(bombParticle, position, Quaternion.identity);
        //score += 1;
    }
}
