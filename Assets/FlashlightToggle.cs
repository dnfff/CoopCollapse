using UnityEngine;

public class FlashlightToggle : MonoBehaviour
{
    public KeyCode toggleKey = KeyCode.F;

    Light flashlight;
    bool isOn = true;

    void Start()
    {
        flashlight = GetComponent<Light>();

        if (flashlight == null)
        {
            Debug.LogError("На объекте нет компонента Light!");
            return;
        }

        flashlight.enabled = isOn;
    }

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            isOn = !isOn;
            flashlight.enabled = isOn;
        }
    }
}
