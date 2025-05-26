using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterItem : MonoBehaviour
{
    public string CharacterName;
    public Text CharacterText;
    private float count;
    public void SetPlayerCharacter()
    {
        switch (CharacterName)
        {
            case "SpeedRate":
                count = (GameManager.Instance.Player().SpeedRate - 1);
                if(count > 0)
                {
                    CharacterText.color = Color.green;
                    CharacterText.text = "+" + count.ToString();
                }
                else if(count == 0)
                {
                    CharacterText.text = count.ToString();
                }
                else if(count < 0)
                {
                    CharacterText.color = Color.red;
                    CharacterText.text = count.ToString();
                }
                break;
            case "AttackBonus":
                count = (GameManager.Instance.Player().AttackBonus - 1);
                if (count > 0)
                {
                    CharacterText.color = Color.green;
                    CharacterText.text = "+" + count.ToString();
                }
                else if (count == 0)
                {
                    CharacterText.text = count.ToString();
                }
                else if (count < 0)
                {
                    CharacterText.color = Color.red;
                    CharacterText.text = count.ToString();
                }
                break;
            case "AutoHealth":
                count = GameManager.Instance.Player().AutoHealCount;
                if (count > 0)
                {
                    CharacterText.color = Color.green;
                    CharacterText.text = "+" + count.ToString();
                }
                else if (count == 0)
                {
                    CharacterText.text = count.ToString();
                }
                else if (count < 0)
                {
                    CharacterText.color = Color.red;
                    CharacterText.text = count.ToString();
                }
                break;
            case "CriticalRate":
                count = GameManager.Instance.Player().CriticalDamageRate;
                if (count > 0)
                {
                    CharacterText.color = Color.green;
                    CharacterText.text = "+" + count.ToString();
                }
                else if (count == 0)
                {
                    CharacterText.text = count.ToString();
                }
                else if (count < 0)
                {
                    CharacterText.color = Color.red;
                    CharacterText.text = count.ToString();
                }
                break;
            case "CriticalBonus":
                count = GameManager.Instance.Player().CriticalDamageBonus;
                if (count > 0)
                {
                    CharacterText.color = Color.green;
                    CharacterText.text = "+" + count.ToString();
                }
                else if (count == 0)
                {
                    CharacterText.text = count.ToString();
                }
                else if (count < 0)
                {
                    CharacterText.color = Color.red;
                    CharacterText.text = count.ToString();
                }
                break;
            case "DodgeRate":
                count = GameManager.Instance.Player().DodgeRate;
                if (count > 0)
                {
                    CharacterText.color = Color.green;
                    CharacterText.text = "+" + count.ToString();
                }
                else if (count == 0)
                {
                    CharacterText.text = count.ToString();
                }
                else if (count < 0)
                {
                    CharacterText.color = Color.red;
                    CharacterText.text = count.ToString();
                }
                break;
            case "AttackRate":
                count = 1 - GameManager.Instance.Player().AttackRate;
                if (count > 0)
                {
                    CharacterText.color = Color.green;
                    CharacterText.text = "+" + count.ToString();
                }
                else if (count == 0)
                {
                    CharacterText.text = count.ToString();
                }
                else if (count < 0)
                {
                    CharacterText.color = Color.red;
                    CharacterText.text = count.ToString();
                }
                break;
            case "PoizonDamage":
                count = GameManager.Instance.Player().PoizonDamage;
                if (count > 0)
                {
                    CharacterText.color = Color.green;
                    CharacterText.text = "+" + count.ToString();
                }
                else if (count == 0)
                {
                    CharacterText.text = count.ToString();
                }
                else if (count < 0)
                {
                    CharacterText.color = Color.red;
                    CharacterText.text = count.ToString();
                }
                break;
            case "ThunderBonus":
                count = GameManager.Instance.Player().ThunderBonus - 1;
                if (count > 0)
                {
                    CharacterText.color = Color.green;
                    CharacterText.text = "+" + count.ToString();
                }
                else if (count == 0)
                {
                    CharacterText.text = count.ToString();
                }
                else if (count < 0)
                {
                    CharacterText.color = Color.red;
                    CharacterText.text = count.ToString();
                }
                break;
            case "ThunderRate":
                count = GameManager.Instance.Player().ThunderRate;
                if (count > 0)
                {
                    CharacterText.color = Color.green;
                    CharacterText.text = "+" + count.ToString();
                }
                else if (count == 0)
                {
                    CharacterText.text = count.ToString();
                }
                else if (count < 0)
                {
                    CharacterText.color = Color.red;
                    CharacterText.text = count.ToString();
                }
                break;
            case "Vulnerability_AttackBonus":
                count = GameManager.Instance.Player().Vulnerability_AttackBonus;
                if (count > 0)
                {
                    CharacterText.color = Color.green;
                    CharacterText.text = "+" + count.ToString();
                }
                else if (count == 0)
                {
                    CharacterText.text = count.ToString();
                }
                else if (count < 0)
                {
                    CharacterText.color = Color.red;
                    CharacterText.text = count.ToString();
                }
                break;
            case "Vulnerability_CriticalRate":
                count = GameManager.Instance.Player().Vulnerability_CriticalRate;
                if (count > 0)
                {
                    CharacterText.color = Color.green;
                    CharacterText.text = "+" + count.ToString();
                }
                else if (count == 0)
                {
                    CharacterText.text = count.ToString();
                }
                else if (count < 0)
                {
                    CharacterText.color = Color.red;
                    CharacterText.text = count.ToString();
                }
                break;
            case "WaterElementBonus":
                count = GameManager.Instance.Player().WaterElementBonus;
                if (count > 0)
                {
                    CharacterText.color = Color.green;
                    CharacterText.text = "+" + count.ToString();
                }
                else if (count == 0)
                {
                    CharacterText.text = count.ToString();
                }
                else if (count < 0)
                {
                    CharacterText.color = Color.red;
                    CharacterText.text = count.ToString();
                }
                break;
            case "DangerousBulletBonus":
                count = GameManager.Instance.Player().DangerousBulletBonus - 2;
                if (count > 0)
                {
                    CharacterText.color = Color.green;
                    CharacterText.text = "+" + count.ToString();
                }
                else if (count == 0)
                {
                    CharacterText.text = count.ToString();
                }
                else if (count < 0)
                {
                    CharacterText.color = Color.red;
                    CharacterText.text = count.ToString();
                }
                break;

        }
    }
    public void SetBossCharacter()
    {
        switch (CharacterName)
        {
            case "AttackPower":
                count = GameManager.Instance.Boss().AttackPower;
                if (count > 1)
                {
                    CharacterText.color = Color.green;
                    CharacterText.text = "+" + count.ToString();
                }
                else if (count == 1)
                {
                    CharacterText.text = count.ToString();
                }
                else if (count < 1)
                {
                    CharacterText.color = Color.red;
                    CharacterText.text = count.ToString();
                }
                break;
            case "AutoHealth":
                count = GameManager.Instance.Boss().AutoHealCount;
                if (count > 0)
                {
                    CharacterText.color = Color.green;
                    CharacterText.text = "+" + count.ToString();
                }
                else if (count == 0)
                {
                    CharacterText.text = count.ToString();
                }
                else if (count < 0)
                {
                    CharacterText.color = Color.red;
                    CharacterText.text = count.ToString();
                }
                break;
            case "DodgeRate":
                count = GameManager.Instance.Boss().DodgeRate;
                if (count > 0)
                {
                    CharacterText.color = Color.green;
                    CharacterText.text = "+" + count.ToString();
                }
                else if (count == 0)
                {
                    CharacterText.text = count.ToString();
                }
                else if (count < 0)
                {
                    CharacterText.color = Color.red;
                    CharacterText.text = count.ToString();
                }
                break;
            case "AttackRate":
                count = 1 - GameManager.Instance.Boss().AttackRate;
                if (count > 0)
                {
                    CharacterText.color = Color.green;
                    CharacterText.text = "+" + count.ToString();
                }
                else if (count == 0)
                {
                    CharacterText.text = count.ToString();
                }
                else if (count < 0)
                {
                    CharacterText.color = Color.red;
                    CharacterText.text = count.ToString();
                }
                break;
            case "ShootBall":
                foreach (var skill in GameManager.Instance.BossSkillList.BossSkills)
                {
                    if(skill.SkillName == CharacterName)
                    {
                        CharacterText.color = Color.yellow;
                        CharacterText.text = SetLevel(skill.SkillLevel);
                    }
                }
                break;
            case "GroundFissue":
                foreach (var skill in GameManager.Instance.BossSkillList.BossSkills)
                {
                    if (skill.SkillName == CharacterName)
                    {
                        CharacterText.color = Color.yellow;
                        CharacterText.text = SetLevel(skill.SkillLevel);
                    }
                }
                break;
            case "Collide":
                foreach (var skill in GameManager.Instance.BossSkillList.BossSkills)
                {
                    if (skill.SkillName == CharacterName)
                    {
                        CharacterText.color = Color.yellow;
                        CharacterText.text = SetLevel(skill.SkillLevel);
                    }
                }
                break;
            case "SummonArmy":
                foreach (var skill in GameManager.Instance.BossSkillList.BossSkills)
                {
                    if (skill.SkillName == CharacterName)
                    {
                        CharacterText.color = Color.yellow;
                        CharacterText.text = SetLevel(skill.SkillLevel);
                    }
                }
                break;
            case "Laser":
                foreach (var skill in GameManager.Instance.BossSkillList.BossSkills)
                {
                    if (skill.SkillName == CharacterName)
                    {
                        CharacterText.color = Color.yellow;
                        CharacterText.text = SetLevel(skill.SkillLevel);
                    }
                }
                break;
            case "Trident":
                foreach (var skill in GameManager.Instance.BossSkillList.BossSkills)
                {
                    if (skill.SkillName == CharacterName)
                    {
                        CharacterText.color = Color.yellow;
                        CharacterText.text = SetLevel(skill.SkillLevel);
                    }
                }
                break;

        }
    }
    private string SetLevel(int level)
    {
        string leveltype = "-";
        switch (level)
        {
            case 0:
                leveltype = "-";
                break;
            case 1:
                leveltype = "I";
                break;
            case 2:
                leveltype = "II";
                break;
            case 3:
                leveltype = "III";
                break;
            case 4:
                leveltype = "IV";
                break;
            case 5:
                leveltype = "V";
                break;
        }
        return leveltype;
    }
}
