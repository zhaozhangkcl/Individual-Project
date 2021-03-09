using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// An Data Structure class that encapsulate the gameobject
// inside a Node, therefore provide an reference to modify
// the gameObject through pointer
public class Node
{
	// Adjacency list of a Node is stored as dictionary where
	// the keys are the neighboring node and the value is the weights
	// to that neighbor
	private Dictionary<Node, int> neighbor;	
	// container for the data 
	private string data;

	public Node(string data) {
		this.data = data;
		neighbor = new Dictionary<Node, int>();

	}

	
	public void addNeighbor(Node destination, int weight) {
		// add the destination to its neighbor
		this.neighbor.Add(destination, weight);
	}


	public void setData(string data) {
		this.data = data;
	}

	public string getData() {
		return data;
	}

	public Dictionary<Node, int> getNeighbor() {
		return neighbor;
	}

	
}
