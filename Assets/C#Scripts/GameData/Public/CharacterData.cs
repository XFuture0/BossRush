using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
[CreateAssetMenu(fileName ="New CharacterData",menuName ="Data/CharacterData")]
public class CharacterData : ScriptableObject
{
    public float MaxHealth;//总生命值
    public float NowHealth;//当前生命值
    public float HealthRate;//生命值加成
    public float Speed;//移速
    public float SpeedRate;//移速加成
    public float AttackPower;//攻击力
    public float WeaponAttackPower;//枪械攻击力
    public int WeaponCount;//枪械数量
    public float AttackBonus;//攻击加成
    public float InvincibleTime;//无敌时间
    public float HealCount;//治疗量
    public float AutoHealCount;//自动回复量
    public float CriticalDamageRate;//暴击率
    public float CriticalDamageBonus;//暴击加成
    public float DodgeRate;//闪避率
    public float AttackRate;//攻击频率
    public bool CanDash;//是否可以冲刺
    public bool DashInvincibleFrame;//冲刺无敌帧
    public int DashCount;//冲刺次数
    public int JumpCount;//跳跃次数
    public float AngerValue;//怒气值
    public float FullAnger;//怒气满值
    public float AngerTime;//怒气时间
    public float PoizonDamage;//剧毒伤害
    public float PoizonTime;//剧毒时间
    public float ThunderBonus;//雷击加成
    public float ThunderRate;//雷击率
    [Header("特殊技能效果")]
    public bool MucusDeathRage;//粘液亡怒
    public bool FearlessFury;//无畏狂怒
    public bool MucousRage;//粘液盛怒
    public bool LrritableSlime;//易怒症史莱姆
    public bool ThreeMinuteHeat;//三分钟热度
    public bool FuriousGatling;//狂怒加特林
    public bool ShengqiCore;//盛气核心
    public bool SprintBuffer;//冲刺缓冲
    public bool SlimeRunningChampion;//粘液长跑冠军
    public bool NetworkLag;//网络卡顿
    public bool AdhesiveExoskeleton;//粘性外骨骼
    public bool SpeedEmblem;//速度纹章
    public bool QuickAngerGel;//速怒凝胶
    public bool ElasticGel;//弹性凝胶
    public bool UrgentEngine;//急行引擎
    public bool GuaranteedFirstPrize;//保底头奖
    public bool BouncyJelly;//弹弹果冻
    public bool ImperialWeapons;//御风系武器
    public bool HormoneGel;//激素凝胶
    public bool DodgeBackstab;//闪避背刺
    public bool PoisonBullet;//剧毒子弹
    public bool PoisonExplosionGel;//毒爆凝胶
    public bool PoizonWeapons;//剧毒系武器
    public bool ToxicWrathSlime;//毒怒症史莱姆
    public bool ToxicHeart;//剧毒心脏
    public bool PoisonousSprint;//剧毒冲刺
    public bool PoisonousStrike;//剧毒猛击
    public bool PersistentToxic;//持久型剧毒
    public bool ThreePartsPoisoning;//入毒三分
    public bool ThunderstormEmblem;//暴雷纹章
    public bool ElectricBullets;//电系子弹
    public bool ThunderBreathIllusion;//雷息幻象
    public bool ThunderWeapon;//惊雷型武器
    public bool ThunderFury;//惊天雷怒
    public bool ElectricStorageSlime;//蓄电型史莱姆
    public bool ThunderstormDamage;//暴雷击破
    public bool ThunderTiming;//落雷Timing
}
