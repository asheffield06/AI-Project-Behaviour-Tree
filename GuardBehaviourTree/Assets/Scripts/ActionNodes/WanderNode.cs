using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WanderNode : Node
{
    private NavMeshAgent agent;
    private AI ai;

    private float wanderRadius = 50.0f;
    private float wanderTimer;

    private float timer;

    

    public WanderNode(NavMeshAgent agent, AI ai)
    {
        this.agent = agent;
        this.ai = ai;
    }

    public override NodeState Evaluate()
    {
        agent.speed = 10.0f;

        agent.GetComponent<GuardAI>().visionLight.color = Color.white;

        if (agent.GetComponent<GuardAI>().wanderPos == new Vector3(0,0,0))
        {
            Vector3 newPos = RandomNavSphere(agent.transform.position, wanderRadius, -1);
            agent.GetComponent<GuardAI>().wanderPos = newPos;
        }

        agent.SetDestination(agent.GetComponent<GuardAI>().wanderPos);

        if (Vector3.Distance(agent.GetComponent<GuardAI>().wanderPos, agent.transform.position) < 1.2f)
        {
            Vector3 newPos = RandomNavSphere(agent.transform.position, wanderRadius, -1);
            newPos = new Vector3(newPos.x, 1, newPos.z);

            agent.transform.LookAt(newPos);
            agent.SetDestination(newPos);
            agent.GetComponent<GuardAI>().wanderPos = newPos;
        } 

        float distance = Vector3.Distance(agent.GetComponent<GuardAI>().wanderPos, agent.transform.position);
        if (distance > 0.2f)
        {
            agent.isStopped = false;
            agent.SetDestination(agent.GetComponent<GuardAI>().wanderPos);
            return NodeState.RUNNING;
        }
        else
        {
            Debug.Log("hey");
            agent.isStopped = true;
            return NodeState.SUCCESS;
        }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }
}
