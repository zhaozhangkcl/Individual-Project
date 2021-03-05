using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
The class is not generic, since it is 
only used as subroutine for the main
A* algorithm
*/
public class PriorityQueue
{
    
    // body of the queue, stores in a List
    private List<Node> queue;
    // size of queue
    public int size;

    public PriorityQueue() {
        queue = new List<Node>();
        size = 0;
    }

    public int getParent(int n) {
        if(n == 1) {
            return -1;
        }
        else return n/2;
    }

    public int getLeftChild(int n) {
        return 2*n;
    }

    public int getRightChild(int n) {
        return 2*n + 1;
        
    }

    public void enqueue(Node x) {
        // add a Node to the storage list
        queue.Add(x);
        size++;
        heapifyUp();

    }

    public Node dequeue() {
         if (this.size == 0) {
            return null;
        }
        swap(0, size - 1);
        Node min = queue[size - 1];
        queue.RemoveAt(size-1);
        size--;
        heapifyDown();
        return min;

    }

    private void heapifyDown() {
        // index maintains the root of the tree
        int current = 0;
        int leftChild = this.getLeftChild(current);
        int rightChild = this.getRightChild(current);
        // iterate through the heap and check if condition is valid
        while(canSwap(current, leftChild, rightChild)) {
            // both leftChild and rightChild exist
            if (this.exist(leftChild) && this.exist(rightChild)) {
                if (queue[leftChild].priority < queue[rightChild].priority) {
                    // swap with root with smaller child
                    swap(current, leftChild);
                    // traverse down left subtree
                    current = leftChild;
                }

                else {
                    swap(current, rightChild);
                    current = rightChild;
                }
            }
            else {
                // if we only have leftChild
                swap(current, leftChild);
                current = leftChild;

            }
            leftChild = getLeftChild(current);
            rightChild = getRightChild(current);
        }

    }

    private bool canSwap(int current, int leftChild, int rightChild) {
        return (exist(leftChild) && queue[current].priority > queue[leftChild].priority) || (exist(rightChild) && queue[current].priority > queue[rightChild].priority);
    }

    private bool exist(int index) {
        return index < size && index >= 0;
    }

    // recursive algorithm to restore heap property
    private void heapifyUp() {
       // an iterating index variable that is used to loop through the tree
       int current = size - 1;
       // compare two node's priority
       while(current > 1 && queue[current].priority < queue[getParent(current)].priority) {
           swap(current, getParent(current));
           current = getParent(current);
       }
    }

    private void swap(int a, int b) {
        Node temp = queue[a];
        queue[a] = queue[b];
        queue[b] = temp;
    }

    


    
}
