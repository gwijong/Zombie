using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI ���� �ڵ�
using UnityEngine.SceneManagement; //�� ������ ���� �ڵ�

/// <summary> �ʿ��� UI�� ��� �����ϰ� ������ �� �ֵ��� ����ϴ� UI �Ŵ��� </summary>
public class UIManager : MonoBehaviour
{
    /// <summary> �̱��� ���ٿ� ������Ƽ </summary>
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

    private static UIManager m_instance; //�̱����� �Ҵ�� ����

    /// <summary> ź�� ǥ�ÿ� �ؽ�Ʈ </summary>
    public Text ammoText; 
    /// <summary> ���� ǥ�ÿ� �ؽ�Ʈ </summary>
    public Text scoreText;  
    /// <summary> �� ���̺� ǥ�ÿ� �ؽ�Ʈ </summary>
    public Text waveText;  
    /// <summary> ���ӿ��� �� Ȱ��ȭ�� UI </summary>
    public GameObject gameoverUI;  

    /// <summary> ź�� �ؽ�Ʈ ���� </summary>
    public void UpdateAmmoText(int magAmmo, int remainAmmo)
    {
        ammoText.text = magAmmo + "/" + remainAmmo;
    }

    /// <summary> ���� �ؽ�Ʈ ���� </summary>
    public void UpdateScoreText(int newScore)
    {
        scoreText.text = "Score : " + newScore;
    }

    /// <summary> �� ���̺� �ؽ�Ʈ ���� </summary>
    public void UpdateWaveText(int waves, int count)
    {
        waveText.text = "Wave : " + waves + "\nEnemy Left : " + count; //  \n : ���� ����
    }

    /// <summary> ���ӿ��� UI Ȱ��ȭ </summary>
    public void SetActiveGameoverUI(bool active)
    {
        gameoverUI.SetActive(active);
    }

    /// <summary> ���� ����� </summary>
    public void GameRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
