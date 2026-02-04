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
    private void OnValidate()
    {
        this.ValidateRefs();
    }
    void Start()
    {
        wayPoints = GameObject.FindGameObjectsWithTag("waypoints").ToList();
        if (wayPoints.Count < 0) return;
        agent.destination = wayPoints[0].transform.position;   
    }
    void Update()
    {
        if (wayPoints.Count < 0) return;
    }
}
