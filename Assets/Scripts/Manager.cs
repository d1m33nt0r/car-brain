using System;
using System.Collections.Generic;
using System.Linq;
using NeuralNet;
using NeuralNet.Core;
using UnityEngine;

public class Manager : MonoBehaviour
{
    private int generation = 1;
    private int deadCount;
    private bool first;
    private bool second;
    private bool third;
    public event Action onGenerationStarted;
    public event Action onGenerationEnded;
    public event Action<int, float> onUpdateUI;
    
    [SerializeField] private Bird birdPrefab;
    [SerializeField] private int populationSize;
    [SerializeField] private Map map;
    [SerializeField] private int top;
    
    private List<BrainController> lastPopupation = new ();
    private List<Bird> birdList = new ();
    private int[] layers = { 4, 8, 8, 1 };
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
        for (var i = 0; i < populationSize; i++)
        {
            birdList.Add(Instantiate(birdPrefab, Vector3.left * 5, Quaternion.identity));
            birdList[i].Inject(map);
            BrainController brain;
            if (generation > 1)
            {
                brain = lastPopupation[i];
            }
            else
            {
                brain = new BrainController("Assets/BestBirdBrainAsset2.json");
            }
            birdList[i].SetBrain(brain);
            birdList[i].onDead += OnBirdDeathCallback;
        }
    }

    private void EndGeneration()
    {
        lastPopupation.Clear();
        birdList.Sort(SortByFitness);
        foreach (var bird in birdList)
        {
            lastPopupation.Add(bird.brain);
            Destroy(bird.gameObject);
        }

        /*if (birdList.First().fitness > 2 && !first)
        {
            Serializer.WriteToJson("Assets/BestBirdBrainAsset1.json", birdList.First().brain.Data, false);
            first = true;
        }
        
        if (birdList.First().fitness > 18 && !second)
        {
            Serializer.WriteToJson("Assets/BestBirdBrainAsset2.json", birdList.First().brain.Data, false);
            second = true;
        }
        
        if (birdList.First().fitness > 75 && !third)
        {
            Serializer.WriteToJson("Assets/BestBirdBrainAsset3.json", birdList.First().brain.Data, false);
            third = true;
        }*/
        ApplyMutation();
        birdList.Clear();
        generation++;
        onGenerationEnded?.Invoke();
    }

    private int SortByFitness(Bird a, Bird b)
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
    
    private void OnBirdDeathCallback(Bird bird)
    {
        deadCount++;
        if (deadCount == populationSize) EndGeneration();
    }
}