using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    public enum State {Ready, Empty, Reloading}  //총의 상태를 표현하는 데 사용할 타입을 선언
    public State state { get; private set; }  //현재 총의 상태
    public Transform fireTransform;  //탄알이 발사될 위치
    public ParticleSystem muzzleFlashEffect;  //총구 화염 효과
    public ParticleSystem shellEjectEffect;  //탄피 배출 효과

    private LineRenderer bulletLineRenderer; //탄알 궤적을 그리기 위한 렌더러

    private AudioSource gunAudioPlayer;  //총 소리 재생기
    //public AudioClip shotClip;  //발사 소리
    //public AudioClip reloadClip;  //재장전 소리
    //public int ammoRemain = 100;  //남은 전체 탄알
    //public int magCapacity = 25;  //탄창 용량
    //public float damage = 25;  //공격력

    public GunData gunData;//총의 현재 데이터

    private float fireDistance = 50f;  //사정거리
    public int magAmmo;  //현재 탄창에 남아 있는 탄알
    //public float timeBetFire = 0.12f;  //탄알 발사 간격
    //public float reloadTime = 1.8f;  //재장전 소요 시간
    private float lastFireTime;  //총을 마지막으로 발사한 시점

    private void Awake()
    {//사용할 컴포넌트의 참조 가져오기

    }

    private void OnEnable()
    {//총 상태 초기화
        
    }

    public void Fire()
    {//발사 시도

    }
    
    private void Shot()
    {//실제 발사 처리

    }

    private IEnumerator ShotEffect(Vector3 hitPosition)  //발사 이펙트와 소리를 재생하고 탄알 궤적을 그림
    {
        bulletLineRenderer.enabled = true;

        yield return new WaitForSeconds(0.03f);  //0.03초 동안 잠시 처리를 대기

        bulletLineRenderer.enabled = false;  //라인 렌더러를 비활성화하여 탄알 궤적을 지움
    }

    public bool Reload()
    {
        return false;
    }

    private IEnumerator ReloadRoutine()  //실제 재장전 처리를 진행
    {
        state = State.Reloading;  //현재 상태를 재장전 중 상태로 전환

        yield return new WaitForSeconds(gunData.reloadTime); //재장전 소요 시간만큼 처리 쉬기

        state = State.Ready; //총의 현재 상태를 발사 준비된 상태로 변경
    }
    
}
