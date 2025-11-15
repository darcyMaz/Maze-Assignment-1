using UnityEngine;

public class EndGateTrigger : MonoBehaviour
{
    public EndGate eg;

    private void OnTriggerStay(Collider other)
    {
        eg.OpenAnimation();
    }
    private void OnTriggerExit(Collider other)
    {
        eg.TryCloseAnimation();
    }


}
