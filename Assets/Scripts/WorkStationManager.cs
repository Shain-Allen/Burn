using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkStationManager : MonoBehaviour
{
    public List<GameObject> availableStations = new();

    void Start()
    {
        // This gets all the children of the object this script is attached to
        // Then it adds that child to the available stations list
        foreach (Transform child in transform)
        {
            availableStations.Add(child.gameObject);
        }
    }

    // Returns an available station for the workers to use, then removes it from the list
    // Stations are re-added again from the WorkerBehavior script
    public GameObject GetStation()
    {
        int randStationID = Random.Range(0, availableStations.Count - 1);
        GameObject stationToReturn = availableStations[randStationID];
        availableStations.RemoveAt(randStationID);
        return stationToReturn;
    }

}
