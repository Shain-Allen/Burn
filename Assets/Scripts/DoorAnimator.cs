using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimator : MonoBehaviour
{
    public Animator animator;
    [SerializeField] public bool thisDoorIsClosed;
    private BoxCollider boxCollider;

    public int requiredNumPhotos = 0;
    public bool requireKeycard = false;

    // Start is called before the first frame update
    // start is whatevers
    void Start()
    {
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider>();
        
        thisDoorIsClosed = true;
    }

    public void DoorCloseBoolfalse()
    {
        thisDoorIsClosed = false;
        //allows player to pass through by setting box collider to trigger status
        boxCollider.isTrigger = true;
    }

    public void DoorCloseBoolTrue()
    {
        thisDoorIsClosed = true;
        //Keep the player from being able to pass through door by setting box collider to nan-Trigger status
        boxCollider.isTrigger = false;
    }
    
  
}
