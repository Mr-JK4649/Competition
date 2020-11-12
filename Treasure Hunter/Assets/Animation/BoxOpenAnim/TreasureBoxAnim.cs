using UnityEngine;

public class TreasureBoxAnim : MonoBehaviour
{
    public Animator anim;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player") {
            anim.SetBool("Open",true);
            
        }
    }
}
