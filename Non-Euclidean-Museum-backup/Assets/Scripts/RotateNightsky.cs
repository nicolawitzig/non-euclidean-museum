using UnityEngine;

public class RotateNightsky : MonoBehaviour
{
    public Vector3 rotationAxis = Vector3.up; // Default to Y-axis
    public float rotationSpeed = 1f; // Degrees per second

    void Update()
    {
        transform.Rotate(rotationAxis, rotationSpeed * Time.deltaTime);
    }
}
