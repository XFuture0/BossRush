using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New PlayerData", menuName = "Data/PlayerData")]
public class PlayerData : ScriptableObject
{
    public int RoomCount;//�ܷ�����
    public int CurrentRoomCount;//��ǰ������
    public float JumpForce;
    public float JumpDownSpeed_Max;
    public int CoinCount;//�����
    public int ScoreCount;//�÷���
    public Vector3 PlayerPosition;//���λ��
    public SceneData CurrentScene;//��ǰ����
    public HatList.HatData HatData;//ñ������
    public CharacterList.CharacterData CharacterData;//��������
    public WeaponList.WeaponData WeaponData;//��������
}
