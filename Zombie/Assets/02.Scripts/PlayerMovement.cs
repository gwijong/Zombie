using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //����� �Է¿� ���� �÷��̾� ĳ���͸� �����̴� ��ũ��Ʈ
    public float moveSpeed = 5f;  //�յ� �������� �ӵ�
    public float rotateSpeed = 180f;  //�¿� ȸ�� �ӵ�

    private PlayerInput playerInput;  //�÷��̾� �Է��� �˷��ִ� ������Ʈ
    private Rigidbody playerRigidbody;  //�÷��̾� ĳ������ ������ٵ�
    private Animator playerAnimator;   //�÷��̾� ĳ������ �ִϸ�����

    void Start()  
    {//����� ������Ʈ���� ���� ��������
        playerInput = GetComponent<PlayerInput>();
        playerRigidbody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
    }

    //FixedUpdate�� ���� ���� �ֱ⿡ ���� �����
    void FixedUpdate()
    {//���� ���� �ֱ⸶�� ������, ȸ��, �ִϸ��̼� ó�� ����
        Rotate();
        Move();
        playerAnimator.SetFloat("Move", playerInput.move);
    }

    //�Է°��� ���� ĳ���͸� �յڷ� ������
    private void Move()
    {
        //��������� �̵��� �Ÿ� ���
        Vector3 moveDistance = playerInput.move * transform.forward * moveSpeed * Time.deltaTime;
        playerRigidbody.MovePosition(playerRigidbody.position + moveDistance);
    }

    //�Է°��� ���� ĳ���͸� �¿�� ȸ��
    private void Rotate()
    {
        float turn = playerInput.rotate * rotateSpeed * Time.deltaTime;

        playerRigidbody.rotation = playerRigidbody.rotation * Quaternion.Euler(0, turn, 0f);
    }
}
