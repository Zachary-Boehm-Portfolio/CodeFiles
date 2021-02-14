/*
    @Author Zachary Boehm
    @Version 12.5.2020 | December 5th, 2020
    
    All algorithms are created and designed by @Author with reference to resources such as Stack Overflow and a Youtube tutorial by Matt MirrorFish
    at this link: https://www.youtube.com/watch?v=6XJXmX95dNc&list=WL&index=208
*/


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/*
This class is ment to hold the 4 vertices for a square which will be used for subdivision of the grid.
There is a District variable used to determine what type of building will go in that certain square
Has a default constructor that makes a square with 1 unit between each point starting at (0,0) for the bottom left vertex
There is also a constructor that allows for all 4 vertices with are Vector2 type to be assigned
Lastely there is constructor that allows for initialization with antoher square which copies the values
*/
public class Square
{
    public Vector2 TL; //Top left coordinate
    public Vector2 TR; //Top right coordinate
    public Vector2 BL; //Bottom left coordinate 
    public Vector2 BR; //Bottom right coordinate
    
    //When the buildings are generated this variable is used to determine what type of buildings go in this square
    public string District; 

    //Default Constructor -- creates a 1x1 unit square
    public Square()
    {
        TL = new Vector2(0, 1);
        TR = new Vector2(1, 1);
        BL = new Vector2(0, 0);
        BR = new Vector2(1, 0);
        District = "NA";
    }
    //Constructor that allows for all 4 of the vertices to be assigned
    //Starts at the bottom right corner and goes clockwise around the square
    public Square(Vector2 _BL, Vector2 _TL, Vector2 _TR, Vector2 _BR)
    {
        //Set the corners of the square to the corresponding incoming coordinates indicated by an "_"
        TL = _TL;
        TR = _TR;
        BL = _BL;
        BR = _BR;
        District = "NA";
    }
    //constructor allows for a square to be initialized with copy of another square
    public Square(Square _copy)
    {
        TL = _copy.TL;
        TR = _copy.TR;
        BL = _copy.BL;
        BR = _copy.BR;
        District = _copy.District;
    }
}
/*
This class is used as the driver to build the grid for the roads and buildings
It will first generate the roads then allow the user to click a button to generate the road GameObjects
Then this class will be used again to generate the buildings within the grid along with the districts
It then will allow the user to again click a button to generate the building GameObjects

!All GameObject Spawning will be handled in another script. This Script only contains Procedural Generation Algorithms.!

*/
public class GridBuilder : MonoBehaviour
{

    [SerializeField]TextMeshProUGUI Display;

    //2d array to hold the x and y coordinate of true or false something was placed there
    private List<List<string>> grid; //Y then X
    private List<List<string>> FinalGrid; //Y then X
    //This list will hold all the squares created by subdividing the grid. The squares will also be used to generate the buildings
    private List<Square> Squares;
    //This list will hold the lines created by subdivision which will be used to generate the roads
    private List<Vector4> Lines; //Is of type vector4 so it can hold 2 coordinates for each end of the line
    //Current Direction that a division will use
    int direction;


    private void Awake() {
        Squares = new List<Square>();
        Lines = new List<Vector4>();
    }

    /*
    Initializes and creates default grid when called
    */
    public void GenerateRoads()
    {
        //Grab and hold the int values of the x and y given by the input fields in Manager
        int X = Convert.ToInt32(Manager._GM.InputFields[0].text);
        int Y = Convert.ToInt32(Manager._GM.InputFields[1].text);
        //initialize the lists
        Squares = new List<Square>();
        Lines = new List<Vector4>();

        //set the seed based on the seed input field within the manager
        UnityEngine.Random.InitState(Convert.ToInt32(Manager._GM.InputFields[2].text));

        //initialize the squares with a single square the size of the entire grid
        Squares.Add(new Square(Point(0, 0), Point(0, Y - 1), Point(X - 1, Y - 1), Point(X - 1, 0)));

        //initialize the starting direction
        direction = 0;

        Debug.Log("Initial Direction: " + direction.ToString()); //!Remove when done testing
        //Initialize the grids
        grid = new List<List<string>>();
        FinalGrid = new List<List<string>>();

        //!Initialization
        //Initialize the grid 2d array with the dimensions given by the player
        for(int i = 0; i < X; i++)
        {
            //add a index to the x
            grid.Add(new List<string>());
            FinalGrid.Add(new List<string>());
            //initialize the new list that was added with a size given by the player
            for(int j = 0; j < Y; j++)
            {
                //Add index to the y with a default value of 0
                grid[i].Add("0");
                FinalGrid[i].Add("");
            }
        }
        PrintGrid();//!For testing purpose
        //When called the grid will be randomly subdivided based on the seed
        Subdivide();

        PrintGrid();//!For testing purpose

        //Set the next step button to interactable
        Manager._GM.NextStep.interactable = true;
    }

