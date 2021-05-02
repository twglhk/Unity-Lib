using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CleverCrow.Fluid.BTs.Tasks;
using CleverCrow.Fluid.BTs.Trees;

public class TestAI : MonoBehaviour
{
    //When creating trees you'll need to store them in a variable to properly cache all the necessary data.
    [SerializeField]
    private BehaviorTree _tree;

    [SerializeField]
    private bool _isTreeUse;

    private void Awake()
    {
        _tree = new BehaviorTreeBuilder(gameObject)
            .Sequence()
                .Condition("Custom Condition", () => {
                    return _isTreeUse;
                })
                .Do("Custom Action", () => {
                    Debug.Log("HI!");
                    return TaskStatus.Success;
                })
            .End()
            .Build();
    }

    private void Update()
    {
        // Update our tree every frame
        _tree.Tick();
    }
}
