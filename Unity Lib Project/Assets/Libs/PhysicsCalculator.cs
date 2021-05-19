using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace John.GameLib.PhysicsLib
{
    class PhysicsCalculator
    {
        #region AddForce
        public static void Addforce(Rigidbody rigidbody, Vector3 force)
        {
            if (rigidbody == null) return;
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
            rigidbody.AddForce(force, ForceMode.Impulse);
        }
        #endregion

        #region Trajectory
        /// <summary>
        /// ������ ��� �ϰ� �ִ� Object�� Ư�� �ð����� ��ġ
        /// </summary>
        /// <param name="originPos"> ���� ��ġ </param>
        /// <param name="Vo"> �ʱ� �ӵ� </param>
        /// <param name="time"> �ð� </param>
        /// <returns></returns>
        public static Vector3 CalculatePosInTime(Vector3 originPos, Vector3 Vo, float time)
        {
            // ������ �ӵ� (������ ���� ������ ����)
            //Vector3 Vxz = Vo;   
            //Vxz.y = 0f;

            Vector3 result = originPos + Vo * time;
            float sY = (-0.5f * Mathf.Abs(Physics.gravity.y) * (time * time)) + (Vo.y * time) + originPos.y;

            result.y = sY;

            return result;
        }

        /// <summary>
        /// origin���� target���� �̵��� �� �̵� �ð� ��� ��� �ӵ� ���ϴ� �޼���
        /// </summary>
        /// <param name="target"></param>
        /// <param name="origin"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public static Vector3 CalculateVelocty(Vector3 target, Vector3 origin, float time)
        {
            // �� �������� �Ÿ�, origin -> target vector
            Vector3 distance = target - origin;

            // origin->master�� ���� ������ vector
            Vector3 distanceXz = distance;
            distanceXz.y = 0f;

            // �� �������� ���� ����
            float sY = distance.y;

            // ������ �Ÿ��� scala��
            float sXz = distanceXz.magnitude;

            // �̵� �ð� ��� ������ ��� �ӵ�
            float Vxz = sXz / time;

            // �̵� �ð� ��� ������ ��� �ӵ�
            float Vy = (sY / time) + (0.5f * Mathf.Abs(Physics.gravity.y) * time);

            // ������ ���� ���� x �ӵ�, ������ �ӵ�, �� ���� ���ؾ� �Ǵ� ���� ����
            Vector3 result = distanceXz.normalized;
            result *= Vxz;
            result.y = Vy;

            return result;
        }

        public static void ResetForce(in Rigidbody rigidbody)
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
        }
        #endregion
    }
}