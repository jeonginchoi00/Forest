using UnityEngine;
using Globals;

public class NPCBase : MonoBehaviour
{
    public virtual void OnCollisionStay2D(Collision2D _collision)
    {
    }

    public virtual void OnCollisionExit2D(Collision2D _collision)
    {
        if (_collision.transform.CompareTag(Tag.PLAYER))
        {
            if (GameManager.GetInstance().Player.IsHand
                && !GameManager.GetInstance().Player.IsBow)
            {
                GameManager.GetInstance().SetInteractionType(InteractionType.ATTACK);
            }
            else if (!GameManager.GetInstance().Player.IsHand
                     && GameManager.GetInstance().Player.IsBow)
            {
                GameManager.GetInstance().SetInteractionType(InteractionType.ATTACK_BOW);
            }
        }
    }
}
