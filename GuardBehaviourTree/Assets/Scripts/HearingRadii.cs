using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HearingRadii : MonoBehaviour
{
	public float viewRadius;

	public LayerMask targetMask;
	public LayerMask obstacleMask;

	public GuardAI guard = null;

	public bool inRange = false;

	[HideInInspector]
	public List<Transform> targetsInHearingRange = new List<Transform>();

	void Start()
	{
		StartCoroutine("FindTargetsWithDelay", .2f);
	}


	IEnumerator FindTargetsWithDelay(float delay)
	{
		while (true)
		{
			yield return new WaitForSeconds(delay);
			FindTargetsInHearingRange();
		}
	}

	void FindTargetsInHearingRange()
	{
		targetsInHearingRange.Clear();
		Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

		for (int i = 0; i < targetsInViewRadius.Length; i++)
		{
			Transform target = targetsInViewRadius[i].transform;
			Vector3 dirToTarget = (target.position - transform.position).normalized;
			if (Vector3.Angle(transform.forward, dirToTarget) < 350 / 2)
			{
				inRange = true;

				float dstToTarget = Vector3.Distance(transform.position, target.position);

				if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget))
				{
					targetsInHearingRange.Add(target);

					gameObject.GetComponent<GuardAI>().TargetTransform = target;
				}
			}
			else
			{
				inRange = false;
				if(gameObject.GetComponent<FieldOfView>().inRange == false)
                {
					gameObject.GetComponent<GuardAI>().TargetTransform = null;
				}
				
			}
		}
	}


	public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
	{
		if (!angleIsGlobal)
		{
			angleInDegrees += transform.eulerAngles.y;
		}
		return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
	}

}
