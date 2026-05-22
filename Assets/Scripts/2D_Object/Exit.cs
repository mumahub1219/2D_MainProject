using UnityEngine;

public class Exit : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            UIManager.Instance.OpenContentUI(UIType.QuitUI);
        }
    }
}