    /*
    This method is used to subdivide the default box into multiple random smaller boxes.
    This will also create roads in the process. The roads will be the shared side between two boxes.
    */
    private void Subdivide()
    {
        //Get the Grid Width
        int Grid_X = Convert.ToInt32(Manager._GM.InputFields[0].text);
        //Get the Grid Height
        int Grid_Y = Convert.ToInt32(Manager._GM.InputFields[1].text);
        //Number of roads is the area of the grid divided by 30 or (divide by 10 then by 3) to get number of roads/subdivisions
        int NRoads = Convert.ToInt32(Manager._GM.InputFields[4].text);;
        //Subdivide the grid based on the NRoads value
        for(int i = 0; i < NRoads; i++)
        {
            //When called a random road will be subdivided
            Divide();
        }
        //When Called the text grid will be updated with the correct characters
        DrawRoads();
        AssignRoadType();
    }
    /*
    This function will choose a random square and divide it into two.
    first it will choose a square, then it will either make a vertical or horizontal division. When the division is made it will be a center
    line division with the line randomly shifted. When the division is finished the function will add 2 new squares and delete the old single square
    along with adding a new line that is the line that is shared by these two squares. This new line is the road that is created
    */
    private void Divide()
    {
        //Randomly select a square from the list of Squares
        int index = UnityEngine.Random.Range(0, Squares.Count);
        Square original = Squares[index];

        //----------------------------------------------

        //If the box is taller than it is wide then horizontal direction. If it is wider than it is taller than vertical direction
        if((original.TR.y - original.BR.y) > (original.BR.x - original.BL.x))//tall and not wide
        {
            direction = 1;
        }
        else // wide and not tall
        {
            direction = 0;
        }

        //----------------------------------------------
        
        //Now take the direction and create a division in that direction with a randomly created shift
        if(direction == 1) // 1 = horizontal
        {
            //shift is the x value which the line is on
            int shift = UnityEngine.Random.Range((int)original.BL.y, (int)original.TL.y);
            //Add a new line that is the division of the square
            Lines.Add(new Vector4(original.BL.x, shift, original.BR.x, shift));
            //create and add the two new squares to the list of squares
            Squares.Add(NewSquare(Point((int)original.BL.x, shift), (int)original.BR.x - (int)original.BL.x, (int)original.TL.y - shift));
            Squares.Add(NewSquare(original.BL, (int)original.BR.x - (int)original.BL.x, shift - (int)original.BL.y));
            //remove the original square from the list of s
            Squares.RemoveAt(index);
        }
        else // 0 = vertical
        {
            //shift is the y value which the line is on
            int shift = UnityEngine.Random.Range((int)original.BL.x, (int)original.BR.x);
            //Add a new line that is the division of the square
            Lines.Add(new Vector4(shift, original.BR.y, shift, original.TR.y));
            //create and add the two new squares to the list of squares
            Squares.Add(NewSquare(original.BL, shift - (int)original.BL.x, (int)original.TL.y - (int)original.BL.y));
            Squares.Add(NewSquare(Point(shift, (int)original.BL.y), (int)original.BR.x - shift, (int)original.TL.y - (int)original.BL.y));
            //remove the original square from the list of s
            Squares.RemoveAt(index);
        }
    }

