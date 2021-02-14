using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    //!This section creates a static public instance of this class
    public static Manager _GM = null; //declare and initialize a Manager variable
    private void Awake() 
    {
        if(_GM == null)
        {
            GameObject Map = GameObject.Find("Map"); //Reference to the Map GameObject in the level
            grid = Map.GetComponent<GridBuilder>(); //Referenct to the GridBuilder Script inside of the scene
            map = Map.GetComponent<MapBuilder>();   //Reference to the MapBuilder Script inside of the scene

            MapGrid = Map.GetComponent<Grid>(); //Reference to the Grid Component that is on the Map GameObject

            RoadsParent = Map.transform.GetChild(0); // Reference to the transform of the container GameObject for the instantiated roads
            BuildingsParent = Map.transform.GetChild(1); // Reference to the transform of the container GameObject for the instantiated buildings
            _GM = this;
        }
        else if (_GM != this)
        {
            Destroy(gameObject);
        }
    }
    //!End creation of static public instance of this class


    //!Start of global variables
    [Header("Input Logic")]
    public TMP_InputField[] InputFields; //Will grab and hold the values from the UI
    public int GenerationStage = 0; //Stage of the generation process which progresses with user input

    [Header("Reference Variables")]
    public GridBuilder grid; //Reference to the driver for the grid builder
    public MapBuilder map; //Reference to the driver for generating the map

    public Button NextStep; //Reference to the next step button so the interactable state can be changed

    public Grid MapGrid; //Reference to the grid component on the Map GameObject

    public Transform RoadsParent; //Holds the transform for the roads container
    public Transform BuildingsParent; //Holds the transform for the buildings container

    [Header("Databases")]
    public GameObject[] Roads;//Database(list) of roads that will be used to instantiate the map roads
    public District[] DistrictBuildings; //Reference to a District which holds a name for the district along with the associated buildings
    public GameObject[] Parks; //Holds reference to the different types of parks and filler objects
    
}

//Local class built so that the 2d array is shown inside of the inspector. Ment to hold the distric specific buildings
[System.Serializable]
public class District
{
    public string Name = "";
    public GameObject[] Buildings;
}
