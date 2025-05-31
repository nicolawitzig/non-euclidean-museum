using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerTrigger : MonoBehaviour
{

    public Player player;
    public GameObject frontObject;

    Camera playerCam;


    int previousSide;
    

   

    void Awake()
    {
        playerCam = Camera.main;
        previousSide = SideOfTrigger(player.transform.position);
        frontObject.SetActive(true);
    } 

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        previousSide = SideOfTrigger(player.transform.position);
        Debug.Log("TRIGGER ENTER: " + other.name);
    }

    void OnTriggerExit(Collider other)
    {
        if (SideOfTrigger(player.transform.position) != previousSide)
        {
            Debug.Log("layer trigger crossed");
            frontObject.SetActive(true);
            previousSide = SideOfTrigger(player.transform.position);
            this.gameObject.SetActive(false);
        }
    }

    int SideOfTrigger(Vector3 pos)
    {
        return System.Math.Sign(Vector3.Dot(pos - transform.position, transform.forward));
    }

    bool SameSideOfPortal(Vector3 posA, Vector3 posB)
    {
        return SideOfTrigger(posA) == SideOfTrigger(posB);
    }

}