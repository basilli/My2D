using Unity.VisualScripting;
using UnityEngine;

/* [0] 개요 : TouchingDirection
		- Collider Cast를 이용하여 바닥, 천장, 벽 체크.
*/

namespace My2D
{
    public class TouchingDirection : MonoBehaviour
    {
        // [1] Variables.
        #region Variables
        public Animator animator;
        private CapsuleCollider2D touchingCollider;
        // [ ] - 1) 캐스팅 범위.
        [SerializeField] private float groundDistance = 0.05f;      // ) 그라운드 체크 범위.
        [SerializeField] private float cellingDistance = 0.05f;      // ) 천장 체크 범위.
        [SerializeField] private float wallDistance = 0.2f;      // ) 벽 체크 범위.
        // [ ] - 2) 캐스팅 필터 조건.
        [SerializeField] private ContactFilter2D contactFilter;
        // [ ] - 3) 캐스팅 된 RaycastHit2D 리스트(배열).
        private RaycastHit2D[] groundHits = new RaycastHit2D[5];
        private RaycastHit2D[] cellingHits = new RaycastHit2D[5];
        private RaycastHit2D[] wallHits = new RaycastHit2D[5];
        // [ ] - 4) 그라운드 체크.
        [SerializeField] private bool isGround;     // ) 그라운드.
        [SerializeField] private bool isCelling;        // ) 천장.
        [SerializeField] private bool isWall;        // ) 벽.
        #endregion


        // [2] Property.
        #region Property
        // [ ] - 1) IsGround.
        public bool IsGround
        {
            get 
            {
                return isGround;
            }
            private set
            {
                isGround = value;
                animator.SetBool(AnimationString.isGround, value);
            }
        }
        // [ ] - 2) IsCelling.
        public bool IsCelling
        {
            get
            {
                return isCelling;
            }
            private set
            {
                isCelling = value;
                animator.SetBool(AnimationString.isCelling, value);
            }
        }
        // [ ] - 3) IsWall.
        public bool IsWall
        {
            get
            {
                return isWall;
            }
            private set
            {
                isWall = value;
                animator.SetBool(AnimationString.isWall, value);
            }
        }
        // [ ] - 4) WallCheckDirection.
        private Vector2 WallCheckDirection => (this.transform.localScale.x > 0) ? Vector2.right : Vector2.left;
        #endregion



        // [3] Unity Event Method.
        #region Unity Event Method
        private void Awake()
        {
            // [ ] - 1) 참조.
            touchingCollider = this.GetComponent<CapsuleCollider2D>();
        }
        // [ ] - 2) FixedUpdate.
        private void FixedUpdate()
        {
            // [ ] - 2 - 1) 바닥 체크.
            IsGround = touchingCollider.Cast(Vector2.down, contactFilter, groundHits, groundDistance) > 0;
            // [ ] - 2 - 2) 천장 체크.
            IsCelling = touchingCollider.Cast(Vector2.up, contactFilter, cellingHits, cellingDistance) > 0;
            // [ ] - 2 - 3) 벽 체크.
            IsWall = touchingCollider.Cast(WallCheckDirection, contactFilter, wallHits, wallDistance) > 0;
        }
        #endregion
    }
}




// [ ] - ) 