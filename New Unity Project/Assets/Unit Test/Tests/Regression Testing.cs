using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TestTools;

public class RegressionTesting: Tutorial
{
    /*
    This test is part of regression testing to test after the error has been changed,
    does nextButton display text correctly
    */
    [Test]
    public void testNextButtonDisplayCorrectly() {
        string expected = nextButton.GetComponentInChildren<Text>().text;
        // check if user is on the first tutorial page
        if(counter == 0) {          
            Assert.AreEqual(expected, "Next");
        }
        // otherwise counter will be 1 and user is in second page of tutorial scene
        else {
            Assert.AreEqual(expected, "Start");
        }
    }

    /*
    This test is part of regression testing to test after the error has been changed,
    does previousButton display text correctly
    */
    [Test]
    public void testPreviousButtonDisplayCorrectly() {
        // This time the algorithm is different because
        // if counter is 0, then there are no previousButton, so we can
        // test to see if it equals to null
        // check if user is on the first tutorial page
        if(counter == 0) {          
            Assert.AreEqual(previousButton, null);
        }
        // otherwise counter will be 1 and user is in second page of tutorial scene
        else {
            string expected = previousButton.GetComponentInChildren<Text>().text;
            Assert.AreEqual(expected, "Previous");
        }
    }



    

}
