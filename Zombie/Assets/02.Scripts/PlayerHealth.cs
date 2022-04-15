using UnityEngine;
using UnityEngine.UI;  //UI ���� �ڵ�

//�÷��̾� ĳ������ ����ü�μ��� ������ ���
public class PlayerHealth : LivingEntity
{
    public Slider healthSlider;  //ü���� ǥ���� UI �����̴�

    public AudioClip deathClip;//��� �Ҹ�
    public AudioClip hitClip;//�ǰ� �Ҹ�
    public AudioClip itemPickupClip;//������ ���� �Ҹ�

    private AudioSource playerAudioPlayer;//�÷��̾� �Ҹ� �����
    private Animator playerAnimator;//�÷��̾��� �ִϸ�����

    private PlayerMovement playerMovement;//�÷��̾� ������ ������Ʈ
    private PlayerShooter playerShooter;//�÷��̾� ���� ������Ʈ

    private void Awake()
    {//����� ������Ʈ ��������
        playerAudioPlayer = GetComponent<AudioSource>();
        playerAnimator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        playerShooter = GetComponent<PlayerShooter>();
    }

    protected override void OnEnable()
    {//LivingEntity�� OnEnable() ����(���� �ʱ�ȭ)
        base.OnEnable();

        //ü�� �����̴� Ȱ��ȭ
        healthSlider.gameObject.SetActive(true);
        //ü�� �����̴��� �ִ��� �⺻ ü�°����� ����
        healthSlider.maxValue = startingHealth;
        //ü�� �����̴��� ���� ���� ü�°����� ����
        healthSlider.value = health;

        //�÷��̾� ������ �޴� ������Ʈ Ȱ��ȭ
        playerMovement.enabled = true;
        playerShooter.enabled = true;
    }

    //ü�� ȸ��
    public override void RestoreHealth(float newHealth)
    {//LivingEntity�� RestoreHealth()_ ����(ü�� ����)
        base.RestoreHealth(newHealth);
        healthSlider.value = health; //���ŵ� ü������ ü�� �����̴� ����
    }

    //����� ó��
    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {//LivingEntity�� OnDamage() ����(����� ����)
        if (!dead)
        {
            playerAudioPlayer.PlayOneShot(hitClip);
        }
        base.OnDamage(damage, hitPoint, hitNormal);
        //���ŵ� ü���� ü�� �����̴��� �ݿ�
        healthSlider.value = health;
    }

    //��� ó��
    public override void Die()
    {//LivingEntity�� Die()����(��� ����)
        base.Die();
        //ü�� �����̴� ��Ȱ��ȭ
        healthSlider.gameObject.SetActive(false);
        //����� ���
        playerAudioPlayer.PlayOneShot(deathClip);
        playerAnimator.SetTrigger("Die");
        //�÷��̾� ������ �޴� ������Ʈ ��Ȱ��ȭ
        playerMovement.enabled = false;
        playerShooter.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {//�����۰� �浹�� ��� �ش� �������� ����ϴ� ó��
        if (!dead)
        {
            // �浹�� �������κ��� Item ������Ʈ �������� �õ�
            IItem item = other.GetComponent<IItem>();
            if(item != null) //�浹�� �������κ��� IItem ������Ʈ�� �������� �� �����ߴٸ�
            {
                item.Use(gameObject);  //Use �޼��带 �����Ͽ� ������ ���
                playerAudioPlayer.PlayOneShot(itemPickupClip);  //������ ���� �Ҹ� ���
            }
        }
    }
}
