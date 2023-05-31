using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Node
{
    protected NodeState currentNodeState;

    // Node state Get function
    public NodeState nodeState { get { return currentNodeState; } }

    // abstract method that every class inheiriting from node will make a implementation for
    public abstract NodeState Evaluate();
}

// Field that desrcibes the state of the node
public enum NodeState
{
    RUNNING, SUCCESS, FAILURE,
}
