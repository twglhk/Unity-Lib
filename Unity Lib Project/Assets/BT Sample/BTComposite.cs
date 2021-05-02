using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class BTComposite : BTNode
{
    public List<BTNode> Children { get; set; }
    public BTComposite(BehaviourTree tree, BTNode[] nodes) : base(tree)
    {
        Children = new List<BTNode>(nodes);
    }
}