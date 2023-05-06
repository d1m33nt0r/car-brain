using System;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using NeuralNet;
using NeuralNet.Core;
using TMPro;
using UnityEngine;

public class CarManager : MonoBehaviour
{
    [SerializeField] private Transform carSpawnPoint;
    [SerializeField] private TextMeshProUGUI gen;
    [SerializeField] private TextMeshProUGUI bestFit;
    [SerializeField] private CameraFollow cameraFollow;
    
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
    
    private List<NeuralBehaviour> carList = new ();
    private BrainController bestBrain;
    
    private void Start()
    {
        StartGeneration();
        onGenerationEnded += StartGeneration;
 
    }

    private void StartGeneration()
    {
        deadCount = 0;
        InstantiatePopulation();
        onGenerationStarted?.Invoke();
    }

    private void InstantiatePopulation()
    {
        for (var i = 0; i < populationSize; i++)
        {
            var carInstance = Instantiate(carPrefab, carSpawnPoint.position, Quaternion.AngleAxis(90, Vector3.up));
            var neuralBehaviour = carInstance.GetComponentInChildren<NeuralBehaviour>();
            carList.Add(neuralBehaviour);
            BrainController brain;
            if (generation > 1)
            {
                brain = bestBrain.Copy();
                brain.Mutate(100, 100, -30, 30, true);
            }
            else
            {
                brain = new BrainController("Assets/SuperCar.json", true);
            }
            carList[i].SetBrain(brain);
            carList[i].onFailed += OnNeuralFail;
        }
        cameraFollow.SetTarget(carList[0].transform);
    }

    [Button("End Generation")]
    private void EndGeneration()
    {
        carList.Sort(SortByFitness);
       
        if (bestBrain != null) 
        {
            if (bestBrain.fitness < carList.First().brain.fitness)
                bestBrain = carList.First().brain.Copy();
        }
        else
        {
            bestBrain = carList.First().brain.Copy();
        }
        
        bestFit.text = "Best score: " + bestBrain.fitness;
        
        foreach (var car in carList)
        {
            Destroy(car.transform.parent.gameObject);
        }
        
        carList.Clear();
        generation++;
        gen.text = "Generation: " + generation;
        onGenerationEnded?.Invoke();
    }

    [Button("Save best")]
    private void SaveBestResult()
    {
        Serializer.WriteToJson("Assets/CarBrainAssetResult2.json", bestBrain.Data, false);
    }

    private int SortByFitness(NeuralBehaviour a, NeuralBehaviour b)
    {
        return -a.brain.fitness.CompareTo(b.brain.fitness);
    }

    private void OnNeuralFail(NeuralBehaviour bird)
    {
        deadCount++;
        if (deadCount == populationSize) EndGeneration();
    }
}