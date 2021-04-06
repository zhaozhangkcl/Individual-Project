using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DropDownLogic : MonoBehaviour
 {
     [SerializeField]
     private InputField startInputField;
     [SerializeField] private InputField destinationInputField;
 
     [SerializeField]
     private Dropdown startDropDown;
     
     [SerializeField]
     private Dropdown destinationDropDown;
     private List<Dropdown.OptionData> startdropdownOptions;
     private List<Dropdown.OptionData> destinationdropdownOptions;
     
     private void Start()
     {
         startdropdownOptions = startDropDown.options;
         destinationdropdownOptions = destinationDropDown.options;
         removeDefaultOption();
     }    
     public void FilterStartDropdown(string data)
     {
         startDropDown.options = startdropdownOptions.FindAll(word => word.text.IndexOf(data) >= 0);
         
     }

     public void FilterDestinationDropDown(string data) {
         destinationDropDown.options = destinationdropdownOptions.FindAll(word => word.text.IndexOf(data) >= 0);
     }

     public void removeDefaultOption() {
         for(int i = 0; i < destinationdropdownOptions.Count; i++){
             if(destinationdropdownOptions[i].text.Contains("Option")) {
                 destinationdropdownOptions.RemoveAt(i);
             }
             if(startdropdownOptions[i].text.Contains("Option")) {
                 startdropdownOptions.RemoveAt(i);
             }
         }
     }

 }
