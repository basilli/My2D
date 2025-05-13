using UnityEngine;

/* [0] 개요 : SetFloatBehaviour
		- Float형 타입 파라미터 변수를 관리하는 클래스.
		- 상태(상태머신)에 들어갈 때와 나올 때 값을 설정해줌.
*/

namespace My2D
{
    public class SetFloatBehaviour : StateMachineBehaviour
    {
        // [1] Variables.
        #region Variables
        // [ ] - 1) 값을 설정할 파라미터 이름.
        public string floatName;
        // [ ] - 2) 작동하는 상태의 들어갈 때와 나올 때 체크.
        public bool updateOnStateEnter;
        public bool updateOnStateExit;
        // [ ] - 3) 작동하는 상태머신의 들어갈 때와 나올 때 체크.
        public bool updateOnStateMachineEnter;
        public bool updateOnStateMachineExit;
        // [ ] - 4) 들어갈 때와 나올 때의 값 설정.
        public float valueEnter;
        public float valueExit;
        #endregion



        // [2] 
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (updateOnStateEnter)
            {
                animator.SetFloat(floatName, valueEnter);
            }
        }

        // OnStateUpdate is called before OnStateUpdate is called on any state inside this state machine
        //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    
        //}



        // [3] 
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (updateOnStateExit)
            {
                animator.SetFloat(floatName, valueExit);
            }
        }
        // OnStateMove is called before OnStateMove is called on any state inside this state machine
        //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    
        //}

        // OnStateIK is called before OnStateIK is called on any state inside this state machine
        //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    
        //}



        // [4] 
        override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
        {
            if (updateOnStateMachineEnter)
            {
                animator.SetFloat(floatName, valueEnter);
            }
        }



        // [5] 
        override public void OnStateMachineExit(Animator animator, int stateMachinePathHash)
        {
            if (updateOnStateMachineExit)
            {
                animator.SetFloat(floatName, valueExit);
            }
        }
    }
}

// [1] 

// [3] 
// [ ] - ) 
// [ ] - [ ] - )