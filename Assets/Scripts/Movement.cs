using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public Vector2[] nodes;
    private Vector2 target;
    public Vector2 endPoint;

    private int targetIndex;
    private int endPointIndex;
    public float speed = 5;

    void Start()
    {
        target = ClosestNode();
        targetIndex = Array.BinarySearch(nodes, target);
        endPointIndex = Array.BinarySearch(nodes, endPoint);
        if (targetIndex < endPointIndex)
        {
            nodes.Reverse();
        }
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, target) < 0.01f)
        {
            if (targetIndex == endPointIndex)
            {
                Debug.Log("End Point Reached");
                Destroy(gameObject);
            }
            targetIndex += 1;
            target = nodes[targetIndex];
        }
    }

    Vector2 ClosestNode()
    {
        Vector2 closestNode = nodes[0];
        foreach (Vector2 node in nodes)
        {
            if (Vector2.Distance(transform.position, node) < Vector2.Distance(transform.position, closestNode))
            {
                closestNode = node;
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
}
