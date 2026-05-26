using UnityEngine;
using UnityEngine.UI;

public class HudSlotUI : MonoBehaviour
{
    [SerializeField] private int slotOffsetY;
    [SerializeField] private Slider Slider_Hp;
    [SerializeField] private Slider Slider_Mp;

    private int _instanceId;
    private Transform _targetTransform;

    public void InitSlot(int instanceId, Transform targetTransform)
    {
        _instanceId = instanceId;
        _targetTransform = targetTransform;
        slotOffsetY = 100;

        TryBingStatChangedEvent(targetTransform.gameObject);
    }

    private void TryBingStatChangedEvent(GameObject gObj)
    {
        var player = gObj.GetComponent<PlayerMove_2D>();
        if (player != null)
        {
            player.BindeOnStatChangedEvent(OnTargetEntitiyHpChanged, OnTargetEntitiyMpChanged);
            return;
        }

        var monster = gObj.GetComponent<MonsterBasic>();
        if (monster != null)
        {
            monster.BindeOnStatChangedEvent(OnTargetEntitiyHpChanged, OnTargetEntitiyMpChanged);
            return;
        }
    }

    private void OnTargetEntitiyHpChanged(int curHp, int maxHp)
    {
        Slider_Hp.value = (curHp / (float)maxHp);
    }

    private void OnTargetEntitiyMpChanged(int curMp, int maxMp)
    {
        Slider_Mp.value = (curMp / (float)maxMp);
    }

    private void Update() 
    {
        if(_targetTransform != null)
        {
            // this.gameObject.transform.position = _targetTransform.position;

            Vector2 screenPos = Camera.main.WorldToScreenPoint(_targetTransform.position);

            var rectTransform = this.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                Vector2 finalScreenPos = new Vector2(screenPos.x, screenPos.y + slotOffsetY);
                rectTransform.anchoredPosition = finalScreenPos;
            }
        }
    }
}
