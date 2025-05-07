using UnityEngine;

/* [0] 개요 : ParallaxEffect
		- 시차에 의해 배경이 이동하는 거리 구하기.
*/

namespace My2D
{
    public class ParallaxEffect : MonoBehaviour
    {
        // [1] Variables.
        #region Variables
        // [ ] - 1) 플레이어 오브젝트.
        public Transform followTarget;
        // [ ] - 2) 카메라 오브젝트.
        public Camera cam;
        // [ ] - 3) 시차적용 배경의 처음 시작 위치(이동하기 전).
        private Vector2 startingPosition;
        // [ ] - 4) 시차적용 배경의 처음 Z 위치값(이동하기 전).
        private float startingZ;
        #endregion



        // [2] Property.
        #region Property
        // [ ] - 1) 카메라 시작지점으로부터 이동거리.
        Vector2 camMoveSinceStart => startingPosition - (Vector2)cam.transform.position;
        // [ ] - 2) 플레이어와 배경사이의 거리. 
        float zDistanceFromTarget => transform.position.z - followTarget.position.z;
        // [ ] - 3) 배경위치에 따라 플레이어와의 거리.
        float clippingPlane => cam.transform.position.z + ((zDistanceFromTarget > 0) ? cam.farClipPlane : cam.nearClipPlane);
        // [ ] - 4) 플레이어 이동에 따른 배경 이동 비율.
        float parallaxFactor => Mathf.Abs(zDistanceFromTarget) / clippingPlane;
        #endregion



        // [3] Unity Event Method
        #region Unity Event Method
        // [ ] - 1) Start.
        private void Start()
        {
            // [ ] - 1 - 1) 초기화(배경 시작 위치 저장).
            startingPosition = this.transform.position;
            startingZ = this.transform.position.z;
        }

        // [ ] - 2) Update.
        private void Update()
        {
            // [ ] - 2 - 1) 플레이어 이동에 따른 배경의 새로운 위치 구하기.
            Vector2 newPosition = startingPosition + camMoveSinceStart * parallaxFactor;
            // [ ] - 2 - 2) 배경의 위치 세팅.
            this.transform.position = new Vector3(newPosition.x, newPosition.y, startingZ);
        }
        #endregion
    }
}



// [ ] - ) 