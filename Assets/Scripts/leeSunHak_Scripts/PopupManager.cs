using UnityEngine;

public class PopupManager : MonoBehaviour
{
    [SerializeField] private GameObject popupObject;

    public void PopupOpen()
    {
        if (popupObject != null)
        {
            popupObject.SetActive(true);
        }
    }

   public void PopupClose()
    {
        if (popupObject != null)
        {
            popupObject.SetActive(false);
        }
    }
}
