using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject backGround;
    [SerializeField] private Text distance;

    [SerializeField] private Dropdown startDropDown;
    [SerializeField] private Dropdown destinationDropDown;
    private Graph graph;

    private Node startingNode;

    // a toggle to displaying GUI elements
    private bool isGUIVisible;


    void Start() {
        backGround.SetActive(false);
        graphInit();
        addVertex();
        addEdge();
        isGUIVisible = true;
        addOption();
    }

    public void addOption() {
        // add all of the vertices to start drop down
        foreach(Node room in graph.getVertices()) {
            startDropDown.options.Add(new Dropdown.OptionData() {text=room.getData()});
            // also add all the room to destinationDropDown
            destinationDropDown.options.Add(new Dropdown.OptionData() {text=room.getData()});
        }
    }

    public void graphInit() {
        graph = new Graph();
        
    }

    public void quitGame() {
        Application.Quit();
    }

    public void addEdge() {
        graph.addEdge(graph.getNodeByValue("Main Entrance"), graph.getNodeByValue("Reception"), 3);
        graph.addEdge(graph.getNodeByValue("Reception"), graph.getNodeByValue("Office"), 2);
        graph.addEdge(graph.getNodeByValue("Office"), graph.getNodeByValue("Toilet"), 2);
        graph.addEdge(graph.getNodeByValue("Main Entrance"), graph.getNodeByValue("Exit"), 5);
        graph.addEdge(graph.getNodeByValue("Reception"), graph.getNodeByValue("Exit"), 3);
        graph.addEdge(graph.getNodeByValue("Office"), graph.getNodeByValue("Exit"), 2);
        graph.addEdge(graph.getNodeByValue("Toilet"), graph.getNodeByValue("Exit"), 3);
        graph.addEdge(graph.getNodeByValue("Exit"), graph.getNodeByValue("Lift0"), 2);
        graph.addEdge(graph.getNodeByValue("Main Exit"), graph.getNodeByValue("Lift0"), 5);
        graph.addEdge(graph.getNodeByValue("Lift1"), graph.getNodeByValue("Lift0"), 6);
        graph.addEdge(graph.getNodeByValue("Front Desk"), graph.getNodeByValue("Lift0"), 4);
        graph.addEdge(graph.getNodeByValue("Front Desk"), graph.getNodeByValue("Main Exit"), 8);
        graph.addEdge(graph.getNodeByValue("Lift1"), graph.getNodeByValue("Lecture Room 1"), 4);
        graph.addEdge(graph.getNodeByValue("Lift1"), graph.getNodeByValue("Lecture Room 2"), 8);
        graph.addEdge(graph.getNodeByValue("Lift1"), graph.getNodeByValue("Lecture Room 3"), 4);
        graph.addEdge(graph.getNodeByValue("Lecture Room 3"), graph.getNodeByValue("Lecture Room 1"), 3);
        graph.addEdge(graph.getNodeByValue("Lecture Room 3"), graph.getNodeByValue("Lecture Room 2"), 3);
    }


    public void addVertex() {
        // add all the objects of given name to the collection
        graph.addVertex("Main Entrance");
        graph.addVertex("Reception");
        graph.addVertex("Office");
        graph.addVertex("Toilet");
        graph.addVertex("Lift0"); 
        graph.addVertex("Lift1");
        graph.addVertex("Main Exit");
        graph.addVertex("Exit");
        graph.addVertex("Front Desk");
        graph.addVertex("Lecture Room 1");
        graph.addVertex("Lecture Room 2");
        graph.addVertex("Lecture Room 3");
    }

    
    public void OnClickPlay() {
        isGUIVisible = false;
        backGround.SetActive(true);
    }

    public void OnClickTutorial() {
         SceneManager.LoadScene(sceneName:"Tutorial Scene");
    }
    

  

    public void OnSubmitStart(string startNode) {
        // this function remembers where user is and save in the UIManger script
        startingNode = graph.getNodeByValue(startNode);
    }

    public void OnSubmitDestination(string destinationNode) {
       isGUIVisible = true; 
       StartCoroutine(displayColour(destinationNode));
       StartCoroutine(displayHalo(destinationNode));
    }

    public IEnumerator displayHalo(string destinationNode) {
        Node destination = graph.getNodeByValue(destinationNode);
        // calculate the shortest path and save in a variable
        List<Node> path = Algorithm.findShortestPath(graph, startingNode, destination);
        foreach(Node v in path) {
            GameObject obj = GameObject.Find(v.getData());
            Behaviour halo = (Behaviour)obj.GetComponent("Halo");
            halo.enabled = true;
            yield return new WaitForSeconds(1f);
            halo.enabled = false;
        }
    }

    /*
    Auxiliary function from https://answers.unity.com/questions/8338/how-to-draw-a-line-using-script.html
    that helps to draw a line segment between two points
    */ 
    public void DrawLine(Vector3 start, Vector3 end, Color color)
         {
             GameObject myLine = new GameObject();
             myLine.transform.position = start;
             myLine.AddComponent<LineRenderer>();
             LineRenderer lr = myLine.GetComponent<LineRenderer>();
             lr.material = new Material(Shader.Find("Sprites/Default"));
             lr.startColor = color;
             lr.endColor =color;
             lr.startWidth = 0.5f;
             lr.endWidth = 0.5f;
             lr.SetPosition(0, start);
             lr.SetPosition(1, end);
             GameObject.Destroy(myLine,5f);
         }


    public IEnumerator displayColour(string destinationNode) {
        // set the user-prompt screen off
        backGround.SetActive(false);
        // switch on the green colour of the starting node
        GameObject start = GameObject.Find(startingNode.getData());
        start.GetComponent<Renderer>().material = new Material(Shader.Find("Sprites/Default"));
        start.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
        // switch on the destinationNode colour to red
        Node destination = graph.getNodeByValue(destinationNode);
        GameObject finish = GameObject.Find(destination.getData());
        finish.GetComponent<Renderer>().material = new Material(Shader.Find("Sprites/Default"));
        finish.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        // calculate the shortest path and save in a variable
        List<Node> path = Algorithm.findShortestPath(graph, startingNode, destination);
        // once we have the path, we will make the Node glow in a order to illustrate the direction should be taken
        // first, we need reference to each object, which we already have
        // iterate through the path, turn on the halo property, wait for 1.5 seconds, then turn it off again
        for(int i=0; i<path.Count-1; i++) {
            GameObject obj = GameObject.Find(path[i].getData());
            // draw a line between all vertices in the path
            DrawLine(obj.transform.position, GameObject.Find(path[i+1].getData()).transform.position, Color.blue);
        }
        // wait for 5 seconds and reset the colors
        yield return new WaitForSeconds(5f);
        start.GetComponent<Renderer>().material.SetColor("_Color", Color.black);
        finish.GetComponent<Renderer>().material.SetColor("_Color", Color.black);
        // once the shortest path is calculated, distance should also be update on the left corner
        distance.text = "The Shortest distance to " + destinationNode + " is " + Algorithm.dijkstra(graph, startingNode).shortestDistanceEstimate[destination].ToString() + " meters";
    }

       // label each object in the list 
        void OnGUI() {
            if(isGUIVisible) {
                List<Node> vertices = graph.getVertices();
                foreach(Node v in vertices) {
                    var style = new GUIStyle();
                    style.fontSize = 50;
                    GameObject obj = GameObject.Find(v.getData());
                    Rect display = new Rect(0,0,200,100);           
                    Vector3 location = Camera.main.WorldToScreenPoint(obj.transform.position + new Vector3(0,0,0.5f));
                    display.x = location.x;
                    display.y = Screen.height - location.y - display.height;
                    //GUIStyle border = new GUIStyle(GUI.skin.label);
                    //border.margin = new RectOffset(10,10,10,10);
                    GUI.Label(display, v.getData(), style);          
                }
            }
        }
   
}
