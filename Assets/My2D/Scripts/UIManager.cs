using TMPro;
using UnityEngine;


/* [0] 개요 : UIManager
		- UI를 관리하는 클래스.
		- 
		- 
		- 
		- 
*/


namespace My2D
{
	public class UIManager : MonoBehaviour
	{
		// [1] Variable.
		#region Variable
		// [ ] - 1) 데미지 텍스트 프리팹.
		public GameObject damageTextPrefab;
        // [ ] - 2) 캔버스.
        public Canvas gameCanvas;
        [SerializeField] private Vector3 offset;        // ) 캐릭터 머리 위로 보정값.
        // [ ] - 3) 카메라.
        public Camera camera;
        #endregion



        // [2] Unity Event Metod.
        #region Unity Event Metod
        // [ ] - 1) Awake.
        private void Awake()
        {
            // [ ] - [ ] - 1) .
            camera = Camera.main;
        }




        // [ ] - 2) OnEnable.
        private void OnEnable()
        {
            // [ ] - [ ] - 1) 이벤트 함수에 함수를 등록.
            CharacterEvents.characterDamaged += CharacterTakeDamage;
        }
        // [ ] - 3) OnDisable.
        private void OnDisable()
        {
            // [ ] - [ ] - 1) 이벤트 함수에 등록된 함수를 제거.
            CharacterEvents.characterDamaged -= CharacterTakeDamage;
        }
        #endregion



        // [3] Custom Method.
        #region Custom Method
        // [ ] - 1) CharacterTakeDamage
        public void CharacterTakeDamage(GameObject character, float damage)
		{
            // [ ] - [ ] - 1) 프리팹 생성(생성된 프리팹의 부모를 Canvas로 지정, Character은 데미지를 입는 캐릭터) → 텍스트에 매개변수로 들어온 데미지량 셋팅.

            Vector3 spawnPosition = camera.WorldToScreenPoint(character.transform.position);

            GameObject textGo = Instantiate(damageTextPrefab, spawnPosition + offset, Quaternion.identity, gameCanvas.transform);
            // [ ] - [ ] - 2) 텍스트 객체.
            TextMeshProUGUI damageText = textGo.GetComponent<TextMeshProUGUI>();
            if (damageText)
            {
                damageText.text = damage.ToString();
            }
        }
        // [ ] - 2) CharacterHeal
        public void CharacterHeal(GameObject character, float healAmount)
        {
            // [ ] - [ ] - 1) 힐 텍스트 프리팹 생성(Character은 체력을 회복한 캐릭터)
        }
        #endregion
    }
}



// [3] 
// [ ] - ) 
// [ ] - [ ] - )