using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Start is called before the first frame update
     public Transform playerTransform;//�÷��̾��� ��ġ
    public Vector3 offset; // �÷��̾�� ī�޶��� �Ÿ��� �����ϱ� ���� ����

    private void Awake(){ // �� �ν��Ͻ��� ������ �Ǿ��� �� ������ ��
        offset = transform.position - playerTransform.position; // �Ÿ��� �� ������ ��ġ�� ���� ��
        //�̰� ������ �� ��ġ�� ���� ����
    }



    // Update is called once per frame
    void LateUpdate() //�������� ���� ������Ʈ�� ��, ���� �� �ֱ� ������
    {
        transform.position = playerTransform.position + offset;   
    }
}
