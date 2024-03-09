using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour
{
    public float rotationSpeed = 90f; // Speed at which the door rotates
    public float doorOpenAngle = 90f; // Angle to open the door
    public Transform hinge; // Pivot point for the door rotation

    private bool isOpen = false;

    public bool IsOpen()
    {
        return isOpen;
    }

    public void OpenDoor()
    {
        if (!isOpen)
        {
            Quaternion targetRotation = Quaternion.Euler(0, doorOpenAngle, 0);
            StartCoroutine(RotateDoor(targetRotation));
            isOpen = true;
        }
    }

    public void CloseDoor()
    {
        if (isOpen)
        {
            Quaternion targetRotation = Quaternion.Euler(0, 0, 0);
            StartCoroutine(RotateDoor(targetRotation));
            isOpen = false;
        }
    }

    private IEnumerator RotateDoor(Quaternion targetRotation)
    {
        Vector3 pivot = hinge.position;
        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.01f)
        {
            transform.RotateAround(pivot, Vector3.up, rotationSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
