using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace My2D
{
    //UI를 관리하는 클래스
    public class UIManager : MonoBehaviour
    {
        #region Variables
        public GameObject damageTextPrefab;
        public GameObject HealTextPrefab;

        private Canvas canvas;
        [SerializeField]private Vector3 healthTextoffset = Vector3.zero;
        #endregion
        private void OnEnable()
        {
            //캐릭터 관련 이벤트 함수 등록
            CharacterEvents.characterDamage += CharacterTakeDamage;
            CharacterEvents.characterHealed += CharacterHealed;
        }
        private void OnDisable()
        {
            //캐릭터 관련 이벤트 함수 제거
            CharacterEvents.characterDamage -= CharacterTakeDamage;
            CharacterEvents.characterHealed -= CharacterHealed;
        }
        private void Awake()
        {
            //참조
            canvas = FindAnyObjectByType<Canvas>();
        }
        public void CharacterTakeDamage(GameObject character, float damage)
        {
            Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);
            GameObject textGo = Instantiate(damageTextPrefab, spawnPosition + healthTextoffset, Quaternion.identity, canvas.transform);
            TextMeshProUGUI damageText = textGo.GetComponent<TextMeshProUGUI>();
            damageText.text = damage.ToString();
        }
        public void CharacterHealed(GameObject character, float restore)
        {
            Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);
            GameObject textGo = Instantiate(HealTextPrefab, spawnPosition + healthTextoffset, Quaternion.identity, canvas.transform);
            TextMeshProUGUI healText = textGo.GetComponent<TextMeshProUGUI>();
            healText.text = restore.ToString();
        }
    }
}