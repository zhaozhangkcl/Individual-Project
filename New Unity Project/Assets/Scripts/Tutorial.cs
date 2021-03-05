using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Tutorial : MonoBehaviour
{
    [SerializeField] private Text tutorialText;
    [SerializeField] private Button nextButton;
    
    [SerializeField] private Button previousButton;
    [SerializeField] private Sprite sprite;

    // counter to see which tutorial is on
    private int counter;
    // Start is called before the first frame update
    void Start()
    {
        previousButton.gameObject.SetActive(false);
    }

    public void OnClickNext() {
        // Change the text of tutorial when clicked
        if(counter == 0) {
        tutorialText.text = "You can type in the destination location of the place you want to go and it will return a direction for you";
        nextButton.GetComponentInChildren<Text>().text = "Start";
        previousButton.gameObject.SetActive(true);
        counter++;
        }
        else {
            // after start of the game, this button should be deactivate
            nextButton.gameObject.SetActive(false);
            previousButton.gameObject.SetActive(false);
            transit();
        }

    }

    public void OnClickPrevious() {
        // if counter is 0, then previous button should deactivated
        
        // if the counter is 1, clicking on previous change text back to welcome text
        if(counter == 1) {
        tutorialText.text = "Welcome to King's College London.\nCongradulations on getting into one of the best Universities in the world.\nDon't worry, with this app, you will be able to find your way around campuses.\nThis tutorial is designed to help you use the app.";
        previousButton.gameObject.SetActive(false);
        counter--;
        }
    }

    /*
    This function is used to transit from tutorial
    scene to the main scene.
    */
    public void transit() {
        SceneManager.LoadScene(sceneName:"MainScene");
    }

}
