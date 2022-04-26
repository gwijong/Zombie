using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�� ���� ������Ʈ�� �ֱ������� ����
public class ZombieSpawner : MonoBehaviour
{
    public Zombie zombiePrefab;// ������ �� AI

    public ZombieData[] zombieDatas;
    public Transform[] spawnPoints; //�� AI�� ��ȯ�� ��ġ

    /*
    public float damageMax = 40f;  //�ִ� ���ݷ�
    public float damageMin = 20f;  //�ּ� ���ݷ�

    public float healthMax = 200f;  //�ִ� ü��
    public float healthMin = 100f;  //�ּ� ü��

    public float speedMax = 3f;  //�ִ� �ӵ�
    public float speedMin = 1f; //�ּ� �ӵ�
    
    public Color strongEnemyColor = Color.red;//���� �� AI�� ������ �� �Ǻλ�
     */
    private List<Zombie> zombies = new List<Zombie>();  //������ ���� ��� ����Ʈ
    private int wave;  //���� ���̺�

    //�� �����Ӹ��� ������ �˻��ϰ� ���� ���� ���̺긦 ����
    private void Update()
    {
        //���ӿ��� �����϶��� �������� ����
        if(GameManager.instance != null && GameManager.instance.isGameover)
        {
            return;
        }

        //���� ��� ����ģ ��� ���� ���� ����
        if(zombies.Count <= 0)
        {
            SpawnWave();
        }

        //UI ����
        UpdateUI();
    }

    //���̺� ������ UI�� ǥ��
    private void UpdateUI()
    {
        //���� ���̺�� ���� �� �� ǥ��
        UIManager.instance.UpdateWaveText(wave, zombies.Count);
    }

    //���� ���̺꿡 ���� �� ����
    private void SpawnWave() 
    {
        wave++;//���̺� 1 ����

        int spawnCount = Mathf.RoundToInt(wave * 1.5f);//���� ���̺� * 1.5�� �ݿø��� ����ŭ �� ����

        for(int i = 0; i<spawnCount; i++)//spawnCount��ŭ �� ����
        {
            float enemyIntensity = Random.Range(0f, 1f); //���� ���⸦ 0%���� 100% ���̿��� ���� ����
            CreateEnemy(enemyIntensity);//�� ���� ó�� ����
        }
    }

    //���� �����ϰ� ������ ����� �Ҵ�
    private void CreateEnemy(float intensity)
    {
        //����� ���� ������ �������� ����
        ZombieData zombieData = zombieDatas[Random.Range(0, zombieDatas.Length)];
        //������ ��ġ�� �������� ����
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        //���� ���������κ��� ���� ����
        Zombie zombie = Instantiate(zombiePrefab, spawnPoint.position, spawnPoint.rotation);
        //������ ������ �ɷ�ġ ����
        zombie.Setup(zombieData);
        //������ ���� ����Ʈ�� �߰�
        zombies.Add(zombie);
        //������ onDeath �̺�Ʈ�� �͸� �޼��� ���
        //����� ���� ����Ʈ���� ����
        zombie.onDeath += () => zombies.Remove(zombie);
        //����� ���� 10�� �ڿ� �ı�
        zombie.onDeath += () => Destroy(zombie.gameObject, 10f);
        //���� ��� �� ���� ���
        zombie.onDeath += () => GameManager.instance.AddScore(100);
    }
}
