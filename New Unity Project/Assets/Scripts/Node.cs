using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// An Data Structure class that encapsulate the gameobject
// inside a Node, therefore provide an reference to modify
// the gameObject through pointer
public class Node
{
	// An important property of Node that will be used 
	// to represent its distance to source Node
	// in the dijkstra's algorithm
    public int priority;
    // name of the object on the graph
    private string data;
    // Adjacency list to track what are the neighbour of this node
    private List<Edge> edges;

	// An reference to gameObject it is pointing
	public GameObject currentGameObject;
    public Node(string inputData) {
		this.data = inputData;
		this.edges = new List<Edge>();
		this.priority = 0;
		currentGameObject = GameObject.Find(inputData);
	}
    
    public void addEdge(Node endVertex, int weight) {
		this.edges.Add(new Edge(this, endVertex, weight));
	}

    public void removeEdge(Node endVertex) {
		foreach(Edge edge in edges) {
            if(edge.getEnd() == endVertex) {
                edges.Remove(edge);
            }
        }
	}

	public string getData() {
		return this.data;
	}

	public List<Edge> getEdges(){
		return this.edges;
	}

    

}
