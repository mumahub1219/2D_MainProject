using UnityEngine;

public class StageLevelButton : MonoBehaviour
{
    [SerializeField] private DaniTechUIButton _stageLevelButton;

    private void OnEnable()
    {
        _stageLevelButton.BindOnClickButtonEvent(Onclick_StageLevel);
    }

    public void Onclick_StageLevel()
    {
        
    }
}
