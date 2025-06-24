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
    public bool StartGame;//��Ϸ��ʼ
    public Vector3 PlayerPosition;//���λ��
    public RoomType RoomType;//��ҵ�ǰ����
    public SceneData CurrentScene;//��ǰ����
    public HatList.HatData HatData;//ñ������
    public CharacterList.CharacterData CharacterData;//��������
    public WeaponList.WeaponData WeaponData;//��������
    public ColorData.Colordata CurrentColor;//��ɫ����
    [Header("С�ӳ�Ա")]
    public SlimeData Player;
    public SlimeData Teamer1;
    public SlimeData Teamer2;
    public SlimeData Teamer3;
    [Header("Я������������")]
    public int PlayerWeaponSlotCount;
    public int Teamer1WeaponSlotCount;
    public int Teamer2WeaponSlotCount;
    public int Teamer3WeaponSlotCount;
    public ExtraGemData PlayerExtraGemData;
    public ExtraGemData Teamer1ExtraGemData;
    public ExtraGemData Teamer2ExtraGemData;
    public ExtraGemData Teamer3ExtraGemData;
    public int FreeWeaponSlotCount;
    public int EmptyWeaponSlotCount;
    public ExtraGemData ExtraGemData;
}
