using UnityEngine;
using UnityEngine.Events;

//純代碼式的事件訂閱，這個專門放沒有回傳值的
//資產文件
[CreateAssetMenu(menuName = "Event/VoidEventSO")]
public class VoidEventSO : ScriptableObject 
{
    public UnityAction OnEventRaised;

    //想啟用就啟用，不用傳任何代碼傳進來
    public void RaisseEvent() 
    {
        OnEventRaised?.Invoke();
    }
}
