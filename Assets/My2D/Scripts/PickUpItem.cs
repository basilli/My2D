using UnityEngine;

/* [0] 개요 : PickUpItem
		- 떨어진 아이템을 픽업하면 아이템 효과를 나타낸다.
		- 아이템 효과 : Health 충전.
		- 필드에서 아이템이 회전함.
		- 
		- 
*/

namespace My2D
{
    public class PickUpItem : MonoBehaviour
    {
        // [1] Variable.
        #region Variable
        // [ ] - 1) 회전 속도 → Y축 기준으로 회전.
        private Vector3 spinRotateSpeed = new Vector3(0f, 180f, 0f);
        // [ ] - 2) Health 충전.
        [SerializeField] private float restoreHealth = 30f;
        // [ ] - 3) 사운드 효과.
        private AudioSource pickupSource;
        #endregion



        // [2] Unity Event Method.
        #region Unity Event Method
        // [ ] - 1) Awake.
        private void Awake()
        {
            // [ ] - [ ] - 1) 참조.
            pickupSource = this.GetComponent<AudioSource>();
        }
        // [ ] - 2) Update.
        private void Update()
        {
            // [ ] - [ ] - 1) 회전.
            transform.eulerAngles += spinRotateSpeed * Time.deltaTime;
        }
        // [ ] - 3) OnTriggerEnter2D.
        private void OnTriggerEnter2D(Collider2D collision)
        {
            // )        Debug.Log($"{collision.name} HP를 {restoreHealth}만큼 회복합니다");
            // [ ] - [ ] - 1) 플레이어가 충돌하면 Damageable 컴포넌트를 찾음 → Damageable에 있는 Heal 함수를 호출함.
            Damageable damageable = collision.GetComponent<Damageable>();
            if (damageable)
            {
                bool isHeal = damageable.Heal(restoreHealth);
                // [ ] - [ ] - [ ] - 1) 아이템 킬 판단.
                if (isHeal)
                {
                    // [ ] - [ ] - [ ] - [ ] - 1) 사운드 효과.
                    if (pickupSource)
                    {
                        pickupSource.PlayOneShot(pickupSource.clip);
                    }
                    Destroy(gameObject);
                }
            }
        }
        #endregion
    }
}

// [3] 
// [ ] - ) 
// [ ] - [ ] - )