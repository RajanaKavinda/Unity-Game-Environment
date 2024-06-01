using UnityEngine;
using UnityEngine.Rendering.Universal;

// Concrete observer class for lighting control, using the Strategy pattern for lighting updates
public class LightingController : MonoBehaviour, IObserver
{
    private Light2D globalLight;
    private ILightingStrategy lightingStrategy;

    private void Awake()
    {
        globalLight = GetComponent<Light2D>();
        if (globalLight == null)
        {
            Debug.LogError("No Light2D component found on the GameObject.");
        }
        EnergyStatusController.Instance.RegisterObserver(this);
    }

    public void UpdateData(float averageEnergyConsumption, int fruitsOrFlowers)
    {
        SetLightingStrategy(averageEnergyConsumption);
        lightingStrategy.UpdateLighting(globalLight, averageEnergyConsumption);
    }

    private void SetLightingStrategy(float averageEnergyConsumption)
    {
        if (averageEnergyConsumption < 0.2f)
        {
            lightingStrategy = new LowConsumptionLighting();
        }
        else if (averageEnergyConsumption < 0.4f)
        {
            lightingStrategy = new MediumConsumptionLighting();
        }
        else if (averageEnergyConsumption < 0.8f)
        {
            lightingStrategy = new HighConsumptionLighting();
        }
        else
        {
            lightingStrategy = new VeryHighConsumptionLighting();
        }
    }
}

// Strategy interface for lighting update
public interface ILightingStrategy
{
    void UpdateLighting(Light2D light, float averageEnergyConsumption);
}

// Concrete strategies for different lighting conditions
public class LowConsumptionLighting : ILightingStrategy
{
    public void UpdateLighting(Light2D light, float averageEnergyConsumption)
    {
        light.color = new Color32(0xFF, 0xFF, 0xFF, 0xFF);
    }
}

public class MediumConsumptionLighting : ILightingStrategy
{
    public void UpdateLighting(Light2D light, float averageEnergyConsumption)
    {
        light.color = new Color32(0xB6, 0x4D, 0x4D, 0xFF);
    }
}

public class HighConsumptionLighting : ILightingStrategy
{
    public void UpdateLighting(Light2D light, float averageEnergyConsumption)
    {
        light.color = new Color32(0x96, 0x28, 0x28, 0xFF);
    }
}

public class VeryHighConsumptionLighting : ILightingStrategy
{
    public void UpdateLighting(Light2D light, float averageEnergyConsumption)
    {
        light.color = new Color32(0x80, 0x1A, 0x1A, 0xFF);
    }
}
