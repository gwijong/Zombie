using UnityEngine;

//[CreateAssetMenu(menuName = "메뉴 경로",fileName = "기본 파일명"), order = 메뉴상에서 순서]
[CreateAssetMenu(menuName = "Scriptable/Gundata", fileName = "Gun Data")]
public class GunData : ScriptableObject
{
    public AudioClip shotClip;
    public AudioClip reloadClip;

    public float damage = 25;

    public int startAmmoRemain = 100; //처음에 주어질 전체 탄알

    public int magCapacity = 25; //탄창 용량

    public float timeBetFire = 0.12f; //탄알 발사 간격

    public float reloadTime = 1.8f;//재장전 소요 시간
}
