using UnityEngine;
using UnityEngine.Events;

//�¥N�X�����ƥ�q�\�A�o�ӱM����S���^�ǭȪ�
//�겣���
[CreateAssetMenu(menuName = "Event/VoidEventSO")]
public class VoidEventSO : ScriptableObject 
{
    public UnityAction OnEventRaised;

    //�Q�ҥδN�ҥΡA���ζǥ���N�X�Ƕi��
    public void RaisseEvent() 
    {
        OnEventRaised?.Invoke();
    }
}
