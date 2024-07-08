using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvidenceObject : MonoBehaviour
{
    public string description = "";
    public bool isNewEvidence = true;
    [SerializeField] int typeID;
    [SerializeField] GameObject evidenceList;
    [SerializeField] List<EvidenceObject> similarEvidenceObjects = new();

    // This method allows multiple objects to only count for one evidence
    public void MarkAsOld()
    {
        // Marks itself and other similar objects to be old
        isNewEvidence = false;
        foreach (EvidenceObject obj in similarEvidenceObjects)
        {
            obj.isNewEvidence = false;
        }

        // Looping through the children of the evidence list at a certain child index
        foreach (Transform child in evidenceList.transform.GetChild(typeID))
        {
            // If a child (predetermined to be a cross or check) is inactive, then it will be activated and break the foreach
            if (!child.gameObject.activeSelf)
            {
                child.gameObject.SetActive(true);
                break;
            }
        }
    }
}
