using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightingController : MonoBehaviour
{
    private Light2D globalLight;

    // Colors to represent different energy consumption levels
    public Color lowConsumptionColor = new Color32(0xFF, 0xFF, 0xFF, 0xFF);
    public Color mediumConsumptionColor = new Color32(0xB6, 0x4D, 0x4D, 0xFF);
    public Color highConsumptionColor = new Color32(0x96, 0x28, 0x28, 0xFF);
    public Color veryHighConsumptionColor = new Color32(0x80, 0x1A, 0x1A, 0xFF);

    private void Awake()
    {
        globalLight = GetComponent<Light2D>();
        if (globalLight == null)
        {
            Debug.LogError("No Light2D component found on the GameObject.");
        }
    }

    public void UpdateLightingColor(float averageEnergyConsumption)
    {
        if (globalLight == null)
        {
            return;
        }

        if (averageEnergyConsumption < 0.2f)
        {
            globalLight.color = lowConsumptionColor;
        }
        else if (averageEnergyConsumption < 0.4f)
        {
            globalLight.color = mediumConsumptionColor;
        }
        else if (averageEnergyConsumption < 0.8f)
        {
            globalLight.color = highConsumptionColor;
        }
        else
        {
            globalLight.color = veryHighConsumptionColor;
        }
    }
}
