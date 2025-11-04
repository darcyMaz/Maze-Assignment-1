using System;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator myAnimator = null;
    Boolean canOpen;

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
        canOpen = false;
    }

    public void Unlock()
    {
        canOpen = true;
    }
    public void Lock()
    {
        canOpen = false;
    }
    public Boolean IsUnlocked()
    {
        return canOpen;
    }

    public void PlayAnimation()
    {
        myAnimator.SetBool("doorCanOpen", true);
    }

    public void ResetAnimation()
    {
        
    }
}
