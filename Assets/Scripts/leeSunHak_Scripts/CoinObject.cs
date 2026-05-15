using UnityEngine;
using UnityEngine.UI;

public class CoinObject : MonoBehaviour
{
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            DaniTechGameManager.Inst.IncreaseCoinScore();
        }
        
        Destroy(gameObject);
    }
}
