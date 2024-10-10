using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace My2D
{
    //플레이어 이동 따른 시차효과 거리 구하기
    public class ParallaxEffect : MonoBehaviour
    {
        #region Variables
        public Camera m_camera;           //카메라
        public Transform followTarget;  //플레이어

        //시작 위치
        private Vector2 startingPosition;   //시작 위치 (배경, 카메라)
        private float startingZ;               //시작할 때 배경의 z축 위치값

        //시작지점으로 부터의 카메라가 있는 위치까지의 거리
        private Vector2 CamMoveSinceStart => startingPosition - (Vector2)m_camera.transform.position;

        //배경과 플레이어와의 z축 거리
        private float zDistanceFromTarget => transform.position.z - followTarget.position.z;
        //
        private float clippingPlane => m_camera.transform.position.z + (zDistanceFromTarget > 0 ? m_camera.farClipPlane : m_camera.nearClipPlane);
        //시차 거리 factor
        private float ParallaxFactor => Mathf.Abs(zDistanceFromTarget) / clippingPlane;
        #endregion

        private void Start()
        {
            //초기화
            startingPosition = transform.position;
            startingZ = transform.position.z;
        }

        private void Update()
        {
            Vector2 newposition = startingPosition + CamMoveSinceStart * ParallaxFactor;
            transform.position = new Vector3(newposition.x, newposition.y, startingZ);
        }
    }
}