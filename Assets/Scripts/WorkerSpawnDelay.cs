using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerSpawnDelay : MonoBehaviour
{
    /*
     * After some testing I dont think this script is necessary and just broke things instead
     * so just ignore it
     */

    GameObject[] workers;

    void Start()
    {
        // Populating the list of workers tagged as AI and disabling them
        // The reason the worker's spawns must be staggered is so 2 workers are not able to choose the same work station
        workers = GameObject.FindGameObjectsWithTag("AIWorker");
        foreach (GameObject worker in workers)
        {
            worker.SetActive(false);
        }

        StartCoroutine(StaggerSpawn());

        //Debug.Log("on");
    }

    IEnumerator StaggerSpawn()
    {
        // Goes through the worker list again, setting only 1 active every frame
        foreach (GameObject worker in workers)
        {
            yield return new WaitForEndOfFrame();
            worker.SetActive(true);
        }
    }
}
