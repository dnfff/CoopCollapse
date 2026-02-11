using UnityEngine;

public class PlayerVisualTilt : MonoBehaviour
{
    public Transform visual;          // сюда перетащим Visual
    public float tiltAmount = 10f;    // сила наклона
    public float tiltSpeed = 5f;      // скорость сглаживания

    CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        Vector3 velocity = controller.velocity;

        // игнорируем вертикальное движение
        velocity.y = 0;

        // если персонаж движется
        if (velocity.magnitude > 0.1f)
        {
            Vector3 localMove = transform.InverseTransformDirection(velocity);

            float tiltX = -localMove.z * tiltAmount;
            float tiltZ = localMove.x * tiltAmount;

            Quaternion targetRotation = Quaternion.Euler(tiltX, 0, tiltZ);

            visual.localRotation = Quaternion.Lerp(
                visual.localRotation,
                targetRotation,
                Time.deltaTime * tiltSpeed
            );
        }
        else
        {
            // возвращаем в исходное положение
            visual.localRotation = Quaternion.Lerp(
                visual.localRotation,
                Quaternion.identity,
                Time.deltaTime * tiltSpeed
            );
        }
    }
}
