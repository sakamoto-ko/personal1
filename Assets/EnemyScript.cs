using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private GameObject player;
    static public Animator animator;

    static public int Hp = 1000;

    int Count = 0;
    bool isHit = false;
    int AtkCount = 150;
    static public bool isAtk = false;

    void Start()
    {
        player = GameObject.Find("Player");Å@
        transform.position = new Vector3(0, 0, 8);
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 2.0f)
        {
            animator.SetBool("isMove", false);
        }
        else
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                new Vector3(player.transform.position.x, 0, player.transform.position.z),
                8f * Time.deltaTime);

            transform.LookAt(player.transform);

            animator.SetBool("isMove", true);
        }

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, GameManagerScript.mapMin, GameManagerScript.mapMax), 0, Mathf.Clamp(transform.position.z, GameManagerScript.mapMin, GameManagerScript.mapMax));

        if (Hp <= 0)
        {
            Destroy(gameObject, 5);
        }
        if (isHit)
        {
            if (Count++ >= 60)
            {
                animator.SetBool("isHit", false);
                isHit = false;
                Count = 0;
            }
        }

        if (isAtk)
        {
            if (AtkCount++ >= 200)
            {
                animator.SetBool("isAttack", false);
                isAtk = false;
                PlayerScript.isInvincible = false;
                AtkCount = 0;
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            isHit = true;
            animator.SetBool("isHit", true);
        }

        if (other.gameObject.name == "Player")
        {
            if (isAtk)
            {
                // ñ≥ìGíÜÇÕèàóùÇµÇ»Ç¢
                if (PlayerScript.isInvincible)
                {
                    return;
                }

                PlayerScript.isInvincible = true;
                PlayerScript.Hp -= 20;

            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            if (!isAtk)
            {
                if (AtkCount++ >= 150)
                {
                    animator.SetBool("isAttack", true);
                    isAtk = true;
                    AtkCount = 0;
                }
            }
        }
    }
}
