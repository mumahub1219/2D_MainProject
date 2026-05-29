using UnityEngine;

public class MainUI_2DMainProject : UIBase
{
    [SerializeField] private UIButton Button_Inventory;
    [SerializeField] private UIButton Button_Dictionary;

    [Header("스킬 영역")]
    [SerializeField] private UIButton Button_NoramalAttack;
    [SerializeField] private UIButton Button_CircleSkill;
    [SerializeField] private UIButton Button_RaySkill;
    [SerializeField] private UIButton Button_ProjectileSkill;

    private void OnEnable()
    {
        Button_Inventory.BindOnClickButtonEvent(Onclick_OpenInventory);
        Button_Dictionary.BindOnClickButtonEvent(Onclick_OpenDictionary);

        Button_NoramalAttack.BindOnClickButtonEvent(Onclick_UseNormalAttack);
        Button_CircleSkill.BindOnClickButtonEvent(Onclick_UseCircleSkill);
        Button_RaySkill.BindOnClickButtonEvent(Onclick_UseRaySkill);
        Button_ProjectileSkill.BindOnClickButtonEvent(Onclick_ProjectileSkill);
    }

    public void Onclick_OpenInventory()
    {
        UIManager.Instance.OpenContentUI(UIType.InventoryUI);
    }

    public void Onclick_OpenDictionary()
    {
        UIManager.Instance.OpenContentUI(UIType.DictionaryUI);
    }

    public void Onclick_UseNormalAttack()
    {
        var localPlayer = GameObjectManager.Inst.GetLocalPlayer();
        localPlayer.UseNormalAttack();
    }

    public void Onclick_UseCircleSkill() 
    {
        var localPlayer = GameObjectManager.Inst.GetLocalPlayer();
        localPlayer.UseUseCircleSkill();
    }

    public void Onclick_UseRaySkill()
    {
        var localPlayer = GameObjectManager.Inst.GetLocalPlayer();
        localPlayer.UseRaySkill();
    }

    public void Onclick_ProjectileSkill()
    {
        var localPlayer = GameObjectManager.Inst.GetLocalPlayer();
        localPlayer.UseProjectileSkill();
    }
}
