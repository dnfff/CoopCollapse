using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform pivot;
    public float openAngle = 90f;
    public float speed = 3f;

    bool isOpen;
    bool isMoving;
    float currentAngle;

    void Update()
    {
        if (!isMoving) return;

        float target = isOpen ? openAngle : 0f;
        currentAngle = Mathf.MoveTowards(
            currentAngle,
            target,
            speed * 100f * Time.deltaTime
        );

        pivot.localRotation = Quaternion.Euler(0, currentAngle, 0);

        if (Mathf.Abs(currentAngle - target) < 0.1f)
            isMoving = false;
    }

    public void Toggle()
    {
        isOpen = !isOpen;
        isMoving = true;
    }
}

