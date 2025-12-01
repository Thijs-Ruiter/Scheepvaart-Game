using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    private RouterScript routerScript;

    public Vector2[] nodes;
    public Vector2 endPoint;

    private int targetIndex;
    private int endPointIndex;
    public float speed = 1;

    void Start()
    {
        routerScript = GetComponentInParent<RouterScript>();
        nodes = routerScript.coastlineNodes;
        targetIndex = GetClosestNodeIndex(transform.localPosition);
        endPointIndex = GetClosestNodeIndex(endPoint);
        Debug.Log("TargetIndex: " + targetIndex + ". EndpointIndex: " + endPointIndex);
        if (targetIndex > endPointIndex)
        {
            nodes.Reverse();
        }
    }

    void Update()
    {
        transform.localPosition = Vector2.MoveTowards(transform.localPosition, nodes[targetIndex], speed * Time.deltaTime);
        if (Vector2.Distance(transform.localPosition, nodes[targetIndex]) < 0.01f)
        {
            if (targetIndex == endPointIndex)
            {
                Debug.Log("End Point Reached");
                Destroy(gameObject);
            }
            else
            {
                targetIndex += 1;
            }
        }
    }

    Vector2 ClosestNode()
    {
        Vector2 closestNode = nodes[0];
        for (int i = 0; i < nodes.Length; i++)
        {
            if (Vector2.Distance(transform.localPosition, nodes[i]) < Vector2.Distance(transform.localPosition, closestNode))
            {
                closestNode = nodes[i];
            }
        }
        return closestNode;
    }

    Vector2 ClosestNode(Vector2 pos)
    {
        Vector2 closestNode = nodes[0];
        foreach (Vector2 node in nodes)
        {
            if (Vector2.Distance(pos, node) < Vector2.Distance(pos, closestNode))
            {
                closestNode = node;
            }
        }
        return closestNode;
    }

    int GetClosestNodeIndex(Vector2 pos)
    {
        int closestNodeIndex = 0;
        for (int i = 0; i < nodes.Length; i++)
        {
            if (Vector2.Distance(pos, nodes[i]) < Vector2.Distance(pos, nodes[closestNodeIndex]))
            {
                closestNodeIndex = i;
            }
        }
        return closestNodeIndex;
    }
}
