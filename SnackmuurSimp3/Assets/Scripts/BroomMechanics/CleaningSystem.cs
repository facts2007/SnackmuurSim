using UnityEngine;

public class CleaningSystem : MonoBehaviour
{
    public float cleanDistance = 100f;
    public LayerMask dirtLayer;
    public BroomPickup broom;

    void Update()
    {
        if (Input.GetMouseButton(0) && broom.IsHeld())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, cleanDistance, dirtLayer))
            {
                Dirt dirt = hit.collider.GetComponent<Dirt>();
                if (dirt != null)
                {
                    dirt.Clean();
                }
            }
        }
    }
}