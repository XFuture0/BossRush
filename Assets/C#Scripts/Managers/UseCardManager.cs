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
    private void HighSpirited()
    {
        GameManager.Instance.Player().AttackBonus += 0.2f;
        GameManager.Instance.Player().CriticalDamageRate += 0.05f;
        GameManager.Instance.Player().SpeedRate += 0.05f;
        GameManager.Instance.Boss().AttackRate -= 0.1f;
        GameManager.Instance.Boss().HealthRate += 0.1f;
        CancelInvoke();
    }
    private void PrideVolition()
    {
        GameManager.Instance.Player().AttackBonus += 0.2f;
        GameManager.Instance.Player().CriticalDamageRate += 0.05f;
        GameManager.Instance.Player().AttackRate -= 0.05f;
        GameManager.Instance.Boss().AttackPower += 1;
        GameManager.Instance.Boss().AttackRate -= 0.1f;
        CancelInvoke();
    }
    private void StandBy()
    {
        GameManager.Instance.Player().MaxHealth += 3;
        GameManager.Instance.Player().NowHealth += 3;
        GameManager.Instance.Boss().HealthRate += 0.1f; 
        CancelInvoke();
    }
    private void FearlessCore()
    {
        GameManager.Instance.Player().MaxHealth += 3;
        GameManager.Instance.Player().NowHealth += 3;
        GameManager.Instance.Player().AttackBonus += 0.1f;
        GameManager.Instance.Player().DodgeRate += 0.05f;
        GameManager.Instance.Player().CriticalDamageRate += 0.05f;
        GameManager.Instance.Boss().AttackPower += 1;
        CancelInvoke();
    }
    private void BigHeart()
    {
        GameManager.Instance.Player().MaxHealth += 5;
        GameManager.Instance.Player().NowHealth += 5;
        GameManager.Instance.Player().AutoHealCount += 1;
        GameManager.Instance.Boss().HealthRate += 0.1f;
        GameManager.Instance.Boss().AutoHealCount += 0.5f;
        CancelInvoke();
    }
    private void EvolutionTime()
    {
        GameManager.Instance.Player().AttackBonus += 0.2f;
        GameManager.Instance.Player().WeaponCount += 1;
        GameManager.Instance.AddBossSkillLevel();
        CancelInvoke();
    }
    private void Truce()
    {
        GameManager.Instance.Player().NowHealth += (int)GameManager.Instance.Player().MaxHealth * 0.5f;
        GameManager.Instance.Boss().HealthRate += 0.2f;
        CancelInvoke();
    }
    private void CombatManiac()
    {
        GameManager.Instance.Player().AttackBonus += 0.2f;
        GameManager.Instance.Player().WeaponCount += 1;
        GameManager.Instance.Player().CriticalDamageRate += 0.05f;
        GameManager.Instance.Player().CriticalDamageBonus += 0.2f;
        GameManager.Instance.Boss().AttackPower += 1;
        GameManager.Instance.Boss().AttackRate -= 0.1f;
        CancelInvoke();
    }
    private void TenaciousWill()
    {
        GameManager.Instance.Player().MaxHealth += 2;
        GameManager.Instance.Player().NowHealth += 2;
        GameManager.Instance.Player().AutoHealCount += 1;
        GameManager.Instance.Player().CriticalDamageRate -= 0.05f;
        GameManager.Instance.Boss().HealthRate += 0.1f;
        GameManager.Instance.Boss().AutoHealCount += 0.5f;
        CancelInvoke();
    }
    private void EmergencyEvacuation()
    {
        GameManager.Instance.Player().NowHealth -= 2;
        GameManager.Instance.Player().MaxHealth -= 2;
        GameManager.Instance.Player().AttackBonus -= 0.3f;
        GameManager.Instance.Player().DodgeRate += 0.15f;
        GameManager.Instance.Player().AutoHealCount += 1;
        GameManager.Instance.Boss().HealthRate -= 0.2f;
        GameManager.Instance.Boss().AttackRate += 0.15f;
        CancelInvoke();
    }
    private void RhythmJitter()
    {
        GameManager.Instance.Player().SpeedRate += 0.05f;
        GameManager.Instance.Player().AttackRate -= 0.05f;
        GameManager.Instance.Player().DodgeRate += 0.05f;
        GameManager.Instance.Boss().AttackRate -= 0.05f;
        GameManager.Instance.Boss().DodgeRate += 0.02f;
        CancelInvoke();
    }
    private void HeroEmblems()
    {
        GameManager.Instance.Player().MaxHealth += 3;
        GameManager.Instance.Player().NowHealth += 3;
        GameManager.Instance.Player().AttackBonus += 0.2f;
        GameManager.Instance.Player().AttackRate -= 0.05f;
        GameManager.Instance.Player().CriticalDamageRate += 0.05f;
        GameManager.Instance.Player().CriticalDamageBonus += 0.25f;
        GameManager.Instance.Player().DodgeRate += 0.05f;
        GameManager.Instance.Boss().AttackPower += 1;
        GameManager.Instance.Boss().AttackRate -= 0.1f;
        GameManager.Instance.Boss().AutoHealCount += 0.5f;
        CancelInvoke();
    }
    private void LeadCharge()
    {
        GameManager.Instance.Player().AttackBonus += 0.05f;
        GameManager.Instance.Player().SpeedRate += 0.05f;
        GameManager.Instance.Player().CriticalDamageBonus += 0.1f;
        GameManager.Instance.Boss().AttackRate -= 0.05f;
        GameManager.Instance.AddBossSkillLevel();
        CancelInvoke();
    }
    private void GiantFear()
    {
        GameManager.Instance.Player().MaxHealth += 4;
        GameManager.Instance.Player().NowHealth += 4;
        GameManager.Instance.Player().AttackBonus -= 0.1f;
        GameManager.Instance.Player().AutoHealCount += 1;
        GameManager.Instance.Player().SpeedRate += 0.05f;
        GameManager.Instance.Boss().HealthRate += 0.2f;
        GameManager.Instance.Boss().AttackPower += 1;
        CancelInvoke();
    }
    private void BattlePassion()
    {
        GameManager.Instance.Player().AttackBonus += 0.2f;
        GameManager.Instance.Player().CriticalDamageRate += 0.05f;
        GameManager.Instance.Player().CriticalDamageBonus += 0.2f;
        GameManager.Instance.Boss().AttackPower += 1;
        GameManager.Instance.Boss().AutoHealCount += 0.5f;
        CancelInvoke();
    }
    private void FlexibleVersatile()
    {
        GameManager.Instance.Player().SpeedRate += 0.05f;
        GameManager.Instance.Player().DodgeRate += 0.05f;
        GameManager.Instance.Boss().DodgeRate += 0.02f;
        GameManager.Instance.Boss().AttackRate -= 0.05f;
        CancelInvoke();
    }
    private void Raid()
    {
        GameManager.Instance.Player().AttackBonus += 0.1f;
        GameManager.Instance.Boss().HealthRate += 0.1f;
        CancelInvoke();
    }
    private void HotBloodedHeart()
    {
        GameManager.Instance.Player().MaxHealth += 5;
        GameManager.Instance.Player().NowHealth += 5;
        GameManager.Instance.Player().AutoHealCount += 2;
        GameManager.Instance.Player().AttackBonus += 0.2f;
        GameManager.Instance.Player().CriticalDamageRate += 0.05f;
        GameManager.Instance.Player().CriticalDamageBonus += 0.2f;
        GameManager.Instance.Boss().HealthRate += 0.1f;
        GameManager.Instance.Boss().AttackPower += 1;
        GameManager.Instance.Boss().AutoHealCount += 0.5f;
        GameManager.Instance.Boss().AttackRate -= 0.1f;
        CancelInvoke();
    }
    //³¡¾°2
    private void WideAwake()
    {
        GameManager.Instance.Player().MaxHealth += 1;
        GameManager.Instance.Player().NowHealth += 1;
        GameManager.Instance.Player().DodgeRate += 0.05f;
        GameManager.Instance.Boss().HealthRate += 0.05f;
        GameManager.Instance.Boss().DodgeRate += 0.02f;
        CancelInvoke();
    }
    private void EagleEyeVision()
    {
        GameManager.Instance.Player().CriticalDamageRate += 0.05f;
        GameManager.Instance.Player().DodgeRate += 0.05f;
        GameManager.Instance.Boss().DodgeRate += 0.02f;
        GameManager.Instance.Boss().AttackRate -= 0.05f;
        CancelInvoke();
    }
    private void BraveHeart()
    {
        GameManager.Instance.Player().MaxHealth += 1;
        GameManager.Instance.Player().NowHealth += 1;
        GameManager.Instance.Player().AttackBonus += 0.1f;
        GameManager.Instance.Player().CriticalDamageRate += 0.05f;
        GameManager.Instance.Player().CriticalDamageBonus += 0.1f;
        GameManager.Instance.Player().SpeedRate += 0.05f;
        GameManager.Instance.Boss().AttackPower += 1;
        GameManager.Instance.Boss().AutoHealCount += 0.5f;
        GameManager.Instance.Boss().AttackRate -= 0.05f;
        CancelInvoke();
    }
    private void GiantConfrontation()
    {
        GameManager.Instance.Player().AttackBonus += 0.2f;
        GameManager.Instance.Player().DodgeRate -= 0.05f;
        GameManager.Instance.Player().AttackRate -= 0.05f;
        GameManager.Instance.Player().CriticalDamageBonus += 0.2f;
        GameManager.Instance.Boss().HealthRate += 0.2f;
        GameManager.Instance.Boss().AttackPower += 1;
        GameManager.Instance.Boss().AttackRate -= 0.05f;
        CancelInvoke();
    }
    private void ActionEmblem()
    {
        GameManager.Instance.Player().MaxHealth += 2;
        GameManager.Instance.Player().NowHealth += 2;
        GameManager.Instance.Player().WeaponCount += 1;
        GameManager.Instance.Player().AttackBonus += 0.25f;
        GameManager.Instance.Player().CriticalDamageRate += 0.1f;
        GameManager.Instance.Player().CriticalDamageBonus += 0.1f;
        GameManager.Instance.Player().SpeedRate += 0.05f;
        GameManager.Instance.Player().AttackRate -= 0.1f;
        GameManager.Instance.Player().AutoHealCount += 1;
        GameManager.Instance.Boss().AttackPower += 1;
        GameManager.Instance.Boss().HealthRate += 0.1f;
        GameManager.Instance.Boss().AttackRate -= 0.1f;
        GameManager.Instance.Boss().DodgeRate += 0.05f;
        GameManager.Instance.AddBossSkillLevel();
        CancelInvoke();
    }
    private void EerieWindForest()
    {
        GameManager.Instance.Player().SpeedRate += 0.1f;
        GameManager.Instance.Player().CriticalDamageRate += 0.05f;
        GameManager.Instance.Player().AttackRate -= 0.1f;
        GameManager.Instance.Player().AutoHealCount -= 1;
        GameManager.Instance.Boss().AttackRate -= 0.05f;
        GameManager.Instance.AddBossSkillLevel();
        CancelInvoke();
    }
    private void NowhereHide()
    {
        GameManager.Instance.Player().NowHealth -= 1;
        GameManager.Instance.Player().MaxHealth -= 1;
        GameManager.Instance.Player().AttackBonus += 0.2f;
        GameManager.Instance.Player().CriticalDamageRate += 0.05f;
        GameManager.Instance.Player().CriticalDamageBonus += 0.2f;
        GameManager.Instance.Player().DodgeRate -= 0.05f;
        GameManager.Instance.Boss().AttackPower += 1;
        GameManager.Instance.Boss().AttackRate -= 0.1f;
        CancelInvoke();
    }
    private void RelicTreasure()
    {
        GameManager.Instance.Player().MaxHealth += 3;
        GameManager.Instance.Player().NowHealth += 3;
        GameManager.Instance.Player().AttackBonus += 0.15f;
        GameManager.Instance.Player().WeaponCount += 1;
        GameManager.Instance.Player().AttackRate -= 0.1f;
        GameManager.Instance.Player().CriticalDamageBonus += 0.2f;
        GameManager.Instance.Boss().HealthRate += 0.15f;
        GameManager.Instance.Boss().AttackPower += 1;
        GameManager.Instance.Boss().AutoHealCount += 0.5f;
        CancelInvoke();
    }
    private void EmotionalShock()
    {
        GameManager.Instance.Player().MaxHealth += 3;
        GameManager.Instance.Player().NowHealth += 3;
        GameManager.Instance.Player().AttackBonus -= 0.1f;
        GameManager.Instance.Player().AttackRate -= 0.1f;
        GameManager.Instance.Player().CriticalDamageRate -= 0.05f;
        GameManager.Instance.Player().AutoHealCount += 1;
        GameManager.Instance.Boss().HealthRate += 0.15f;
        GameManager.Instance.Boss().AttackRate += 0.05f;
        GameManager.Instance.AddBossSkillLevel();
        CancelInvoke();
    }
    private void ArmedAlert()
    {
        GameManager.Instance.Player().AttackBonus += 0.2f;
        GameManager.Instance.Player().CriticalDamageRate += 0.05f;
        GameManager.Instance.Player().CriticalDamageBonus += 0.2f;
        GameManager.Instance.Player().DodgeRate += 0.05f;
        GameManager.Instance.Boss().AttackPower += 1;
        GameManager.Instance.Boss().AttackRate -= 0.05f;
        GameManager.Instance.AddBossSkillLevel();
        CancelInvoke();
    }
    private void ForestHoly()
    {
        GameManager.Instance.Player().NowHealth += (int)GameManager.Instance.Player().MaxHealth * 0.5f;
        GameManager.Instance.Boss().HealthRate += 0.1f;
        CancelInvoke();
    }
    private void DejaVu()
    {
        GameManager.Instance.Player().MaxHealth += 1;
        GameManager.Instance.Player().NowHealth += 1;
        GameManager.Instance.Player().AttackBonus -= 0.1f;
        GameManager.Instance.Player().DodgeRate -= 0.05f;
        GameManager.Instance.Boss().HealthRate -= 0.05f;
        GameManager.Instance.Boss().AttackPower -= 1;
        GameManager.Instance.Boss().AttackRate += 0.05f;
        CancelInvoke();
    }
    private void EqualDifferenceTrading()
    {
        GameManager.Instance.Player().WeaponCount += 1;
        GameManager.Instance.Player().NowHealth -= 1;
        GameManager.Instance.Boss().HealthRate += 0.1f;
        GameManager.Instance.Boss().AttackPower += 1;
        CancelInvoke();
    }
    private void Cautious()
    {
        GameManager.Instance.Player().MaxHealth += 2;
        GameManager.Instance.Player().NowHealth += 2;
        GameManager.Instance.Player().AttackBonus += 0.05f;
        GameManager.Instance.Player().DodgeRate += 0.05f;
        GameManager.Instance.Boss().HealthRate += 0.1f;
        GameManager.Instance.Boss().DodgeRate += 0.02f;
        CancelInvoke();
    }
    private void PeaceReconciliation()
    {
        GameManager.Instance.Player().NowHealth += (int)GameManager.Instance.Player().MaxHealth * 0.3f;
        GameManager.Instance.Player().AttackBonus -= 0.1f;
        GameManager.Instance.Player().AttackRate -= 0.05f;
        GameManager.Instance.Player().AutoHealCount += 1;
        GameManager.Instance.Boss().HealthRate += 0.1f;
        GameManager.Instance.Boss().AttackPower -= 1;
        GameManager.Instance.Boss().AttackRate += 0.05f;
        GameManager.Instance.Boss().AutoHealCount += 0.5f;
        CancelInvoke();
    }
    private void MonsterHeart()
    {
        GameManager.Instance.Player().MaxHealth -= (int)GameManager.Instance.Player().MaxHealth * 0.5f;
        GameManager.Instance.Player().AttackBonus += 0.5f;
        GameManager.Instance.Player().CriticalDamageRate += 0.2f;
        GameManager.Instance.Player().CriticalDamageBonus += 0.25f;
        GameManager.Instance.Player().AttackRate -= 0.15f;
        GameManager.Instance.Player().AutoHealCount += 1;
        GameManager.Instance.Boss().AttackPower += 2;
        GameManager.Instance.Boss().AttackRate -= 0.2f;
        GameManager.Instance.Boss().AutoHealCount += 1;
        CancelInvoke();
    }
    private void ThirdHand()
    {
        GameManager.Instance.Player().WeaponCount += 1;
        GameManager.Instance.Player().AttackBonus += 0.1f;
        GameManager.Instance.Boss().AttackPower += 1;
        GameManager.Instance.Boss().AttackRate -= 0.1f;
        CancelInvoke();
    }
    private void HesitationDuel()
    {
        GameManager.Instance.Player().MaxHealth += 3;
        GameManager.Instance.Player().NowHealth += 3;
        GameManager.Instance.Player().AttackBonus -= 0.1f;
        GameManager.Instance.Player().CriticalDamageRate -= 0.05f;
        GameManager.Instance.Player().CriticalDamageBonus -= 0.15f;
        GameManager.Instance.Boss().AttackPower -= 1;
        GameManager.Instance.Boss().AttackRate += 0.05f;
        GameManager.Instance.Boss().AutoHealCount -= 0.5f;
        CancelInvoke();
    }
    private void SurpriseAttack()
    {
        GameManager.Instance.Player().AttackBonus += 0.1f;
        GameManager.Instance.Player().AttackRate -= 0.05f;
        GameManager.Instance.Player().SpeedRate += 0.05f;
        GameManager.Instance.Boss().AttackRate -= 0.1f;
        GameManager.Instance.Boss().AutoHealCount += 0.5f;
        GameManager.Instance.AddBossSkillLevel();
        CancelInvoke();
    }
    private void PeaceAgreement()
    {
        GameManager.Instance.Player().WeaponCount -= 1;
        GameManager.Instance.Player().AutoHealCount += 1;
        GameManager.Instance.Boss().AttackPower -= 2;
        GameManager.Instance.Boss().AttackRate += 0.1f;
        CancelInvoke();
    }
    private void EqualExchange()
    {
        GameManager.Instance.Player().AttackBonus += 0.1f;
        GameManager.Instance.Player().AttackRate -= 0.05f;
        GameManager.Instance.Boss().AttackPower += 1;
        GameManager.Instance.Boss().AttackRate -= 0.05f;
        CancelInvoke();
    }
    private void PeekingWood()
    {
        GameManager.Instance.Player().MaxHealth += 2;
        GameManager.Instance.Player().NowHealth += 2;
        GameManager.Instance.Player().AttackRate -= 0.05f;
        GameManager.Instance.Player().SpeedRate += 0.05f;
        GameManager.Instance.Boss().HealthRate += 0.1f;
        GameManager.Instance.Boss().AttackRate -= 0.1f;
        CancelInvoke();
    }
    private void AdhereWill()
    {
        GameManager.Instance.Player().MaxHealth += 3;
        GameManager.Instance.Player().NowHealth += 3;
        GameManager.Instance.Player().AttackBonus += 0.2f;
        GameManager.Instance.Player().CriticalDamageRate += 0.05f;
        GameManager.Instance.Player().CriticalDamageBonus += 0.2f;
        GameManager.Instance.Player().DodgeRate += 0.05f;
        GameManager.Instance.Player().AutoHealCount += 1;
        GameManager.Instance.Boss().HealthRate += 0.25f;
        GameManager.Instance.Boss().AttackPower += 1;
        GameManager.Instance.Boss().DodgeRate += 0.05f;
        GameManager.Instance.Boss().AutoHealCount += 0.5f;
        GameManager.Instance.AddBossSkillLevel();
        CancelInvoke();
    }
    private void GoguryeoFan()
    {
        GameManager.Instance.Player().AttackBonus += 0.2f;
        GameManager.Instance.Player().CriticalDamageRate += 0.05f;
        GameManager.Instance.Boss().HealthRate += 0.1f;
        GameManager.Instance.Boss().AttackPower += 1;
        CancelInvoke();
    }
}
