using UnityEngine;
using UnityEngine.UI;

public class VitalFunctionsVisual : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    [SerializeField] private Image thirstBar;
    [SerializeField] private Image hungerBar;
    [SerializeField] private Image energyBar;

    private float transitionTime = 1f;
    private Vector3 initialBarSize;

    private void Start()
    {
        initialBarSize = healthBar.rectTransform.sizeDelta;
    }

    public void UpdateHealthBar(float health, int healthMax)
    {
        healthBar.rectTransform.LeanSize(CalcBarDeltaSize(health, healthMax), transitionTime);
    }

    public void UpdateThirstBar(float thirst, int thirstMax)
    {
        thirstBar.rectTransform.LeanSize(CalcBarDeltaSize(thirst, thirstMax), transitionTime);
    }

    public void UpdateHungerBar(float hunger, int hungerMax)
    {
        hungerBar.rectTransform.LeanSize(CalcBarDeltaSize(hunger, hungerMax), transitionTime);
    }

    public void UpdateEnergyBar(float energy, int energyMax)
    {
        energyBar.rectTransform.sizeDelta = CalcBarDeltaSize(energy, energyMax);
    }

    private Vector2 CalcBarDeltaSize(float value, float maxValue)
    {
        return new Vector2((initialBarSize.x / maxValue) * value, initialBarSize.y);
    }
}
