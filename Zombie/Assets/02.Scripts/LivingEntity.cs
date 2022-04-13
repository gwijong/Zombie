using System;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamageble
{
    //����ü�� ������ ���� ������Ʈ���� ���� ���븦 ����
    //ü��, ����� �޾Ƶ��̱�, ��� ���, ��� �̺�Ʈ�� ����
    public float startingHealth = 100f;//���� ü��
    public float health { get; protected set; }//���� ü��
    public bool dead { get; protected set; }//��� ����
    public event Action onDeath;//��� �� �ߵ��� �̺�Ʈ

    //����ü�� Ȱ��ȭ�� �� ���¸� ����
    protected virtual void OnEnable()
    {
        dead = false;//������� ���� ���·� ����
        health = startingHealth; //ü���� ���� ü������ �ʱ�ȭ
    }

    //������� �Դ� ���
    public virtual void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        health -= damage; //�������ŭ ü�� ����

        if(health <= 0 && !dead)//ü���� 0 ���� && ���� ���� �ʾҴٸ� ��� ó�� ����
        {
            Die();
        }
    }

    //ü���� ȸ���ϴ� ���
    public virtual void RestoreHealth(float newHealth) 
    {
        if (dead)
        {
            //�̹� ����� ��� ü���� ȸ���� �� ����
            return;
        }
        //ü�� �߰�
        health += newHealth;
    }

    // ��� ó��
    public virtual void Die()
    {
        //onDeath �̺�Ʈ�� ��ϵ� �޼��尡 �ִٸ� ����
        if(onDeath != null)
        {
            onDeath();
        }
        //��� ���¸� ������ ����
        dead = true;
    }


}
