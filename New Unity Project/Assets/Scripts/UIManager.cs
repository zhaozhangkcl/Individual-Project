using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject canva;
    private Graph graph;

    private Node startingNode;

    void Start() {
        graphInit();
        addVertex();
        addEdges();
    }

    public void graphInit() {
        graph = new Graph();
        
    }

    public void addEdges() {
        graph.addEdge(graph.getVertexByValue("Main Entrance"), graph.getVertexByValue("Reception"), 5);
        graph.addEdge(graph.getVertexByValue("Main Entrance"), graph.getVertexByValue("Exit"), 8);
        graph.addEdge(graph.getVertexByValue("Reception"), graph.getVertexByValue("Stair"), 2);
        graph.addEdge(graph.getVertexByValue("Stair"), graph.getVertexByValue("Toilet"), 2);
        graph.addEdge(graph.getVertexByValue("Stair"), graph.getVertexByValue("Exit"), 7);
        graph.addEdge(graph.getVertexByValue("Exit"), graph.getVertexByValue("North Wing Exit"), 3);
        graph.addEdge(graph.getVertexByValue("North Wing Exit"), graph.getVertexByValue("Main Exit"), 10);
    }


    public void addVertex() {

        // add all the objects of given name to the collection
        graph.getVertices().Add(new Node("North Wing Exit"));
        graph.getVertices().Add(new Node("Main Exit"));
        graph.getVertices().Add(new Node("Exit"));
        graph.getVertices().Add(new Node("Main Entrance"));
        graph.getVertices().Add(new Node("Toilet"));
        graph.getVertices().Add(new Node("Reception"));
        graph.getVertices().Add(new Node("Stair"));      
    }

    

  

    public void OnSubmitStart(string startNode) {
        // this function remembers where user is and save in the UIManger script
        startingNode = graph.getVertexByValue(startNode);
    }

    public void OnSubmitDestination(string destinationNode) {
        canva.SetActive(false);
        Node destination = graph.getVertexByValue(destinationNode);
        // calculate the shortest path and save in a variable
        List<Node> path = Algorithm.shortestPathBetween(graph, startingNode, destination);
        // once we have the path, we will make the Node glow in a order to illustrate the direction should be taken
        // first, we need reference to each object, which we already have
        // iterate through the path, turn on the halo property, wait for 1.5 seconds, then turn it off again
        foreach(Node v in path) {
            Debug.Log(v.getData());
            GameObject obj = GameObject.Find(v.getData());
            Debug.Log(obj);
            // Display a text message UI on the game screen showing the shortest path
            // reference the UI
        }
    }
   
}
