using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Layers
{
    public const int all = -1;
}

public class ColliderChecker : MonoBehaviour
{
    private static Collider[] _allocatedColliderArr = new Collider[20];
    private static List<Collider> _resultColliderList = new List<Collider>();

    public static Collider CheckCollider(Transform _objTr, float _radius = 1f, int _layer = Layers.all)
    {
        return CheckCollider(_objTr, _radius, _layer, (c) => true);
    }

    public static Collider CheckCollider(Vector3 targetPos, float _radius = 1f, int _layer = Layers.all)
    {
        return CheckCollider(targetPos, _radius, _layer, (c) => true);
    }

    /// <summary>
    /// Get the closest collider from transform in range
    /// </summary>
    /// <param name="_objTr"> target transform </param>
    /// <param name="_conditionDelegate"> condition delegate of searching </param>
    /// <param name="_radius"> searching range </param>
    /// <param name="_layer"> searching layer </param>
    /// <returns></returns>
    public static Collider CheckCollider(Transform _objTr, float _radius, int _layer, Func<Collider, bool> _conditionDelegate)
    {
        var searchedColliders = Physics.OverlapSphere(_objTr.position, _radius, _layer).ToList();
        var targetColliders = from col in searchedColliders
                              where col.transform != _objTr && _conditionDelegate(col)
                              select col;

        if (targetColliders.Count() == 0) return null;
        return targetColliders.Aggregate((Collider a, Collider b)
            => Vector3.SqrMagnitude(a.transform.position - _objTr.position) < Vector3.SqrMagnitude(b.transform.position - _objTr.position) ? a : b);
    }

    /// <summary>
    /// Get the closest collider from transform in range, but it will not be call GC
    /// </summary>
    /// <param name="_objTr"> target transform </param>
    /// <param name="_conditionDelegate"> condition delegate of searching </param>
    /// <param name="_radius"> searching range </param>
    /// <param name="_layer"> searching layer </param>
    /// <returns></returns>
    /// <returns></returns>
    public static Collider CheckColliderNonAlloc(Transform _objTr, float _radius, int _layer, Func<Collider, bool> _conditionDelegate)
    {
        var bufferCount = Physics.OverlapSphereNonAlloc(_objTr.position, _radius, _allocatedColliderArr, _layer);
        if (bufferCount == 0)
            return null;

        Collider minCollider = null;
        float minSqrDistance = 1000f;

        for (int i = 0; i < bufferCount; ++i)
        {
            if (_allocatedColliderArr[i].transform == _objTr)
                continue;
            if (!_conditionDelegate(_allocatedColliderArr[i]))
                continue;

            var curColSqrDistance = Vector3.SqrMagnitude(_allocatedColliderArr[i].transform.position - _objTr.position);

            if (curColSqrDistance < minSqrDistance || minCollider == null)
            {
                minCollider = _allocatedColliderArr[i];
                minSqrDistance = curColSqrDistance;
            }
        }

        return minCollider;
    }

    /// <summary>
    /// Get the closest collider from position in range
    /// </summary>
    /// <param name="_objTr"> target transform </param>
    /// <param name="_conditionDelegate"> condition delegate of searching </param>
    /// <param name="_radius"> searching range </param>
    /// <param name="_layer"> searching layer </param>
    /// <returns></returns>
    public static Collider CheckCollider(Vector3 targetPos, float _radius, int _layer, Func<Collider, bool> _conditionDelegate)
    {
        var searchedColliders = Physics.OverlapSphere(targetPos, _radius, _layer).ToList();
        var targetColliders = from col in searchedColliders
                              where _conditionDelegate(col)
                              select col;

        if (targetColliders.Count() == 0) return null;
        return targetColliders.Aggregate((Collider a, Collider b)
            => Vector3.SqrMagnitude(a.transform.position - targetPos) < Vector3.SqrMagnitude(b.transform.position - targetPos) ? a : b);
    }

