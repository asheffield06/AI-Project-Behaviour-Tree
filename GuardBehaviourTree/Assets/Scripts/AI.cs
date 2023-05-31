using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    [SerializeField] protected float chasingRange;
    [SerializeField] protected float shootingRange;


    public Transform TargetTransform;

    protected Material material;
    protected Transform bestCoverSpot;
    protected NavMeshAgent agent;

    protected Node topNode;

    public Vector3 wanderPos;

    public Light visionLight;

    public virtual void SetColor(Color color)
    {

    }

    public virtual void SetBestCoverSpot(Transform bestCoverSpot)
    {
        
    }

    public virtual Transform GetBestCoverSpot()
    {
        return null;
    }
}
