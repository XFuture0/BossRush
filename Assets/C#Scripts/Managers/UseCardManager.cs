using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class UseCardManager : SingleTons<UseCardManager>
{
    public void StartInvoke(string CardName)
    {
        Invoke(CardName, 0);
    }
    //³¡¾°1
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
        GameManager.Instance.Player().SpeedRate -= 0.1f;
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
    //³¡¾°2
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
        GameManager.Instance.Player().SacredAnger = true;
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
}
