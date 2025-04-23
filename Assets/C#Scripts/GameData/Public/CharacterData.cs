using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
[CreateAssetMenu(fileName ="New CharacterData",menuName ="Data/CharacterData")]
public class CharacterData : ScriptableObject
{
    public float MaxHealth;//������ֵ
    public float NowHealth;//��ǰ����ֵ
    public float HealthRate;//����ֵ�ӳ�
    public float Speed;//����
    public float SpeedRate;//���ټӳ�
    public float AttackPower;//������
    public float WeaponAttackPower;//ǹе������
    public int WeaponCount;//ǹе����
    public float AttackBonus;//�����ӳ�
    public float InvincibleTime;//�޵�ʱ��
    public float HealCount;//������
    public float AutoHealCount;//�Զ��ظ���
    public float CriticalDamageRate;//������
    public float CriticalDamageBonus;//�����ӳ�
    public float DodgeRate;//������
    public float AttackRate;//����Ƶ��
    public bool CanDash;//�Ƿ���Գ��
    public bool DashInvincibleFrame;//����޵�֡
    public int DashCount;//��̴���
    public int JumpCount;//��Ծ����
}
