using System.Collections;
using UnityEngine;

public class VitalFunctionsManager : MonoBehaviour
{
    public static VitalFunctionsManager Instance { get; set; }

    [SerializeField] private VitalFunctionsVisual vitalFunctionsVisual;

    private int healthMax = 100;
    private int thirstMax = 100;
    private int hungerMax = 100;
    private int energyMax = 100;

    private float healthValue = 100;
    private float thirstValue = 100;
    private float hungerValue = 100;
    private float energyValue = 100;

    [SerializeField] private float starvationTime = 20;
    [SerializeField] private float thirstTime = 10;

    [SerializeField] private float starveDecrease = -10;
    [SerializeField] private float thirstDecrease = -5;

    public void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartCoroutine(StarvingCouroutine());
        StartCoroutine(ThirstingCouroutine());
    }

    IEnumerator StarvingCouroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(starvationTime);
            UpdateHunger(starveDecrease);
        }
    }

    IEnumerator ThirstingCouroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(thirstTime);
            UpdateThirst(thirstDecrease);
        }
    }

    public void UpdateHunger(float value)
    {
        hungerValue += value;

        if (hungerValue < 0)
        {
            hungerValue = 0;
        }
        else if (hungerValue > hungerMax)
        {
            hungerValue = hungerMax;
        }

        vitalFunctionsVisual.UpdateHungerBar(hungerValue, hungerMax);
    }

    public void UpdateThirst(float value)
    {
        thirstValue += value;

        if (thirstValue < 0)
        {
            thirstValue = 0;
        }
        else if (thirstValue > thirstMax)
        {
            thirstValue = thirstMax;
        }

        vitalFunctionsVisual.UpdateThirstBar(thirstValue, thirstMax);
    }

    public void UpdateHealth(float value)
    {
        healthValue += value;

        if (healthValue < 0)
        {
            healthValue = 0;
        }
        else if (healthValue > healthMax)
        {
            healthValue = healthMax;
        }

        vitalFunctionsVisual.UpdateHealthBar(healthValue, healthMax);
    }

    public void UpdateEnergy(float value)
    {
        energyValue += value;

        if (energyValue < 0)
        {
            energyValue = 0;
        }
        else if (energyValue > energyMax)
        {
            energyValue = energyMax;
        }

        vitalFunctionsVisual.UpdateEnergyBar(energyValue, energyMax);
    }

    public bool HasEnergy()
    {
        return energyValue > 0;
    }
}
