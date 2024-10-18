using TMPro;
using UnityEngine;

namespace My2D
{
    public class HealthText : MonoBehaviour
    {
        #region Variables
        private TextMeshProUGUI damageText;
        private RectTransform textTranform;

        //이동
        [SerializeField]private float moveSpeed = 5f;

        //페이드 효과
        private Color startcolor;
        public float fadeTimer = 1f;
        private float countdown = 0f;
        #endregion

        private void Awake()
        {
            //참조
            damageText = GetComponent<TextMeshProUGUI>();
            textTranform = GetComponent<RectTransform>();
        }
        private void Start()
        {
            //초기화
            startcolor = damageText.color;
            countdown = fadeTimer;
        }
        private void Update()
        {
            //이동
            textTranform.position += Vector3.up * moveSpeed * Time.deltaTime;

            //페이드 효과
            FadeEffect();
        }
        void FadeEffect()
        {
            //페이드 효과
            countdown -= Time.deltaTime;
            float newAlpha = startcolor.a * (countdown / fadeTimer);
            damageText.color = new Color(startcolor.r, startcolor.g, startcolor.b, newAlpha);
            if (countdown <= 0f)
            {
                Destroy(damageText.gameObject);
            }
        }
    }
}