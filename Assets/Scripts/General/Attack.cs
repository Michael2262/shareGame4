using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    // Start is called before the first frame update
    public int damage;
    public float attackRange;
    public float attackRate;
    
    private void OnTriggerStay2D(Collider2D other)
    {
        //利用other來訪問被攻擊物體上的Component<Character>裡的方法TakeDamage，有可能沒有所以?，
        other.GetComponent<Character>()?.TakeDamage(this);
    }
}
