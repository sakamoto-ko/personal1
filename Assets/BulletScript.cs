using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 5);
    }

    void Update()
    {

    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            EnemyScript.Hp -= 5;
            EnemyScript.animator.SetBool("isHit", true);
            Destroy(this.gameObject);
        }
        else
        {
            return;
        }
    }
}