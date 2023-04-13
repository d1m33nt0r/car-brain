using System;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public event Action<Obstacle> onPlayerPassingObstacle;
    public Vector3 position;
    public float minHeight { get; private set; }
    public float maxHeight { get; private set; }
    
    [SerializeField] private Transform min;
    [SerializeField] private Transform max;
    
    private void Start()
    {
        minHeight = min.localScale.y;
        maxHeight = max.localScale.y;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player")) onPlayerPassingObstacle?.Invoke(this);
    }
}