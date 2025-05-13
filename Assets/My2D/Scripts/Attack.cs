using UnityEngine;

/* [0] 개요 : Attack
		- 공격시 충돌체에게 데미지를 줌.
*/

namespace My2D
{
	public class Attack : MonoBehaviour
	{
        // [1] Variables.
        #region Variables
        // [ ] - 1) 공격력.
        [SerializeField] private float attackDamage = 10f;
        // [ ] - 2) 넉백 효과 → 뒤로 이동.
        [SerializeField] private Vector2 knockback = Vector2.zero;
        #endregion



        // [2] Unity Envent Method.
        #region Unity Envent Method
        // [ ] - 1) OnTriggerEnter2D.
        private void OnTriggerEnter2D(Collider2D collision)
        {
            // )        Debug.Log($"플레이어에게 데미지{attackDamage}를 주었습니다.");
            // )        collision에서 Damageable 컴포넌트를 찾아서 TakeDamage를 주시오.
            Damageable damageable = collision.GetComponent<Damageable>();
            if (damageable != null)
            {
                // [ ] - [ ] - 1) 공격하는 캐릭터의 방향에 따라 밀리는 방향 설정.
                Vector2 deliverdKnockback = this.transform.parent.parent.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);

                bool isHit = damageable.TakeDamage(attackDamage, deliverdKnockback);
                if (isHit)
                {
                    Debug.Log(collision.name + "hit for" + attackDamage);
                }
            }
        }
        #endregion
    }
}



// [3] 
// [ ] - ) 
// [ ] - [ ] - )