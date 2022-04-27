using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ź���� �����ϴ� ������
public class AmmoPack : MonoBehaviour, IItem
{
    public int ammo = 30; //������ ź�� ��

    public void Use(GameObject target)
    {
        //���޹��� ���� ������Ʈ�κ��� PlayerShooter ������Ʈ �������� �õ�
        PlayerShooter playerShooter = target.GetComponent<PlayerShooter>();

        //PlayerShooter ������Ʈ�� ������ �� ������Ʈ�� �����ϸ�
        if(playerShooter != null && playerShooter.gun != null)
        {
            //���� ���� ź�� ���� ammo��ŭ ����
            playerShooter.gun.ammoRemain += ammo;
        }

        //���Ǿ����Ƿ� �ڽ��� �ı�
        Destroy(gameObject);
    }
}
