using System.Collections;
using UnityEngine;

// Concrete observer class for tree control
public class TreeController : MonoBehaviour, IObserver
{
    private Animator treeAnim;

    // Array of grow animation states
    private readonly string[] growAnimations = {
        "grow1", "grow2", "grow3", "grow4", "grow5", "grow6", "grow7", "grow8", "grow9", "grow10", "grow11"
    };

    // Death animation states
    private const string DIE1_ANIMATION = "die1";
    private const string DIE2_ANIMATION = "die2";

    private void Awake()
    {
        // Get the Animator component attached to the GameObject
        treeAnim = GetComponent<Animator>();
        // Reset all animations at the start
        ResetAllAnimations();
        // Register this TreeController as an observer
        EnergyStatusController.Instance.RegisterObserver(this);
    }

    private void OnDestroy()
    {
        // Remove this TreeController from the observer list when it is destroyed
        if (EnergyStatusController.Instance != null)
        {
            EnergyStatusController.Instance.RemoveObserver(this);
        }
    }

    // Implementation of IObserver interface method to update data
    public void UpdateData(float averageEnergyConsumption, int fruitsOrFlowers)
    {
        // Start a coroutine to update the tree based on the fruitsOrFlowers value
        StartCoroutine(UpdateCoroutine(fruitsOrFlowers));
    }

    private IEnumerator UpdateCoroutine(int fruitsOrFlowers)
    {
        if (fruitsOrFlowers <= -5)
        {
            // Set the tree to die completely
            SetDieAnimation(true, true);
            SetGrowAnimation(false);
        }
        else if (fruitsOrFlowers < 0)
        {
            // Set the tree to die partially
            SetDieAnimation(true, false);
            SetGrowAnimation(false);
        }
        else if (fruitsOrFlowers <= 5)
        {
            // Keep the tree in a neutral state
            SetDieAnimation(false, false);
            SetGrowAnimation(false);
        }
        else
        {
            // Set the tree to grow based on the fruitsOrFlowers value
            SetDieAnimation(false, false);
            SetGrowAnimationBasedOnFruitsOrFlowers(fruitsOrFlowers);
        }
        yield return null;
    }

    // Method to set death animations
    private void SetDieAnimation(bool die1, bool die2)
    {
        treeAnim.SetBool(DIE1_ANIMATION, die1);
        treeAnim.SetBool(DIE2_ANIMATION, die2);
    }

    // Method to set all grow animations to a specific state
    private void SetGrowAnimation(bool grow)
    {
        foreach (var animation in growAnimations)
        {
            treeAnim.SetBool(animation, grow);
        }
    }

    // Method to set grow animations based on the fruitsOrFlowers value
    private void SetGrowAnimationBasedOnFruitsOrFlowers(int fruitsOrFlowers)
    {
        for (int i = 0; i < growAnimations.Length; i++)
        {
            treeAnim.SetBool(growAnimations[i], i < fruitsOrFlowers / 10);
        }
    }

    // Method to reset all animations to their default states
    private void ResetAllAnimations()
    {
        SetDieAnimation(false, false);
        SetGrowAnimation(false);
    }
}
