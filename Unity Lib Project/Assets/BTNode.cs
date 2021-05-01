using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTNode
{
    public enum Result { Running, Failure, Success };

    public BehaviourTree Tree { get; set; }

    public BTNode(BehaviourTree tree)
    {
        Tree = tree;
    }

    public virtual Result Execute()
    {
        return Result.Failure;
    }
}
