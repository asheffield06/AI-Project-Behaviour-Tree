using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : Node
{
    // Refrence to the child nodes for evaluation
    protected List<Node> nodes = new List<Node>();

    // Constructor using list of nodes as a paramter
    public Sequence(List<Node> nodes)
    {
        this.nodes = nodes;
    }

    // Implementation of abstract method from Node class
    public override NodeState Evaluate()
    {
        // Will check to see if any child of the sequence is currently running
        bool isAnyNodeRunning = false;

        // Iterate through the nodes and call evaluate method on them
        foreach (var node in nodes)
        {
            switch (node.Evaluate())
            {
                case NodeState.RUNNING:
                    isAnyNodeRunning = true;
                    break;
                case NodeState.SUCCESS:
                    break;
                case NodeState.FAILURE:
                    currentNodeState = NodeState.FAILURE;
                    return currentNodeState;
                default:
                    break;
            }
        }

        // Check if a noide is running and set the node state accordingly
        if (isAnyNodeRunning)
        {
            currentNodeState = NodeState.RUNNING;
        }
        else
        {
            currentNodeState = NodeState.SUCCESS;
        }
        return currentNodeState;
    }
}

