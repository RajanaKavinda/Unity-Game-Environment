using System.Collections;
using UnityEngine;

// Concrete observer class for tree control
public class TreeController : MonoBehaviour, IObserver
{
    private Animator treeAnim;

    private readonly string[] growAnimations = {
        "grow1", "grow2", "grow3", "grow4", "grow5", "grow6", "grow7", "grow8", "grow9", "grow10", "grow11"
    };
    private const string DIE1_ANIMATION = "die1";
    private const string DIE2_ANIMATION = "die2";

    private void Awake()
    {
        treeAnim = GetComponent<Animator>();
        ResetAllAnimations();
        EnergyStatusController.Instance.RegisterObserver(this);
    }

    public void UpdateData(float averageEnergyConsumption, int fruitsOrFlowers)
    {
        StartCoroutine(UpdateCoroutine(fruitsOrFlowers));
    }

    private IEnumerator UpdateCoroutine(int fruitsOrFlowers)
    {
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
        yield return null;
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
            treeAnim.SetBool(growAnimations[i], i <= fruitsOrFlowers / 10);
        }
    }

    private void ResetAllAnimations()
    {
        SetDieAnimation(false, false);
        SetGrowAnimation(false);
    }
}
