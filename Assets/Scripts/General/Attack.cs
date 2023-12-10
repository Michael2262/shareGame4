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
        //?代表如果有這個代碼才會執行
        other.GetComponent<Character>()?.TakeDamage(this);
    }
}
