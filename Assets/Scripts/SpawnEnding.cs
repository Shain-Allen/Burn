using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnding : MonoBehaviour
{
    [SerializeField] GameObject endingObjects;

    private void Awake()
    {
        endingObjects.SetActive(true);
    }
}
