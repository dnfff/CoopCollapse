using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public float distance = 3f;
    public Camera cam;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = new Ray(cam.transform.position, cam.transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, distance))
            {
                Door door = hit.collider.GetComponentInParent<Door>();
                if (door != null)
                {
                    door.Toggle();
                }
            }
        }
    }
}
