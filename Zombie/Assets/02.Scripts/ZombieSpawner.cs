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

    //매 프레임마다 조건을 검사하고 좀비 생성 웨이브를 실행
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
        wave++;//웨이브 1 증가

        int spawnCount = Mathf.RoundToInt(wave * 1.5f);//현재 웨이브 * 1.5를 반올림한 수만큼 적 생성

        for(int i = 0; i<spawnCount; i++)//spawnCount만큼 적 생성
        {
            float enemyIntensity = Random.Range(0f, 1f); //적의 세기를 0%에서 100% 사이에서 랜덤 결정
            CreateEnemy(enemyIntensity);//적 생성 처리 실행
        }
    }

    //적을 생성하고 추적할 대상을 할당
    private void CreateEnemy(float intensity)
    {
        //사용할 좀비 데이터 랜덤으로 결정
        ZombieData zombieData = zombieDatas[Random.Range(0, zombieDatas.Length)];
        //생성할 위치를 랜덤으로 결정
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        //좀비 프래팹으로부터 좀비 생성
        Zombie zombie = Instantiate(zombiePrefab, spawnPoint.position, spawnPoint.rotation);
        //생성한 좀비의 능력치 설정
        zombie.Setup(zombieData);
        //생성된 좀비를 리스트에 추가
        zombies.Add(zombie);
        //좀비의 onDeath 이벤트에 익명 메서드 등록
        //사망한 좀비를 리스트에서 제거
        zombie.onDeath += () => zombies.Remove(zombie);
        //사망할 좀비를 10초 뒤에 파괴
        zombie.onDeath += () => Destroy(zombie.gameObject, 10f);
        //좀비 사망 시 점수 상승
        zombie.onDeath += () => GameManager.instance.AddScore(100);
    }
}
