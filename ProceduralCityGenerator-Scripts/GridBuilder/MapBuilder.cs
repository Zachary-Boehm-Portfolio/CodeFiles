using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBuilder : MonoBehaviour
{
    GridBuilder grid; //Hold a reference to the grid builder script on the Map GameObject
    private void Start() 
    {
        grid = Manager._GM.grid; //Initialize the grid reference variable
    }

    /*
        This function is a driver function that will take the list of road combination values from the GridBuilder script and spawn
        the corresponding road type into that point onto the grid component of the Map GameObject
    */
    public void SpawnRoads()
    {
        //Set the next step button to none interactable while this step is in progress
        Manager._GM.NextStep.interactable = false;

        List<List<string>> Grid = grid.GetFinalGrid();//Hold a reference to the final grid.
        GameObject[] Roads = Manager._GM.Roads;
        //Iterate through the FinalGrid road combination values (x)
        for(int x = 0; x < Grid.Count; x++)
        {
            //Iterate through the FinalGrid road combination values (y)
            for(int y = 0; y < Grid[x].Count; y++)
            {
                //If value is a road
                if(Grid[x][y] != "")
                {
                   //Find the correct road prefab
                    for(int i = 0; i < Roads.Length; i++)
                    {
                        if(Roads[i].name == Grid[x][y])
                        {
                            //Instantiate a road GameObject where name == combination put into the grid component at (x , y) of iteration (grid cell size = 4x4)
                            GameObject temp = Instantiate(Roads[i], new Vector3(x * 4, 0, y * 4), Roads[i].transform.rotation) as GameObject;
                            //Assign the new objects parent transform the Roads container object
                            temp.transform.parent = Manager._GM.RoadsParent;
                        }
                    }
                    
                }
            }
        }

        //Set the next step button to interactable
        Manager._GM.NextStep.interactable = true;
    }

    /*
        This function is a driver function. When called it will proceduraly generate the buildings and fill in the squares.
        Before generating the buildings the algorithm will first assign each square with a random district
        Then the algorith will go from square to square where it will generate buildings on each point.
        The algorithm, with a set amount of iterations, will try and place random buildings; any empty space after this will be filled with parks
    */
    public void SpawnBuildings()
    {
        //List of districts that can be spawned
        string[] districts = {"Housing", "Industry", "Commerce"};
        //Grab a reference to the squares
        List<Square> squares = grid.GetSquares();
        //Grab a reference to grid of values
        List<List<string>> finalgrid = grid.GetFinalGrid();
        //Reference to the types of buildings
        District[] Buildings = Manager._GM.DistrictBuildings;


        //Generate random districts for each square
        for(int i = 0; i < squares.Count; i++)
        {
            squares[i].District = districts[UnityEngine.Random.Range(0, districts.Length)];
            Debug.Log("Square " + i + " has a district of " + squares[i].District);
        }

        int iterations = Convert.ToInt32(Manager._GM.InputFields[3].text);
        //Iterate through all the squares
        for(int i = 0; i < squares.Count; i++){
            //Loop for a set amount of times
            for(int loop = 0; loop < iterations; loop++){
                
                //Iterate through all the values (y)
                for(int y = (int)squares[i].BL.y; y < (int)squares[i].TL.y;y++){
                    //Iterate through all the value (x)
                    for(int x = (int)squares[i].BL.x; x < (int)squares[i].BR.x;x++){
                        //If (x , y) of FinalGrid == ""
                        if(finalgrid[x][y] == ""){
                            //Choose a building at random from list of buildings from squares district
                            GameObject randBuilding = null;
                            for(int j = 0; j < Buildings.Length; j++){
                                if(Buildings[j].Name == squares[i].District){
                                    randBuilding = Buildings[j].Buildings[UnityEngine.Random.Range(0, Buildings[j].Buildings.Length)];
                                }
                            }

                            //Hold the width and height of the building - First 3 characters of building name is the dimensions
                            string dimension = randBuilding.name.Substring(0, 3); //In the format of (axb)
                            //If larger than 1x1 building
                            if(dimension != "1x1"){
                                //Choose random orientation of either horizontal or vertical
                                int direction = 1;
                                //If horizontal
                                if(direction == 1){
                                    //If area is valid
                                    bool ValidSpace = true;
                                    int width = Convert.ToInt32(dimension.Substring(0, 1)); //Grab the width from the (axb)
                                    int height = Convert.ToInt32(dimension.Substring(2, 1)); // Grab the height form the (axb)
                                    
                                    //check if there is enough space for the buildings width
                                    if(width > 1){
                                        if(finalgrid[x + 1][y] != "")
                                        {
                                            ValidSpace = false;
                                        }
                                    }
                                    //check if ther is enough space for the buildings height. If the width already failed then just skip
                                    if(height > 1 && ValidSpace)
                                    {
                                        if(finalgrid[x][y + 1] != "")
                                        {
                                            ValidSpace = false;
                                        }
                                    }
                                    if(ValidSpace)
                                    {
                                        //Instantiate building with correct orientation and set transform to (x , y) of grid component
                                        GameObject NewBuilding = Instantiate(randBuilding, new Vector3(x * 4, 0, y * 4), randBuilding.transform.rotation) as GameObject;
                                        //Set parent transform to Buildings container object
                                        NewBuilding.transform.parent = Manager._GM.BuildingsParent;
                                        //Set value of (x , y) of Finish Grid to "D" for done on all points taken up by the building
                                        for(int w = 0; w < width; w++)
                                        {
                                            for(int h = 0; h < height; h++)
                                            {
                                                finalgrid[x + w][y + h] = "D";
                                            }
                                        }
                                    }
                                }

                                //!Potential for adding different directions for buildings

                                // //If Vertical
                                // else if(direction == 0){
                                //     //If area is valid
                                //     bool ValidSpace = true;
                                //     int height = Convert.ToInt32(dimension.Substring(0, 1)); //Grab the width from the (axb)
                                //     int width = Convert.ToInt32(dimension.Substring(2, 1)); // Grab the height form the (axb)
                                //     //check if there is enough space for the buildings width
                                //     if(width > 1){
                                //         if(finalgrid[x + 1][y] != "")
                                //         {
                                //             ValidSpace = false;
                                //         }
                                //     }
                                //     //check if ther is enough space for the buildings height. If the width already failed then just skip
                                //     if(height > 1 && ValidSpace)
                                //     {
                                //         if((y - 1) > -1)
                                //         {
                                //             if(finalgrid[x][y - 1] != "")
                                //             {
                                //                 ValidSpace = false;
                                //             }
                                //         }
                                //     }
                                //     if(ValidSpace)
                                //     {
                                //         //Instantiate building with correct orientation and set transform to (x , y) of grid component
                                //         GameObject NewBuilding = Instantiate(randBuilding, new Vector3(x * 4, 0, y * 4), Quaternion.Euler(new Vector3(randBuilding.transform.rotation.x, randBuilding.transform.rotation.y + 90, randBuilding.transform.rotation.z))) as GameObject;
                                //         //Set parent transform to Buildings container object
                                //         NewBuilding.transform.parent = Manager._GM.BuildingsParent;
                                //         //Set value of (x , y) of Finish Grid to "D" for done on all points taken up by the building
                                //         for(int w = 0; w < width; w++)
                                //         {
                                //             for(int h = 0; h > (x - height); h--)
                                //             {
                                //                 finalgrid[x + w][y + h] = "D";
                                //             }
                                //         }
                                //     }
                                // }
                            }
                            //If a 1x1
                            else if (dimension == "1x1"){
                                //Instantiate building with correct orientation and set transform to (x , y) of grid component
                                GameObject NewBuilding = Instantiate(randBuilding, new Vector3(x * 4, 0, y * 4), randBuilding.transform.rotation) as GameObject;
                                //Set parent transform to Buildings container object
                                NewBuilding.transform.parent = Manager._GM.BuildingsParent;
                                //Set value of (x , y) of Finish Grid to "D" for done
                                finalgrid[x][y] = "D";
                            }
                        }
                    }
                }
            }
            //This next part will look through the entire square and fill the remaining gaps with either pavement or a park
            //Iterate through all the values (y)
            for(int y = (int)squares[i].BL.y; y <= (int)squares[i].TL.y;y++){
                //Iterate through all the value (x)
                for(int x = (int)squares[i].BL.x; x <= (int)squares[i].BR.x;x++){
                    //If (x , y) of FinalGrid == ""
                    if(finalgrid[x][y] == ""){
                        //Instantiate park and set transform to (x , y) of grid component
                        GameObject NewBuilding = null;
                        if(squares[i].District != "Industry")//Spawn a park
                        {
                            GameObject temp = Manager._GM.Parks[UnityEngine.Random.Range(0, 2)];
                            NewBuilding = Instantiate(temp, new Vector3(x * 4, 0, y * 4), temp.transform.rotation);
                        }
                        else //Spawn pavement
                        {
                            GameObject temp = Manager._GM.Parks[UnityEngine.Random.Range(2, 4)];
                            NewBuilding = Instantiate(temp, new Vector3(x * 4, 0, y * 4), temp.transform.rotation);
                        }
                        //Set parent transform to Buildings container object
                        NewBuilding.transform.parent = Manager._GM.BuildingsParent;
                        //Set value of (x , y) of FinishGrid to "D" for done
                        finalgrid[x][y] = "D";
                    }
                }
            }
        }

    }
}
