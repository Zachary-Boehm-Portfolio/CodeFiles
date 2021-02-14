using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField]private GridBuilder grid;
    [SerializeField]private MapBuilder map;

    private Camera camera;

    [SerializeField]private GameObject TextDisplay;//reference to the display used for the text portion of generation

    private void Start() 
    {
        //Initialize the reference variables to the Gridbuilder and MapBuilder Scripts
        grid = Manager._GM.grid;
        map = Manager._GM.map;
        camera = Camera.main;
    }
    public void ProgressStage()
    {
        switch(Manager._GM.GenerationStage)
        {
            case 0: //Generate the roads for the map
                Manager._GM.GenerationStage++;
                if(Manager._GM.RoadsParent.transform.childCount > 0)
                {
                    foreach(Transform child in Manager._GM.RoadsParent.transform)
                    {
                       Destroy(child.gameObject);
                    }
                }
                if(Manager._GM.BuildingsParent.transform.childCount > 0)
                {
                    foreach(Transform child in Manager._GM.BuildingsParent.transform)
                    {
                       Destroy(child.gameObject);
                    }
                }
                grid.GenerateRoads();
                MoveCamera();
                map.SpawnRoads();
            break;
            case 1: //Generate the buildings and extras for the grid
                //Set the next step button to not interactable
                Manager._GM.NextStep.interactable = false;
                map.SpawnBuildings();
                Manager._GM.GenerationStage++;
            break;
            default: //Default case
            break;
        }
    }
    public void StartGeneration()
    {
        //TextDisplay.SetActive(true);
        Manager._GM.GenerationStage = 0;
    }
    private void MoveCamera()
    {
        int height = Mathf.Max(grid.GetFinalGrid().Count, grid.GetFinalGrid()[0].Count); 
        camera.transform.position = new Vector3(grid.GetFinalGrid().Count * 2, height * 4, grid.GetFinalGrid()[0].Count * 2);
    }
}
