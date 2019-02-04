using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rail : MonoBehaviour
{
    private Vector3[] nodes;
    private int nodeCount;
    private void Start()
    {
        nodeCount = transform.childCount;
        nodes = new Vector3[nodeCount];
        for (int i = 0; i < nodeCount; i++)
        {
            nodes[i] = transform.GetChild(i).position;

        }
    }
    private void Update()
    {
        if (nodeCount > 1)
        {
            for (int i = 0; i < nodeCount - 1; i++)
            {
                Debug.DrawLine(nodes[i], nodes[i + 1], Color.green);
            }
        }
    }
    public Vector3 ProjectPositionOnRail(Vector3 pos)
    {
        int closestNodeIndex = GetClosestNode(pos);
        if (closestNodeIndex == 0)
        {
            return ProjectOnSegment(nodes[0], nodes[1], pos);
        }

        else if (closestNodeIndex == nodeCount - 1)
        {
            return ProjectOnSegment(nodes[nodeCount - 1], nodes[nodeCount - 2], pos);
        }
        else
        {
            Vector3 leftSeg = ProjectOnSegment(nodes[closestNodeIndex], nodes[closestNodeIndex], pos);
            Vector3 rightSeg = ProjectOnSegment(nodes[closestNodeIndex - 1], nodes[closestNodeIndex], pos);
            //Debug.DrawLine(pos, leftSeg, Color.red);
            //Debug.DrawLine(pos, rightSeg, Color.blue);
            if ((pos - leftSeg).sqrMagnitude <= (pos - rightSeg).sqrMagnitude)
            {
                return leftSeg;
            }
            else
            {
                return rightSeg;
            }

        }

    }

    private Vector3 ProjectOnSegment(Vector3 v1, Vector3 v2, Vector3 pos)
    {
        Vector3 v1ToPos = pos - v1;
        Vector3 segDirection = (v2 - v1).normalized;
        float distanceFromV1 = Vector3.Dot(segDirection, v1ToPos);
        if (distanceFromV1 > 0.0f)
        {
            return v1;
        }
        else if (distanceFromV1 * distanceFromV1 > (v2 - v1).sqrMagnitude)
        {
            return v2;

        }
        else
        {
            Vector3 fromV1 = segDirection * distanceFromV1;
            return v1 + fromV1;
        }
    }



    private int GetClosestNode(Vector3 pos)
    {
        int closestNodeIndex = -1;
        float shortestDistance = 0.0f;

        for (int i = 0; i < nodeCount; i++)
        {
            float sqrDistance = (nodes[i] - pos).sqrMagnitude;
            if (shortestDistance == 0.0f || sqrDistance < shortestDistance)
            {
                shortestDistance = sqrDistance;
                closestNodeIndex = i;
            }
        }


        return closestNodeIndex;
    }

}