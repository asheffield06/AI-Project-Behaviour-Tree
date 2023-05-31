using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardAI : AI
{
    private float timer = 0;
    private bool resetTarget = false;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        material = GetComponentInChildren<MeshRenderer>().material;
    }

    private void Start()
    {
        ConstructBehahaviourTree();
    }

    private void ConstructBehahaviourTree()
    {
        if (PlayerController.instance.LootCollected)
        {
            gameObject.GetComponent<GuardAI>().TargetTransform = PlayerController.instance.transform;

            ChaseNode chaseNode = new ChaseNode(TargetTransform, agent, this);
            RangeNode chasingRangeNode = new RangeNode(100, TargetTransform, transform);

            RangeNode shootingRangeNode = new RangeNode(shootingRange, TargetTransform, transform);
            AttackNode shootNode = new AttackNode(agent, this, TargetTransform);

            Sequence chaseSequence = new Sequence(new List<Node> { chasingRangeNode, chaseNode });
            Sequence attackSequence = new Sequence(new List<Node> { shootingRangeNode, shootNode });

            WanderNode wanderNode = new WanderNode(agent, this);
            Sequence WanderingSequence = new Sequence(new List<Node> { wanderNode });

            topNode = new Selector(new List<Node> { attackSequence, chaseSequence, WanderingSequence });
        }
        else
        {
            ChaseNode chaseNode = new ChaseNode(TargetTransform, agent, this);
            RangeNode chasingRangeNode = new RangeNode(chasingRange, TargetTransform, transform);

            RangeNode shootingRangeNode = new RangeNode(shootingRange, TargetTransform, transform);
            AttackNode shootNode = new AttackNode(agent, this, TargetTransform);

            Sequence chaseSequence = new Sequence(new List<Node> { chasingRangeNode, chaseNode });
            Sequence attackSequence = new Sequence(new List<Node> { shootingRangeNode, shootNode });

            WanderNode wanderNode = new WanderNode(agent, this);
            Sequence WanderingSequence = new Sequence(new List<Node> { wanderNode });

            topNode = new Selector(new List<Node> { attackSequence, chaseSequence, WanderingSequence });
        }
    }

    private void Update()
    {
        if (resetTarget)
        {
            timer += Time.deltaTime;
            if(timer > 2.5f)
            {
                timer = 0;
                TargetTransform = null;
                resetTarget = false;
            }
        }

        topNode.Evaluate();
        if (topNode.nodeState == NodeState.FAILURE)
        {
            SetColor(Color.red);
            agent.isStopped = true;
        }


        ConstructBehahaviourTree();

    }

    public override void SetColor(Color color)
    {
        material.color = color;
    }

    public override void SetBestCoverSpot(Transform bestCoverSpot)
    {
        this.bestCoverSpot = bestCoverSpot;
    }

    public override Transform GetBestCoverSpot()
    {
        return bestCoverSpot;
    }

}