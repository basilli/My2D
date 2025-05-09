using UnityEngine;

/* [0] 개요 : SetBoolBehaviour
		- Animator의 Bool형을 말함 → Bool형 타입 파라미터 변수를 관리하는 클래스.
		- 상태(상태머신)에 들어갈 때와 나올 때 값을 설정해줌.
		- 
		- 
		- 
*/

namespace My2D
{

    public class SetBoolBehaviour : StateMachineBehaviour
    {
        // [1] Variables.
        #region Variables
        // [ ] - 1) 값을 설정할 파라미터 이름.
        public string boolName;
        // [ ] - 2) 작동하는 상태, 상태머신 체크.
        public bool updateOnstate;
        public bool updateOnstateMachine;
        // [ ] - 3) 들어갈 때와 나올 때의 값 설정.
        public bool valueEnter;
        public bool valueExit;
        #endregion



        // [2] OnStateEnter.
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (updateOnstate)
            {
                animator.SetBool(boolName, valueEnter);
            }
        }

        // OnStateUpdate is called before OnStateUpdate is called on any state inside this state machine
        //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    
        //}



        // [3] OnStateExit.
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (updateOnstate)
            {
                animator.SetBool(boolName, valueExit);
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



        // [4] OnStateMachineEnter.
        override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
        {
            if (updateOnstateMachine)
            {
                animator.SetBool(boolName, valueEnter);
            }
        }



        // [5] OnStateMachineExit.
        override public void OnStateMachineExit(Animator animator, int stateMachinePathHash)
        {
            if (updateOnstateMachine)
            {
                animator.SetBool(boolName, valueExit);
            }
        }
    }
}




// [ ] - ) 
// [ ] - [ ] - )