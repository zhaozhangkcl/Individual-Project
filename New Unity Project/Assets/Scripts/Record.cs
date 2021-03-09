using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
The record container class encapsulate the 2 dictionationaries which keep track of both the distance and 
parent node of each vertex
*/
public class Record 
{
    public Dictionary<Node, int> shortestDistanceEstimate;
    public Dictionary<Node, Node> parent;

    public Record() {
        shortestDistanceEstimate = new Dictionary<Node, int>();
        parent = new Dictionary<Node, Node>();
    }

}
