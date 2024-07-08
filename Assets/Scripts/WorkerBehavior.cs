using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorkerBehavior : MonoBehaviour
{
    NavMeshAgent agent;
    public WorkStationManager workStationParent;
    // need a reference to the workers Animator here probably

    bool isWorking = false;
    GameObject assignedStation;

    [SerializeField] float minWorkTime = 20f;
    [SerializeField] float maxWorkTime = 60f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Wake up and go to work, so relatable
    void Awake()
    {
        StartCoroutine(FirstWork());
    }

    void FixedUpdate()
    {
        if (!isWorking && Vector3.Distance(transform.position, assignedStation.transform.position) < 1)
        {
            // This will activate if the worker is not working, but they are standing on their assigned work station
            StartCoroutine(StartWorking());
        } else if (!isWorking)
        {
            // If the worker isn't working and not next to their station, then they should be walking
            // make sure the walking animation is playing here
        }
    }

    IEnumerator StartWorking()
    {
        isWorking = true;

        //Rotating the worker to face the correct direction
        agent.enabled = false;
        transform.eulerAngles = new Vector3(0, assignedStation.transform.eulerAngles.y, 0);
        // start playing the work animation; Information about what type of station this is is stored in the tag
        Debug.Log("Started working at a station of type: " + assignedStation.tag);

        yield return new WaitForSeconds(Random.Range(minWorkTime, maxWorkTime));
        
        agent.enabled = true;

        // The worker is now done with this station, so it adds it back to the available list and gets a new one
        workStationParent.availableStations.Add(assignedStation);
        assignedStation = workStationParent.GetStation();
        agent.SetDestination(assignedStation.transform.position);

        isWorking = false;
    }

    IEnumerator FirstWork()
    {
        yield return new WaitForFixedUpdate();
        assignedStation = workStationParent.GetStation();
        agent.SetDestination(assignedStation.transform.position);
    }
}
