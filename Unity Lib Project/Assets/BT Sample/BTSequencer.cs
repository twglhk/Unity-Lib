using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class BTSequencer : BTComposite
{
    private int _currentNode = 0;
    public BTSequencer(BehaviourTree tree, BTNode[] children) : base(tree, children)
    {

    }

    public override Result Execute()
    {
        if (_currentNode < Children.Count())
        {
            Result result = Children[_currentNode].Execute();

            if (result == Result.Running)
                return Result.Running;
            else if (result == Result.Failure)
            {
                _currentNode = 0;
                return Result.Failure;
            }
            else // Success
            {
                _currentNode++;
                if (_currentNode < Children.Count)
                    return Result.Running;
                else
                {
                    _currentNode = 0;
                    return Result.Success;
                }
            }
        }
        return Result.Success;
    }
}