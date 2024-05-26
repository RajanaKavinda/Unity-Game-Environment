using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightingController : MonoBehaviour
{
    private Light2D globalLight;
    private EnergyStatusController energyStatusController;

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
            return;
        }

        energyStatusController = FindObjectOfType<EnergyStatusController>();
        if (energyStatusController == null)
        {
            Debug.LogError("No EnergyStatusController component found in the scene.");
            return;
        }
    }

    private void Start()
    {
        StartCoroutine(UpdateLightingCoroutine());
    }

    private IEnumerator UpdateLightingCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f);
            UpdateLightingColor();
        }
    }

    private void UpdateLightingColor()
    {
        if (globalLight == null || energyStatusController == null)
        {
            return;
        }

        float averageEnergyConsumption = energyStatusController.averageEnergyConsumption;

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
