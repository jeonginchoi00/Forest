using Globals;
using UnityEngine;

public class NPC_Hp : NPCBase
{
    public override void OnCollisionStay2D(Collision2D _collision)
    {
        base.OnCollisionStay2D(_collision);

        if (_collision.transform.CompareTag(Tag.PLAYER))
        {
            GameManager.GetInstance().SetInteractionType(InteractionType.NPC_HP);
        }
    }
}
