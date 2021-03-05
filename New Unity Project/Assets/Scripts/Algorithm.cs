using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
/*
the class takes a graph 
and a current location and 
destination as input. It returns 
the shortest distance and also a direction
path to the location
*/
public class Algorithm
{
    // priority queue data structure for algorithm
    public static (Dictionary<string,int>, Dictionary<string, Node>) dijkstra(Graph g, Node startingVertex) {
        Dictionary<string, int> distances = new Dictionary<string, int>();
        Dictionary<string, Node> previous = new Dictionary<string, Node>();
        PriorityQueue q = new PriorityQueue();
        q.enqueue(startingVertex);
        foreach(Node v in g.getVertices()) {
            if(v != startingVertex) {
                // initilise the distances dictionary
                distances.Add(v.getData(), int.MaxValue);
            }
            previous.Add(v.getData(), null);
        }
        distances.Add(startingVertex.getData(), 0);
        while(q.size != 0) {
            Node current = q.dequeue();
            foreach(Edge e in current.getEdges()) {
                int estimate = distances[current.getData()] + e.getWeight();
                string neighborValue = e.getEnd().getData();
                if(estimate < distances[neighborValue]) {
                    // check if key are already contained
                    if(distances.ContainsKey(neighborValue)) {
                        distances[neighborValue] = estimate;
                        previous[neighborValue] = current;
                        e.getEnd().priority = distances[neighborValue];
                        q.enqueue(e.getEnd());
                    }

                    else {
                    distances.Add(neighborValue, estimate);
                    previous.Add(neighborValue, current);
                    e.getEnd().priority = distances[neighborValue];
                    q.enqueue(e.getEnd());
                    }

                }
            }
            
        }
        return (distances, previous);

    }

     public static List<Node> shortestPathBetween(Graph g, Node startingVertex, Node targetVertex) {
        var dijkstraDicts = dijkstra(g, startingVertex);
        Dictionary<string, int> distances = dijkstraDicts.Item1;
        Dictionary<string, Node> previous = dijkstraDicts.Item2;
        int distance = (int) distances[targetVertex.getData()];
        List<Node> path = new List<Node>();
        // Node v = targetVertex;
        Node v = new Node("Toilet");
        if(targetVertex == null) {
            Debug.Log("targetVertex is null!");
        }
        while(v != null){
            path.Insert(0,v);
            v = (Node) previous[v.getData()];
        }
        return path;
     }
  
    
}
