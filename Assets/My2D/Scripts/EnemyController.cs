using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.EventSystems.StandaloneInputModule;

/* [0] 개요 : EnemyController
		- 적 캐릭터를 관리하는 클래스.
*/

namespace My2D
{
    // [0] 이동방향.
    public enum WalkableDirection
    { 
        Left,
        Right
    }

    // [0] 적 캐릭터를 관리하는 클래스.
    [RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirection))]
    public class EnemyController : MonoBehaviour
    {
        // [1] Variables.
        #region Variables
        // [ ] - 1) 강체(Rigid) → 참조.
        private Rigidbody2D rb2D;
        private TouchingDirection touchingDirection;
        // [ ] - 2) 애니메이터.
        public Animator animator;
        // [ ] - 3) 플레이어 감지.
        public DetectionZone detectionZone;
        // [ ] - 4) .
        public Damageable damageable;
        // [ ] - 5) 낭떠러지 탐색.
        public DetectionZone cliffDetection;
        // [ ] - 6) 이동.
        // [ ] - [ ] - 1) 속도.
        [SerializeField] private float walkSpeed = 4f;
        // [ ] - [ ] - 2) 방향 Vector.
        private Vector2 directionVector = Vector2.right;
        // [ ] - [ ] - 3) 현재 이동 방향을 저장.
        private WalkableDirection walkDirection = WalkableDirection.Right;
        // [ ] - 7) 정지.
        private float stopRate = 0.2f;
        // [ ] - 8) 적 감지.
        private bool hasTarget;
        #endregion



        // [2] Property.
        #region Property
        // [ ] - 1) 
        public WalkableDirection WalkDirection
        {
            get
            {
                return walkDirection;
            }
            set
            {
                // 방향전환이 일어나는 시점.
                if (walkDirection != value)
                {
                    // 반대방향을 바라보도록 함 → 이미지 플립.
                    this.transform.localScale *= new Vector2(-1, 1);
                    // 반대쪽으로 위치 이동.
                    if (value == WalkableDirection.Left)
                    {
                        directionVector = Vector2.left;
                    }
                    else if (value == WalkableDirection.Right)
                    {
                        directionVector = Vector2.right;
                    }
                }
                walkDirection = value;
            }
        }
        // [ ] - 2) 이동 제한 → 애니메이터 파라미터값 읽어오기.
        public bool CannotMove
        {
            get
            {
                return animator.GetBool(AnimationString.cannotMove);
            }
        }
        // [ ] - 3) 적 감지.
        public bool HasTarget
        {
            get
            {
                return hasTarget;
            }
            set
            {
                hasTarget = value;
                animator.SetBool(AnimationString.hasTarget, value);
            }
        }
        // [ ] - 4) 공격 쿨타임 → 읽어서 0보다 크면 3초 타이머를 돌려서 0으로 다시 파라미터 값을 셋팅.
        public float CooldownTime
        {
            get
            {
                return animator.GetFloat(AnimationString.cooldownTime);
            }
            set
            {
                animator.SetFloat(AnimationString.cooldownTime, value);
            }
        }
        #endregion



        // [3] Unity Event Method.
        #region Unity Event Method
        // [ ] - 1) Awake.
        private void Awake()
        {

            // [ ] - [ ] - 1) 참조.
            rb2D = this.GetComponent<Rigidbody2D>();
            touchingDirection = this.GetComponent<TouchingDirection>();
            damageable = this.GetComponent<Damageable>();
            // [ ] - [ ] - 2) 델리게이트 함수 등록.
            damageable.hitAction += OnHit;
            // [ ] - [ ] - 3) CliffDetection 이벤트 함수 등록.
            cliffDetection.noColliderRamain += Flip;
        }
        // [ ] - 2) Update.
        private void Update()
        {
            // [ ] - [ ] - 1) 적 감지.
            HasTarget = (detectionZone.detectedColliders.Count > 0);
            // [ ] - [ ] - 2) CooldownTimer.
            if (CooldownTime > 0)
            {
                CooldownTime -= Time.deltaTime;
            }
        }
        // [ ] - 3) FixedUpdate.
        private void FixedUpdate()
        {
            // [ ] - [ ] - 1) 벽을 만났을 때 방향을 전환하여 이동함. 
            if (touchingDirection.IsWall && touchingDirection.IsGround)
            {
                Flip();
            }
            // [ ] - [ ] - 2) 좌우 이동. 
            if (damageable.LockVelocity == false)
            {
                if (CannotMove)
                {
                    rb2D.linearVelocity = new Vector2(Mathf.Lerp(rb2D.linearVelocityX, 0f, stopRate), rb2D.linearVelocityY);
                }
                else
                {
                    rb2D.linearVelocity = new Vector2(directionVector.x * walkSpeed, rb2D.linearVelocityY);
                }
            }
        }
        #endregion



        // [4] Custom Metod.
        #region Custom Metod
        // [ ] - 1) 방향전환.
        void Flip()
        {
            Debug.Log("방향전환");
            if (WalkDirection == WalkableDirection.Left)
            {
                WalkDirection = WalkableDirection.Right;
            }
            else if (WalkDirection == WalkableDirection.Right)
            {
                WalkDirection = WalkableDirection.Left;
            }
            else
            {
                Debug.Log("방향전환 에러");
            }
        }
        // [ ] - 2) 데미지 입을 때 호출되는 함수 → 데미지를 입을 때의 속도 셋팅..
        public void OnHit(float damage, Vector2 knockback)
        {
            rb2D.linearVelocity = new Vector2(knockback.x, rb2D.linearVelocityY + knockback.y);
        }
        #endregion
    }
}

// [1] 
// [2] 
// [3] 
// [ ] - ) 
// [ ] - [ ] - )