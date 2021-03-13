using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject backGround;
    [SerializeField] private Text distance;

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

    
    public void OnClickPlay() {
        backGround.SetActive(true);
    }

    public void OnClickTutorial() {
        
    }
    

  

    public void OnSubmitStart(string startNode) {
        // this function remembers where user is and save in the UIManger script
        startingNode = graph.getNodeByValue(startNode);
    }

    public void OnSubmitDestination(string destinationNode) {
       delay(destinationNode);
    }

    /*
    Auxiliary function from https://answers.unity.com/questions/8338/how-to-draw-a-line-using-script.html
    that helps to draw a line segment between two points
    */
    public void DrawLine(Vector3 start, Vector3 end, Color color, float duration = 0.2f)
         {
             GameObject myLine = new GameObject();
             myLine.transform.position = start;
             myLine.AddComponent<LineRenderer>();
             LineRenderer lr = myLine.GetComponent<LineRenderer>();
             lr.startColor = color;
             lr.endColor =color;
             lr.startWidth = 0.5f;
             lr.endWidth = 0.5f;
             lr.SetPosition(0, start);
             lr.SetPosition(1, end);
            // GameObject.Destroy(myLine, duration);
         }

    public void delay(string destinationNode) {
        // set the user-prompt screen off
        backGround.SetActive(false);
        // switch on the green colour of the starting node
        GameObject start = GameObject.Find(startingNode.getData());
        start.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
        // switch on the destinationNode colour to red
        Node destination = graph.getNodeByValue(destinationNode);
        GameObject finish = GameObject.Find(destination.getData());
        finish.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        // calculate the shortest path and save in a variable
        List<Node> path = Algorithm.findShortestPath(graph, startingNode, destination);
        // once we have the path, we will make the Node glow in a order to illustrate the direction should be taken
        // first, we need reference to each object, which we already have
        // iterate through the path, turn on the halo property, wait for 1.5 seconds, then turn it off again
        for(int i=0; i<path.Count-1; i++) {
            Debug.Log(path[i].getData());
            GameObject obj = GameObject.Find(path[i].getData());
            Debug.Log(obj);
            // draw a line between all vertices in the path
            DrawLine(obj.transform.position, GameObject.Find(path[i+1].getData()).transform.position, Color.blue);
        }
        // once the shortest path is calculated, distance should also be update on the left corner
        distance.text = "The Shortest distance to " + destinationNode + " is " + Algorithm.dijkstra(graph, startingNode).shortestDistanceEstimate[destination].ToString();
    }
   
}
