using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class UnitTest
    {
        // A Test behaves as an ordinary method
        [Test]
        public void UnitTestSimplePasses()
        {
            // Use the Assert class to test conditions
        }

        /*
        This unit test is very similiar to the test for distance
        Since we are doing unit testing, its good idea to test for different 
        things in a separate function
        */
        [UnityTest]
        public IEnumerator PathEqualToTarget() {
            Graph g = new Graph();
            g.addVertex("Start");
            g.addVertex("Finish");
            g.addVertex("Intermediate");
            g.addEdge(g.getVertexByValue("Start"), g.getVertexByValue("Finish"), 10);
            g.addEdge(g.getVertexByValue("Start"), g.getVertexByValue("Intermediate"), 5);
            g.addEdge(g.getVertexByValue("Intermediate"), g.getVertexByValue("Finish"), 4);
            // Now since we are after the shortest path - which is a collection of Nodes, we are going to use
            // shortestPathBetween function and check if our list is equals to start-intermediate-finish
            List<Node> shortestPath = Algorithm.shortestPathBetween(g, g.getVertexByValue("Start"), g.getVertexByValue("Finish"));
            List<Node> target = new List<Node>() {g.getVertexByValue("Start"), g.getVertexByValue("Intermediate"), g.getVertexByValue("Finish")};
            CollectionAssert.AreEqual(shortestPath, target);
            yield return null;
        }

        /*
        This unit test's aim is just to test the functionality
        of Algorithm we are using for shortest path calculations.
        The test should pass given a graph with collection of nodes
        Note this test does not use the actual graph in the game since
        we are going to test that separately in Integration testing when
        we want to see if Model-View-Controller are working correctly
        */
        [UnityTest]
        public IEnumerator DistanceEqualToTarget()
        {
            // the truth asserts if final calculated distance 
            // should equals to distance returned by algorithm
            // thus proving algorithm return the correct distance
            // initialise a graph
            Graph g = new Graph();
            // add three vertex into the graph, since we only wants to find out
            // if algorithm return correct shortest path distance
            g.addVertex("Start");
            g.addVertex("Finish");
            g.addVertex("Intermediate");
            // now we are going to add edges, to test the algorithm, we test the case
            // when path going through intermediate is shorter than the direct path
            // between start and finish
            g.addEdge(g.getVertexByValue("Start"), g.getVertexByValue("Finish"), 10);
            g.addEdge(g.getVertexByValue("Start"), g.getVertexByValue("Intermediate"), 5);
            g.addEdge(g.getVertexByValue("Intermediate"), g.getVertexByValue("Finish"), 4);
            // Now we can calculate the shortest distance between 
            // start and finish which we know prior hand is 9
            Dictionary<string, int> shortestDistanceDictionary = Algorithm.dijkstra(g, g.getVertexByValue("Start")).Item1;
            int shortestDistance = shortestDistanceDictionary["Finish"];
            // use assert function to check if shortestDistane equals to 9
            Assert.AreEqual(shortestDistance, 9);
            yield return null;
        }

        

    }
}
