using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Assertions.Comparers;
using UnityEngine.Rendering;

public class PlayerScript : MonoBehaviour
{
    public Rigidbody rb;

    //アニメーターコントローラー
    public Animator animator;

    public GameObject gameManager;
    public GameObject ShotPoint;
    public GameObject bullet;

    private GameManagerScript gameManagerScript;

    //体力
    static public int Hp = 100;

    // 無敵時間
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
        //移動速度定数
        float moveSpeed = 5.0f;

        //回転速度定数
        float rotateSpeed = 90.0f;

        //移動
        //奥
        if (isFront)
        {
            rb.velocity = transform.forward * moveSpeed;
            animator.SetBool("dush", true);
        }
        //手前
        else if (isBack)
        {
            rb.velocity = -transform.forward * moveSpeed;
            animator.SetBool("dush", true);
        }
        else
        {
            rb.velocity = Vector3.zero;
        }

        //回転
        //右
        if (isRight)
        {
            transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime * rotateSpeed, Space.World);
            animator.SetBool("dush", true);
        }
        //左
        else if (isLeft)
        {
            transform.Rotate(new Vector3(0, -1, 0) * Time.deltaTime * rotateSpeed, Space.World);
            animator.SetBool("dush", true);
        }

        //移動制限
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

        //x軸
        //右
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
        //左
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

        //z軸
        //奥
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
        //手前
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

        //弾発射
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
