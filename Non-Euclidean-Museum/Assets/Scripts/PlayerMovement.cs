using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Joystick Movement")]
    public SteamVR_Action_Vector2 touchpadInput;
    public Transform cameraTransform;

    [Header("Room-Scale Settings")]
    [Tooltip("How much to amplify your real-world movement.")]
    public float roomScaleMultiplier = 2.0f;

    [Header("Teleport")]
    public SteamVR_Action_Boolean teleportAction;
    public SteamVR_Input_Sources teleportHand;

    private CapsuleCollider capsuleCollider;

    // Track the last frame’s head local pos to compute delta
    private Vector3 lastHeadLocalPos;

    void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
        lastHeadLocalPos = cameraTransform.localPosition;
    }

    void Update()
    {
        // Teleport back to spawn on button press
        if (teleportAction.GetStateDown(teleportHand))
        {
            transform.position = Vector3.zero;
            // reset lastHeadLocalPos so we don't get a big delta next frame
            lastHeadLocalPos = cameraTransform.localPosition;
            RefitCollider();
        }
    }

    void FixedUpdate()
    {
        // 1) Joystick locomotion (unchanged)
        Vector3 moveDir = new Vector3(touchpadInput.axis.x, 0, touchpadInput.axis.y);
        Vector3 worldMove = cameraTransform.TransformDirection(moveDir);
        transform.position += Vector3.ProjectOnPlane(worldMove, Vector3.up)
                              * 2.0f * Time.deltaTime;

        // 2) Room-scale movement amplification
        Vector3 currentHeadLocal = cameraTransform.localPosition;
        Vector3 headDelta = currentHeadLocal - lastHeadLocalPos;
        
        headDelta.y = 0;
        // Transform this local rig-space delta into a world-space movement vector
        Vector3 worldSpaceHeadDelta = transform.TransformDirection(headDelta);
        transform.position += worldSpaceHeadDelta * roomScaleMultiplier;
        lastHeadLocalPos = currentHeadLocal;

        // 3) Re-fit collider to current head height
        RefitCollider();
    }

    private void RefitCollider()
    {
        float h = Vector3.Dot(cameraTransform.localPosition, Vector3.up);
        capsuleCollider.height = Mathf.Max(capsuleCollider.radius, h);
        capsuleCollider.center = cameraTransform.localPosition
                                 - 0.5f * h * Vector3.up;
    }
}
