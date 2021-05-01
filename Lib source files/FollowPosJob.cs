using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Jobs;
using UnityEngine.Serialization;
using Unity.Jobs;
using Unity.Burst;
using Unity.Mathematics;
using Unity.Collections;
using System.Linq;

/// <summary>
/// Set GameObject to [Follow Target] from others with C# JobSystem.
/// </summary>
public class FollowPosJob : MonoBehaviour
{
    [Header("Setting *")]
    [FormerlySerializedAs("Follow Target Transform")]
    [SerializeField] Transform _followTargetTr; 
            
    private readonly List<Transform> _followedTransformList = new List<Transform>();
    private readonly List<Vector3> _fixPosList = new List<Vector3>();

    void Start()
    {
        if (_followTargetTr == null)
            _followTargetTr = transform;
    }

    // Update is called once per frame
    void Update()
    {
        var listCount = _followedTransformList.Count;
        NativeArray<float3> transformPosArr = new NativeArray<float3>(listCount, Allocator.TempJob);
        NativeArray<float3> fixPosArr = new NativeArray<float3>(listCount, Allocator.TempJob);

        for (int i = 0; i < listCount; ++i)
        {
            transformPosArr[i] = _followedTransformList[i].position;
            fixPosArr[i] = _fixPosList[i];
        }

        FollowTransformJob followJob = new FollowTransformJob
        {
            targetPos = _followTargetTr.position,
            transformPosArr = transformPosArr,
            fixPosArr = fixPosArr
        };

        JobHandle followJobHandle = followJob.Schedule(_followedTransformList.Count, 20);

        followJobHandle.Complete();

        for (int i = 0; i < listCount; ++i)
        {
            _followedTransformList[i].position = transformPosArr[i];
        }
                    
        transformPosArr.Dispose();
        fixPosArr.Dispose();
    }

    [BurstCompile]
    private struct FollowTransformJob : IJobParallelFor
    {
        public float3 targetPos;
        public NativeArray<float3> transformPosArr;
        public NativeArray<float3> fixPosArr;

        public void Execute(int index)
        {
            transformPosArr[index] = targetPos + fixPosArr[index];
        }
    }

    public void AddFollowedTransform(Transform followedTr, Vector3 fixPos)
    {
        _followedTransformList.Add(followedTr);
        _fixPosList.Add(fixPos);
    }
}