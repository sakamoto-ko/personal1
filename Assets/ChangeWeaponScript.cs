using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWeaponScript : MonoBehaviour
{
    public GameObject Character; //キャラクターゲームオブジェクト

    /// <summary>
    /// 武器の種類
    /// </summary>
    public enum WeaponTypes : int
    {
        OHUDA_GREEN,
        OHUDA_BLUE,
        OHUDA_RED,
        NUM,
    }

    private int m_weaponNo = 0; //武器番号

    //武器prefabのファイルパス
    private string[] m_weaponPath = new string[]
    {
        "ohuda_green",
        "ohuda_blue",
        "ohuda_red"
    };

    private float m_keyInterval = 0f; //連続でキーが押されないように

    private EquipmentWeaponScript m_characterEquipmentManager;

    /// <summary>
    /// 次の武器へ
    /// </summary>
    private void NextWeapon()
    {
        m_weaponNo++;
        if (m_weaponNo >= (int)WeaponTypes.NUM)
        {
            m_weaponNo = 0;
        }
        //武器を変更
        m_characterEquipmentManager.EquipWeapon(m_weaponPath[m_weaponNo]);
    }

    /// <summary>
    /// 前の武器へ
    /// </summary>
    private void PrevWeapon()
    {
        m_weaponNo--;
        if (m_weaponNo < 0)
        {
            m_weaponNo = (int)WeaponTypes.NUM - 1;
        }
        //武器を変更
        m_characterEquipmentManager.EquipWeapon(m_weaponPath[m_weaponNo]);
    }

    void Start()
    {
        m_characterEquipmentManager = Character.GetComponent<EquipmentWeaponScript>();
    }

    void Update()
    {
        //マウスホイールの値を取得
        float wh = Input.GetAxis("Mouse ScrollWheel");

        //キーが連続で押せないように制御
        if (Time.time - m_keyInterval > 0.5f)
        {
            //マウスホイールが下スクロールされたら次の武器へ
            if (wh < 0.0f)
            {
                m_keyInterval = Time.time;
                wh = 0.0f;
                NextWeapon();
            }
            //マウスホイールが上スクロールされたら前の武器へ
            else if (wh > 0.0f)
            {
                m_keyInterval = Time.time;
                wh = 0.0f;
                PrevWeapon();
            }
        }
    }
}
