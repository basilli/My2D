using NUnit.Framework;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/* [0] 개요 : DetectionZone
		- Detection Zone에 들어오는 콜라이더 감지하는 클래스.
		- 
		- 
		- 
		- 
*/

namespace My2D
{
    public class DetectionZone : MonoBehaviour
    {
        // [1] Variables.
        #region Variables
        // [ ] - 1) Detection Zone에 들어온 콜라이더를 저장하는 리스트 → 현재 Zone 안에 있는 콜라이더 목록.
        public List<Collider2D> detectedColliders = new List<Collider2D>();
        #endregion



        // [2] OnTriggerEnter2D & OnTriggerExit2D.
        private void OnTriggerEnter2D(Collider2D collision)
        {
            // [ ] - 1) 충돌체가 존에 들어오면 리스트에 추가.
            // )        Debug.Log($"{collision.name} 충돌체가 존 안에 들어옴.");
            detectedColliders.Add(collision);
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            // [ ] - 2) 충돌체가 존에서 나가면 리스트에서 제거.
            // )        Debug.Log($"{collision.name} 충돌체가 존 밖으로 나감.");
            detectedColliders.Remove(collision);
        }

    }
}



// [3] 
// [ ] - ) 
// [ ] - [ ] - )