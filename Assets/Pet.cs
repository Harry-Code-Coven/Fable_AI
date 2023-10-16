using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pet : MonoBehaviour
{

    NavMeshAgent agent;

    public GameObject objectToTrack;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        InvokeRepeating("MoveToTarget", 1.0f, 2.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MoveToTarget()
    {
        agent.SetDestination(objectToTrack.transform.position);
    }

    bool HasLineOfSight()
    {
        Vector3 origin = transform.position;
        Vector3 dir = (objectToTrack.transform.position - transform.position).normalized;
        Ray ray = new Ray(origin, dir);

        RaycastHit hit;
        Debug.DrawRay(origin, dir * 500, Color.green, 20);

        Physics.Raycast(ray, out hit, 500);
        Debug.Log(hit.transform.gameObject.name);
        return true;
    }
}
