using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    public enum State {Ready, Empty, Reloading}  //���� ���¸� ǥ���ϴ� �� ����� Ÿ���� ����
    public State state { get; private set; }  //���� ���� ����
    public Transform fireTransform;  //ź���� �߻�� ��ġ
    public ParticleSystem muzzleFlashEffect;  //�ѱ� ȭ�� ȿ��
    public ParticleSystem shellEjectEffect;  //ź�� ���� ȿ��

    private LineRenderer bulletLineRenderer; //ź�� ������ �׸��� ���� ������

    private AudioSource gunAudioPlayer;  //�� �Ҹ� �����
    //public AudioClip shotClip;  //�߻� �Ҹ�
    //public AudioClip reloadClip;  //������ �Ҹ�
    public int ammoRemain;// = 100;  //���� ��ü ź��
    public int magCapacity;// = 25;  //źâ �뷮
    //public float damage;// = 25;  //���ݷ�

    public GunData gunData;//���� ���� ������

    private float fireDistance = 50f;  //�����Ÿ�
    public int magAmmo;  //���� źâ�� ���� �ִ� ź��
    //public float timeBetFire;// = 0.12f;  //ź�� �߻� ����
    public float reloadTime;// = 1.8f;  //������ �ҿ� �ð�
    private float lastFireTime;  //���� ���������� �߻��� ����

    private void Awake()
    {//����� ������Ʈ�� ���� ��������
        gunAudioPlayer = GetComponent<AudioSource>();
        bulletLineRenderer = GetComponent<LineRenderer>();
        bulletLineRenderer.positionCount = 2; //����� ���� �ΰ��� ����
        bulletLineRenderer.enabled = false;  //���η����� ��Ȱ��ȭ
    }

    private void OnEnable()
    {//�� ���� �ʱ�ȭ
        //��ü ���� ź�� ���� �ʱ�ȭ
        ammoRemain = gunData.startAmmoRemain;
        magAmmo = gunData.magCapacity; //���� źâ�� ���� ä���
        state = State.Ready; //���� ���� ���¸� ���� �� �غ� �� ���·� ����
        lastFireTime = 0;  //���������� ���� �� ������ �ʱ�ȭ

    }

    public void Fire()
    {//�߻� �õ�
        //���� ���°� �߻� ������ ����
        if(state == State.Ready && Time.time >= lastFireTime + gunData.timeBetFire)  // && ������ �� �߻� �������� timeBetFire �̻��� �ð��� ����
        {
            lastFireTime = Time.time;  //������ �� �߻� ���� ����
            Shot();  //���� �߻� ó�� ����
        }
    }
    
    private void Shot()
    {//���� �߻� ó��
        RaycastHit hit;  //����ĳ��Ʈ�� ���� �浹 ������ �����ϴ� �����̳�
        Vector3 hitPosition = Vector3.zero;  //ź���� ���� ���� ������ ����

        if(Physics.Raycast(fireTransform.position,fireTransform.forward,out hit, fireDistance))  //����ĳ��Ʈ(��������,����,�浹���������̳�,�����Ÿ�)
        {
            //���̰� � ��ü�� �浹�� ���

            IDamageble target = hit.collider.GetComponent<IDamageble>();//�浹�� �������κ��� IDamageable ������Ʈ �������� �õ�
                                    //�ݶ��̴��� ������Ʈ �����;ߵ�
            if(target != null)//�������κ��� IDamagealbe ������Ʈ�� �������� �� �����ߴٸ�
            {
                target.OnDamage(gunData.damage, hit.point, hit.normal);//������ OnDamage �Լ��� ������� ���濡 ����� �ֱ�
            }
            hitPosition = hit.point;  //���̰� �浹�� ��ġ ����,  �浹���� �ʾҾ ���η����������� ����
        }
        else
        {
            hitPosition = fireTransform.position + fireTransform.forward * fireDistance;//���̰� �ٸ� ��ü�� �浹���� �ʾҴٸ� ź���� �ִ� �����Ÿ����� ���ư��� ���� ��ġ�� �浹 ��ġ�� ���
        }
        StartCoroutine(ShotEffect(hitPosition));//�߻� ����Ʈ ��� ����

        magAmmo--;//���� ź�� ���� -1
        if (magAmmo <= 0)
        {
            state = State.Empty;//źâ�� ���� ź���� ���ٸ� ���� ���� ���¸� Empty�� ����
        }
    }

    private IEnumerator ShotEffect(Vector3 hitPosition)  //�߻� ����Ʈ�� �Ҹ��� ����ϰ� ź�� ������ �׸�
    {
        muzzleFlashEffect.Play(); //�ѱ� ȭ�� ȿ�� ���
        shellEjectEffect.Play();  //ź�� ���� ȿ�� ���
        gunAudioPlayer.PlayOneShot(gunData.shotClip);  //�Ѱ� �Ҹ� ���
        bulletLineRenderer.SetPosition(0, fireTransform.position);  //���� �������� �ѱ��� ��ġ
        bulletLineRenderer.SetPosition(1, hitPosition); //���� ������ �Է����� ���� �浹 ��ġ
        bulletLineRenderer.enabled = true;  //���� �������� Ȱ��ȭ�Ͽ� ź�� ������ �׸�
        yield return new WaitForSeconds(0.03f);  //0.03�� ���� ��� ó���� ���
        bulletLineRenderer.enabled = false;  //���� �������� ��Ȱ��ȭ�Ͽ� ź�� ������ ����
    }

    public bool Reload()
    {
        return false;     
    }

    private IEnumerator ReloadRoutine()  //���� ������ ó���� ����
    {
        state = State.Reloading;  //���� ���¸� ������ �� ���·� ��ȯ

        yield return new WaitForSeconds(gunData.reloadTime); //������ �ҿ� �ð���ŭ ó�� ����

        state = State.Ready; //���� ���� ���¸� �߻� �غ�� ���·� ����
    }
    
}
