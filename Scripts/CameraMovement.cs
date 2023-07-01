using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Start is called before the first frame update
     public Transform playerTransform;//플레이어의 위치
    public Vector3 offset; // 플레이어와 카메라의 거리를 정의하기 위한 변수

    private void Awake(){ // 이 인스턴스가 시작이 되었을 때 실행이 됨
        offset = transform.position - playerTransform.position; // 거리는 둘 사이의 위치를 빼면 됨
        //이게 시작할 때 위치를 정한 거임
    }



    // Update is called once per frame
    void LateUpdate() //프레임의 끝에 업데이트를 함, 꼬일 수 있기 때문에
    {
        transform.position = playerTransform.position + offset;   
    }
}
