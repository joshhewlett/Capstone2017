using System.Collections;
using System.Collections.Generic;
using HoloToolkit.Unity.InputModule;
using UnityEngine;

//TODO: determine how to check which panel is currently showing
public class DropdownPanel : MonoBehaviour {

    public struct panel
    {
        int pageNumber;
        int totalItems;
        bool isActive;
        int totalPages;
    }
    // private static implicit operator panel(string value) {
    // return new panel() { s = value, length = value.Length };
    // }

    private enum PageDirections
    {
        PageUp,
        PageDown
    };

    public panel objPanel;
    public panel slidePanel;
    public panel presentationPanel;

    public GameObject upArrow, downArrow;
    public GameObject[] tiles;
    public int gridSize = 2 * 2;

    void Start(){
        //TODO: Link to slide manager for data values
        //FIXME: Structs are failing to access and am unsure why
        // tiles = new GameObject[4];

        // objPanel.pageNumber = 1;
        // objPanel.totalItem = 4;
        // objPanel.totalPages = 0;
        // objPanel.isActive = false;

        // slidePanel.pageNumber = 1;
        // slidePanel.totalItem = 4;
        // slidePanel.totalPages = 1;
        // slidePanel.isActive = false;

        // presentationPanel.pageNumber = 1;
        // presentationPanel.totalItem = 4;
        // presentationPanel.totalPages = 1;
        // presentationPanel.isActive = false;
    }

    void Update() {
        
    }

    public void CalculateActiveThumbnails(panel currentPanel) {

    }
    //FIXME: Compiler complaining about struct access layer
    public void CheckPageButtons(panel currentPanel) {
        // switch(currentPanel.pageNumber){
        //     case 1:
        //         upArrow.SetActive(false);
        //         downArrow.SetActive(true);
        //         break;
        //     case currentPanel.totalPages:
        //         upArrow.SetActive(true);
        //         downArrow.SetActive(false);
        //         break;
        //     default:
        //         upArrow.SetActive(true);
        //         downArrow.SetActive(true);
        //         break;
        // }
    }

    public void ParseThumbnail() {

    }
}