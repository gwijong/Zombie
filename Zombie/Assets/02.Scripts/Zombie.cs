using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;   //AI ������̼� �ý��� ���� �ڵ� ��������

public class Zombie : LivingEntity
{
    public LayerMask whatIsTarget;  //���� ��� ���̾�

    private LivingEntity targetEntity;  // ���� ���
    private NavMeshAgent pathFinder;  //��� ��� AI ������Ʈ

    public ParticleSystem hitEffect;  //�ǰ� �� ����� ��ƼŬ ȿ��
    public AudioClip deathSound;  //��� �� ����� �Ҹ�
    public AudioClip hitSount;  //�ǰ� �� ����� �Ҹ�

    private Animator enemyAnimator;  //�ִϸ����� ������Ʈ
    private AudioSource enemyAudioPlayer;  //����� �ҽ� ������Ʈ
    private Renderer enemyRenderer;//������ ������Ʈ

    public float damaage = 20f;  //���ݷ�
    public float timeBetAttack = 0.5f;  //���� ����
    private float lastAttackTime; //������ ���� ����

    private bool hasTarget
    {
        get
        {
            //������ ����� �����ϰ�, ����� ������� �ʾҴٸ� true
            if(targetEntity != null && !targetEntity.dead)
            {
                return true;
            }
            //�׷��� �ʴٸ� false
            return false;
        }
    }

    private void Awake()
    {
        pathFinder = GetComponent<NavMeshAgent>();
        enemyAnimator = GetComponent<Animator>();
        enemyAudioPlayer = GetComponent<AudioSource>();

        enemyRenderer = GetComponentInChildren<Renderer>();
    }

    //�� AI�� �ʱ� ������ �����ϴ� �¾� �޼���
    public void Setup(float newHealth, float newDamage, float newSpeed, Color skinColor)
    {

    }

    private void Start()
    {
        //���� ������Ʈ Ȱ��ȭ�� ���ÿ� AI�� ���� ��ƾ ����
        StartCoroutine(UpdatePath());
    }

    private void Update()
    {
        //���� ����� ���� ���ο� ���� �ٸ� �ִϸ��̼� ���
        enemyAnimator.SetBool("HasTarget", hasTarget);
    }

    //�ֱ������� ������ ����� ��ġ�� ã�� ��� ����
    private IEnumerator UpdatePath()
    {
        //��� �ִ� ���� ���� ����
        while (!dead)
        {
            //0.25�� �ֱ�� ó�� �ݺ�
            yield return new WaitForSeconds(0.25f);
        }
    }

    //������� �Ծ��� �� ������ ó��
    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        base.OnDamage(damage, hitPoint, hitNormal);
    }

    //��� ó��
    public override void Die()
    {
        base.Die();
    }

    private void OnTriggerStay(Collider other)
    {
        //Ʈ���� �浹�� ���� ���� ������Ʈ�� ���� ����̶�� ���� ����
    }
}
