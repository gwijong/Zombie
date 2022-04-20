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
    public AudioClip hitSound;  //�ǰ� �� ����� �Ҹ�

    private Animator enemyAnimator;  //�ִϸ����� ������Ʈ
    private AudioSource enemyAudioPlayer;  //����� �ҽ� ������Ʈ
    private Renderer enemyRenderer;//������ ������Ʈ

    public float damage = 20f;  //���ݷ�
    public float timeBetAttack = 0.25f;  //���� ����
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
    public void Setup(ZombieData zombieData)
    {
        //ü�� ����
        startingHealth = zombieData.health;
        health = zombieData.health;
        //���ݷ�
        damage = zombieData.damage;
        //����޽� ������Ʈ�� �̵� �ӵ� ����
        pathFinder.speed = zombieData.speed;
        enemyRenderer.material.color = zombieData.skinColor;
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
            if (hasTarget)
            {
                //���� ��� ����: ��θ� �����ϰ� AI �̵��� ��� ����
                pathFinder.isStopped = false;
                pathFinder.SetDestination(targetEntity.transform.position);
            }
            else
            {
                //���� ��� ����: AI �̵� ����
                pathFinder.isStopped = true;

                //20������ �������� ���� ������ ���� �׷��� �� ���� ��ġ�� ��� �ݶ��̴��� ������
                //�� whatIsTarget ���̾ ���� �ݶ��̴��� ���������� ���͸�
                Collider[] colliders = Physics.OverlapSphere(transform.position, 20f, whatIsTarget);
                
                //��� �ݶ��̴��� ��ȸ�ϸ鼭 ��� �ִ� LicingEntity ã��
                for(int i = 0; i<colliders.Length; i++)
                {
                    //�ݶ��̴��κ��� LivingEntity ������Ʈ ��������
                    LivingEntity livingEntity = colliders[i].GetComponent<LivingEntity>();

                    //LivingEntity ������Ʈ�� �����ϸ�, �ش� LivingEntity�� ��� �ִٸ�
                    if (livingEntity != null && !livingEntity.dead)
                    {
                        //���� ����� �ش� LivingEntity�� ����
                        targetEntity = livingEntity;

                        //for �� ���� ��� ����
                        break;
                    }
                }
            }
            //0.25�� �ֱ�� ó�� �ݺ�
            yield return new WaitForSeconds(0.25f);
        }
    }

    //������� �Ծ��� �� ������ ó��
    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        //���� ������� ���� ��쿡�� �ǰ� ȿ�� ���
        if (!dead)
        {
            //���ݹ��� ������ �������� ��ƼŬ ȿ�� ���
            hitEffect.transform.position = hitPoint;
            hitEffect.transform.rotation = Quaternion.LookRotation(hitNormal);
            hitEffect.Play();
            //�ǰ� ȿ���� ���
            enemyAudioPlayer.PlayOneShot(hitSound);
        }
        //LivingEntity�� OnDamage()�� �����Ͽ� ����� ����
        base.OnDamage(damage, hitPoint, hitNormal);
    }

    //��� ó��
    public override void Die()
    {
        //LivingEntity�� Die()�� �����Ͽ� �⺻ ��� ó�� ����
        base.Die();

        //�ٸ� AI�� �������� �ʵ��� �ڽ��� ��� �ݶ��̴��� ��Ȱ��ȭ
        Collider[] enemyColliders = GetComponents<Collider>();
        for(int i = 0; i<enemyColliders.Length; i++)
        {
            enemyColliders[i].enabled = false;
        }

        //AI ������  �����ϰ� ����޽� ������Ʈ ��Ȱ��ȭ
        pathFinder.isStopped = true;
        pathFinder.enabled = false;

        //��� �ִϸ��̼� ���
        enemyAnimator.SetTrigger("Die");
        //��� ȿ���� ���
        enemyAudioPlayer.PlayOneShot(deathSound);
    }

    private void OnTriggerStay(Collider other)
    {
        //�ڽ��� ������� �ʾ�����
        //�ֱ� ���� �������� timeBetAttack �̻� �ð��� �����ٸ� ���� ����
        if(!dead && Time.time >= lastAttackTime + timeBetAttack)
        {
            //������ LivingEntity Ÿ�� �������� �õ�
            LivingEntity attackTarget = other.GetComponent<LivingEntity>();
            //������ LivingEntity�� �ڽ��� ���� ����̶�� ���� ����
            if(attackTarget!=null && attackTarget == targetEntity)
            {
                //�ֱ� ���� �ð� ����
                lastAttackTime = Time.time;
                //������ �ǰ� ��ġ�� �ǰ� ������ �ٻ����� ���
                Vector3 hitPoint = other.ClosestPoint(transform.position);
                Vector3 hitNormal = transform.position - other.transform.position;
                //���� ����
                attackTarget.OnDamage(damage, hitPoint, hitNormal);
                enemyAnimator.SetTrigger("Attack");
            }
        }
    }
}
