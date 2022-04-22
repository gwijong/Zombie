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
    
    }

    //���� �����ϰ� ������ ����� �Ҵ�
    private void CreateEnemy()
    {

    }

}