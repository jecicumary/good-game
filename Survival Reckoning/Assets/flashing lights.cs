using UnityEngine;

public class FlashingLights : MonoBehaviour
{
    public float flashInterval = 1.0f; // Time between flashes in seconds
    public float intensityMultiplier = 2.0f; // Intensity multiplier for the flashing effect

    private Light flashingLight;
    private float originalIntensity;
    private float timer;

    void Start()
    {
        flashingLight = GetComponent<Light>();
        if (flashingLight == null)
        {
            Debug.LogError("FlashingLights script requires a Light component on the same GameObject.");
            enabled = false;
            return;
        }

        originalIntensity = flashingLight.intensity;
        timer = 0f;
    }

    void Update()
    {
        // Update the timer
        timer += Time.deltaTime;

        // Check if it's time to flash
        if (timer >= flashInterval)
        {
            // Toggle the light on/off
            flashingLight.intensity = (flashingLight.intensity == 0f) ? originalIntensity * intensityMultiplier : 0f;

            // Reset the timer
            timer = 0f;
        }
    }
}
