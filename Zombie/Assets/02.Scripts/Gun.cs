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
    //public int ammoRemain = 100;  //���� ��ü ź��
    //public int magCapacity = 25;  //źâ �뷮
    //public float damage = 25;  //���ݷ�

    public GunData gunData;//���� ���� ������

    private float fireDistance = 50f;  //�����Ÿ�
    public int magAmmo;  //���� źâ�� ���� �ִ� ź��
    //public float timeBetFire = 0.12f;  //ź�� �߻� ����
    //public float reloadTime = 1.8f;  //������ �ҿ� �ð�
    private float lastFireTime;  //���� ���������� �߻��� ����

    private void Awake()
    {//����� ������Ʈ�� ���� ��������

    }

    private void OnEnable()
    {//�� ���� �ʱ�ȭ
        
    }

    public void Fire()
    {//�߻� �õ�

    }
    
    private void Shot()
    {//���� �߻� ó��

    }

    private IEnumerator ShotEffect(Vector3 hitPosition)  //�߻� ����Ʈ�� �Ҹ��� ����ϰ� ź�� ������ �׸�
    {
        bulletLineRenderer.enabled = true;

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
