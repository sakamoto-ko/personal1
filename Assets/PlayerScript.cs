using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Assertions.Comparers;
using UnityEngine.Rendering;

public class PlayerScript : MonoBehaviour
{
    public Rigidbody rb;

    //�A�j���[�^�[�R���g���[���[
    public Animator animator;

    public GameObject gameManager;
    public GameObject ShotPoint;
    public GameObject bullet;

    private GameManagerScript gameManagerScript;

    //�̗�
    static public int Hp = 100;

    // ���G����
    static public bool isInvincible;

    bool isLeft = false, isRight = false, isFront = false, isBack = false, isAtk = false;
    int bulletTimer = 0;

    void Start()
    {
        gameManagerScript = gameManager.GetComponent<GameManagerScript>();

        ShotPoint = transform.GetChild(0).gameObject;
        Hp = 100;
        bulletTimer = 0;
    }

    void FixedUpdate()
    {
        //�ړ����x�萔
        float moveSpeed = 5.0f;

        //��]���x�萔
        float rotateSpeed = 90.0f;

        //�ړ�
        //��
        if (isFront)
        {
            rb.velocity = transform.forward * moveSpeed;
            animator.SetBool("dush", true);
        }
        //��O
        else if (isBack)
        {
            rb.velocity = -transform.forward * moveSpeed;
            animator.SetBool("dush", true);
        }
        else
        {
            rb.velocity = Vector3.zero;
        }

        //��]
        //�E
        if (isRight)
        {
            transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime * rotateSpeed, Space.World);
            animator.SetBool("dush", true);
        }
        //��
        else if (isLeft)
        {
            transform.Rotate(new Vector3(0, -1, 0) * Time.deltaTime * rotateSpeed, Space.World);
            animator.SetBool("dush", true);
        }

        //�ړ�����
        rb.position = new Vector3(Mathf.Clamp(rb.position.x, GameManagerScript.mapMin, GameManagerScript.mapMax), 0, Mathf.Clamp(rb.position.z, GameManagerScript.mapMin, GameManagerScript.mapMax));
    }

    void Update()
    {
        PlayerInput();

        ShotUpdate();

        if (!isRight && !isLeft && !isFront && !isBack)
        {
            animator.SetBool("dush", false);
        }
    }

    void PlayerInput()
    {
        float holStick = Input.GetAxis("Horizontal");
        float verStick = Input.GetAxis("Vertical");

        //x��
        //�E
        if (Input.GetKey(KeyCode.D) ||
            Input.GetKey(KeyCode.RightArrow)
            || holStick > 50
            )
        {
            if (transform.position.x < GameManagerScript.mapMax)
            {
                isRight = true;
                isLeft = false;
            }
        }
        //��
        else if (Input.GetKey(KeyCode.A) ||
            Input.GetKey(KeyCode.LeftArrow)
            || holStick < -50
            )
        {
            if (transform.position.x > GameManagerScript.mapMin)
            {
                isRight = false;
                isLeft = true;
            }
        }
        else
        {
            isRight = false;
            isLeft = false;
        }

        //z��
        //��
        if (Input.GetKey(KeyCode.W) ||
            Input.GetKey(KeyCode.UpArrow)
            || verStick > 50
            )
        {
            if (transform.position.z < GameManagerScript.mapMax)
            {
                isFront = true;
                isBack = false;
            }
        }
        //��O
        else if (Input.GetKey(KeyCode.S) ||
            Input.GetKey(KeyCode.DownArrow)
            || verStick < -50
            )
        {
            if (transform.position.z > GameManagerScript.mapMin)
            {
                isFront = false;
                isBack = true;
            }
        }
        else
        {
            isFront = false;
            isBack = false;
        }

        //�e����
        if (--bulletTimer == 0)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                isAtk = true;
                bulletTimer = 20;
            }

            bulletTimer = 1;
        }
    }

    void ShotUpdate()
    {
        if (isAtk)
        {
            GameObject ball = (GameObject)Instantiate(bullet, ShotPoint.transform.position, Quaternion.identity);
            Rigidbody ballRigidbody = ball.GetComponent<Rigidbody>();
            ballRigidbody.AddForce(transform.forward * 1000);
        }
        else
        {
            EnemyScript.animator.SetBool("isHit", false);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (isAtk)
            {
                EnemyScript.Hp -= 10;
                EnemyScript.animator.SetBool("isHit", true);
            }
        }
    }
    void AlertObservers()
    {

    }
}
