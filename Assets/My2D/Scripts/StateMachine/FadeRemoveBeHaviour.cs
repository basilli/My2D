using UnityEditor.SceneManagement;
using UnityEngine;

/* [0] 개요 : FadeRemoveBeHaviour
		- FadeOut 효과 시작 전 대기 시간 설정(지연시간).
		- FadeOut 효과로 서서히 사라진 후 오브젝트 킬.
*/

namespace My2D
{

    public class FadeRemoveBeHaviour : StateMachineBehaviour
    {
        // [1] Variables.
        #region Variables
        // [ ] - 1) 참조.
        private SpriteRenderer spriteRenderer;
        private GameObject removeObject;        // ) 효과 후 킬 할 오브젝트.
        // [ ] - 2) 지연시간 타이머.
        public float delayTime = 1f;
        private float delayCountdown = 0f;
        // [ ] - 3) 페이드 효과 타이머.
        public float fadeTime = 1f;
        private float fadeCountdown = 0f;
        private Color startColor;
        #endregion

        // [2] OnStateEnter.
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            // [ ] - 1) 참조.
            spriteRenderer = animator.GetComponent<SpriteRenderer>();
            removeObject = animator.transform.parent.gameObject;
            // [ ] - 2) 초기화.
            startColor = spriteRenderer.color;
        }


        // [3] OnStateUpdate.
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            // [ ] - 1) 지연시간 타이머.
            if (delayCountdown < delayTime)
            {
                delayCountdown += Time.deltaTime;
            }
            else        // ) 지연시간이 지남.
            {
                // [ ] - 2) FadeOut 타이머.
                fadeCountdown += Time.deltaTime;        // ) 1 → fadeTime : (1-0)
                
                float newAlpha = startColor.a * (1 - fadeCountdown / fadeTime);         // ) startColor.a = 1 → 0.
                spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, newAlpha);

                if (fadeCountdown >= fadeTime)
                {
                    Destroy(removeObject);
                }
            }


        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    
        //}

        // OnStateMove is called right after Animator.OnAnimatorMove()
        //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    // Implement code that processes and affects root motion
        //}

        // OnStateIK is called right after Animator.OnAnimatorIK()
        //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    // Implement code that sets up animation IK (inverse kinematics)
        //}
    }
}




// [ ] - ) 
// [ ] - [ ] - )
