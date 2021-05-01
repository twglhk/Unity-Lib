using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class BTRandomWalk : BTNode
{
    protected Vector3 NextDestination { get; set; }
    public float speed = 10;
    public BTRandomWalk(BehaviourTree tree) : base(tree)
    {
        NextDestination = Vector3.zero;
        FindNextDestination();
    }

    public bool FindNextDestination()
    {
        Vector3 vec;
        bool found = false;
        found = Tree.Blackboard.TryGetValue("WorldBounds", out vec);
        if (found)
        {
            float x = UnityEngine.Random.value * vec.x;
            float y = UnityEngine.Random.value * vec.y;
            float z = UnityEngine.Random.value * vec.z;
            NextDestination = new Vector3(x, y, z);
        }

        return found;
    }

    public override Result Execute()
    {
        // if we've arrived at the point, then find the next destination
        if (Tree.gameObject.transform.position == NextDestination)
        {
            if (!FindNextDestination())
                return Result.Failure;
            else
                return Result.Success;
        }
        else
        {
            Tree.gameObject.transform.position = Vector3.MoveTowards(Tree.gameObject.transform.position,
                NextDestination, Time.deltaTime * speed);
            return Result.Running;
        }
    }
}