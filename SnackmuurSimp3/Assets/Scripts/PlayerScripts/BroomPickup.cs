using UnityEngine;

public class BroomPickup : MonoBehaviour
{
    public Transform holdPoint;

    private Vector3 originalPosition;
    private Quaternion originalRotation;

    private bool isHeld = false;

    [Header("Sway Settings")]
    public float swaySpeed = 10f;
    public float maxAngle = 30f;

    private float swayTimer = 0f;

    void Start()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ToggleBroom();
        }

        if (isHeld)
        {
            HandleSway();
        }
    }

    void ToggleBroom()
    {
        if (!isHeld)
        {
            transform.SetParent(holdPoint);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            isHeld = true;
        }
        else
        {
            transform.SetParent(null);
            transform.position = originalPosition;
            transform.rotation = originalRotation;
            isHeld = false;
        }
    }

    void HandleSway()
    {
        if (Input.GetMouseButton(0))
        {
            swayTimer += Time.deltaTime * swaySpeed;

            float angle = Mathf.Sin(swayTimer) * maxAngle;
            transform.localRotation = Quaternion.Euler(0, angle, 0);
        }
        else
        {
            swayTimer = 0f;
            transform.localRotation = Quaternion.Lerp(
                transform.localRotation,
                Quaternion.identity,
                Time.deltaTime * 5f
            );
        }
    }

    public bool IsHeld()
    {
        return isHeld;
    }
}