using UnityEngine;

public class GameManager : MonoBehaviour //������ ���ӿ��� ���θ� �����ϴ� ���� �Ŵ���
{
    public static GameManager instance  //�̱��� ���ٿ� ������Ƽ
    {
        get
        {
            if(m_instance == null)  //���� �̱��� ������ ���� ������Ʈ�� �Ҵ���� �ʾҴٸ�
            {
                m_instance = FindObjectOfType<GameManager>();  //������ GameManager ������Ʈ�� ã�Ƽ� �Ҵ�
            }
            return m_instance;//�̱��� ������Ʈ ��ȯ
        }
    }

    private static GameManager m_instance; //�̱����� �Ҵ�� static ����

    private int score = 0;  //���� ���� ����
    public bool isGameover { get; private set;}  //���ӿ��� ����

    private void Awake()
    {
        if(instance != this)  //���� �̱��� ������Ʈ�� �� �ٸ� GameManager ������Ʈ�� �ִٸ�
        {
            Destroy(gameObject);//�ڽ��� �ı�
        }
    }

    private void Start()
    {
        //�÷��̾� ĳ������ ��� �̺�Ʈ �߻� �� ���� ����
        FindObjectOfType<PlayerHealth>().onDeath += EndGame;  //��ȯ���� �Ű������� ���� ��������Ʈ Action�� EndGame �߰�    
    }

    //������ �߰��ϰ� UI ����
    public void AddScore(int newScore)
    {
        //���ӿ����� �ƴ� ���¿����� ���� �߰� ����
        if (!isGameover)
        {
            //���� �߰�
            score += newScore;
            //���� UI �ؽ�Ʈ ����
            UIManager.instance.UpdateScoreText(score);
        }
    }

    //���ӿ��� ó��
    public void EndGame()
    {
        //���ӿ��� ���¸� ������ ����
        isGameover = true;
        //���ӿ��� UI Ȱ��ȭ
        UIManager.instance.SetActiveGameoverUI(true);
    } 
}
