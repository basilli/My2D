using UnityEngine;
using UnityEngine.InputSystem;

namespace My2D
{
    /* [0] 개요 : PlayerController
            - 플레이어를 제어하는 클래스.
    */


    public class PlayerController : MonoBehaviour
    {
        // [1] Variables.
        #region Variables
        // [ ] - 1) 강체(Rigid).
        private Rigidbody2D rb2D;
        // [ ] - 2) 애니메이터.
        public Animator animator;
        // [ ] - 3) 그라운드, 벽 체크.
        private TouchingDirection touchingDirection;
        // [ ] - 4) 이동.
        // [ ] - [ ] - 1) 걷는 속도 → 좌,우로 걸음.
        [SerializeField] private float walkSpeed = 4f;
        // [ ] - [ ] - 2) 뛰는 속도 → 좌,우로 뜀.
        [SerializeField] private float runSpeed = 7f;
        // [ ] - [ ] - 3) 점프시 좌우 이동속도.
        [SerializeField] private float airSpeed = 2f;
        // [ ] - [ ] - 4) 이동 입력값.
        private Vector2 inputMove;
        // [ ] - [ ] - 5) 이동 키입력.
        private bool isMoving = false;
        // [ ] - [ ] - 6) 런 키입력.
        private bool isRunning = false;
        // [ ] - 5) 반전.
        // [ ] - [ ] - 1) 캐릭터 이미지가 바라보는 방향 상태 : 오른쪽을 바라보면 true.
        private bool isFacingRight = true;
        // [ ] - 6) 점프키를 눌렀을 때 위로 올라가는 속도값. 
        [SerializeField] private float jumpForce = 5f;
        #endregion



        // [2] Property.
        #region Property
        // [ ] - 1) 이동 키 입력값 → 애니메이션 파라미터 세팅.
        public bool IsMoving
        {
            get
            { 
                return isMoving;
            }
            set
            {
                isMoving = value;
                animator.SetBool(AnimationString.isMoving, value);
            }
        }
        // [ ] - 2) 런 키 입력값 → 애니메이션 파라미터 세팅.
        public bool IsRunning
        {
            get
            {
                return isRunning;
            }
            set
            {
                isRunning = value;
                animator.SetBool(AnimationString.isRunning, value);
            }
        }
        // [ ] - 3) 현재 이동속도 셋팅 → 읽기전용.
        public float CurrentSpeed
        {
            get
            {
                // ) 공격시 이동 제어.
                if (CanNotMove)
                {
                    return 0f;
                }

                // ) 인풋값이 들어왔을 때 & 벽에 부딪히지않았을 때.
                if (IsMoving && touchingDirection.IsWall == false)
                {
                    if (touchingDirection.IsGround)     // ) 땅에 있을 때.
                    {
                        if (IsRunning)      // ) 시프트를 누르고 있을 때.
                        {
                            return runSpeed;
                        }
                        else
                        {
                            return walkSpeed;
                        }
                    }
                    else        // ) 공중에 떠 있을 때.
                    {
                        return airSpeed;
                    }
                }
                else
                {
                    return 0f;      // ) idle state, 벽에 부딪히고 있는 경우.
                }
            }
        }
        // [ ] - 4) 반전이동.
        public bool IsFacingRight
        {
            get
            {
                return isFacingRight;
            }
            set
            {
                // [ ] - 4 - 1) 반전 구현.
                if (isFacingRight != value)
                {
                    transform.localScale *= new Vector2(-1, 1);
                }
                isFacingRight = value;
            }
        }
        // [ ] - 5) 공격시 이동 제어값 읽어오기.
        public bool CanNotMove
        {
            get 
            {
                return animator.GetBool(AnimationString.cannotMove);
            }
        }


        #endregion



        // [3] Unity Event Method.
        #region Unity Event Method
        // [ ] - 1) Awake.
        private void Awake()
        {
            rb2D = this.GetComponent<Rigidbody2D>();
            touchingDirection = this.GetComponent<TouchingDirection>();
        }
        // [ ] - 2) FixedUpdate.
        private void FixedUpdate()
        {
            // [ ] - [ ] - 1) 인풋값에 따라 플레이어 좌우 이동. 
            rb2D.linearVelocity = new Vector2(inputMove.x*CurrentSpeed, rb2D.linearVelocityY);
            // [ ] - [ ] - 2) 애니메이터 속도값 세팅.
            animator.SetFloat(AnimationString.yVelocity, rb2D.linearVelocityY);
        }
        #endregion



        // [4] Custom Method.
        #region Custom Method
        // [ ] - 1) OnMove.
        public void OnMove(InputAction.CallbackContext context)
        {
            inputMove = context.ReadValue<Vector2>();
            // [ ] - [ ] - 1) 입력값에 따른 반전.
            SetFacingDirection(inputMove);
            // [ ] - [ ] - 2) 인풋값이 들어오면 IsMoving 파라미터 세팅.
            IsMoving = (inputMove != Vector2.zero);
        }
        // [ ] - 2) OnRun.
        public void OnRun(InputAction.CallbackContext context)
        {
            if (context.started)        // ) Button Down.
            {
                IsRunning = true;
            }
            else if (context.canceled)      // ) Button Up.
            {
                IsRunning = false;
            }
        }
        // [ ] - 3) 반전, 바라보는 방향 전환 → 입력값에 따라.
        void SetFacingDirection(Vector2 moveInput)
        {
            // [ ] - [ ] - 1) 좌로 이동, 우로 이동.
            if (moveInput.x > 0f && IsFacingRight == false)       // ) 왼쪽을 바라보고 있고, 우로 이동.
            {
                IsFacingRight = true;
            }
            else if(moveInput.x < 0f && IsFacingRight == true)       // ) 오른쪽을 바라보고 있고, 좌로 이동.
            {
                IsFacingRight = false;
            }
        }
        // [ ] - 4) OnJump.
        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.started && touchingDirection.IsGround)        // ) Button Down.
            {
                // [ ] - [ ] - 1) 속도 연산.
                rb2D.linearVelocity = new Vector2(rb2D.linearVelocityX, jumpForce);
                // [ ] - [ ] - 2) 애니메이션.
                animator.SetTrigger(AnimationString.jumpTrigger);
            }
        }
        // [ ] - 5) OnAttack.
        public void OnAttack(InputAction.CallbackContext context)
        {
            if (context.started && touchingDirection.IsGround)        // ) Mouse Left Button Down.
            {
                // [ ] - [ ] - 1) 애니메이션.
                animator.SetTrigger(AnimationString.attackTrigger);
            }
        }
            #endregion
        }
}

// [ ] - ) 
// [ ] - [ ] - )