using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BoidsBehavior : ScriptableObject
{
    public abstract Vector2 CalculateMove(BoidsAgent agent, List<Transform> context, Boids boid);
}
