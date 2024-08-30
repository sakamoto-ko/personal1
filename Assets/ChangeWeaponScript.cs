using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWeaponScript : MonoBehaviour
{
    public GameObject Character; //�L�����N�^�[�Q�[���I�u�W�F�N�g

    /// <summary>
    /// ����̎��
    /// </summary>
    public enum WeaponTypes : int
    {
        OHUDA_GREEN,
        OHUDA_BLUE,
        OHUDA_RED,
        NUM,
    }

    private int m_weaponNo = 0; //����ԍ�

    //����prefab�̃t�@�C���p�X
    private string[] m_weaponPath = new string[]
    {
        "ohuda_green",
        "ohuda_blue",
        "ohuda_red"
    };

    private float m_keyInterval = 0f; //�A���ŃL�[��������Ȃ��悤��

    private EquipmentWeaponScript m_characterEquipmentManager;

    /// <summary>
    /// ���̕����
    /// </summary>
    private void NextWeapon()
    {
        m_weaponNo++;
        if (m_weaponNo >= (int)WeaponTypes.NUM)
        {
            m_weaponNo = 0;
        }
        //�����ύX
        m_characterEquipmentManager.EquipWeapon(m_weaponPath[m_weaponNo]);
    }

    /// <summary>
    /// �O�̕����
    /// </summary>
    private void PrevWeapon()
    {
        m_weaponNo--;
        if (m_weaponNo < 0)
        {
            m_weaponNo = (int)WeaponTypes.NUM - 1;
        }
        //�����ύX
        m_characterEquipmentManager.EquipWeapon(m_weaponPath[m_weaponNo]);
    }

    void Start()
    {
        m_characterEquipmentManager = Character.GetComponent<EquipmentWeaponScript>();
    }

    void Update()
    {
        //�}�E�X�z�C�[���̒l���擾
        float wh = Input.GetAxis("Mouse ScrollWheel");

        //�L�[���A���ŉ����Ȃ��悤�ɐ���
        if (Time.time - m_keyInterval > 0.5f)
        {
            //�}�E�X�z�C�[�������X�N���[�����ꂽ�玟�̕����
            if (wh < 0.0f)
            {
                m_keyInterval = Time.time;
                wh = 0.0f;
                NextWeapon();
            }
            //�}�E�X�z�C�[������X�N���[�����ꂽ��O�̕����
            else if (wh > 0.0f)
            {
                m_keyInterval = Time.time;
                wh = 0.0f;
                PrevWeapon();
            }
        }
    }
}
