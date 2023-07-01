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
       void Start() // �� �� ģ���� ȣ������ �ʾƵ� ���ư���. start�� update�Ѵ� ����Ƽ���� �˾Ƽ� �����Ѵ�.
    {
        speed = 0.1f;
        rigid = GetComponent<Rigidbody>(); // �� ������Ʈ �ȿ� �ִ� �ڵ� �߿��� Rigidbody��� ������Ʈ�� �������ڴ�. ���� �� ������Ʈ �ȿ� ���ٸ� ���θ��� �ž�
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

        //�� ģ���� ��� ��ü�� ������ �ִ�. �׷��� ���� ������ �� �ʿ䰡 ������? �� ģ���� class�̴� translate�� �����̰� �ϴ� �Լ��̴�
        //Vector3�� 3���� ���� �ǹ��� x�� �� ��, �� �ڴ� z��
        //class�� ��ü�� �ƴϴ� �׷� class�� ��ü�� �־��־�� �Ѵ�. ��� �ұ�?
        //transform.Translate(new Vector3(h,0,v)); �� ģ���� ��ȭ��Ų��.
        //ForceMode.Impulse�̰� ���� �ٰ� �Ѵ�
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
    
    private void OnCollisionEnter(Collision other){ //�Ѵ� Ʈ���Ű� ���� �ִ� ����, �浹 ����
            if(other.gameObject.tag == "Floor") //�浹���� �� �� �±װ� floor�̶�� ���� �ߵ��ؾ��� ������� ���� ���Ѱž�
            {
                isJump = false;
                playerAnimator.SetBool("IsJump",isJump);
            }
        }
     private void OnTriggerEnter(Collider other){ // ���� �ϳ��� Ʈ���Ű� ���� �ִ� ����?, ���� ����
            if(other.gameObject.tag == "Item") //�浹���� �� �� �±װ� floor�̶�� ���� �ߵ��ؾ��� ������� ���� ���Ѱž�
            {
                gameManager.itemCount++;
                gameManager.GetItem(gameManager.itemCount);
                Destroy(other.gameObject); // ��� �ƿ� ������� ��
                //other.gameObject.SetActive(false); // �̰� ��Ȱ��ȭ ��Ű�� �Ű� ������� �� �ƴ�
            }
            if(other.gameObject.tag == "Finish") //�浹���� �� �� �±װ� floor�̶�� ���� �ߵ��ؾ��� ������� ���� ���Ѱž�
            {
                gameManager.MoveNextStage();// ��� �ƿ� ������� ��
                // other.gameObject.SetActive(false); // �̰� ��Ȱ��ȭ ��Ű�� �Ű� ������� �� �ƴ�
            }
    }
}
