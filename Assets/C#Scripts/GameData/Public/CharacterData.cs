using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
[CreateAssetMenu(fileName ="New CharacterData",menuName ="Data/CharacterData")]
public class CharacterData : ScriptableObject
{
    public float MaxHealth;//������ֵ
    public float NowHealth;//��ǰ����ֵ
    public float Speed;//����
    public float AttackPower;//������
    public float InvincibleTime;//�޵�ʱ��
    public float HealCount;//������
    public float AutoHealTime;//�Զ��ظ�ʱ��
    public float AutoHealCount;//�Զ��ظ���
    public float CriticalDamageRate;//������
    public float CriticalDamageBonus;//�����ӳ�
    public float DodgeRate;//������
    public float AttackRate;//����Ƶ��
}
