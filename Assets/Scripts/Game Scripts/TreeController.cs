using System.Collections;
using UnityEngine;

public class TreeController : MonoBehaviour
{
    private Animator treeAnim;
    private EnergyStatusController energyStatusController;

    private readonly string[] growAnimations = {
        "grow1", "grow2", "grow3", "grow4", "grow5", "grow6", "grow7", "grow8", "grow9", "grow10", "grow11"
    };
    private const string DIE1_ANIMATION = "die1";
    private const string DIE2_ANIMATION = "die2";

    private void Awake()
    {
        treeAnim = GetComponent<Animator>();
        energyStatusController = FindObjectOfType<EnergyStatusController>();

        ResetAllAnimations();
    }

    void Start()
    {
        // Start the update coroutine
        StartCoroutine(UpdateCoroutine());
    }

    IEnumerator UpdateCoroutine()
    {
        while (true)
        {
            // Wait for 10 seconds before the next update
            yield return new WaitForSeconds(10f);

            // Update the tree animation
            UpdateTreeAnimation();
        }
    }

    void UpdateTreeAnimation()
    {
        int fruitsOrFlowers = energyStatusController.fruitsOrFlowers;

        if (fruitsOrFlowers <= -5)
        {
            SetDieAnimation(true, true);
            SetGrowAnimation(false);
        }
        else if (fruitsOrFlowers < 0)
        {
            SetDieAnimation(true, false);
            SetGrowAnimation(false);
        }
        else if (fruitsOrFlowers <= 5)
        {
            SetDieAnimation(false, false);
            SetGrowAnimation(false);
        }
        else
        {
            SetDieAnimation(false, false);
            SetGrowAnimationBasedOnFruitsOrFlowers(fruitsOrFlowers);
        }
    }

    private void SetDieAnimation(bool die1, bool die2)
    {
        treeAnim.SetBool(DIE1_ANIMATION, die1);
        treeAnim.SetBool(DIE2_ANIMATION, die2);
    }

    private void SetGrowAnimation(bool grow)
    {
        foreach (var animation in growAnimations)
        {
            treeAnim.SetBool(animation, grow);
        }
    }

    private void SetGrowAnimationBasedOnFruitsOrFlowers(int fruitsOrFlowers)
    {
        for (int i = 0; i < growAnimations.Length; i++)
        {
            treeAnim.SetBool(growAnimations[i], i <= (fruitsOrFlowers / 10));
        }
    }

    private void ResetAllAnimations()
    {
        SetDieAnimation(false, false);
        SetGrowAnimation(false);
    }
}
