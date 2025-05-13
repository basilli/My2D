using TMPro;
using UnityEngine;

/* [0] 개요 : HealthText
		- Damage가 위쪽으로 이동함.
		- Damage 페이드 아웃효과, 페이드 아웃 효과 후 킬 → text의 컬러값으로 페이드 효과.
		- 
		- 
		- 
*/

namespace My2D
{
    public class HealthText : MonoBehaviour
    {
        // [1] Variables.
        #region Variables
        // [ ] - 1) 이동.
        [SerializeField] private float moveSpeed = 10f;
        private RectTransform textTransform;


        // [ ] - ) 페이드 효과 타이머.
        public float fadeTime = 1f;
        private float fadeCountdown = 0f;
        private Color startColor;
        // [ ] - ) 참조.
        private TextMeshProUGUI healthText;
        #endregion



        // [2] Awake.
        private void Awake()
        {
            // [ ] - 1) 참조.
            textTransform = this.GetComponent<RectTransform>();
            healthText = this.GetComponent<TextMeshProUGUI>();
            // [ ] - 2) 초기화.
            startColor = healthText.color;
        }



        // [3] Unity Event Metod.
        #region Unity Event Metod
        // [ ] - 1) Update.
        private void Update()
        {
            // [ ] - [ ] - 1) 이동.
            transform.position += Vector3.up * Time.deltaTime * moveSpeed;
            // [ ] - [ ] - 2) FadeOut 타이머.
            fadeCountdown += Time.deltaTime;        // ) 1 → fadeTime : (1-0)
            float newAlpha = startColor.a * (1 - fadeCountdown / fadeTime);         // ) startColor.a = 1 → 0.
            healthText.color = new Color(startColor.r, startColor.g, startColor.b, newAlpha);

            if (fadeCountdown >= fadeTime)
            {
                Destroy(this.gameObject);
            }
        }
        #endregion
    }
}




// [ ] - ) 
// [ ] - [ ] - )
