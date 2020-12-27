using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge : MonoBehaviour
{
    private Node start;
	private Node end;
	private int weight;
	
	public Edge(Node start, Node end, int inputWeight) {
		this.start = start;
		this.end = end;
		this.weight = inputWeight;
	}
	
	public Node getStart() {
		return this.start;
	}
	
	public Node getEnd() {
		return this.end;
	}
	
	public int getWeight() {
		return this.weight;
	}
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
