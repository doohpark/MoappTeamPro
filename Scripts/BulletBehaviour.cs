using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{  
    public float bulletSpeed;
    private Rigidbody bulletRigid;
    // Start is called before the first frame update

    // Update is called once per frame
    void Awake()
    {
        bulletRigid=GetComponent<Rigidbody>();
        bulletRigid.AddForce(transform.forward,ForceMode.Impulse);
    }
}
