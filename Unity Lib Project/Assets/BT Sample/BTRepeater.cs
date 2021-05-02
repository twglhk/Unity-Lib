using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class BTRepeater : BTDecorator
{
    public BTRepeater(BehaviourTree tree, BTNode child) : base(tree, child)
    {

    }

    public override Result Execute()
    {
        Debug.Log("Child returned " + Child.Execute());
        return Result.Running;
    }
}