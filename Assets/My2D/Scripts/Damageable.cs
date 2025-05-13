using UnityEngine;
using UnityEngine.Events;

/* [0] 개요 : Damageable
		- Health를 관리하는 클래스.
		- Takedamage, Die를 구현.
*/

namespace My2D
{
	public class Damageable : MonoBehaviour
	{
        // [1] Variables.
        #region Variables
        // [ ] - 1) 참조.
        public Animator animator;


        // [ ] - 2) 체력.
        private float currentHealth;
        // [ ] - 3) 초기 체력(최대 체력).
        [SerializeField] private float maxHealth = 100;
        // [ ] - 4) 죽음 체크.
        private bool isDeath = false;
        // [ ] - 5) 무적 타이머.
        private bool isInvincible = false;      // ) ture일 경우 데미지를 입지 않음.
        [SerializeField] private float invincibleTime = 3f;     // ) 무적 타이머.
        private float countdown = 0f;
        // [ ] - 6) 델리게이트 이벤트 함수 → 매개변수로 float, Vector2가 있는 함수 등록 가능.
        public UnityAction<float, Vector2> hitAction;
        // [ ] - 7) UI효과 → 데미지 텍스트 프리팹을 생성하는 함수가 등록된 이벤트 함수 호출.
        // 
        #endregion



        // [2] Property
        #region Property
        // [ ] - 1) 체력.
        public float CurrentHealth
        {
            get
            {
                return currentHealth;
            }
            private set
            {
                currentHealth = value;
                if (currentHealth <= 0)
                {
                    Die();
                }
            }
        }
        // [ ] - 2) 최대체력.
        public float MaxHealth
        {
            get
            {
                return maxHealth;
            }
            private set
            {
                maxHealth = value;
            }
        }
        // [ ] - 3) 죽음 체크.
        public bool IsDeath
        {
            get
            {
                return isDeath;
            }
            private set
            {
                isDeath = value;
            }
        }
        // [ ] - 4) 이동 속도 잠그기.
        public bool LockVelocity
        {
            get
            {
                return animator.GetBool(AnimationString.lockvelocity);
            }
            set
            {
                animator.SetBool(AnimationString.lockvelocity, value);
            }
        }
        // [ ] - 5) .
        public bool IsHealthFull => CurrentHealth >= maxHealth;
        #endregion



        // [3] Unity Event Method.
        #region Unity Event Method
        // [ ] - 1) Start.
        private void Start()
        {
            // [ ] - [ ] - 1) 초기화.
            CurrentHealth = MaxHealth;
        }
        // [ ] - 2) Update.
        private void Update()
        {
            if (isInvincible)
            {
                countdown += Time.deltaTime;
                if (countdown >= invincibleTime)
                {
                    // [ ] - [ ] - 1) 타이머 기능.
                    isInvincible = false;
                    // [ ] - [ ] - 2) 타이머 초기화.
                }
            }
        }
        #endregion



        // [4] Custom Method.
        #region Custom Method
        // [ ] - 1) TakeDamage → 매개변수로 데미지와 뒤로 밀리는 값을 받아옴.
        public bool TakeDamage(float damage, Vector2 knockback)
        {
            if (IsDeath || isInvincible)
            {
                return false;
            }
            CurrentHealth -= damage;
            Debug.Log($"CurrentHealth : {CurrentHealth}");
            // [ ] - [ ] - 1) 무적모드 셋팅 → 타이머 초기화.
            isInvincible = true;
            countdown = 0;
            // [ ] - [ ] - 2) 애니메이션.
            animator.SetTrigger(AnimationString.hitTrigger);
            LockVelocity = true;
            // [ ] - [ ] - 3) 델리게이트 함수에 등록된 함수들을 호출 → 효과 연출에 필요한 함수 등록 → 넉백효과(효과 : SFX, VFX, 넉백효과...).
            /*
            if (hitAction != null)
            {
                hitAction.Invoke(damage, knockback);
            }
            */
            hitAction?.Invoke(damage, knockback);
            // [ ] - [ ] - 4) UI 효과 → 데미지Text 프리팹 생성하는 이벤트 함수 호출.
            CharacterEvents.characterDamaged?.Invoke(gameObject, damage);

            return true;
        }
        // [ ] - 2) Die
        private void Die()
        {
            IsDeath = true;
            animator.SetBool(AnimationString.isDeath, true);
            Debug.Log("Death!");
        }
        // [ ] - 3) Health 가산 → 매개변수만큼 Health 충전 → 참을 반환할 경우, Health를 실질적으로 충전, 거짓을 반환할 경우, 충전하지 않음.
        public bool Heal(float healAmount)
        {
            // [ ] - [ ] - 1) 죽음 체크.
            if (IsDeath || IsHealthFull)
            {
                return false;
            }
            CurrentHealth += healAmount;
            if (CurrentHealth > MaxHealth)
            {
                CurrentHealth = MaxHealth;
            }
            Debug.Log($"Current Health : {CurrentHealth}");
            // [ ] - [ ] - 2) UI효과 → 힐 텍스트 프리팹 생성하는 이벤트 함수 호출. 





            return true;
            #endregion
        }
    }
}




// [ ] - ) 
// [ ] - [ ] - )