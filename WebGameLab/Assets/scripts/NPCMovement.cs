using KBCore.Refs;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;
using System.Linq;
[RequireComponent(typeof(NavMeshAgent))]
public class NPCMovement : MonoBehaviour
{
    [SerializeField, Self] private NavMeshAgent agent;
    [SerializeField] private List<GameObject> wayPoints = new List<GameObject>();
    private Vector3 destination;
    private int index;
    private void OnValidate() => this.ValidateRefs();
 
    void Start()
    {
        wayPoints = GameObject.FindGameObjectsWithTag("waypoint").ToList();
        if (wayPoints.Count < 0) return;
        agent.destination = destination = wayPoints[index].transform.position;   
    }
    void Update()
    {
        if (wayPoints.Count < 0) return;
        if (Vector3.Distance(transform.position, destination) < 3f)
        {
            index = (index + 1) % wayPoints.Count;
            destination = wayPoints[index].transform.position;
            agent.destination = destination;
        }
            
        
    }
}
