using UnityEngine;

public class EndGate : MonoBehaviour
{
    private Animator animator;
    private bool isTransitioning;
    private float runTime;
    private float elapsedRunTime;
    private bool isOpen;
    private bool tryClose;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        runTime = 1.5f;
        elapsedRunTime = runTime;
        isTransitioning = false;
        isOpen = false;
        tryClose = false;
    }

    private void Update()
    {
        
        if (tryClose && !isTransitioning && isOpen)
        {
            CloseAnimation();
            tryClose = false;
        }

        if (isTransitioning)
        {
            elapsedRunTime -= Time.deltaTime;
        }
        if (elapsedRunTime <= 0)
        {
            isTransitioning = false;
            elapsedRunTime = runTime;
        }
    }

    public void OpenAnimation()
    {
        tryClose = false;
        if (!isOpen && !isTransitioning)
        {
            animator.SetBool("canClose", false);
            animator.SetBool("canOpen", true);
            isOpen = true;
            isTransitioning = true;
        }
    }

    private void CloseAnimation()
    {
        animator.SetBool("canOpen", false);
        animator.SetBool("canClose", true);
        isTransitioning = true;
        isOpen = false;
        tryClose = false;
    }

    public void TryCloseAnimation()
    {
        tryClose = true;
    }


}