    /*
    This function will draw look at the lines created from subdivision and update the points along all the lines to be a base road value
    */
    private void DrawRoads()
    {
        //iterate through all the lines
        for(int i = 0; i < Lines.Count; i++)
        {
            //If Horizontal
            if((int)Lines[i].y == (int)Lines[i].w)
            {
                int length = (int)Lines[i].z - (int)Lines[i].x; // find the width which is also the amound of values to change
                //Start at Y index in grid. Start at the left x point of the line and move right
                //until the right x point of line and mark all values as roads
                for(int j = (int)Lines[i].x; j <= (int)Lines[i].z; j++)
                {
                    grid[j][(int)Lines[i].y] = "$";
                }
            }
            //If Vertical
            if((int)Lines[i].x == (int)Lines[i].z)
            {
                // find the width which is also the amound of values to change
                int length = (int)Lines[i].w - (int)Lines[i].y;
                //Start at the X index in grid. Start at lower y value of the line and move up
                //until the top y value of the line and mark as road 
                for(int j = (int)Lines[i].y; j <= (int)Lines[i].w; j++)
                {
                    grid[(int)Lines[i].x][j] = "$";
                }
            }
        }
    }

    /*
    This function will loop through all the points on the grid and assign the raod types
    Checks all 4 sides that are touching creating the combonation of sides with roads in order of Left Up Right Down
    */
    private void AssignRoadType()
    {
        Debug.Log("---------------------------------------------------------------------------------------");
        Debug.Log("Now starting the road assignment");
        Debug.Log("---------------------------------------------------------------------------------------");
        Debug.Log(grid.Count);
        //Iterate through all values of grid
        for(int x = 0; x < grid.Count; x++)
        {
            for(int y = 0; y < grid[x].Count; y++)
            {
                if(grid[x][y] != "0")
                {
                    //Check all sides of index and check for roads

                    //This is the combination of what sides have roads
                    string Combo = ""; 

                    //!Left
                    if(x - 1 > -1) // Make sure left point exists
                    {
                        if(grid[x - 1][y] != "0") // check to see if there is a  road on the left
                        {
                            //If there is a road on the left add that to the combination
                            Combo += "L";
                        }
                    }
                    //!Up
                    if(y + 1 < grid[0].Count) // Make sure top point exists
                    {
                        if(grid[x][y + 1] != "0") // check to see if there is a  road on the top
                        {
                            //If there is a road on the left add that to the combination
                            Combo += "U";
                        }
                    }
                    //!Right
                    if(x + 1 < grid.Count) // Make sure right point exists
                    {
                        if(grid[x + 1][y] != "0") // check to see if there is a  road on the right
                        {
                            //If there is a road on the left add that to the combination
                            Combo += "R";
                        }
                    }
                    //!Down
                    if(y - 1 > -1) // Make sure top point exists
                    {
                        if(grid[x][y - 1] != "0") // check to see if there is a  road on the top
                        {
                            //If there is a road on the left add that to the combination
                            Combo += "D";
                        }
                    }
                    FinalGrid[x][y] = Combo;
                    Debug.Log("Point: (" + (x + 1) + " , " + (y + 1) + ")" + " has a combo of " + FinalGrid[x][y]); //!For testing - Remove when done testing
                }
            }
        }
    }

    /*
    Helper function that returns a new Vector2
    This allows for a "Point(x,y)" call instead of a "new Vector2(x, y)" call.
    @return Vector2
    */
    private Vector2 Point(int _X, int _Y)
    {
        return new Vector2(_X, _Y);
    }

    /*
    Helper function that takes in the bottom left point along with width and height to return a new square
    @return Square
    */
    private Square NewSquare(Vector2 _BL, int width, int height)
    {
        return new Square(_BL, Point((int)_BL.x, (int)_BL.y + height), Point((int)_BL.x + width, (int)_BL.y + height), Point((int)_BL.x + width, (int)_BL.y));
    }

    /*
    Getter for the FinalGrid 2d List 
    @return List<List<string>>
    */
    public List<List<string>> GetFinalGrid()
    {
        return FinalGrid;
    }

    /*
    Getter for the Squares List 
    @return List<Square>
    */
    public List<Square> GetSquares()
    {
        return Squares;
    }

    /*
    print out the grid given the string values 
    */
    public void PrintGrid()
    {
        string Final = ""; //this is for the UI display
        //Start at the top of the grid
        for(int i = grid[1].Count - 1; i > -1; i--)
        {
            //variable to hold an entire row of the grid
            string output = "";
            //interate through the row
            for(int j = 0; j < grid.Count; j++)
            {
                //add the index and some spacing to the output of the row
                output += grid[j][i] + "      ";
            }
            //trim the extra spacing of the end of the row's output
            output = output.TrimEnd();
            //print the output to the unity console
            Debug.Log(output);
            Final += output + "\n";
        }
        //Display.text = Final; //!uncomment to have the print go to the UI Text display
    }
    
}
