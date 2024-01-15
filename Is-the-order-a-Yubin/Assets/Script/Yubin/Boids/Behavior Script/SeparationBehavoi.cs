using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boids/Behavior/Separation")]
public class SeparationBehavoi : BoidsBehavior
{
    public override Vector2 CalculateMove(BoidsAgent agent, List<Transform> context, Boids boid)
    {
        // 근처에 없다면 0을 반환
        if (context.Count == 0)
        {
            return Vector2.zero;
        }

        Vector2 separationMove = Vector2.zero;
        int nSeparation = 0;    
        foreach (Transform item in context)
        {
            if(Vector2.SqrMagnitude(item.position - agent.transform.position) <boid.SquareAvoidanceRadius)
            {
                nSeparation++;
                separationMove += (Vector2)(agent.transform.position - item.position);
            }
        }

        if (nSeparation > 0)
        {
            separationMove /= nSeparation;
        }

        return separationMove;
    }
}
