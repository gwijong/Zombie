using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI 관련 코드
using UnityEngine.SceneManagement; //씬 관리자 관련 코드

/// <summary> 필요한 UI에 즉시 접근하고 변경할 수 있도록 허용하는 UI 매니저 </summary>
public class UIManager : MonoBehaviour
{
    /// <summary> 싱글턴 접근용 프로퍼티 </summary>
    public static UIManager instance
    {
        get
        {
            if(m_instance == null)
            {
                m_instance = FindObjectOfType<UIManager>();
            }
            return m_instance;
        }
    }

    private static UIManager m_instance; //싱글턴이 할당될 변수

    /// <summary> 탄알 표시용 텍스트 </summary>
    public Text ammoText; 
    /// <summary> 점수 표시용 텍스트 </summary>
    public Text scoreText;  
    /// <summary> 적 웨이브 표시용 텍스트 </summary>
    public Text waveText;  
    /// <summary> 게임오버 시 활성화할 UI </summary>
    public GameObject gameoverUI;  

    /// <summary> 탄알 텍스트 갱신 </summary>
    public void UpdateAmmoText(int magAmmo, int remainAmmo)
    {
        ammoText.text = magAmmo + "/" + remainAmmo;
    }

    /// <summary> 점수 텍스트 갱신 </summary>
    public void UpdateScoreText(int newScore)
    {
        scoreText.text = "Score : " + newScore;
    }

    /// <summary> 적 웨이브 텍스트 갱신 </summary>
    public void UpdateWaveText(int waves, int count)
    {
        waveText.text = "Wave : " + waves + "\nEnemy Left : " + count; //  \n : 개행 문자
    }

    /// <summary> 게임오버 UI 활성화 </summary>
    public void SetActiveGameoverUI(bool active)
    {
        gameoverUI.SetActive(active);
    }

    /// <summary> 게임 재시작 </summary>
    public void GameRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
