using UnityEngine;

public class Portal : MonoBehaviour
{
    public Transform teleportDestination;
    private static bool isTeleporting = false;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player entered Portal: " + gameObject.name);
        if (other.CompareTag("Player") && !isTeleporting)
        {
            
            StartCoroutine(ResetTeleportCooldown(other));
        }
    }

    private System.Collections.IEnumerator ResetTeleportCooldown(Collider player)
    {
        isTeleporting = true;

        CharacterController cc = player.GetComponent<CharacterController>();

        if (cc != null) cc.enabled = false;

        player.transform.position = teleportDestination.transform.position + (player.transform.position - gameObject.transform.position);
        Debug.Log("Player was teleported");
        if (cc != null) cc.enabled = true;

        yield return new WaitForSeconds(1f);
       
        isTeleporting = false;
        


    }
}
