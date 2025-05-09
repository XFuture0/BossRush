using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Rendering;

public class UseCardManager : SingleTons<UseCardManager>
{
    public void StartInvoke(string CardName)
    {
        Invoke(CardName, 0);
    }
    //通用卡牌
    private void Mobility()
    {
        GameManager.Instance.Player().DashCount += 1;
        CancelInvoke();
    }
    private void FlyingSlime()
    {
        GameManager.Instance.Player().JumpCount += 1;
        CancelInvoke();
    }
    private void UltimateMobility()
    {
        CardManager.Instance.FindCard_Open("TearShadows");
        GameManager.Instance.Player().DashInvincibleFrame = true;
        CancelInvoke();
    }
    private void TearShadows()
    {
        GameManager.Instance.Player().DashDamage = true;
        CancelInvoke();
    }
    private void HPBottle()
    {
        GameManager.Instance.Player().NowHealth += (int)(GameManager.Instance.Player().MaxHealth * 0.8f);
        CancelInvoke();
    }
    private void MetalBullets()
    {
        GameManager.Instance.Player().AttackBonus += 0.2f;
        CancelInvoke();
    }
    private void WeaknessBreakdown()
    {
        GameManager.Instance.Player().CriticalDamageRate += 0.05f;
        CancelInvoke();
    }
    private void WowMushroom()
    {
        GameManager.Instance.Player().WeaponCount += 1;
        CancelInvoke();
    }
    private void Adrenaline()
    {
        GameManager.Instance.Player().AttackRate -= 0.1f;
        CancelInvoke();
    }
    private void ClockUp()
    {
        GameManager.Instance.Player().DodgeRate += 0.05f;
        CancelInvoke();
    }
    private void AngerBottleFull()
    {
        GameManager.Instance.Player().AngerValue = 1;
        CancelInvoke();
    }
    private void Padam()
    {
        GameManager.Instance.Player().SpeedRate += 0.05f;
        CancelInvoke();
    }
    //场景1
    private void MucusDeathRage()
    {
        GameManager.Instance.Player().NowHealth -= 2;
        GameManager.Instance.Player().AttackBonus += 0.3f;
        GameManager.Instance.Player().AngerValue += 0.25f;
        GameManager.Instance.Player().MucusDeathRage = true;
        GameManager.Instance.Boss().HealthRate += 0.1f;
        GameManager.Instance.Boss().AttackRate -= 0.1f;
        CancelInvoke();
    }
    private void SlimeCharge()
    {
        GameManager.Instance.Player().NowHealth -= 1;
        GameManager.Instance.Player().AttackBonus += 0.2f;
        GameManager.Instance.Player().AngerValue += 0.2f;
        GameManager.Instance.Boss().AttackPower += 1;
        CancelInvoke();
    }
    private void FearlessFury()
    {
        GameManager.Instance.Player().NowHealth -= 1;
        GameManager.Instance.Player().FearlessFury = true;
        GameManager.Instance.Boss().AttackPower += 1;
        GameManager.Instance.Boss().AttackRate -= 0.1f;
        CancelInvoke();
    }
    private void EmergencyEvacuation()
    {
        GameManager.Instance.Player().MaxHealth += 2;
        GameManager.Instance.Player().NowHealth += 3;
        GameManager.Instance.Player().SpeedRate += 0.1f;
        GameManager.Instance.Player().DodgeRate += 0.05f;
        GameManager.Instance.Player().AngerValue -= 0.2f;
        GameManager.Instance.Boss().HealthRate += 0.1f;
        GameManager.Instance.Boss().AttackRate += 0.05f;
        CancelInvoke();
    }
    private void MucousRage()
    {
        GameManager.Instance.Player().MaxHealth += 1;
        GameManager.Instance.Player().NowHealth += 1;
        GameManager.Instance.Player().AttackBonus += 0.25f;
        GameManager.Instance.Player().CriticalDamageRate += 0.1f;
        GameManager.Instance.Player().MucousRage = true;
        GameManager.Instance.Boss().AttackPower += 1;
        GameManager.Instance.Boss().AttackRate -= 0.1f;
        GameManager.Instance.AddBossSkillLevel();
        CancelInvoke();
    }
    private void ConfidentCampaign()
    {
        GameManager.Instance.Player().MaxHealth += 1;
        GameManager.Instance.Player().NowHealth += 1;
        GameManager.Instance.Player().AngerValue += 0.2f;
        GameManager.Instance.AddBossSkillLevel();
        CancelInvoke();
    }
    private void SlimeRareSepsis()
    {
        GameManager.Instance.Player().NowHealth -= 1;
        GameManager.Instance.Player().MaxHealth -= 1;
        GameManager.Instance.Player().AttackRate -= 0.1f;
        GameManager.Instance.Player().CriticalDamageRate += 0.1f;
        GameManager.Instance.Boss().HealthRate -= 0.1f;
        GameManager.Instance.Boss().AttackRate -= 0.1f;
        CancelInvoke();
    }
    private void SevenPointCookedSmokedSlime()
    {
        GameManager.Instance.Player().NowHealth -= 1;
        GameManager.Instance.Player().AngerValue += 0.7f;
        GameManager.Instance.Boss().AttackRate -= 0.1f;
        CancelInvoke();
    }
    private void ArmedPreparation()
    {
        GameManager.Instance.Player().WeaponCount += 1;
        GameManager.Instance.Player().SpeedRate -= 0.1f;
        GameManager.Instance.Player().AngerValue += 0.1f;
        GameManager.Instance.Boss().AttackPower += 1;
        GameManager.Instance.AddBossSkillLevel();
        CancelInvoke();
    }
    private void MucousPeristalsis()
    {
        GameManager.Instance.Player().NowHealth += 2;
        GameManager.Instance.Player().SpeedRate -= 0.05f;
        GameManager.Instance.Boss().HealthRate += 0.1f;
        CancelInvoke();
    }
    private void PreciseShot()
    {
        GameManager.Instance.Player().CriticalDamageRate += 0.1f;
        GameManager.Instance.Player().CriticalDamageBonus += 0.25f;
        GameManager.Instance.Player().AngerValue += 0.2f;
        GameManager.Instance.Boss().AttackPower += 1;
        CancelInvoke();
    }
    private void LrritableSlime()
    {
        GameManager.Instance.Player().LrritableSlime = true;
        GameManager.Instance.Player().AngerValue += 0.2f;
        GameManager.Instance.Boss().AttackPower += 1;
        GameManager.Instance.Boss().AttackRate -= 0.15f;
        CancelInvoke();
    }
    private void SmoothMucus()
    {
        GameManager.Instance.Player().SpeedRate += 0.15f;
        GameManager.Instance.Boss().AttackRate -= 0.1f;
        CancelInvoke();
    }
    private void BreathHoldingExpert()
    {
        GameManager.Instance.Player().AngerTime += 1;
        GameManager.Instance.Player().AngerValue += 0.2f;
        GameManager.Instance.Boss().AttackRate -= 0.1f;
        CancelInvoke();
    }
    private void ThreeMinuteHeat()
    {
        GameManager.Instance.Player().AngerTime -= 1;
        GameManager.Instance.Player().ThreeMinuteHeat = true;
        GameManager.Instance.Player().FullAnger = 0.5f;
        GameManager.Instance.AddBossSkillLevel();
        CancelInvoke();
    }
    private void MucusRegeneration()
    {
        GameManager.Instance.Player().AutoHealCount += 1;
        GameManager.Instance.Boss().AutoHealCount += 0.5f;
        CancelInvoke();
    }
    private void MucousLevelVisualColor()
    {
        GameManager.Instance.Player().DodgeRate += 0.1f;
        GameManager.Instance.Player().AngerValue += 0.2f;
        GameManager.Instance.Boss().AttackRate -= 0.1f;
        CancelInvoke();
    }
    private void SlimeStaredBlankly()
    {
        GameManager.Instance.Player().CriticalDamageRate += 0.15f;
        GameManager.Instance.Boss().HealthRate += 0.1f;
        GameManager.Instance.Boss().AttackRate -= 0.1f;
        CancelInvoke();
    }
    private void FuriousGatling()
    {
        GameManager.Instance.Player().FuriousGatling = true;
        GameManager.Instance.Boss().AttackPower += 1;
        CancelInvoke();
    }
    private void ShengqiCore()
    {
        GameManager.Instance.Player().MaxHealth += 1;
        GameManager.Instance.Player().NowHealth += 1;
        GameManager.Instance.Player().ShengqiCore = true;
        GameManager.Instance.Boss().AttackPower += 1;
        CancelInvoke();
    }
    //场景2
    private void SprintBuffer()
    {
        GameManager.Instance.Player().SpeedRate += 0.1f;
        GameManager.Instance.Player().SprintBuffer = true;
        GameManager.Instance.Boss().AttackRate -= 0.1f;
        CancelInvoke();
    }
    private void SlimeRunningChampion()
    {
        GameManager.Instance.Player().SpeedRate += 0.1f;
        GameManager.Instance.Player().AttackRate -= 0.05f;
        GameManager.Instance.Player().SlimeRunningChampion = true;
        GameManager.Instance.Boss().AttackRate -= 0.1f;
        CancelInvoke();
    }
    private void SprintPreparation()
    {
        GameManager.Instance.Player().SpeedRate += 0.1f;
        GameManager.Instance.Player().AttackRate -= 0.05f;
        GameManager.Instance.Player().DashCount += 1;
        GameManager.Instance.Boss().AttackRate -= 0.1f;
        GameManager.Instance.AddBossSkillLevel();
        CancelInvoke();
    }
    private void ADHDSlime()
    {
        GameManager.Instance.Player().NowHealth -= 1;
        GameManager.Instance.Player().SpeedRate += 0.05f;
        GameManager.Instance.Player().AttackRate -= 0.05f;
        GameManager.Instance.Boss().AttackRate -= 0.1f;
        CancelInvoke();
    }
    private void JungleEscapeInstinct()
    {
        GameManager.Instance.Player().SpeedRate += 0.15f;
        GameManager.Instance.Player().AttackRate += 0.15f;
        GameManager.Instance.Player().DodgeRate += 0.05f;
        GameManager.Instance.Boss().AttackRate -= 0.1f;
        CancelInvoke();
    }
    private void JungleCombatManual()
    {
        GameManager.Instance.Player().SpeedRate -= 0.15f;
        GameManager.Instance.Player().AttackRate -= 0.15f;
        GameManager.Instance.Player().DodgeRate += 0.05f;
        GameManager.Instance.AddBossSkillLevel();
        CancelInvoke();
    }
    private void NetworkLag()
    {
        GameManager.Instance.Player().MaxHealth += 1;
        GameManager.Instance.Player().NowHealth += 1;
        GameManager.Instance.Player().NetworkLag = true;
        GameManager.Instance.AddBossSkillLevel();
        CancelInvoke();
    }
    private void AdhesiveExoskeleton()
    {
        GameManager.Instance.Player().MaxHealth += 1;
        GameManager.Instance.Player().NowHealth += 1;
        GameManager.Instance.Player().AdhesiveExoskeleton = true;
        GameManager.Instance.AddBossSkillLevel();
        CancelInvoke();
    }
    private void SpeedEmblem()
    {
        GameManager.Instance.Player().SpeedRate += 0.1f;
        GameManager.Instance.Player().AttackRate -= 0.1f;
        GameManager.Instance.Player().DodgeRate += 0.05f;
        GameManager.Instance.Player().SpeedEmblem = true;
        GameManager.Instance.Boss().AttackRate -= 0.1f;
        GameManager.Instance.Boss().DodgeRate += 0.1f;
        CancelInvoke();
    }
    private void QuickAngerGel()
    {
        GameManager.Instance.Player().SpeedRate += 0.05f;
        GameManager.Instance.Player().QuickAngerGel = true;
        GameManager.Instance.Boss().HealthRate += 0.1f;
        GameManager.Instance.Boss().AttackRate -= 0.05f;
        CancelInvoke();
    }
    private void HavePrepared()
    {
        GameManager.Instance.Player().MaxHealth += 1;
        GameManager.Instance.Player().NowHealth += 1;
        GameManager.Instance.Player().WeaponCount += 1;
        GameManager.Instance.Boss().AttackPower += 1;
        GameManager.Instance.AddBossSkillLevel();
        CancelInvoke();
    }
    private void SyndromeSlime()
    {
        GameManager.Instance.Player().SpeedRate += 0.05f;
        GameManager.Instance.Player().AttackRate -= 0.05f;
        GameManager.Instance.Player().DodgeRate -= 0.05f;
        GameManager.Instance.Player().CriticalDamageRate += 0.1f;
        GameManager.Instance.Player().AngerValue += 0.2f;
        GameManager.Instance.Boss().AttackRate -= 0.1f;
        CancelInvoke();
    }
    private void SacredAnger()
    {
        GameManager.Instance.ChangePlayerAngerSkill(2);
        GameManager.Instance.Boss().HealthRate += 0.1f;
        GameManager.Instance.AddBossSkillLevel();
        CancelInvoke();
    }
    private void ElasticGel()
    {
        GameManager.Instance.Player().NowHealth += 1;
        GameManager.Instance.Player().SpeedRate += 0.05f;
        GameManager.Instance.Player().ElasticGel = true;
        GameManager.Instance.AddBossSkillLevel();
        CancelInvoke();
    }
    private void UrgentEngine()
    {
        GameManager.Instance.Player().UrgentEngine = true;
        GameManager.Instance.Boss().AttackRate -= 0.1f;
        CancelInvoke();
    }
    private void HolySpringForest()
    {
        GameManager.Instance.Player().MaxHealth += 3;
        GameManager.Instance.Player().NowHealth += 3;
        GameManager.Instance.Boss().HealthRate += 0.2f;
        CancelInvoke();
    }
    private void SelfProliferativeSlime()
    {
        GameManager.Instance.Player().NowHealth += 1;
        GameManager.Instance.Player().AutoHealCount += 1;
        GameManager.Instance.Boss().HealthRate += 0.1f;
        GameManager.Instance.Boss().AutoHealCount += 0.5f;
        CancelInvoke();
    }
    private void GuaranteedFirstPrize()
    {
        GameManager.Instance.Player().AttackRate -= 0.05f;
        GameManager.Instance.Player().CriticalDamageRate += 0.05f;
        GameManager.Instance.Player().GuaranteedFirstPrize = true;
        GameManager.Instance.AddBossSkillLevel();
        CancelInvoke();
    }
    private void BouncyJelly()
    {
        GameManager.Instance.Player().DodgeRate += 0.25f;
        GameManager.Instance.Player().BouncyJelly = true;
        GameManager.Instance.Boss().DodgeRate += 0.05f;
        CancelInvoke();
    }
    private void ImperialWeapons()
    {
        GameManager.Instance.Player().SpeedRate += 0.1f;
        GameManager.Instance.Player().ImperialWeapons = true;
        GameManager.Instance.Boss().AttackPower += 1;
        CancelInvoke();
    }
    private void HormoneGel()
    {
        GameManager.Instance.Player().CanDash = false;
        GameManager.Instance.Player().HormoneGel = true;
        GameManager.Instance.Boss().AttackRate -= 0.1f;
        CancelInvoke();
    }
    private void PowerHotSprings()
    {
        GameManager.Instance.Player().MaxHealth += 1;
        GameManager.Instance.Player().NowHealth += 1;
        GameManager.Instance.Player().AttackBonus += 0.2f;
        GameManager.Instance.Player().SpeedRate -= 0.1f;
        GameManager.Instance.Player().AttackRate += 0.1f;
        GameManager.Instance.Boss().AttackPower += 1;
        GameManager.Instance.Boss().AttackRate += 0.1f;
        CancelInvoke();
    }
    private void GoutySlime()
    {
        GameManager.Instance.Player().SpeedRate -= 0.1f;
        GameManager.Instance.Player().AngerValue += 0.5f;
        GameManager.Instance.AddBossSkillLevel();
        CancelInvoke();
    }
    private void DodgeBackstab()
    {
        GameManager.Instance.Player().DodgeRate += 0.1f;
        GameManager.Instance.Player().DodgeBackstab = true;
        GameManager.Instance.Boss().DodgeRate += 0.05f;
        GameManager.Instance.AddBossSkillLevel();
        CancelInvoke();
    }
    private void CowardlySlime()
    {
        GameManager.Instance.Player().DodgeRate += 0.1f;
        GameManager.Instance.Player().AttackRate += 0.05f;
        GameManager.Instance.Boss().AttackRate -= 0.1f;
        CancelInvoke();
    }
    //场景3
    private void PoisonBullet()
    {
        GameManager.Instance.Player().PoisonBullet = true;
        GameManager.Instance.Player().PoizonDamage += 0.1f;
        GameManager.Instance.Boss().HealthRate += 0.1f;
        CancelInvoke();
    }
    private void PharmacistSlime()
    {
        GameManager.Instance.Player().PoisonBullet = true;
        GameManager.Instance.Player().PoizonDamage += 0.1f;
        GameManager.Instance.AddBossSkillLevel();
        CancelInvoke();
    }
    private void YakPoizon()
    {
        GameManager.Instance.Player().PoisonBullet = true;
        GameManager.Instance.Player().AttackRate += 0.5f;
        GameManager.Instance.Player().PoizonDamage += 0.2f;
        GameManager.Instance.Boss().AttackPower += 1;
        CancelInvoke();
    }
    private void PoisonousHotSprings()
    {
        GameManager.Instance.Player().NowHealth -= 1;
        GameManager.Instance.Player().AttackRate -= 0.05f;
        GameManager.Instance.Player().PoizonDamage += 0.1f;
        GameManager.Instance.Boss().HealthRate += 0.1f;
        GameManager.Instance.AddBossSkillLevel();
        CancelInvoke();
    }
    private void PoisonExplosionGel()
    {
        GameManager.Instance.Player().PoisonExplosionGel = true;
        GameManager.Instance.Player().AttackRate -= 0.1f;
        GameManager.Instance.Boss().AttackRate -= 0.2f;
        CancelInvoke();
    }
    private void AlcoholAddictedSlime()
    {
        GameManager.Instance.ChangePlayerAngerSkill(3);
        GameManager.Instance.Player().PoizonDamage += 0.1f;
        GameManager.Instance.Boss().HealthRate += 0.1f;
        GameManager.Instance.AddBossSkillLevel();
        CancelInvoke();
    }
    private void AlcoholicGel()
    {
        GameManager.Instance.Player().AngerValue += 0.5f;
        GameManager.Instance.Player().AutoHealCount += 1;
        GameManager.Instance.Boss().AutoHealCount += 0.5f;
        CancelInvoke();
    }
    private void PoizonWeapons()
    {
        GameManager.Instance.Player().PoizonWeapons = true;
        GameManager.Instance.Boss().HealthRate += 0.1f;
        GameManager.Instance.Boss().AttackPower += 1;
        CancelInvoke();
    }
    private void AntitoxicGel()
    {
        GameManager.Instance.Player().MaxHealth += 1;
        GameManager.Instance.Player().NowHealth += 2;
        GameManager.Instance.Player().PoizonDamage -= 0.1f;
        GameManager.Instance.Player().AttackRate -= 0.1f;
        GameManager.Instance.Boss().HealthRate += 0.1f;
        GameManager.Instance.Boss().AttackRate -= 0.1f;
        CancelInvoke();
    }
    private void ToxicWrathSlime()
    {
        GameManager.Instance.Player().ToxicWrathSlime = true;
        GameManager.Instance.Boss().AttackRate -= 0.15f;
        CancelInvoke();
    }
    private void StickySlipperyVenom()
    {
        GameManager.Instance.Player().SpeedRate += 0.1f;
        GameManager.Instance.Player().PoizonDamage += 0.05f;
        GameManager.Instance.Boss().AttackRate -= 0.1f;
        CancelInvoke();
    }
    private void PoisonEmblem()
    {
        GameManager.Instance.Player().PoizonTime += (int)(GameManager.Instance.Player().PoizonTime * 0.5f);
        GameManager.Instance.Player().PoizonDamage += 0.1f;
        GameManager.Instance.Player().AttackRate -= 0.1f;
        GameManager.Instance.Boss().HealthRate += 0.25f;
        GameManager.Instance.Boss().AttackRate -= 0.1f;
        CancelInvoke();
    }
    private void HallucinogenicSlime()
    {
        GameManager.Instance.Player().NowHealth -= 1;
        GameManager.Instance.Player().WeaponCount += 1;
        GameManager.Instance.Player().AttackBonus -= 0.25f;
        GameManager.Instance.Boss().DodgeRate += 0.1f;
        GameManager.Instance.Boss().AttackRate -= 0.05f;
        CancelInvoke();
    }
    private void ChickenFlavoredMushroom()
    {
        GameManager.Instance.Player().NowHealth += 3;
        GameManager.Instance.Boss().HealthRate += 0.15f;
        CancelInvoke();
    }
    private void ToxicHeart()
    {
        GameManager.Instance.Player().ToxicHeart = true;
        GameManager.Instance.Boss().HealthRate += 0.1f;
        GameManager.Instance.AddBossSkillLevel();
        CancelInvoke();
    }
    private void PureSlime()
    {
        var PoizonCount = GameManager.Instance.Player().PoizonDamage;
        GameManager.Instance.Player().PoizonDamage = 0;
        GameManager.Instance.Player().AttackRate -= PoizonCount * 0.5f;
        GameManager.Instance.AddBossSkillLevel();
        CancelInvoke();
    }
    private void AngerOverload()
    {
        GameManager.Instance.Player().PoizonDamage -= 0.1f;
        GameManager.Instance.Player().AngerValue = 1;
        GameManager.Instance.Boss().AttackPower += 1;
        CancelInvoke();
    }
    private void PoisonousSprint()
    {
        GameManager.Instance.Player().PoisonousSprint = true;//TODO:之后做冲刺伤害时添加
        GameManager.Instance.Player().SpeedRate += 0.05f;
        GameManager.Instance.Boss().AttackRate -= 0.05f;
        CancelInvoke();
    }
    private void NonLuckySlime()
    {
        GameManager.Instance.Player().NowHealth -= 1;
        GameManager.Instance.Player().PoizonDamage += 0.05f;
        GameManager.Instance.Boss().DodgeRate += 0.05f;
        CancelInvoke();
    }
    private void PathologicalSlime()
    {
        GameManager.Instance.Player().PoizonDamage += 0.05f;
        GameManager.Instance.Player().AttackRate -= 0.05f;
        GameManager.Instance.Player().SpeedRate += 0.05f;
        GameManager.Instance.Player().CriticalDamageRate += 0.05f;
        GameManager.Instance.AddBossSkillLevel();
        CancelInvoke();
    }
    private void PoisonousStrike()
    {
        GameManager.Instance.Player().PoisonousStrike = true;
        GameManager.Instance.Player().CriticalDamageRate += 0.1f;
        GameManager.Instance.Boss().AttackPower += 1;
        CancelInvoke();
    }
    private void PersistentToxic()
    {
        GameManager.Instance.Player().PersistentToxic = true;
        GameManager.Instance.Player().PoizonTime += 3;
        GameManager.Instance.Boss().HealthRate += 0.1f;
        GameManager.Instance.Boss().AttackRate -= 0.1f;
        CancelInvoke();
    }
    private void ImbalanceDrunkenness()
    {
        GameManager.Instance.Player().AttackRate = 2 - GameManager.Instance.Player().AttackRate;
        GameManager.Instance.Boss().AttackRate -= 0.1f;
        GameManager.Instance.Boss().DodgeRate += 0.05f;
        CancelInvoke();
    }
    private void JuniorPharmacistCertificate()
    {
        GameManager.Instance.Player().PoizonTime += 2;
        GameManager.Instance.Player().PoizonDamage += 0.05f;
        GameManager.Instance.AddBossSkillLevel();
        CancelInvoke();
    }
    private void PowerHerb()
    {
        GameManager.Instance.Player().NowHealth += 1;
        GameManager.Instance.Player().AttackBonus += 0.2f;
        GameManager.Instance.Player().PoizonDamage -= 0.1f;
        GameManager.Instance.Boss().HealthRate += 0.1f;
        GameManager.Instance.Boss().AttackPower += 1;
        CancelInvoke();
    }
    private void Manna()
    {
        GameManager.Instance.Player().PoizonTime += 1;
        GameManager.Instance.Player().PoizonDamage += 0.05f;
        GameManager.Instance.Boss().AttackRate -= 0.05f;
        CancelInvoke();
    }
    private void XPoisonNeedle()
    {
        GameManager.Instance.Player().PoisonBullet = true;
        GameManager.Instance.Player().PoizonDamage += 0.05f;
        GameManager.Instance.Player().PoizonTime += 2;
        GameManager.Instance.Boss().HealthRate += 0.2f;
        CancelInvoke();
    }
    private void ThreePartsPoisoning()
    {
        GameManager.Instance.Player().ThreePartsPoisoning = true;
        GameManager.Instance.Player().PoizonDamage += 0.1f;
        GameManager.Instance.AddBossSkillLevel();
        CancelInvoke();
    }
    private void RaisingGuSlime()
    {
        GameManager.Instance.Player().MaxHealth += 3;
        GameManager.Instance.Player().NowHealth -= 1;
        GameManager.Instance.Boss().HealthRate += 0.1f;
        CancelInvoke();
    }
    private void RapidGel()
    {
        GameManager.Instance.Player().SpeedRate += 0.05f;
        GameManager.Instance.Player().AttackRate -= 0.05f;
        GameManager.Instance.Player().DodgeRate += 0.05f;
        GameManager.Instance.Boss().DodgeRate += 0.05f;
        CancelInvoke();
    }
    //场景4
    private void TraineeThunderboltMage()
    {
        GameManager.Instance.Player().ThunderBonus += 0.05f;
        GameManager.Instance.Player().ThunderRate += 0.05f;
        GameManager.Instance.Boss().HealthRate += 0.1f;
        GameManager.Instance.AddBossSkillLevel();
        CancelInvoke();
    }
    private void ThunderstormEmblem()
    {
        GameManager.Instance.Player().ThunderstormEmblem = true;
        GameManager.Instance.Player().ThunderBonus += 0.2f;
        GameManager.Instance.Player().ThunderRate += 0.1f;
        GameManager.Instance.Boss().AttackPower += 1;
        GameManager.Instance.Boss().AttackRate -= 0.1f;
        CancelInvoke();
    }
    private void ThunderGodWrath()
    {
        GameManager.Instance.ChangePlayerAngerSkill(4);
        GameManager.Instance.Player().ThunderBonus += 0.2f;
        GameManager.Instance.Boss().AttackPower += 1;
        GameManager.Instance.AddBossSkillLevel();
        CancelInvoke();
    }
    private void ConductiveSlime()
    {
        GameManager.Instance.Player().ThunderRate += 0.05f;
        GameManager.Instance.Player().ThunderBonus += 0.1f;
        GameManager.Instance.Boss().AttackRate -= 0.1f;
        CancelInvoke();
    }
    private void SonThunderGod()
    {
        GameManager.Instance.Player().ThunderBonus += 0.25f;
        GameManager.Instance.Player().SpeedRate += 0.1f;
        GameManager.Instance.Player().AttackRate -= 0.15f;
        GameManager.Instance.Boss().AttackRate -= 0.15f;
        GameManager.Instance.AddBossSkillLevel();
        CancelInvoke();
    }
    private void InsulatedSlime()
    {
        GameManager.Instance.Player().MaxHealth += 2;
        GameManager.Instance.Player().NowHealth += 2;
        GameManager.Instance.Player().AutoHealCount += 1;
        GameManager.Instance.Boss().HealthRate += 0.2f;
        CancelInvoke();
    }
    private void BreakdownVoltage()
    {
        GameManager.Instance.Player().AttackBonus += 0.1f;
        GameManager.Instance.Player().ThunderBonus += 0.1f;
        GameManager.Instance.Boss().AttackRate -= 0.1f;
        CancelInvoke();
    }
    private void ElectricShockResuscitation()
    {
        GameManager.Instance.Player().MaxHealth += 2;
        GameManager.Instance.Player().NowHealth += 2;
        GameManager.Instance.Player().ThunderBonus -= 0.1f;
        GameManager.Instance.Boss().HealthRate += 0.1f;
        GameManager.Instance.Boss().AutoHealCount += 0.5f;
        CancelInvoke();
    }
    private void LightningFlash()
    {
        GameManager.Instance.Player().ThunderRate += 0.05f;
        GameManager.Instance.Player().AttackRate -= 0.1f;
        GameManager.Instance.Boss().AttackRate -= 0.1f;
        CancelInvoke();
    }
    private void ElectricBullets()
    {
        GameManager.Instance.Player().ElectricBullets = true;
        GameManager.Instance.Player().ThunderBonus += 0.1f;
        GameManager.Instance.Player().AttackRate -= 0.1f;
        GameManager.Instance.Boss().HealthRate += 0.1f;
        GameManager.Instance.Boss().AttackRate -= 0.1f;
        CancelInvoke();
    }
    private void VigorousResolute()
    {
        GameManager.Instance.Player().ThunderRate -= 0.05f;
        GameManager.Instance.Player().SpeedRate += 0.1f;
        GameManager.Instance.Boss().AttackRate -= 0.1f;
        CancelInvoke();
    }
    private void ThunderBreathIllusion()
    {
        GameManager.Instance.Player().ThunderBreathIllusion  = true;
        GameManager.Instance.Player().ThunderRate += 0.05f;
        GameManager.Instance.Boss().HealthRate += 0.1f;
        GameManager.Instance.Boss().AutoHealCount += 0.5f;
        CancelInvoke();
    }
    private void ThunderWeapon()
    {
        GameManager.Instance.Player().ThunderWeapon = true;
        GameManager.Instance.Player().ThunderBonus += 0.1f;
        GameManager.Instance.Boss().AttackPower += 1;
        CancelInvoke();
    }
    private void LightningParalyze()
    {
        GameManager.Instance.Player().ThunderRate += 0.1f;
        GameManager.Instance.Player().SpeedRate -= 0.1f;
        GameManager.Instance.Player().DodgeRate -= 0.1f;
        GameManager.Instance.Boss().HealthRate += 0.15f;
        CancelInvoke();
    }
    private void LightningFighting()
    {
        GameManager.Instance.Player().ThunderBonus += 0.05f;
        GameManager.Instance.Player().AngerValue += 0.5f;
        GameManager.Instance.Boss().AttackRate -= 0.05f;
        CancelInvoke();
    }
    private void ThunderFury()
    {
        GameManager.Instance.Player().ThunderFury = true;
        GameManager.Instance.Player().ThunderRate += 0.05f;
        GameManager.Instance.Boss().HealthRate += 0.1f;
        GameManager.Instance.AddBossSkillLevel();
        CancelInvoke();
    }
    private void ThunderMountainFountain()
    {
        GameManager.Instance.Player().NowHealth += (int)(GameManager.Instance.Player().MaxHealth * 0.5f);
        GameManager.Instance.Boss().HealthRate += 0.1f;
        CancelInvoke();
    }
    private void ProficientElectricGuns()
    {
        GameManager.Instance.Player().ThunderRate += 0.1f;
        GameManager.Instance.Player().ThunderBonus += 0.1f;
        GameManager.Instance.Boss().HealthRate += 0.1f;
        GameManager.Instance.Boss().AttackPower += 1;
        CancelInvoke();
    }
    private void LeakageGel()
    {
        GameManager.Instance.Player().NowHealth -= 1;
        GameManager.Instance.Player().ThunderBonus += 0.2f;
        GameManager.Instance.Boss().HealthRate += 0.2f;
        CancelInvoke();
    }
    private void StorageGel()
    {
        GameManager.Instance.Player().NowHealth += 1;
        GameManager.Instance.Player().AttackBonus += 0.1f;
        GameManager.Instance.Player().ThunderBonus += 0.1f;
        GameManager.Instance.AddBossSkillLevel();
        CancelInvoke();
    }
    private void ElectricStorageSlime()
    {
        GameManager.Instance.Player().ElectricStorageSlime = true;
        GameManager.Instance.Player().ThunderRate += 0.05f;
        GameManager.Instance.Boss().HealthRate += 0.15f;
        GameManager.Instance.Boss().AttackRate -= 0.05f;
        CancelInvoke();
    }
    private void ThunderstormDamage()
    {
        GameManager.Instance.Player().ThunderstormDamage = true;
        GameManager.Instance.Player().CriticalDamageRate += 0.1f;
        GameManager.Instance.Player().CriticalDamageBonus += 0.2f;
        GameManager.Instance.Boss().HealthRate += 0.1f;
        GameManager.Instance.Boss().AttackRate -= 0.1f;
        CancelInvoke();
    }
    private void TianXuanFallingThunder()
    {
        GameManager.Instance.Player().CriticalDamageRate += 0.05f;
        GameManager.Instance.Player().CriticalDamageBonus += 0.1f;
        GameManager.Instance.Player().ThunderRate += 0.05f;
        GameManager.Instance.AddBossSkillLevel();
        CancelInvoke();
    }
    private void AngrySlime()
    {
        GameManager.Instance.Player().CriticalDamageRate -= 0.05f;
        GameManager.Instance.Player().AngerValue += 0.5f;
        GameManager.Instance.Boss().AttackRate -= 0.05f;
        CancelInvoke();
    }
    private void PoisonThunderSlime()
    {
        GameManager.Instance.Player().CriticalDamageBonus += 0.1f;
        GameManager.Instance.Player().PoizonDamage += 0.1f;
        GameManager.Instance.Player().AttackRate -= 0.05f;
        GameManager.Instance.Boss().AttackPower += 1;
        GameManager.Instance.AddBossSkillLevel();
        CancelInvoke();
    }
    private void PoisonousBacklash()
    {
        GameManager.Instance.Player().ThunderBonus -= 0.1f;
        GameManager.Instance.Player().PoizonDamage += 0.05f;
        GameManager.Instance.Player().PoizonTime += 1;
        GameManager.Instance.AddBossSkillLevel();
        CancelInvoke();
    }
    private void LightningElementization()
    {
        GameManager.Instance.Player().SpeedRate += 0.05f;
        GameManager.Instance.Player().DodgeRate += 0.1f;
        GameManager.Instance.Boss().HealthRate += 0.1f;
        GameManager.Instance.Boss().DodgeRate += 0.02f;
        CancelInvoke();
    }
    private void RKeyReload()
    {
        GameManager.Instance.Player().WeaponCount += 1;
        GameManager.Instance.Boss().HealthRate += 0.1f;
        GameManager.Instance.AddBossSkillLevel();
        CancelInvoke();
    }
    private void ThunderAlchemy()
    {
        GameManager.Instance.Player().WeaponCount += 1;
        GameManager.Instance.Player().ThunderRate -= 0.1f;
        GameManager.Instance.Player().ThunderBonus -= 0.2f;
        GameManager.Instance.AddBossSkillLevel();
        CancelInvoke();
    }
    private void ThunderTiming()
    {
        GameManager.Instance.Player().ThunderTiming = true;
        GameManager.Instance.Player().AttackRate -= 0.05f;
        GameManager.Instance.Boss().HealthRate += 0.1f;
        GameManager.Instance.Boss().AttackRate -= 0.1f;
        CancelInvoke();
    }
    private void ThunderboltRobbery()
    {
        GameManager.Instance.Player().NowHealth -= 1;
        GameManager.Instance.Player().ThunderBonus += 0.1f;
        GameManager.Instance.AddBossSkillLevel();
        CancelInvoke();
    }
    private void ThunderInducingGel()
    {
        GameManager.Instance.Player().NowHealth -= 1;
        GameManager.Instance.Player().ThunderRate += 0.05f;
        GameManager.Instance.Boss().AttackRate -= 0.05f;
        CancelInvoke();
    }
    private void LuckyStarSlime()
    {
        GameManager.Instance.Player().CriticalDamageRate += 0.1f;
        GameManager.Instance.Player().DodgeRate += 0.1f;
        GameManager.Instance.Player().ThunderRate += 0.1f;
        GameManager.Instance.Boss().HealthRate += 0.2f;
        GameManager.Instance.Boss().DodgeRate += 0.05f;
        CancelInvoke();
    }
    private void ThunderInducedStress()
    {
        GameManager.Instance.Player().NowHealth -= 1;
        GameManager.Instance.Player().ThunderBonus += 0.05f;
        GameManager.Instance.Player().AttackRate -= 0.05f;
        GameManager.Instance.Boss().AttackRate -= 0.05f;
        CancelInvoke();
    }
    private void JuniorThunderSummoning()
    {
        GameManager.Instance.Player().ThunderBonus += 0.05f;
        GameManager.Instance.Player().ThunderRate += 0.05f;
        GameManager.Instance.AddBossSkillLevel();
        CancelInvoke();
    }
    //场景5
    private void CrownPrinceBullet()
    {
        GameManager.Instance.Player().Vulnerability = true;
        GameManager.Instance.Player().Vulnerability_AttackBonus += 0.02f;
        GameManager.Instance.AddBossSkillLevel();
        CancelInvoke();
    }
    private void BleedingGel()
    {
        GameManager.Instance.Player().Vulnerability = true;
        GameManager.Instance.Player().Vulnerability_AttackBonus += 0.02f;
        GameManager.Instance.Boss().HealthRate += 0.1f;
        CancelInvoke();
    }
    private void FragileSlime()
    {
        GameManager.Instance.Player().Vulnerability = true;
        GameManager.Instance.Player().Vulnerability_AttackBonus += 0.02f;
        GameManager.Instance.Boss().AttackRate -= 0.05f;
        CancelInvoke();
    }
    private void WeaknessTelescope()
    {
        GameManager.Instance.Player().Vulnerability = true;
        GameManager.Instance.Player().Vulnerability_CriticalRate += 0.01f;
        GameManager.Instance.Boss().AttackRate -= 0.05f;
        CancelInvoke();
    }
    private void HurtInsight()
    {
        GameManager.Instance.Player().Vulnerability = true;
        GameManager.Instance.Player().Vulnerability_AttackBonus += 0.01f;
        GameManager.Instance.Player().VulnerabilityTime += 0.5f;
        GameManager.Instance.Boss().AttackRate -= 0.05f;
        CancelInvoke();
    }
    private void WoundTearing()
    {
        GameManager.Instance.Player().WoundTearing = true;
        GameManager.Instance.Player().VulnerabilityTime += 0.5f;
        GameManager.Instance.Boss().HealthRate += 1;
        GameManager.Instance.Boss().AttackRate -= 0.05f;
        CancelInvoke();
    }
    private void BloodyBomb()
    {
        GameManager.Instance.Player().BloodyBomb = true;
        GameManager.Instance.Player().Vulnerability_AttackBonus += 0.02f;
        GameManager.Instance.Boss().AttackPower += 1;
        CancelInvoke();
    }
    private void SacrificeOneselfResist()
    {
        GameManager.Instance.Player().NowHealth -= 1;
        GameManager.Instance.Player().Vulnerability_AttackBonus += 0.04f;
        GameManager.Instance.Boss().HealthRate += 0.2f;
        CancelInvoke();
    }
    private void RaisePlagPreview()
    {
        GameManager.Instance.Player().Vulnerability_CriticalRate += 0.02f;
        GameManager.Instance.Boss().AttackRate -= 0.1f;
        CancelInvoke();
    }
    private void PassionBloodAnger()
    {
        GameManager.Instance.ChangePlayerAngerSkill(5);
        GameManager.Instance.Player().Vulnerability_AttackBonus += 0.04f;
        GameManager.Instance.Player().Vulnerability_CriticalRate += 0.02f;
        GameManager.Instance.Boss().AttackPower += 1;
        GameManager.Instance.Boss().AttackRate -= 0.1f;
        CancelInvoke();
    }
    private void ClumsyDoctorSlime()
    {
        GameManager.Instance.Player().Vulnerability_AttackBonus += 0.04f;
        GameManager.Instance.Player().VulnerabilityTime += 0.5f;
        GameManager.Instance.Boss().HealthRate += 0.1f;
        GameManager.Instance.Boss().AttackRate -= 0.05f;
        CancelInvoke();
    }
    private void SevereOpenWounds()
    {
        GameManager.Instance.Player().NowHealth -= 1;
        GameManager.Instance.Player().VulnerabilityTime += 1;
        GameManager.Instance.Boss().AttackRate -= 0.05f;
        CancelInvoke();
    }
    private void ExecutionerSlime()
    {
        GameManager.Instance.Player().MaxVulnerabilityRate = 0.25f;
        GameManager.Instance.Player().Vulnerability_AttackBonus += 0.04f;
        GameManager.Instance.Boss().HealthRate += 0.1f;
        GameManager.Instance.Boss().AttackPower += 1;
        CancelInvoke();
    }
    private void ApprenticeAssassin()
    {
        GameManager.Instance.Player().MaxVulnerabilityRate = 0.1f;
        GameManager.Instance.Player().Vulnerability_AttackBonus += 0.02f;
        GameManager.Instance.AddBossSkillLevel();
        CardManager.Instance.FindCard_Open("ExecutionerSlime");
        CancelInvoke();
    }
    private void SlowBloodthirsty()
    {
        GameManager.Instance.Player().MaxHealth -= 1;
        GameManager.Instance.Player().NowHealth -= 1;
        GameManager.Instance.Player().SlowBloodthirsty = true;
        GameManager.Instance.Boss().HealthRate += 0.2f;
        CancelInvoke();
    }
    private void BloodthirstyEmblem()
    {
        GameManager.Instance.Player().BloodthirstyEmblem = true;
        GameManager.Instance.Player().Vulnerability_AttackBonus += 0.04f;
        GameManager.Instance.Player().Vulnerability_CriticalRate += 0.02f;
        GameManager.Instance.Player().VulnerabilityTime += 1;
        GameManager.Instance.Boss().HealthRate += 0.2f;
        GameManager.Instance.Boss().AttackPower += 1;
        GameManager.Instance.Boss().AttackRate -= 0.05f;
        CancelInvoke();
    }
    private void ShuraGel()
    {
        GameManager.Instance.Player().MaxHealth += 3;
        GameManager.Instance.Player().NowHealth -= 1;
        GameManager.Instance.Player().Vulnerability_AttackBonus += 0.04f;
        GameManager.Instance.Player().AttackBonus += 0.2f;
        GameManager.Instance.Boss().AttackRate -= 0.1f;
        GameManager.Instance.AddBossSkillLevel();
        CancelInvoke();
    }
    private void BloodthirstyWeapon()
    {
        GameManager.Instance.Player().BloodthirstyWeapon = true;
        GameManager.Instance.Player().AttackBonus += 0.2f;
        GameManager.Instance.Player().Vulnerability_AttackBonus += 0.02f;
        GameManager.Instance.Boss().AttackRate -= 0.1f;
        GameManager.Instance.Boss().HealthRate += 0.1f;
        CancelInvoke();
    }
    private void EmergencyGel()
    {
        GameManager.Instance.Player().NowHealth += 2;
        GameManager.Instance.Player().AttackRate -= 0.1f;
        GameManager.Instance.Player().Vulnerability_AttackBonus -= 0.02f;
        GameManager.Instance.Boss().HealthRate += 0.1f;
        GameManager.Instance.AddBossSkillLevel();
        CancelInvoke();
    }
    private void StrongWill()
    {
        GameManager.Instance.Player().MaxHealth += 1;
        GameManager.Instance.Player().NowHealth += 1;
        GameManager.Instance.Player().Vulnerability_AttackBonus += 0.02f;
        GameManager.Instance.Player().Vulnerability_CriticalRate += 0.01f;
        GameManager.Instance.Boss().HealthRate += 0.1f;
        GameManager.Instance.Boss().AttackPower += 1;
        CancelInvoke();
    }
    private void BloodBath()
    {
        GameManager.Instance.Player().NowHealth += (int)(GameManager.Instance.Player().MaxHealth * 0.5f);
        GameManager.Instance.Player().Vulnerability_AttackBonus -= 0.02f;
        GameManager.Instance.Boss().HealthRate += 0.1f;
        CancelInvoke();
    }
    private void VeteranBattle()
    {
        GameManager.Instance.Player().WeaponCount += 1;
        GameManager.Instance.Player().AttackBonus += 0.1f;
        GameManager.Instance.Player().AttackRate -= 0.05f;
        GameManager.Instance.Player().Vulnerability_AttackBonus += 0.02f;
        GameManager.Instance.Player().Vulnerability_CriticalRate += 0.01f;
        GameManager.Instance.Boss().HealthRate += 0.2f;
        GameManager.Instance.AddBossSkillLevel();
        CancelInvoke();
    }
    private void BloodLossAnger()
    {
        GameManager.Instance.Player().Vulnerability_AttackBonus -= 0.02f;
        GameManager.Instance.Player().Vulnerability_CriticalRate -= 0.01f;
        GameManager.Instance.Player().AngerValue += 0.5f;
        GameManager.Instance.Boss().AttackRate -= 0.05f;
        CancelInvoke();
    }
    private void BloodthirstyIncreasesAnger()
    {
        GameManager.Instance.Player().BloodthirstyIncreasesAnger = true;
        GameManager.Instance.Player().Vulnerability_AttackBonus += 0.04f;
        GameManager.Instance.Boss().AttackRate -= 0.15f;
        CancelInvoke();
    }
    private void InjuredFleeing()
    {
        GameManager.Instance.Player().Vulnerability_AttackBonus -= 0.02f;
        GameManager.Instance.Player().SpeedRate += 0.1f;
        GameManager.Instance.Boss().HealthRate += 0.1f;
        CancelInvoke();
    }
    private void BloodPhantom()
    {
        GameManager.Instance.Player().Vulnerability_AttackBonus -= 0.02f;
        GameManager.Instance.Player().Vulnerability_CriticalRate -= 0.01f;
        GameManager.Instance.Player().DodgeRate += 0.05f;
        GameManager.Instance.AddBossSkillLevel();
        CancelInvoke();
    }
    private void ToxinImmersionInjury()
    {
        GameManager.Instance.Player().ToxinImmersionInjury = true;
        GameManager.Instance.Player().PoizonDamage += 0.05f;
        GameManager.Instance.Player().Vulnerability_AttackBonus += 0.02f;
        GameManager.Instance.Boss().HealthRate += 0.1f;
        GameManager.Instance.AddBossSkillLevel();
        CancelInvoke();
    }
    private void ToxicotherapyGel()
    {
        GameManager.Instance.Player().NowHealth += 1;
        GameManager.Instance.Player().Vulnerability_AttackBonus -= 0.04f;
        GameManager.Instance.Player().PoizonDamage += 0.1f;
        GameManager.Instance.Boss().HealthRate += 0.1f;
        GameManager.Instance.AddBossSkillLevel();
        CancelInvoke();
    }
    private void ConductiveWound()
    {
        GameManager.Instance.Player().ConductiveWound = true;
        GameManager.Instance.Player().ThunderBonus += 0.1f;
        GameManager.Instance.Player().Vulnerability_AttackBonus += 0.02f;
        GameManager.Instance.Boss().HealthRate += 0.1f;
        GameManager.Instance.Boss().AttackPower += 1;
        CancelInvoke();
    }
    private void ElectrotherapyGel()
    {
        GameManager.Instance.Player().NowHealth += 1;
        GameManager.Instance.Player().Vulnerability_AttackBonus -= 0.04f;
        GameManager.Instance.Player().ThunderBonus += 0.2f;
        GameManager.Instance.Boss().HealthRate += 0.2f;
        CancelInvoke();
    }
    private void ChargeForward()
    {
        GameManager.Instance.Player().AttackRate -= 0.1f;
        GameManager.Instance.Player().Vulnerability_AttackBonus += 0.02f;
        GameManager.Instance.Boss().AttackRate -= 0.1f;
        CancelInvoke();
    }
    private void HonestSlime()
    {
        GameManager.Instance.Player().AttackBonus += 0.2f;
        GameManager.Instance.Player().Vulnerability_CriticalRate += 0.01f;
        GameManager.Instance.Boss().AttackRate -= 0.1f;
        CancelInvoke();
    }
    private void SelfHarmingSlime()
    {
        GameManager.Instance.Player().WeaponCount -= 1;
        GameManager.Instance.Player().NowHealth -= 1;
        GameManager.Instance.Player().Vulnerability_AttackBonus -= 0.02f;
        GameManager.Instance.Player().Vulnerability_CriticalRate -= 0.01f;
        GameManager.Instance.Boss().AttackPower += 1;
        CancelInvoke();
    }
    private void HerbGel()
    {
        GameManager.Instance.Player().NowHealth += 2;
        GameManager.Instance.Player().VulnerabilityTime -= 0.5f;
        GameManager.Instance.Boss().HealthRate += 0.1f;
        CancelInvoke();
    }
    private void UnilateralInjury()
    {
        GameManager.Instance.Player().Vulnerability_AttackBonus += 0.06f;
        GameManager.Instance.Player().Vulnerability_CriticalRate -= 0.02f;
        GameManager.Instance.Player().HealthRate += 0.05f;
        GameManager.Instance.AddBossSkillLevel();
        CancelInvoke();
    }
    private void EqualExchange_Attack()
    {
        var VulnerabilityAttackBonus = GameManager.Instance.Player().Vulnerability_AttackBonus;
        GameManager.Instance.Player().Vulnerability_AttackBonus = 0;
        GameManager.Instance.Player().AttackBonus += VulnerabilityAttackBonus * 10;
        GameManager.Instance.AddBossSkillLevel();
        CancelInvoke();
    }
    private void EqualExchange_Critical()
    {
        var VulnerabilityCriticalRate = GameManager.Instance.Player().Vulnerability_CriticalRate;
        GameManager.Instance.Player().Vulnerability_CriticalRate = 0;
        GameManager.Instance.Player().CriticalDamageRate += VulnerabilityCriticalRate * 5;
        GameManager.Instance.AddBossSkillLevel();
        CancelInvoke();
    }
    private void ChasingThroughInjuries()
    {
        GameManager.Instance.Player().ChasingThroughInjuries = true;
        GameManager.Instance.Player().Vulnerability_CriticalRate += 0.01f;
        GameManager.Instance.Boss().HealthRate += 0.1f;
        GameManager.Instance.Boss().AttackPower += 1;
        CancelInvoke();
    }
    private void BarbedGel()
    {
        GameManager.Instance.Player().Vulnerability_AttackBonus += 0.02f;
        GameManager.Instance.Player().Vulnerability_CriticalRate += 0.01f;
        GameManager.Instance.Boss().HealthRate += 0.1f;
        GameManager.Instance.Boss().AttackRate += 0.1f;
        GameManager.Instance.Boss().AutoHealCount += 0.5f;
        CancelInvoke();
    }
    private void MedicalSlime()
    {
        GameManager.Instance.Player().MaxHealth += 2;
        GameManager.Instance.Player().NowHealth += 2;
        GameManager.Instance.Player().AutoHealCount += 1;
        GameManager.Instance.Boss().HealthRate += 0.15f;
        GameManager.Instance.AddBossSkillLevel();
        CancelInvoke();
    }
    //场景6
    private void TraineeWaterSystemMaster()
    {
        GameManager.Instance.Player().WaterElementBullet = true;
        GameManager.Instance.Player().WaterElementBonus += 0.1f;
        GameManager.Instance.Boss().HealthRate += 0.1f;
        CancelInvoke();
    }
    private void WaterBullets()
    {
        GameManager.Instance.Player().WaterElementBullet = true;
        GameManager.Instance.Player().WaterElementBonus += 0.05f;
        GameManager.Instance.Player().AttackRate -= 0.05f;
        GameManager.Instance.Boss().AttackRate -= 0.1f;
        CancelInvoke();
    }
    private void FlowingGel()
    {
        GameManager.Instance.Player().WaterElementBullet = true;
        GameManager.Instance.Player().WaterElementBonus += 0.05f;
        GameManager.Instance.Player().AttackBonus += 0.1f;
        GameManager.Instance.AddBossSkillLevel();
        CancelInvoke();
    }
    private void WaterElementSlime()
    {
        GameManager.Instance.Player().WaterElementBullet = true;
        GameManager.Instance.Player().CriticalDamageRate += 0.05f;
        GameManager.Instance.Player().WaterElementBonus += 0.05f;
        GameManager.Instance.Boss().AttackRate -= 0.05f;
        CancelInvoke();
    }
    private void ColdSlime()
    {
        GameManager.Instance.Player().MaxHealth += 1;
        GameManager.Instance.Player().NowHealth += 1;
        GameManager.Instance.Player().WaterElementBullet = true;
        GameManager.Instance.Player().WaterElementBonus += 0.05f;
        GameManager.Instance.Boss().AttackRate -= 0.05f;
        CancelInvoke();
    }
    private void WaterAccumulationIllness()
    {
        GameManager.Instance.Player().EasyWater = true;
        GameManager.Instance.BossStats.gameObject.GetComponent<EasyWater>().SetEasyWater(1);
        GameManager.Instance.Player().WaterElementBonus += 0.1f;
        GameManager.Instance.AddBossSkillLevel();
        CardManager.Instance.FindCard_Open("WaterCondensesCold");
        CancelInvoke();
    }
    private void WaterCondensesCold()
    {
        CardManager.Instance.FindCard_Open("ExtremelyColdPiercing");
        GameManager.Instance.PlayerStats.gameObject.GetComponent<EasyWater>().SetEasyWater(2);
        GameManager.Instance.Player().WaterElementBonus += 0.2f;
        GameManager.Instance.Player().HealthRate += 0.1f;
        GameManager.Instance.Player().AttackPower += 1;
        CancelInvoke();
    }
    private void ExtremelyColdPiercing()
    {
        GameManager.Instance.PlayerStats.gameObject.GetComponent<EasyWater>().SetEasyWater(3);
        GameManager.Instance.Player().WaterElementBonus += 0.2f;
        GameManager.Instance.Boss().AttackRate -= 0.1f;
        GameManager.Instance.AddBossSkillLevel();
        CancelInvoke();
    }
    private void ElementalMystery()
    {
        GameManager.Instance.Player().WaterElementBonus += 0.1f;
        GameManager.Instance.Player().AttackRate -= 0.1f;
        GameManager.Instance.AddBossSkillLevel();
        CancelInvoke();
    }
    private void WaterEmblem()
    {
        GameManager.Instance.Player().WaterEmblem = true;
        GameManager.Instance.Player().WaterElementBonus += 0.2f;
        GameManager.Instance.Player().AttackRate -= 0.05f;
        GameManager.Instance.Player().CriticalDamageRate += 0.1f;
        GameManager.Instance.Player().HealthRate += 0.2f;
        GameManager.Instance.Player().AttackPower += 1;
        CancelInvoke();
    }
    private void SolidificationWater()
    {
        GameManager.Instance.Player().EasyWaterTime += 1;
        GameManager.Instance.Player().WaterElementBonus += 0.1f;
        GameManager.Instance.AddBossSkillLevel();
        CancelInvoke();
    }
    private void SevereCold()
    {
        GameManager.Instance.Player().NowHealth -= 1;
        GameManager.Instance.Player().WaterElementBonus += 0.1f;
        GameManager.Instance.Boss().HealthRate += 0.1f;
        CancelInvoke();
    }
    private void CondensingColdPuncture()
    {
        GameManager.Instance.Player().CondensingColdPuncture = true;
        GameManager.Instance.Player().EasyWaterTime += 1;
        GameManager.Instance.Boss().HealthRate += 0.1f;
        GameManager.Instance.AddBossSkillLevel();
        CancelInvoke();
    }
    private void DragonEnraged()
    {
        GameManager.Instance.ChangePlayerAngerSkill(6);
        GameManager.Instance.Player().WaterElementBonus += 0.2f;
        GameManager.Instance.Player().CriticalDamageRate += 0.05f;
        GameManager.Instance.Boss().HealthRate += 0.2f;
        GameManager.Instance.Boss().AttackPower += 1;
        CancelInvoke();
    }
    private void FlowingFountain()
    {
        GameManager.Instance.Player().MaxHealth += 2;
        GameManager.Instance.Player().NowHealth += 2;
        GameManager.Instance.Player().WaterElementBonus += 0.1f;
        GameManager.Instance.Boss().HealthRate += 0.15f;
        GameManager.Instance.Boss().AutoHealCount += 0.5f;
        CancelInvoke();
    }
    private void WalkWater()
    {
        GameManager.Instance.Player().WaterElementBonus += 0.1f;
        GameManager.Instance.Player().SpeedRate += 0.1f;
        GameManager.Instance.Boss().AttackRate -= 0.1f;
        CancelInvoke();
    }
    private void WaterInsight()
    {
        GameManager.Instance.Player().WaterElementBonus += 0.1f;
        GameManager.Instance.Player().CriticalDamageRate += 0.05f;
        GameManager.Instance.Boss().AttackRate -= 0.1f;
        CancelInvoke();
    }
    private void Mirage()
    {
        GameManager.Instance.Player().EasyWaterTime += 0.5f;
        GameManager.Instance.Player().DodgeRate += 0.5f;
        GameManager.Instance.Boss().DodgeRate += 0.02f;
        GameManager.Instance.AddBossSkillLevel();
        CancelInvoke();
    }
    private void Dishuichou()
    {
        GameManager.Instance.Player().EasyWaterTime -= 1f;
        GameManager.Instance.Player().WaterElementBonus += 0.15f;
        GameManager.Instance.Boss().HealthRate += 0.1f;
        GameManager.Instance.Boss().AttackRate -= 0.05f;
        CancelInvoke();
    }
    private void SeaGodMessenger()
    {
        GameManager.Instance.Player().SeaGodMessenger = true;
        GameManager.Instance.Player().AttackRate -= 0.1f;
        GameManager.Instance.Boss().HealthRate += 0.1f;
        GameManager.Instance.Boss().AttackRate -= 0.1f;
        CancelInvoke();
    }
    private void SeaGodGrace()
    {
        GameManager.Instance.Player().NowHealth += (int)(GameManager.Instance.Player().MaxHealth * 0.5f);
        GameManager.Instance.Player().WaterElementBonus += 0.1f;
        GameManager.Instance.Boss().HealthRate += 0.1f;
        GameManager.Instance.AddBossSkillLevel();
        CancelInvoke();
    }
    private void WaterPhantom()
    {
        GameManager.Instance.Player().DashCount += 1;
        GameManager.Instance.Player().SpeedRate += 0.05f;
        GameManager.Instance.Player().WaterElementBonus += 0.05f;
        GameManager.Instance.Boss().DodgeRate += 0.02f;
        GameManager.Instance.AddBossSkillLevel();
        CancelInvoke();
    }
    private void WeaponsSeaGod()
    {
        GameManager.Instance.Player().WeaponsSeaGod = true;
        GameManager.Instance.Player().WaterElementBonus += 0.1f;
        GameManager.Instance.Boss().AttackRate -= 0.05f;
        GameManager.Instance.Boss().AttackPower += 1;
        CancelInvoke();
    }
    private void WaterVortex()
    {
        GameManager.Instance.Player().SpeedRate -= 0.05f;
        GameManager.Instance.Player().WaterElementBonus += 0.05f;
        GameManager.Instance.Boss().AttackRate += 0.05f;
        CancelInvoke();
    }
    private void WaterFlowProtectionBody()
    {
        GameManager.Instance.Player().MaxHealth += 1;
        GameManager.Instance.Player().NowHealth += 1;
        GameManager.Instance.Player().InvincibleTime += 0.5f;
        GameManager.Instance.Player().EasyWaterTime += 0.5f;
        GameManager.Instance.Boss().DodgeRate += 0.02f;
        GameManager.Instance.AddBossSkillLevel();
        CancelInvoke();
    }
    private void TurbulentRadiation()
    {
        GameManager.Instance.Player().TurbulentRadiation = true;
        GameManager.Instance.Player().AttackRate -= 0.1f;
        GameManager.Instance.Boss().AttackRate -= 0.1f;
        CancelInvoke();
    }
    private void ExchangeSeaGod()
    {
        GameManager.Instance.Player().WeaponCount += 1;
        GameManager.Instance.Player().WaterElementBonus -= 0.1f;
        GameManager.Instance.Player().EasyWaterTime -= 0.5f;
        GameManager.Instance.Boss().HealthRate += 0.2f;
        GameManager.Instance.Boss().AttackRate += 0.1f;
        GameManager.Instance.Boss().AttackPower += 1;
        CancelInvoke();
    }
    private void UnderwaterTreasure()
    {
        GameManager.Instance.Player().MaxHealth += 1;
        GameManager.Instance.Player().NowHealth += 1;
        GameManager.Instance.Player().WeaponCount += 1;
        GameManager.Instance.Boss().HealthRate += 0.3f;
        GameManager.Instance.Boss().AttackRate -= 0.1f;
        CancelInvoke();
    }
    private void Hydrogel()
    {
        GameManager.Instance.Player().WaterElementBonus += 0.15f;
        GameManager.Instance.Boss().AttackRate -= 0.1f;
        CancelInvoke();
    }
    private void DrinkenChickenSlime()
    {
        GameManager.Instance.Player().WaterElementBonus -= 0.05f;
        GameManager.Instance.Player().AngerValue += 0.5f;
        GameManager.Instance.Boss().HealthRate += 0.1f;
        CancelInvoke();
    }
    private void AngerSeaGod()
    {
        GameManager.Instance.Player().AngerSeaGod = true;
        GameManager.Instance.Player().WaterElementBonus += 0.05f;
        GameManager.Instance.AddBossSkillLevel();
        CancelInvoke();
    }
    private void GlidingWaterSurface()
    {
        GameManager.Instance.Player().GlidingWaterSurface = true;
        GameManager.Instance.Player().SpeedRate += 0.05f;
        GameManager.Instance.Boss().DodgeRate += 0.02f;
        GameManager.Instance.Boss().AttackRate -= 0.05f;
        CancelInvoke();
    }
    private void RainforestTrails()
    {
        GameManager.Instance.Player().WaterElementBonus += 0.1f;
        GameManager.Instance.Player().DodgeRate += 0.05f;
        GameManager.Instance.Boss().HealthRate += 0.1f;
        GameManager.Instance.Boss().DodgeRate += 0.04f;
        CancelInvoke();
    }
    private void VenomCoagulation()
    {
        GameManager.Instance.Player().VenomCoagulation = true;
        GameManager.Instance.Player().WaterElementBonus += 0.1f;
        GameManager.Instance.Boss().HealthRate += 0.1f;
        GameManager.Instance.Boss().AttackPower += 1;
        CancelInvoke();
    }
    private void HighlyToxicInfection()
    {
        GameManager.Instance.Player().WaterElementBonus -= 0.2f;
        GameManager.Instance.Player().PoizonDamage += 0.1f;
        GameManager.Instance.AddBossSkillLevel();
        CancelInvoke();
    }
    private void ConductiveWaterFlow()
    {
        GameManager.Instance.Player().ConductiveWaterFlow = true;
        GameManager.Instance.Player().WaterElementBonus += 0.1f;
        GameManager.Instance.Boss().HealthRate += 0.1f;
        GameManager.Instance.AddBossSkillLevel();
        CancelInvoke();
    }
    private void TorpedoStrike()
    {
        GameManager.Instance.Player().WaterElementBonus += 0.1f;
        GameManager.Instance.Player().ThunderBonus += 0.1f;
        GameManager.Instance.Boss().AttackPower += 1;
        CancelInvoke();
    }
    private void WaterCoagulation()
    {
        GameManager.Instance.Player().Vulnerability_AttackBonus -= 0.02f;
        GameManager.Instance.Player().AutoHealCount += 1;
        GameManager.Instance.Boss().AutoHealCount += 0.5f;
        CancelInvoke();
    }
    private void RapidsPiercingInjury()
    {
        GameManager.Instance.Player().WaterElementBonus -= 0.1f;
        GameManager.Instance.Player().Vulnerability_AttackBonus += 0.02f;
        GameManager.Instance.Boss().AttackRate -= 0.1f;
        CancelInvoke();
    }
    //场景7
    private void OldDangerousTrigger()
    {
        GameManager.Instance.Player().DangerousBullet = true;
        GameManager.Instance.Player().DangerousBulletBonus += 0.5f;
        GameManager.Instance.AddBossSkillLevel();
        CancelInvoke();
    }
    private void NewDangerousTrigger()
    {
        GameManager.Instance.Player().DangerousBulletCount = 5;
        GameManager.Instance.Player().DangerousBulletBonus += 0.5f;
        GameManager.Instance.Player().AttackRate -= 0.1f;
        GameManager.Instance.Boss().HealthRate += 0.1f;
        GameManager.Instance.Boss().AttackRate -= 0.1f;
        GameManager.Instance.Boss().AttackPower += 1;
        CancelInvoke();
    }
    private void EvilFunSlime()
    {
        GameManager.Instance.Player().DangerousBullet = true;
        GameManager.Instance.Player().DangerousBulletBonus += 0.5f;
        GameManager.Instance.Boss().HealthRate += 0.1f;
        GameManager.Instance.Boss().AttackRate -= 0.1f;
        CancelInvoke();
    }
    private void DangerousBullets()
    {
        GameManager.Instance.Player().DangerousBullet = true;
        GameManager.Instance.Player().DangerousBulletBonus += 0.5f;
        GameManager.Instance.Boss().HealthRate += 0.2f;
        GameManager.Instance.Boss().AttackRate -= 0.05f;
        CancelInvoke();
    }
    private void DangerousGel()
    {
        GameManager.Instance.Player().DangerousBullet = true;
        GameManager.Instance.Player().DangerousBulletBonus += 0.8f;
        GameManager.Instance.Boss().AttackPower += 1;
        CancelInvoke();
    }
    private void DeepDangerous()
    {
        GameManager.Instance.Player().DeepDangerous = true;
        GameManager.Instance.Player().DangerousBulletBonus += 0.5f;
        GameManager.Instance.Boss().HealthRate += 0.1f;
        GameManager.Instance.Boss().AttackRate -= 0.1f;
        GameManager.Instance.AddBossSkillLevel();
        CancelInvoke();
    }
    private void ExplosiveFrenzySlime()
    {
        GameManager.Instance.Player().NowHealth -= 1;
        GameManager.Instance.Player().DangerousBulletBonus += 0.5f;
        GameManager.Instance.Boss().AttackRate -= 0.05f;
        CancelInvoke();
    }
    private void DangerousWeapons()
    {
        GameManager.Instance.Player().DangerousWeapons = true;
        GameManager.Instance.Player().DangerousBulletBonus += 0.5f;
        GameManager.Instance.Player().AttackBonus += 0.2f;
        GameManager.Instance.Boss().AttackRate -= 0.1f;
        GameManager.Instance.AddBossSkillLevel();
        CancelInvoke();
    }
    private void DangerousSprint()
    {
        GameManager.Instance.Player().DangerousSprint = true;
        GameManager.Instance.Player().DashCount += 1;
        GameManager.Instance.Boss().HealthRate += 0.2f;
        GameManager.Instance.Boss().DodgeRate += 0.05f;
        CancelInvoke();
    }
    private void SlowHeatingSlime()
    {
        GameManager.Instance.Player().MaxHealth += 2;
        GameManager.Instance.Player().NowHealth += 2;
        GameManager.Instance.Player().DangerousBulletBonus += 0.25f;
        GameManager.Instance.Boss().HealthRate += 0.2f;
        GameManager.Instance.Boss().AutoHealCount += 0.5f;
        CancelInvoke();
    }
    private void SpiderSense()
    {
        GameManager.Instance.Player().SpiderSense = true;
        GameManager.Instance.Player().DangerousBulletBonus += 0.25f;
        GameManager.Instance.Boss().AttackRate -= 0.1f;
        GameManager.Instance.Boss().AttackPower += 1;
        CancelInvoke();
    }
    private void HeatwaveFountain()
    {
        GameManager.Instance.Player().NowHealth += (int)(GameManager.Instance.Player().MaxHealth * 0.5f);
        GameManager.Instance.Player().DangerousBulletBonus += 0.25f;
        GameManager.Instance.Boss().HealthRate += 0.15f;
        CancelInvoke();
    }
    private void DangerousVision()
    {
        GameManager.Instance.Player().AttackBonus += 0.2f;
        GameManager.Instance.Player().DangerousBulletBonus += 0.25f;
        GameManager.Instance.Boss().AttackPower += 1;
        CancelInvoke();
    }
    private void CowardlyMushroomSlime()
    {
        GameManager.Instance.Player().MaxHealth += 2;
        GameManager.Instance.Player().NowHealth += 3;
        GameManager.Instance.Player().DangerousBulletBonus -= 0.25f;
        GameManager.Instance.Boss().AttackRate -= 0.05f;
        CancelInvoke();
    }
    private void CrazyShooting()
    {
        GameManager.Instance.Player().WeaponCount += 1;
        GameManager.Instance.Player().AttackRate -= 0.1f;
        GameManager.Instance.Player().DangerousBulletBonus += 0.25f;
        GameManager.Instance.Boss().AttackRate -= 0.15f;
        GameManager.Instance.AddBossSkillLevel();
        CancelInvoke();
    }
    private void SavedFromDeath()
    {
        GameManager.Instance.Player().NowHealth -= 2;
        GameManager.Instance.Player().MaxHealth -= 2;
        GameManager.Instance.Player().WeaponCount += 1;
        GameManager.Instance.Player().DangerousBulletBonus += 0.25f;
        GameManager.Instance.Boss().HealthRate += 0.2f;
        CancelInvoke();
    }
    private void DangerousSedative()
    {
        GameManager.Instance.Player().DangerousBulletBonus -= 0.5f;
        GameManager.Instance.Player().AttackBonus += 0.2f;
        GameManager.Instance.Player().CriticalDamageRate += 0.1f;
        GameManager.Instance.Player().CriticalDamageBonus += 0.25f;
        GameManager.Instance.Boss().HealthRate += 0.2f;
        GameManager.Instance.AddBossSkillLevel();
        CancelInvoke();
    }
    private void DangerousMastery()
    {
        GameManager.Instance.Player().DangerousBulletCount -= 1;
        GameManager.Instance.Player().DangerousBulletBonus += 0.25f;
        GameManager.Instance.AddBossSkillLevel();
        CancelInvoke();
    }
    private void DangerAvoidance()
    {
        GameManager.Instance.Player().DangerousBulletBonus -= 0.25f;
        GameManager.Instance.Player().SpeedRate -= 0.1f;
        GameManager.Instance.Player().DodgeRate += 0.05f;
        GameManager.Instance.Boss().HealthRate += 0.1f;
        GameManager.Instance.Boss().DodgeRate += 0.02f;
        CancelInvoke();
    }
    private void DangerousNuclearBomb()
    {
        GameManager.Instance.Player().DangerousNuclearBomb = true;
        GameManager.Instance.Player().DangerousBulletBonus += 0.5f;
        GameManager.Instance.Boss().AttackRate -= 0.1f;
        CancelInvoke();
    }
    private void DangerAnger()
    {
        GameManager.Instance.Player().DangerAnger = true;
        GameManager.Instance.Player().DangerousBulletBonus += 0.5f;
        GameManager.Instance.Boss().AttackRate -= 0.1f;
        CancelInvoke();
    }
    private void AngerDisorder()
    {
        GameManager.Instance.Player().DangerousBulletBonus -= 0.25f;
        GameManager.Instance.Player().AngerValue += 0.75f;
        GameManager.Instance.Boss().AttackRate -= 0.05f;
        CancelInvoke();
    }
    private void DangerousShooting()
    {
        GameManager.Instance.Player().DangerousBulletBonus += 0.25f;
        GameManager.Instance.Player().AttackRate -= 0.1f;
        GameManager.Instance.Boss().AttackRate -= 0.1f;
        CancelInvoke();
    }
    private void DangerousPoison()
    {
        GameManager.Instance.Player().DangerousBulletBonus += 0.25f;
        GameManager.Instance.Player().PoizonDamage += 0.1f;
        GameManager.Instance.Boss().HealthRate += 0.1f;
        GameManager.Instance.AddBossSkillLevel();
        CancelInvoke();
    }
    private void UndergroundToxicGel()
    {
        GameManager.Instance.Player().UndergroundToxicGel = true;
        GameManager.Instance.Player().DangerousBulletBonus += 0.25f;
        GameManager.Instance.Player().PoizonTime += 0.5f;
        GameManager.Instance.Boss().AttackPower += 1;
        CancelInvoke();
    }
    private void BrutalLightningStrikes()
    {
        GameManager.Instance.Player().DangerousBulletBonus += 0.25f;
        GameManager.Instance.Player().ThunderBonus += 0.15f;
        GameManager.Instance.Boss().HealthRate += 0.1f;
        GameManager.Instance.AddBossSkillLevel();
        CancelInvoke();
    }
    private void TurnDangerThunder()
    {
        GameManager.Instance.Player().DangerousBulletBonus -= 0.5f;
        GameManager.Instance.Player().ThunderRate += 0.1f;
        GameManager.Instance.Boss().AttackRate -= 0.05f;
        CancelInvoke();
    }
    private void DangerousInjury()
    {
        GameManager.Instance.Player().DangerousBulletBonus += 0.25f;
        GameManager.Instance.Player().Vulnerability_AttackBonus += 0.02f;
        GameManager.Instance.AddBossSkillLevel();
        CancelInvoke();
    }
    private void ExtremelyDangerous()
    {
        GameManager.Instance.Player().DangerousBulletBonus -= 0.25f;
        GameManager.Instance.Player().MaxVulnerabilityRate += 0.1f;
        GameManager.Instance.Boss().AttackPower += 1;
        CancelInvoke();
    }
    private void ColdWaterBrainwash()
    {
        GameManager.Instance.Player().DangerousBulletBonus -= 0.25f;
        GameManager.Instance.Player().WaterElementBonus += 0.1f;
        GameManager.Instance.Boss().HealthRate += 0.1f;
        CancelInvoke();
    }
    private void FierceImmersionWater()
    {
        GameManager.Instance.Player().DangerousBulletBonus += 0.25f;
        GameManager.Instance.Player().EasyWaterTime += 0.5f;
        GameManager.Instance.Boss().HealthRate += 0.15f;
        CancelInvoke();
    }
    private void DangerousAttack()
    {
        GameManager.Instance.Player().AttackBonus += GameManager.Instance.Player().DangerousBulletBonus * 0.1f;
        GameManager.Instance.Player().AttackRate += GameManager.Instance.Player().DangerousBulletBonus * 0.05f;
        GameManager.Instance.Player().DangerousBulletBonus += 0.5f;
        GameManager.Instance.Boss().AttackRate -= 0.1f;
        CancelInvoke();
    }
    private void DangerousConfrontation()
    {
        GameManager.Instance.Player().AttackBonus += 0.5f;
        GameManager.Instance.Player().AttackRate -= 0.15f;
        GameManager.Instance.Player().DangerousBulletBonus += 1.5f;
        GameManager.Instance.Boss().AttackRate -= 0.5f;
        GameManager.Instance.Boss().AttackPower += 1;
        CancelInvoke();
    }
    private void BasicCombatGel()
    {
        GameManager.Instance.Player().AttackBonus += 0.1f;
        GameManager.Instance.Player().DangerousBulletBonus += 0.25f;
        GameManager.Instance.Boss().AttackRate -= 0.05f;
        CancelInvoke();
    }
    private void BattleMasteryGel()
    {
        GameManager.Instance.Player().AttackBonus += 0.1f;
        GameManager.Instance.Player().DangerousBulletCount -= 1;
        GameManager.Instance.AddBossSkillLevel();
        CancelInvoke();
    }
    private void DangerousTransaction()
    {
        GameManager.Instance.Player().DangerousBulletBonus -= 0.25f;
        GameManager.Instance.Player().CriticalDamageRate += 0.1f;
        GameManager.Instance.Boss().HealthRate += 0.1f;
        CancelInvoke();
    }
    private void Furious()
    {
        GameManager.Instance.Player().MaxHealth -= 1;
        GameManager.Instance.Player().NowHealth += 1;
        GameManager.Instance.Player().DangerousBulletBonus += 0.5f;
        GameManager.Instance.Player().AngerValue += 0.25f;
        GameManager.Instance.Boss().HealthRate += 0.1f;
        GameManager.Instance.Boss().AttackRate -= 0.1f;
        CancelInvoke();
    }
    private void DangerousLife()
    {
        GameManager.Instance.Player().DangerousLife = true;
        GameManager.Instance.Player().NowHealth += 2;
        GameManager.Instance.Boss().HealthRate += 0.15f;
        GameManager.Instance.AddBossSkillLevel();
        CancelInvoke();
    }
    private void DangerousNothingness()
    {
        GameManager.Instance.Player().DangerousBulletBonus += 0.25f;
        GameManager.Instance.Player().DodgeRate += 0.1f;
        GameManager.Instance.Boss().DodgeRate += 0.03f;
        CancelInvoke();
    }
    private void DangerousBloodthirsty()
    {
        GameManager.Instance.Player().DangerousBulletBonus += 0.5f;
        GameManager.Instance.Player().AutoHealCount += 1;
        GameManager.Instance.Boss().HealthRate += 0.1f;
        GameManager.Instance.Boss().AutoHealCount += 0.5f;
        CancelInvoke();
    }
}
