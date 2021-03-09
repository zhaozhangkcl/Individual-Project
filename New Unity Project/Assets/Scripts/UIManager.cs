using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject backGround;
    [SerializeField] private Text distance;
    [SerializeField] private Text displayPath;
     private Graph graph;

    private Node startingNode;

    void Start() {
        graphInit();
        addVertex();
        addEdge();
    }

    public void graphInit() {
        graph = new Graph();
        
    }

    public void addEdge() {
        graph.addEdge(graph.getNodeByValue("Main Entrance"), graph.getNodeByValue("Reception"), 3);
        graph.addEdge(graph.getNodeByValue("Reception"), graph.getNodeByValue("Stair"), 2);
        graph.addEdge(graph.getNodeByValue("Stair"), graph.getNodeByValue("Toilet"), 2);
        graph.addEdge(graph.getNodeByValue("Main Entrance"), graph.getNodeByValue("North Wing Exit"), 5);
        graph.addEdge(graph.getNodeByValue("Reception"), graph.getNodeByValue("North Wing Exit"), 3);
        graph.addEdge(graph.getNodeByValue("Stair"), graph.getNodeByValue("North Wing Exit"), 2);
        graph.addEdge(graph.getNodeByValue("Toilet"), graph.getNodeByValue("North Wing Exit"), 3);
    }


    public void addVertex() {
        // add all the objects of given name to the collection
        graph.addVertex("Main Entrance");
        graph.addVertex("Reception");
        graph.addVertex("Stair");
        graph.addVertex("Toilet");
        graph.addVertex("North Wing Exit");   
    }

    

    

  

    public void OnSubmitStart(string startNode) {
        // this function remembers where user is and save in the UIManger script
        startingNode = graph.getNodeByValue(startNode);
    }

    public void OnSubmitDestination(string destinationNode) {
       delay(destinationNode);
    }

    public void delay(string destinationNode) {
         backGround.SetActive(false);
        Node destination = graph.getNodeByValue(destinationNode);
        // calculate the shortest path and save in a variable
        List<Node> path = Algorithm.findShortestPath(graph, startingNode, destination);
        // once we have the path, we will make the Node glow in a order to illustrate the direction should be taken
        // first, we need reference to each object, which we already have
        // iterate through the path, turn on the halo property, wait for 1.5 seconds, then turn it off again
        displayPath.text = "";
        foreach(Node v in path) {
            Debug.Log(v.getData());
            GameObject obj = GameObject.Find(v.getData());
            Debug.Log(obj);
            // Display a text message UI on the game screen showing the shortest path
            // reference the UI
            displayPath.text += v.getData() + ", ";

        }
        // once the shortest path is calculated, distance should also be update on the left corner
        distance.text = "The Shortest distance to " + destinationNode + " is " + Algorithm.dijkstra(graph, startingNode).shortestDistanceEstimate[destination].ToString();
    }
   
}
