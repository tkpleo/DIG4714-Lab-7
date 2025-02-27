using UnityEngine;
using UnityEngine.AI;
public class Enemy3 : MonoBehaviour
{
    public Transform patrolRoute; // Parent containing waypoints
    public Transform player; // Player reference
    private NavMeshAgent agent;
    private Transform[] locations;
    private int currentLocation = 0;
    private bool chasingPlayer = false;
    public GameObject enemy;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        InitializePatrolRoute();
        MoveToNextPatrolLocation();
        Renderer enemy = GetComponent<Renderer>();
        enemy.material.color = Color.red;
    }
    void Update()
    {
        if (!chasingPlayer && !agent.pathPending && agent.remainingDistance < 0.2f)
        {
            MoveToNextPatrolLocation();
        }
    }
    void MoveToNextPatrolLocation()
    {
        if (locations.Length == 0) return;
        agent.SetDestination(locations[currentLocation].position);
        currentLocation = (currentLocation + 1) % locations.Length;
    }
    void InitializePatrolRoute()
    {
        locations = new Transform[patrolRoute.childCount];
        for (int i = 0; i < patrolRoute.childCount; i++)
        {
            locations[i] = patrolRoute.GetChild(i);
        }
    }
    // Detect when player enters enemy's range
    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            Debug.Log("Player detected - start chasing!");
            chasingPlayer = true;
            agent.SetDestination(player.position);
            Renderer enemy = GetComponent<Renderer>();
            enemy.material.color = Color.blue;
        }
    }
    // Detect when player leaves enemy's range
    void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            Debug.Log("Player out of range - resume patrol.");
            chasingPlayer = false;
            MoveToNextPatrolLocation();
            Renderer enemy = GetComponent<Renderer>();
            enemy.material.color = Color.red;
        }
    }
}
