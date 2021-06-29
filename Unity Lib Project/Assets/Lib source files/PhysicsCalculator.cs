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
        /// 포물선 운동을 하고 있는 Object의 특정 시간에서 위치
        /// </summary>
        /// <param name="originPos"> 시작 위치 </param>
        /// <param name="Vo"> 초기 속도 </param>
        /// <param name="time"> 시간 </param>
        /// <returns></returns>
        public static Vector3 CalculatePosInTime(Vector3 originPos, Vector3 Vo, float time)
        {
            // 가로축 속도 (저항이 없기 때문에 일정)
            //Vector3 Vxz = Vo;   
            //Vxz.y = 0f;

            Vector3 result = originPos + Vo * time;
            float sY = (-0.5f * Mathf.Abs(Physics.gravity.y) * (time * time)) + (Vo.y * time) + originPos.y;

            result.y = sY;

            return result;
        }

        /// <summary>
        /// origin에서 target으로 이동할 때 이동 시간 대비 평균 속도 구하는 메서드
        /// </summary>
        /// <param name="target"></param>
        /// <param name="origin"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public static Vector3 CalculateVelocty(Vector3 target, Vector3 origin, float time)
        {
            // 두 지점간의 거리, origin -> target vector
            Vector3 distance = target - origin;

            // origin->master로 가는 가로축 vector
            Vector3 distanceXz = distance;
            distanceXz.y = 0f;

            // 두 지점간의 높이 차이
            float sY = distance.y;

            // 가로축 거리의 scala값
            float sXz = distanceXz.magnitude;

            // 이동 시간 대비 가로축 평균 속도
            float Vxz = sXz / time;

            // 이동 시간 대비 세로축 평균 속도
            float Vy = (sY / time) + (0.5f * Mathf.Abs(Physics.gravity.y) * time);

            // 가로축 방향 벡터 x 속도, 세로축 속도, 즉 힘을 가해야 되는 방향 벡터
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