using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    /* This script makes use of NavMeshes to move a character around a scene
     * the NPC will patrol around the scene until it has a clear line of sight to the player
     * When the NPC 'sees' the player, it will follow them
     * For details on how to implement NavMeshes, see https://docs.unity3d.com/Manual/nav-BuildingNavMesh.html
     * */
    NavMeshAgent agent;

    public GameObject objectToTrack; // Set to player in the scene
    public Vector3 offset; // Needed to properly draw a line to the centre of the player (otherwise it would draw one to the players feet)

    public List<GameObject> patrolPoints; // A list of gameobjects the NPC will patrol to by default
    int currentPatrolIndex = 0;


    // Start is called before the first frame update
    void Start()
    {
        // Setup Nav Component
        agent = GetComponent<NavMeshAgent>();

        //Set Behaviour to update every second
        InvokeRepeating("UpdateNPCBehaviour", 1.0f, 1.0f);
        
    }

    void UpdateNPCBehaviour()
    {
        
        if(HasLineOfSight())
        {
            //Follow the player
            agent.SetDestination(objectToTrack.transform.position);
        }
        else
        {
            Debug.Log("Where did you go?");
            Patrol();
        }
       
    }

    bool HasLineOfSight()
    {
        Vector3 origin = transform.position;
        Vector3 dir = ((objectToTrack.transform.position + offset) - transform.position).normalized;
        Ray ray = new Ray(origin, dir);

        RaycastHit hit;

        Physics.Raycast(ray, out hit, 500);

        return hit.transform == objectToTrack.transform;
    }

    void Patrol()
    {
        agent.SetDestination(patrolPoints[currentPatrolIndex].transform.position);

        if(Vector3.Distance(transform.position, agent.destination) < 1.0f)
        {
            //Move to next patrol point
            currentPatrolIndex++;
            if(currentPatrolIndex >= patrolPoints.Count)
            {
                currentPatrolIndex = 0;
            }
        }

    }

}
