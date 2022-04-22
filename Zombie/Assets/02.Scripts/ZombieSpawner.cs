using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//적 게임 오브젝트를 주기적으로 생성
public class ZombieSpawner : MonoBehaviour
{
    public Zombie zombiePrefab;// 생성할 적 AI

    public ZombieData[] zombieDatas;
    public Transform[] spawnPoints; //적 AI를 소환할 위치

    /*
    public float damageMax = 40f;  //최대 공격력
    public float damageMin = 20f;  //최소 공격력

    public float healthMax = 200f;  //최대 체력
    public float healthMin = 100f;  //최소 체력

    public float speedMax = 3f;  //최대 속도
    public float speedMin = 1f; //최소 속도
    
    public Color strongEnemyColor = Color.red;//강한 적 AI가 가지게 될 피부색
     */
    private List<Zombie> zombies = new List<Zombie>();  //생성된 적을 담는 리스트
    private int wave;  //현재 웨이브

    private void Update()
    {
        //게임오버 상태일때는 생성하지 않음
        if(GameManager.instance != null && GameManager.instance.isGameover)
        {
            return;
        }

        //적을 모두 물리친 경우 다음 스폰 실행
        if(zombies.Count <= 0)
        {
            SpawnWave();
        }

        //UI 갱신
        UpdateUI();
    }

    //웨이브 정보를 UI로 표시
    private void UpdateUI()
    {
        //현재 웨이브와 남은 적 수 표시
        UIManager.instance.UpdateWaveText(wave, zombies.Count);
    }

    //현재 웨이브에 맞춰 적 생성
    private void SpawnWave() 
    { 
    
    }

    //적을 생성하고 추적할 대상을 할당
    private void CreateEnemy()
    {

    }

}
