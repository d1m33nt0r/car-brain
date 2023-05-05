using System;
using System.Collections.Generic;
using System.Linq;
using NeuralNet;
using NeuralNet.Core;
using TMPro;
using UnityEngine;

public class CarManager : MonoBehaviour
{
    [SerializeField] private Transform carSpawnPoint;
    [SerializeField] private TextMeshProUGUI gen;
    
    private int generation = 1;
    private int deadCount;
    private bool first;
    private bool second;
    private bool third;
    public event Action onGenerationStarted;
    public event Action onGenerationEnded;
    public event Action<int, float> onUpdateUI;
    
    [SerializeField] private GameObject carPrefab;
    [SerializeField] private int populationSize;
    [SerializeField] private int top;
    
    private List<BrainController> lastPopupation = new ();
    private List<NeuralBehaviour> birdList = new ();
    private float bestScore;
    
    private void Start()
    {
        StartGeneration();
        onGenerationEnded += StartGeneration;
        onGenerationEnded += UpdateUI;
    }

    private void UpdateUI()
    {
        birdList.Sort(SortByFitness);
        bestScore = birdList[0].fitness;
        onUpdateUI?.Invoke(generation, bestScore);
    }

    private void Update()
    {
        UpdateUI();
    }

    private void StartGeneration()
    {
        deadCount = 0;
        InstantiatePopulation();
        onGenerationStarted?.Invoke();
    }

    private void InstantiatePopulation()
    {
        var mutation = false;
        for (var i = 0; i < populationSize; i++)
        {
            var carInstance = Instantiate(carPrefab, carSpawnPoint.position, Quaternion.identity);
            var neuralBehaviour = carInstance.GetComponentInChildren<NeuralBehaviour>();
            birdList.Add(neuralBehaviour);
            BrainController brain;
            if (generation > 1)
            {
                brain = lastPopupation[0].Copy();
                if (mutation)
                {
                    brain.Mutate(50, 100, -30, 30, true);
                }
                mutation = true;
            }
            else
            {
                brain = new BrainController("Assets/BrainAssetNew.json", true);
            }
            birdList[i].SetBrain(brain);
            birdList[i].onFailed += OnNeuralFail;
        }
    }

    private void EndGeneration()
    {
        lastPopupation.Clear();
        birdList.Sort(SortByFitness);
        foreach (var bird in birdList)
        {
            lastPopupation.Add(bird.brain);
            Destroy(bird.transform.parent.gameObject);
        }
        
        if (birdList.First().fitness > 75 && !third)
        {
            Serializer.WriteToJson("Assets/CarBrainAssetResult.json", birdList.First().brain.Data, false);
            third = true;
        }
        
       // ApplyMutation();
        birdList.Clear();
        generation++;
        gen.text = "Gen " + generation;
        onGenerationEnded?.Invoke();
    }

    private int SortByFitness(NeuralBehaviour a, NeuralBehaviour b)
    {
        return -a.fitness.CompareTo(b.fitness);
    }
    
    private void ApplyMutation()
    {
       
        var i = 0;
        foreach (var brain in lastPopupation)
        {
            if (i < top)
            {
                brain.Mutate(50, 100, -10f, 10f, false);
            }
            i++;
        }
    }
    
    private void OnNeuralFail(NeuralBehaviour bird)
    {
        deadCount++;
        if (deadCount == populationSize) EndGeneration();
    }
}