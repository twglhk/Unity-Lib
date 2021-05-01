using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTree : MonoBehaviour
{
    private BTNode _root;
    private bool _startedBehaviour;
    private Coroutine _behaviour;

    public Dictionary<string, Vector3> Blackboard { get; set; }
    public BTNode Root { get { return _root; } }

    // Start is called before the first frame update
    void Start()
    {
        Blackboard = new Dictionary<string, Vector3>();
        Blackboard.Add("WorldBounds", new Vector3(5f, 5f, 5f));

        // initial behaviour is stooped
        _startedBehaviour = false;

        //_root = new BTNode(this);
        _root = new BTRepeater(this, new BTSequencer(this,
            new BTNode[] { new BTRandomWalk(this) }));
    }

    // Update is called once per frame
    void Update()
    {
        if (!_startedBehaviour)
        {
            _behaviour = StartCoroutine(CoRunBehaviour());
            _startedBehaviour = true;
        }
    }

    IEnumerator CoRunBehaviour()
    {
        BTNode.Result result = Root.Execute();
        while (result == BTNode.Result.Running)
        {
            Debug.Log("Root result: " + result);
            yield return null;
            result = Root.Execute();
        }

        Debug.Log("Behaviour has finished with " + result);
    }
}
