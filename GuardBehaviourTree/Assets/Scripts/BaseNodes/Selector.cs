using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : Node
{
    // Refrence to the child nodes for evaluation
    protected List<Node> nodes = new List<Node>();

    // Constructor using list of nodes as a paramter
    public Selector(List<Node> nodes)
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
                    currentNodeState = NodeState.RUNNING;
                    return currentNodeState;
                case NodeState.SUCCESS:
                    currentNodeState = NodeState.SUCCESS;
                    return currentNodeState;
                case NodeState.FAILURE:
                    break;
                default:
                    break;
            }
        }
        // If code gets to this point we know child nodes are failures so we set the state to failure and return 
        currentNodeState = NodeState.FAILURE;
        return currentNodeState;
    }
}

