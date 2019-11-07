using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private float hp = 100f;
    public Transform goal;
    private NavMeshAgent agent;
    private Rigidbody body;
    public EnemySpawner spawner;
    void Start () 
    {
        body = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        
    }

    void Update()
    {
        agent.destination = goal.position;
    }

    public void damage(float amount, Vector3 pos)
    {
        hp -= amount;
        Vector3 knockdir = transform.position - pos;

        if(hp <= 0)
        {
            spawner.enemies.Remove(this.gameObject);
            Destroy(gameObject);
        }
    }
}
