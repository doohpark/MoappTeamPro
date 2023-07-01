using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

private Rigidbody rigid;
private bool isJump=false;
private bool isRun;
private Animator playerAnimator;
private float bulletCoolTime;

public float speed;
public float runSpeed;
public float jumpPower=10;
public GameManager gameManager;
public float rotateSpeed;
public Transform weaponTransform;
public GameObject bullet;
public WeaponBehaviour weaponBehaviour;

private Vector2 input;
private Vector3 targetDirection;
private Camera mainCamera;
public float turnSpeedMultiplier;



    // Start is called before the first frame update
       void Start() // 요 두 친구는 호출하지 않아도 돌아간다. start랑 update둘다 유니티에서 알아서 실행한다.
    {
        speed = 0.1f;
        rigid = GetComponent<Rigidbody>(); // 이 오브젝트 안에 있는 코드 중에서 Rigidbody라는 컴포넌트를 가져오겠다. 만약 이 오브젝트 안에 없다면 못부르는 거야
        playerAnimator = GetComponent<Animator>();
        StartCoroutine(Fire(bulletCoolTime));
        mainCamera = Camera.main;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isJump == false)
        {
            isJump = true;
            playerAnimator.SetBool("IsWalk", false);
            playerAnimator.SetBool("IsJump", isJump);
            rigid.AddForce(new Vector3(0, jumpPower, 0), ForceMode.Impulse);
            isJump = !isJump;
        }

        input.x = Input.GetAxisRaw("Horizontal");
    input.y = Input.GetAxisRaw("Vertical");

    UpdateTargetDirection();

    isRun = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

    if (isRun && isJump==false)
    {
        rigid.AddForce(targetDirection * runSpeed, ForceMode.Impulse);
        playerAnimator.SetBool("IsRun", true);
        playerAnimator.SetBool("IsWalk", false);
    }
    else
    {
        rigid.AddForce(targetDirection * speed, ForceMode.Impulse);
        playerAnimator.SetBool("IsRun", false);
        playerAnimator.SetBool("IsWalk",true);
    }

    if (input == Vector2.zero)
    {
        playerAnimator.SetBool("IsWalk", false);
    }
    else
    {
        if (targetDirection.magnitude > 0.1f)
        {
            Vector3 lookDirection = targetDirection.normalized;
            Quaternion freeRotation = Quaternion.LookRotation(lookDirection, transform.up);
            var differenceRotation = freeRotation.eulerAngles.y - transform.eulerAngles.y;
            var eulerY = transform.eulerAngles.y;

        if (differenceRotation < 0 || differenceRotation > 0)
        {
            eulerY = freeRotation.eulerAngles.y;
        }

        var euler = new Vector3(0, eulerY, 0);
        playerAnimator.SetBool("IsWalk", true);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(euler), rotateSpeed * turnSpeedMultiplier);
    }
}

        //요 친구는 모든 객체가 가지고 있다. 그러면 따로 선언을 할 필요가 없겠지? 이 친구는 class이다 translate는 움직이게 하는 함수이다
        //Vector3는 3가지 축을 의미함 x는 양 옆, 앞 뒤는 z축
        //class는 객체가 아니다 그럼 class를 객체에 넣어주어야 한다. 어떻게 할까?
        //transform.Translate(new Vector3(h,0,v)); 이 친구를 변화시킨다.
        //ForceMode.Impulse이건 팡팡 뛰게 한다
                }
        public void UpdateTargetDirection(){
            turnSpeedMultiplier  = 1.0f;
            var forward = mainCamera.transform.TransformDirection(Vector3.forward);
            forward.y=0;

            var right = mainCamera.transform.TransformDirection(Vector3.right);
            targetDirection = (input.x * right) + (input.y * forward);
        }
    
        private IEnumerator Fire(float coolTime){
            while(true){
                if(Input.GetKeyDown(KeyCode.Return)){
                    Instantiate(bullet, weaponTransform.position, Quaternion.identity);
                    weaponBehaviour.PlayWeaponSound();
                    yield return new WaitForSeconds(coolTime);
                }
                yield return null;
            }
        }
    
    private void OnCollisionEnter(Collision other){ //둘다 트리거가 꺼져 있는 상태, 충돌 판정
            if(other.gameObject.tag == "Floor") //충돌했을 때 그 태그가 floor이라면 이제 발동해야해 닿았으면 점프 안한거야
            {
                isJump = false;
                playerAnimator.SetBool("IsJump",isJump);
            }
        }
     private void OnTriggerEnter(Collider other){ // 둘중 하나가 트리거가 켜져 있는 상태?, 접촉 판정
            if(other.gameObject.tag == "Item") //충돌했을 때 그 태그가 floor이라면 이제 발동해야해 닿았으면 점프 안한거야
            {
                gameManager.itemCount++;
                gameManager.GetItem(gameManager.itemCount);
                Destroy(other.gameObject); // 요건 아예 사라지게 함
                //other.gameObject.SetActive(false); // 이건 비활성화 시키는 거고 사라지는 게 아님
            }
            if(other.gameObject.tag == "Finish") //충돌했을 때 그 태그가 floor이라면 이제 발동해야해 닿았으면 점프 안한거야
            {
                gameManager.MoveNextStage();// 요건 아예 사라지게 함
                // other.gameObject.SetActive(false); // 이건 비활성화 시키는 거고 사라지는 게 아님
            }
    }
}
