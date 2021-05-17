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
    public static Collider CheckCollider(Transform _objTr, float _radius = 1f, int _layer = Layers.all)
    {
        return CheckCollider(_objTr, _radius, _layer, (c) => true);
    }

    public static Collider CheckCollider(Vector3 targetPos, float _radius = 1f, int _layer = Layers.all)
    {
        return CheckCollider(targetPos, _radius, _layer, (c) => true);
    }

    /// <summary>
    /// ���� ���� Ž������ ������ �����ϴ� ���� ����� �ݶ��̴��� ����
    /// </summary>
    /// <param name="_objTr"> Ž���� ������ transform </param>
    /// <param name="_conditionDelegate"> Ž���� Collider ���� delegate </param>
    /// <param name="_radius"> Ž�� ���� </param>
    /// <param name="_layer"> Ž���� layer </param>
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
    /// ���� ���� Ž������ ������ �����ϴ� ���� ����� �ݶ��̴��� ����
    /// </summary>
    /// <param name="_objTr"> Ž���� ������ transform </param>
    /// <param name="_conditionDelegate"> Ž���� Collider ���� delegate </param>
    /// <param name="_radius"> Ž�� ���� </param>
    /// <param name="_layer"> Ž���� layer </param>
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

    public static List<Collider> CheckColliders(Transform _objTr, float _radius = 1f, int _layer = Layers.all)
    {
        return CheckColliders(_objTr, _radius, _layer, (c) => true);
    }

    /// <summary>
    /// ���� ���� Ž������ ������ �����ϴ� IEnumerable<Colliider>�� ����
    /// </summary>
    /// <param name="_objTr"> Ž���� ������ transform </param>
    /// <param name="_conditionDelegate"> Ž���� Collider ���� delegate </param>
    /// <param name="_radius"> Ž�� ���� </param>
    /// <param name="_layer"> Ž���� layer </param>
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
}
