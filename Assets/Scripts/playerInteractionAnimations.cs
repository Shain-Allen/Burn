using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class playerInteractionAnimations : MonoBehaviour
{
    private Transform playerCamTransform;
    [SerializeField] float raycastDistance = 15f;
    [SerializeField] int keyCardTextTimer = 4;
    [SerializeField] bool doorIsOpen = false;
    [SerializeField] TMP_Text interactionText;
    [SerializeField] TMP_Text aquiredKeyCardTxt;
    TMP_Text feedbackText;
    bool hasKeycard = false;

    private DoorAnimator doorAnimator;
    private DestroySelf destroySelf;

    
    void Start()
    {
        interactionText.enabled = false;
        aquiredKeyCardTxt.enabled = false;
        playerCamTransform = Camera.main.transform; 
        doorAnimator = GetComponent<DoorAnimator>();
        destroySelf = GetComponent<DestroySelf>();
        feedbackText = GetComponent<PlayerPhotoMode>().feedbackText;
    }

    
    void Update()
    {
        PlayerAnimatorRaycast();
    }

  
    void PlayerAnimatorRaycast()
    {
        //Player raycase set to main cam in scene(player cam) with variable for distance in inspecter
        if (Physics.Raycast(playerCamTransform.position, playerCamTransform.forward, out RaycastHit hitInfo, raycastDistance))
        {

            //determines null return for script components
            doorAnimator = null;
            destroySelf = null;

            if(hitInfo.collider.TryGetComponent<DoorAnimator>(out doorAnimator) || hitInfo.collider.TryGetComponent<DestroySelf>(out destroySelf))
            {
                //Debug.Log("Press E to interact");
                if (!aquiredKeyCardTxt.enabled == true)
                {
                    interactionText.enabled = true;
                    interactionText.text = ("Press 'E' to interact");
                }
              

            }

            else
            {
                interactionText.enabled = false;
            }

            //Key card or other object with destroy self script with be destroyed on button press

            if (hitInfo.collider.TryGetComponent<DestroySelf>(out destroySelf) && Input.GetKeyDown(KeyCode.E))
            {
                //Debug.Log("Destory Self");
                PickedUpKeyCardText();
                destroySelf.gameObject.GetComponent<AudioSource>().Play();
                destroySelf.DestroyThisObject();
                hasKeycard = true;
            }

          
            //Check object collider for DoorAnimator script and keyinput E than uses bool within that script to determine if animation can trigger

            if (hitInfo.collider.TryGetComponent<DoorAnimator>(out doorAnimator) && Input.GetKeyUp(KeyCode.E))
            {
                if (doorAnimator.requiredNumPhotos <= GetComponent<PlayerPhotoMode>().photosTaken || (doorAnimator.requireKeycard && hasKeycard))
                {
                    doorAnimator.gameObject.GetComponent<AudioSource>().Play();

                    if (doorAnimator.thisDoorIsClosed == true)
                    {
                        //Debug.Log("Animator on Door opening ");
                        doorAnimator.GetComponent<Animator>().SetBool("DoorIsClosed", true);
                        StartCoroutine(DoorDelayCheck());
                    }

                    if (doorAnimator.thisDoorIsClosed == false)
                    {
                        //Debug.Log("Animator on Door closing");
                        doorAnimator.GetComponent<Animator>().SetBool("DoorIsClosed", false);
                        StartCoroutine(DoorDelayCheck());
                    }
                }
                else
                {
                    feedbackText.gameObject.SetActive(false);
                    feedbackText.text = "I think I'm missing something... I should look more closely before moving on.";
                    feedbackText.gameObject.SetActive(true);
                }
            }
            

        }


    }

    //Delay for the bool in door animator to prevent overlap of if statement logic. Without sometimes was running into situation where animator bool was triggering true and false on button press
    //There are other solutions to this, but this works fine for now
    IEnumerator DoorDelayCheck()
    {
        if (doorAnimator.thisDoorIsClosed == true)
        {
            yield return new WaitForSeconds(0.5f);
            doorAnimator.DoorCloseBoolfalse();
            //Debug.Log("Wait half Sec");
        }
        else if(!doorAnimator.thisDoorIsClosed)
        {
            yield return new WaitForSeconds(0.5f);
            doorAnimator.DoorCloseBoolTrue();
            //Debug.Log("Wait half Sec the second");
        }

      
    }
    //Enables key card aquired text and starts a timer to take text off the screen. While "You have aquired..." txt active other text will not appear on screen(but doors will still open)
    void PickedUpKeyCardText()
    {
        aquiredKeyCardTxt.enabled = true;
        aquiredKeyCardTxt.text = ("You have aquired a key card");
        StartCoroutine(WaitForSeconds(keyCardTextTimer));
        

    }
    //Timer to determine how long somthing will be happening, in this cast the keycard aquired text
    IEnumerator WaitForSeconds(int timer)
    {
        yield return new WaitForSeconds(timer);
        aquiredKeyCardTxt.enabled = false;
    }
}
