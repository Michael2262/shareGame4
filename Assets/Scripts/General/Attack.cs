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
        //�Q��other�ӳX�ݳQ��������W��Component<Character>�̪���kTakeDamage�A���i��S���ҥH?�A
        other.GetComponent<Character>()?.TakeDamage(this);
    }
}
