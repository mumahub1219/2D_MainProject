using UnityEngine;

public class CheckPoint : MonoBehaviour
{

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") == false) return;

        var objectComponent = collision.gameObject.GetComponent<PlayerMove_2D>();
        if (objectComponent == null) return;

        GameManager.Inst.SetRespawnPosition(objectComponent.transform.position);
    }

}
