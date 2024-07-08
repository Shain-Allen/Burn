using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

enum EvidenceRating { Good, Okay, Bad };

public class PlayerPhotoMode : MonoBehaviour
{
    Camera camera;
    Plane[] cameraFrustum;
    GameObject[] EvidenceInScene;
    [SerializeField] LayerMask mask;
    [SerializeField] float detectionRange = 10f;
    [SerializeField] float tooCloseRange = 1.75f;
    bool canTakePhoto = true;

    [SerializeField] TMP_Text rotationDigits;

    [SerializeField] Light flashlight;
    float defaultLightIntensity = 5f;
    [SerializeField] float flashIntensity = 100f;

    VolumeProfile profile;
    ColorAdjustments colorAdjustments;
    float defaultExposure = -0.5f;
    [SerializeField] float exposureAfterFlash = -5f;

    public TMP_Text feedbackText;
    [HideInInspector] public int photosTaken = 0;

    [SerializeField] AudioSource cameraClick;
    [SerializeField] AudioSource cameraWinding;
    [SerializeField] AudioSource feedbackAudioSource;
    [SerializeField] List<AudioClip> goodSounds = new();
    [SerializeField] List<AudioClip> okSounds = new();
    [SerializeField] List<AudioClip> badSounds = new();

    void Start()
    {
        camera = Camera.main;
        EvidenceInScene = GameObject.FindGameObjectsWithTag("Evidence");

        defaultLightIntensity = flashlight.intensity;

        profile = GetComponent<ToggleCamera>().viewfinderVolumeProfile;
        profile.TryGet(out colorAdjustments);
        defaultExposure = colorAdjustments.postExposure.value;

        this.enabled = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canTakePhoto) // Activates when clicking left mouse button
        {
            StartCoroutine(TakePhoto());
        }

        rotationDigits.text = transform.rotation.eulerAngles.y.ToString();
    }

    void DisplayFeedbackText(string[] textToSayArray)
    {
        feedbackText.gameObject.SetActive(false);
        feedbackText.text = textToSayArray[Random.Range(0, textToSayArray.Length)];
        feedbackText.gameObject.SetActive(true);
    }

    IEnumerator TakePhoto()
    {
        GetComponent<ToggleCamera>().canToggle = false;
        canTakePhoto = false;
        cameraClick.Play();

        yield return new WaitForSeconds(0.5f);

        // The following checks for evidence
        List<GameObject> EvidenceInView = new();
        EvidenceRating rating;

        // Calculating the area which the camera can see
        cameraFrustum = GeometryUtility.CalculateFrustumPlanes(camera);

        // Loop through all evidence objects and test which ones the camera can see
        foreach (GameObject evidence in EvidenceInScene)
        {
            Bounds objectBounds = evidence.GetComponent<Collider>().bounds;

            if (GeometryUtility.TestPlanesAABB(cameraFrustum, objectBounds) &&  // Is the object in front of the camera?
                !Physics.Linecast(camera.transform.position, evidence.transform.position, mask)) //Is the object blocked by another object?
            {
                // If the object can be seen, add it to a new list
                EvidenceInView.Add(evidence);
            }
        }

        if (EvidenceInView.Count > 0)
        {
            //Checking to see if any of the visible evidence is a new object
            int oldEvidenceCount = 0;
            foreach (GameObject evidence in EvidenceInView)
            {
                if (!evidence.GetComponent<EvidenceObject>().isNewEvidence)
                    oldEvidenceCount++;
            }

            if (oldEvidenceCount == EvidenceInView.Count)
            {
                // Already have a picture of this evidence
                string[] duplicateObjectText = { "I’ve already photographed this subject. No need for two.", "One picture is enough, let’s find something else.", "It’s pointless to have duplicates. I don’t need any for the fridge!", "Oh okay, I guess we’ll just have another of these…" };
                DisplayFeedbackText(duplicateObjectText);
                rating = EvidenceRating.Okay;
            }
            else
            {
                float minDistance = Mathf.Infinity;
                int i = 0;
                int closestObjectIndex = i;

                // Loop through the visible evidence list again to find the closest one. Making sure not to count old evidence 
                foreach (GameObject evidence in EvidenceInView)
                {
                    float objectDistance = Vector3.Distance(evidence.transform.position, transform.position);
                    if (objectDistance < minDistance && evidence.GetComponent<EvidenceObject>().isNewEvidence)
                    {
                        minDistance = objectDistance;
                        closestObjectIndex = i;
                    }
                    i++;
                }

                float closestObjectDist = Vector3.Distance(EvidenceInView[closestObjectIndex].transform.position, transform.position);
                if (closestObjectDist > detectionRange)
                {
                    //too far away
                    string[] farAwayText = { "That won't work, I need to be closer.", "No good, I'm too far away", "I’ll be scolded for wasting film like this!", "C'mon, get closer next time!" };
                    DisplayFeedbackText(farAwayText);
                    rating = EvidenceRating.Okay;
                }
                else if (closestObjectDist < tooCloseRange)
                {
                    //too close
                    string[] tooCloseText = { "I need to back up, this is too close", "Too close, I can’t tell what it is.", "Director Larry won’t appreciate this slop. Back up a bit.", "How’s anyone supposed to know what this is?!" };
                    DisplayFeedbackText(tooCloseText);
                    rating = EvidenceRating.Okay;
                }
                else
                {
                    // Give the closest new and visible object's name and description. Then mark it as old evidence
                    string[] goodObjectText = { EvidenceInView[closestObjectIndex].GetComponent<EvidenceObject>().description };
                    DisplayFeedbackText(goodObjectText);
                    EvidenceInView[closestObjectIndex].GetComponent<EvidenceObject>().MarkAsOld();
                    photosTaken++;
                    rating = EvidenceRating.Good;
                }
            }
        }
        else
        {
            // No objects in view
            string[] noEvidenceText = { "I need to find actual evidence.", "There’s nothing interesting here…", "Huh? Did my finger slip?", "Must be the coffee. I’m too on edge right now!" };
            DisplayFeedbackText(noEvidenceText);
            rating = EvidenceRating.Bad;
        }
        // Done checking for evidence

        flashlight.intensity = flashIntensity;
        flashlight.spotAngle = 180;
        colorAdjustments.postExposure.value = exposureAfterFlash;

        switch (rating)
        {
            case EvidenceRating.Good:
                PlayFeedbackAudio(goodSounds);
                break;
            case EvidenceRating.Okay:
                PlayFeedbackAudio(okSounds);
                break;
            case EvidenceRating.Bad:
                PlayFeedbackAudio(badSounds);
                break;
            default:
                break;
        }

        yield return new WaitForSeconds(0.3f);

        flashlight.intensity = defaultLightIntensity;
        flashlight.spotAngle = flashlight.innerSpotAngle;
        colorAdjustments.postExposure.value = defaultExposure;

        cameraWinding.Play();

        canTakePhoto = true;
        GetComponent<ToggleCamera>().canToggle = true;
    }

    void PlayFeedbackAudio(List<AudioClip> ratingAudioList)
    {
        feedbackAudioSource.clip = ratingAudioList[Random.Range(0, ratingAudioList.Count)];
        feedbackAudioSource.Play();
    }

}

