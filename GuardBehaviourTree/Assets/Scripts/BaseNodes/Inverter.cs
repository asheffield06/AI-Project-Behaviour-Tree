using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inverter : Node
{
	// Only one node to modify
	protected Node node;

	public Inverter(Node node)
	{
		this.node = node;
	}

	// Flips the success and failure of the node
	public override NodeState Evaluate()
	{
		// Switch by the evaulation of the node
		switch (node.Evaluate())
		{
			case NodeState.RUNNING:
				currentNodeState = NodeState.RUNNING;
				break;
			case NodeState.SUCCESS:
				currentNodeState = NodeState.FAILURE;
				break;
			case NodeState.FAILURE:
				currentNodeState = NodeState.SUCCESS;
				break;
			default:
				break;
		}
		return currentNodeState;
	}
}
