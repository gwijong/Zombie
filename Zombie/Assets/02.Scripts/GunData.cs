using UnityEngine;

//[CreateAssetMenu(menuName = "�޴� ���",fileName = "�⺻ ���ϸ�"), order = �޴��󿡼� ����]
[CreateAssetMenu(menuName = "Scriptable/Gundata", fileName = "Gun Data")]
public class GunData : ScriptableObject
{
    public AudioClip shotClip;
    public AudioClip reloadClip;

    public float damage = 25;

    public int startAmmoRemain = 100; //ó���� �־��� ��ü ź��

    public int magCapacity = 25; //źâ �뷮

    public float timeBetFire = 0.12f; //ź�� �߻� ����

    public float reloadTime = 1.8f;//������ �ҿ� �ð�
}
