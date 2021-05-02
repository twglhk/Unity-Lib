using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
   
public class BTDecorator : BTNode
{
    public BTNode Child { get; set; }

    public BTDecorator(BehaviourTree tree, BTNode child) : base(tree)
    {
        Child = child;
    }
}