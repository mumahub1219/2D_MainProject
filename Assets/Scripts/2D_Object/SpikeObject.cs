using UnityEngine;

public class SpikeObject : ObjectBase_2D
{

    public void OnTriggerEnter2D(Collider2D collision)
    {
        OnTriggerPlayerRespawn(collision);
    }
}
