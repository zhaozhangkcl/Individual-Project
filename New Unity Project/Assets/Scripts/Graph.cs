using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph
{
    // A instance variable for holding list of Vertex
    private List<Node> vertices;
   
   public Graph() {
       vertices =  new List<Node>();
   }

   public Node addVertex(string data) {
		Node newVertex = new Node(data);
		this.vertices.Add(newVertex);

		return newVertex;
	}

    public void addEdge(Node vertex1, Node vertex2, int weight) {

		vertex1.addEdge(vertex2, weight);		
		vertex2.addEdge(vertex1, weight);
		
	}

	public void removeEdge(Node vertex1, Node vertex2) {
		vertex1.removeEdge(vertex2);
		vertex2.removeEdge(vertex1);
	}

	public void removeVertex(Node vertex) {
		this.vertices.Remove(vertex);
	}

	public List<Node> getVertices() {
		return this.vertices;
	}


	public Node getVertexByValue(string value) {
		foreach(Node v in vertices) { 
			if (v.getData() == value) {
				return v;
			}
		}

		return null;
	}

}
