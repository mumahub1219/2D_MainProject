using UnityEngine;

public class ObjectBase_2D : MonoBehaviour
{
    
    public void OnTriggerPlayerRespawn(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") == false) return;

        var objectComponent = collision.gameObject.GetComponent<PlayerMove_2D>();
        if (objectComponent == null) return;

        GameManager.Inst.RespawnPlayer();
    }

}
