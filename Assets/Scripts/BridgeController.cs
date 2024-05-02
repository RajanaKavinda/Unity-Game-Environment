using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeController : MonoBehaviour
{
    public int lands = 1; // Variable to store the number of lands

    void Start()
    {
        UpdateBridgesVisibility();
    }

    void Update()
    {
        // Check if the number of lands is greater than the bridge number
        // If so, set the bridges with numbers less than or equal to lands as visible
        UpdateBridgesVisibility();
    }

    void UpdateBridgesVisibility()
    {
        // Loop through each child of the ground container
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;

            // Check if the child's name contains "bridge"
            if (child.name.Contains("bridge"))
            {
                // Get the bridge number from the child's name
                int bridgeNumber = int.Parse(child.name.Replace("bridge", ""));

                // Set the bridge's visibility based on the number of lands
                child.SetActive(lands >= bridgeNumber+1);
            }
        }
    }
}
