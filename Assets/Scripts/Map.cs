using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Map : MonoBehaviour
{
    public Obstacle nearestObstacle { get; private set; }
    
    [SerializeField] private List<Obstacle> obstacleVariants;
    [SerializeField] private float obstacleDistance;
    [SerializeField] private float movementSpeed;
    [SerializeField] private int mapWidth;
    [SerializeField] private Vector3 startPosition;
    [SerializeField] public Transform movedObstacles;
    [SerializeField] private Manager manager;
    
    private Queue<Obstacle> obstacles;
    private bool gameLoopIsActive;
    private bool wait;
    
    private void Start()
    {
        manager.onGenerationStarted += () => gameLoopIsActive = true;
        manager.onGenerationEnded += () =>
        {
            gameLoopIsActive = false;
            ResetMap();
        };
        Initialize();
    }

    private void Initialize()
    {
        obstacles = new Queue<Obstacle>();

        for (var i = 0; i < mapWidth; i++)
        {
            var randomWallIndex = Random.Range(0, obstacleVariants.Count);
            var wallPosition = new Vector3(startPosition.x + i * obstacleDistance, startPosition.y, startPosition.z);
            var wall = Instantiate(obstacleVariants[randomWallIndex], wallPosition, Quaternion.identity, movedObstacles);
            wall.onPlayerPassingObstacle += ObstaclePassingCallback;
            obstacles.Enqueue(wall);
        }
        
        nearestObstacle = obstacles.Dequeue();
        wait = false;
    }

    private void Update()
    {
        if (wait)
        {
            nearestObstacle = obstacles.Dequeue();
            wait = false;
        }
        if (gameLoopIsActive) return;
        movedObstacles.position += Vector3.left * movementSpeed * Time.deltaTime;
    }

    private void ObstaclePassingCallback(Obstacle obstacle)
    {
        Destroy(obstacle.gameObject);
        wait = true;
    }

    private void ResetMap()
    {
        movedObstacles.position = startPosition;
        var countIterations = obstacles.Count;
        for (var i = 0; i < countIterations; i++)
        {
            var obstacleInstance = obstacles.Dequeue();
            Destroy(obstacleInstance.gameObject);
        }
        if (nearestObstacle != null) Destroy(nearestObstacle.gameObject);
        Initialize();
    }
}
