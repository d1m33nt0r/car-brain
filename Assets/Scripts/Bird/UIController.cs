using System;
using System.Globalization;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gen;
    [SerializeField] private TextMeshProUGUI fitness;

    [SerializeField] private Manager manager;
    
    private void Start()
    {
        manager.onUpdateUI += UpdateUI;
    }

    private void UpdateUI(int gen, float fitness)
    {
        this.gen.text = "Gen: " + gen;
        this.fitness.text = "Fitness: " + fitness.ToString("F1", CultureInfo.InvariantCulture);
    }
}