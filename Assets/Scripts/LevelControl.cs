using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelControl : MonoBehaviour
{
    //Declare public variables
    public GameObject[] Buildings; //Available buildings
    public GameObject Pole;
    public GameObject Player;
    public float SpaceAround = 70.0f;
    public float BuildingHeight = 258.8f;
    public int NumSpawns = 3;

    //Declare private variables
    private List<Vector3> SpawnPositions = new List<Vector3>(); //Declare positions where the buildings will be created on
    private List<int> SpawnIndexes = new List<int>(); //List of spawn positions in order
    private List<Building> BuildingProps = new List<Building>();
    private List<Transform> StartPoints = new List<Transform>();
    private List<Transform> EndPoints = new List<Transform>();

    // Start is called before the first frame update
    void Awake()
    {
        SpawnPositions.Add(new Vector3(-SpaceAround, 0, -SpaceAround)); //Bottom Left, i = 0
        SpawnPositions.Add(new Vector3(-SpaceAround, 0, SpaceAround)); //Top Left i = 1
        SpawnPositions.Add(new Vector3(SpaceAround, 0, -SpaceAround)); //Bottom Right i = 2
        SpawnPositions.Add(new Vector3(SpaceAround, 0, SpaceAround)); //Top Right i = 3

        //Spawn buildings
        for(int i = 0; i < NumSpawns; i++)
        {
            var location = Random.Range(0, 4);
            //add the first index directly into the spawn index list
            if(SpawnIndexes.Count == 0)
            {
                SpawnIndexes.Add(location);
            }
            //From second index onwards
            else
            {
                //We don't want to spawn buildings diagonally
                var lastIndex = SpawnIndexes[i-1];
                //If index sum is 3 (Diagonal) or spawn index was already added, then repeat
                while (lastIndex + location == 3 || SpawnIndexes.Contains(location))
                {
                    location = Random.Range(0, 4);
                }
                SpawnIndexes.Add(location);
            }
            //Create building
            var building = Instantiate(Buildings[Random.Range(0,Buildings.Length)], SpawnPositions[location], Quaternion.identity);
            BuildingProps.Add(building.GetComponent<Building>());

            //Set-up poles between the buildings, starting from the second building
            if (i > 0)
            {
                var middle = (SpawnPositions[location] + SpawnPositions[SpawnIndexes[i - 1]])/2 + new Vector3(0, BuildingHeight, 0);
                Quaternion angle;
                if(middle.x == 0)
                {
                    angle = Quaternion.Euler(0, 0, 90);
                }
                else
                {
                    angle = Quaternion.Euler(90, 0, 0);
                }
                Instantiate(Pole, middle, angle);

            }
        }

        PopulateCheckPoints();
        for(int i = 0; i < StartPoints.Count; i++)
        {
            Debug.Log(StartPoints[i].ToString());
            Debug.Log(EndPoints[i].ToString());
        }

        //TODO: need to set initial player starting point
        Player.transform.position = StartPoints[0].transform.position;
    }

    //Populate checkpoints :)
    private void PopulateCheckPoints()
    {
        for (int i = 0; i < SpawnIndexes.Count-1; i++) { 
            //Which direction is the next building compared to the current one?
            var direction = SpawnIndexes[i + 1] - SpawnIndexes[i];
            //North
            if (direction == 1) 
            {
                StartPoints.Add(BuildingProps[i].NorthPos);
                EndPoints.Add(BuildingProps[i + 1].SouthPos);
            }
            //West
            else if (direction == 2)
            {
                StartPoints.Add(BuildingProps[i].EastPos);
                EndPoints.Add(BuildingProps[i + 1].WestPos);
            }
            //South
            else if (direction == -1)
            {
                StartPoints.Add(BuildingProps[i].SouthPos);
                EndPoints.Add(BuildingProps[i + 1].NorthPos);
            }
            //East
            else if (direction == -2) {
                StartPoints.Add(BuildingProps[i].WestPos);
                EndPoints.Add(BuildingProps[i + 1].EastPos);
            }
            StartPoints[i].gameObject.tag = "StartPoint";
            EndPoints[i].gameObject.tag = "EndPoint";
        }
    }

    //Startpoints getter
    public List<Transform> GetStartPoints()
    {
        return StartPoints;
    }

    //Endpoints getter
    public List<Transform> GetEndPoints()
    {
        return EndPoints;
    }
}
