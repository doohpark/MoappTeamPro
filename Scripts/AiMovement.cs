using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiMovement : MonoBehaviour
{
    private NavMeshAgent enemyAgent;
    private float dist;
    public float followThreshold;

    [SerializeField]
    Transform targetTransform;

    // Start is called before the first frame update
    void Start()
    {
        enemyAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        dist=Vector3.Distance(targetTransform.position, transform.position);
        if(dist < followThreshold){
            enemyAgent.isStopped=false;
            enemyAgent.SetDestination(targetTransform.position);
        }
        else{
            enemyAgent.isStopped=true;
        }
    }
}
