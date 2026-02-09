using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float gravity = -9.8f;
    public float jumpForce = 8f;
    public float mouseSensitivity = 3f;

    public Transform cameraTransform;

    private CharacterController controller;
    private Vector3 velocity;
    private float xRotation;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        // автоматически подцепляем камеру, если не назначена
        if (cameraTransform == null)
            cameraTransform = GetComponentInChildren<Camera>().transform;

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Move();
        Look();
    }

    void Move()
    {
        // ---------- ВВОД ----------
        Vector2 input = Keyboard.current != null
            ? new Vector2(
                (Keyboard.current.dKey.isPressed ? 1 : 0) - (Keyboard.current.aKey.isPressed ? 1 : 0),
                (Keyboard.current.wKey.isPressed ? 1 : 0) - (Keyboard.current.sKey.isPressed ? 1 : 0)
              )
            : Vector2.zero;

        // ---------- ГОРИЗОНТАЛЬНОЕ ДВИЖЕНИЕ ----------
        Vector3 move = transform.right * input.x + transform.forward * input.y;
        controller.Move(move * speed * Time.deltaTime);

        // ---------- НАДЁЖНАЯ ПРОВЕРКА ЗЕМЛИ (Raycast) ----------
        bool grounded = Physics.Raycast(
            transform.position,
            Vector3.down,
            controller.height / 2f + 0.15f
        );

        // прижимаем к земле
        if (grounded && velocity.y < 0)
            velocity.y = -2f;

        // ---------- ПРЫЖОК (РАБОТАЕТ НА МЕСТЕ) ----------
        if (grounded && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            velocity.y = jumpForce;
        }

        // ---------- ГРАВИТАЦИЯ ----------
        velocity.y += gravity * Time.deltaTime;

        // ❗ всегда двигаем по Y
        controller.Move(Vector3.up * velocity.y * Time.deltaTime);
    }

    void Look()
    {
        if (Mouse.current == null) return;

        Vector2 mouse = Mouse.current.delta.ReadValue();

        float mouseX = mouse.x * mouseSensitivity * Time.deltaTime;
        float mouseY = mouse.y * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
}