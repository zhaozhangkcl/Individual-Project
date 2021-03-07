using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
A ordered list implementation of Priority Queue, the 
minimum element i.e. Node with smallest priority is stored
at A[0] position of the list. Each enqueue operation is followed
by sort operation. The sorting algorithm used is quicksort due to its 
fast average time. The quick sort is also implemented in this class
as auxiliary function
Since the class itself act as auxiliary class to the dijkstra's algorithm, the
implementation does not need to be generic and can only store data type of Node
*/
public class PriorityQueue
{ 
    // the underlying storage is a list
    private List<Node> queue;


    public void enqueue(Node node) {
        queue.Add(node);
        bubbleSort(queue);
    }

    /*
    delete and return the first element
    */
    public Node dequeue() {
        // save the first element in a temporary variable
        Node temp = queue[0];
        queue.RemoveAt(0);
        return queue[0];
    }

    /*
 
    */
    public void bubbleSort(List<Node> queue) {
        for(int i = 0; i < queue.Count; i++) {
            for(int j = queue.Count -1; j > i; j--) {
                if(queue[j].getPriority() < queue[j-1].getPriority()) {
                    Node temp = queue[j];
                    queue[j] = queue[j-1];
                    queue[j-1] = temp;
                }
            }
        }
    }


    
    

    


    
}
