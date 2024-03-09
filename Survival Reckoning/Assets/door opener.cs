using UnityEngine;

public class DoorOpenerScript : MonoBehaviour
{
    public float interactionDistance = 2f;
    private DoorScript door;

    void Start()
    {
        door = GetComponent<DoorScript>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, interactionDistance))
            {
                DoorScript hitDoor = hit.collider.GetComponent<DoorScript>();
                if (hitDoor != null)
                {
                    ToggleDoor(hitDoor);
                }
            }
        }
    }

    void ToggleDoor(DoorScript doorToToggle)
    {
        if (doorToToggle != null)
        {
            if (doorToToggle.IsOpen())
            {
                doorToToggle.CloseDoor();
            }
            else
            {
                doorToToggle.OpenDoor();
            }
        }
    }
}
