using System;
using NeuralNet;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public event Action<Bird> onDead;
    public bool isAlive { get; private set; }
    public float fitness { get; private set; }
    public NeuralNetwork brain { get; private set; }
    
    [SerializeField] private float jumpPower;

    private float nearestObstacleDistance;
    private float minHeight;
    private float maxHeight;
    private Map map;
    private Rigidbody2D rb;
    private float spawnTime;

    public void Inject(Map map)
    {
        this.map = map;
    }
    
    public void SetBrain(NeuralNetwork brain)
    {
        this.brain = brain;
        fitness = 0;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        spawnTime = Time.time;
        isAlive = true;
    }

    private void Update()
    {
        CalculateNeuralInputParams();
        if (brain == null) return;
        UseBrain();
        if (isAlive) fitness = Time.time - spawnTime;
    }
    
    private void CalculateNeuralInputParams()
    {
        nearestObstacleDistance = Mathf.Abs(transform.position.x - map.transform.position.x - 
                                            map.movedObstacles.transform.localPosition.x - 
                                            map.nearestObstacle.transform.localPosition.x);
        minHeight = map.nearestObstacle.minHeight;
        maxHeight = map.nearestObstacle.maxHeight;
       
    }
    
    private void UseBrain()
    {
        var inputs = new[]
        {
            nearestObstacleDistance,
            minHeight, 
            maxHeight, 
            transform.position.y
        };
        var output = brain.FeedForward(inputs);
        if (output[0] > 0) Jump();
    }

    private void Jump()
    {
        rb.velocity = Vector2.zero;
        rb.AddRelativeForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") || col.CompareTag("CheckPoint")) return;
        
        rb.isKinematic = true;
        rb.velocity = Vector2.zero;
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        isAlive = false;
        onDead?.Invoke(this);
    }
}