    /// <summary>
    /// Get the closest collider from transform in range, but it will not be call GC
    /// </summary>
    /// <param name="targetPos"> target position </param>
    /// <param name="_conditionDelegate"> condition delegate of searching </param>
    /// <param name="_radius"> searching range </param>
    /// <param name="_layer"> searching layer </param>
    /// <returns></returns>
    public static Collider CheckColliderNonAlloc(Vector3 targetPos, float _radius, int _layer, Func<Collider, bool> _conditionDelegate)
    {
        var bufferCount = Physics.OverlapSphereNonAlloc(targetPos, _radius, _allocatedColliderArr, _layer);
        if (bufferCount == 0)
            return null;

        Collider minCollider = null;
        float minSqrDistance = 1000f;

        for (int i = 0; i < bufferCount; ++i)
        {
            if (!_conditionDelegate(_allocatedColliderArr[i]))
                continue;

            var curColSqrDistance = Vector3.SqrMagnitude(_allocatedColliderArr[i].transform.position - targetPos);

            if (curColSqrDistance < minSqrDistance || minCollider == null)
            {
                minCollider = _allocatedColliderArr[i];
                minSqrDistance = curColSqrDistance;
            }
        }

        return minCollider;
    }

    public static List<Collider> CheckColliders(Transform _objTr, float _radius = 1f, int _layer = Layers.all)
    {
        return CheckColliders(_objTr, _radius, _layer, (c) => true);
    }
    
    public static List<Collider> CheckColliders(Vector3 targetPos, float _radius = 1f, int _layer = Layers.all)
    {
        return CheckColliders(targetPos, _radius, _layer, (c) => true);
    }


    /// <summary>
    /// Get IEnumerable<Colliider> from transform in range
    /// </summary>
    /// <param name="_objTr"> target transform </param>
    /// <param name="_conditionDelegate"> condition delegate of searching </param>
    /// <param name="_radius"> searching range </param>
    /// <param name="_layer"> searching layer </param>
    /// <returns></returns>
    public static List<Collider> CheckColliders(Transform _objTr, float _radius, int _layer, Func<Collider, bool> _conditionDelegate)
    {
        var searchedColliders = Physics.OverlapSphere(_objTr.position, _radius, _layer).ToList();
        var targetColliders = from col in searchedColliders
                              where col.transform != _objTr && _conditionDelegate(col)
                              select col;

        if (targetColliders.Count() == 0) return null;
        return targetColliders.ToList();
    }
    
    public static List<Collider> CheckColliders(Vector3 targetPos, float _radius, int _layer, Func<Collider, bool> _conditionDelegate)
    {
        var searchedColliders = Physics.OverlapSphere(targetPos, _radius, _layer).ToList();
        var targetColliders = from col in searchedColliders
                              where _conditionDelegate(col)
                              select col;

        if (targetColliders.Count() == 0) return null;
        return targetColliders.ToList();
    }

    public static List<Collider> CheckCollidersNonAlloc(Transform _objTr, float _radius, int _layer, Func<Collider, bool> _conditionDelegate)
    {
        var bufferCount = Physics.OverlapSphereNonAlloc(_objTr.position, _radius, _allocatedColliderArr, _layer);
        if (bufferCount == 0)
            return null;

        _resultColliderList.Clear();
        for (int i = 0; i < bufferCount; ++i)
        {
            if (_allocatedColliderArr[i].transform == _objTr || !_conditionDelegate(_allocatedColliderArr[i]))
            {
                continue;
            }
            _resultColliderList.Add(_allocatedColliderArr[i]);
        }

        if (_resultColliderList.Count == 0)
            return null;
        return _resultColliderList;
    }

    public static List<Collider> CheckCollidersNonAlloc(Vector3 targetPos, float _radius, int _layer, Func<Collider, bool> _conditionDelegate)
    {
        var bufferCount = Physics.OverlapSphereNonAlloc(targetPos, _radius, _allocatedColliderArr, _layer);
        if (bufferCount == 0)
            return null;

        _resultColliderList.Clear();
        for (int i = 0; i < bufferCount; ++i)
        {
            if (!_conditionDelegate(_allocatedColliderArr[i]))
            {
                continue;
            }
            _resultColliderList.Add(_allocatedColliderArr[i]);
        }

        if (_resultColliderList.Count == 0)
            return null;
        return _resultColliderList;
    }
}
