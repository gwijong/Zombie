using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//탄알을 충전하는 아이템
public class Coin : MonoBehaviour, IItem  //인터페이스는 규격이다
{
    public int score = 200;
    public void Use(GameObject target)
    {
        //게임 매니저에 접근해 점수 추가
        GameManager.instance.AddScore(score);
        //사용되었으므로 자신을 파괴
        Destroy(gameObject);
    }
}
