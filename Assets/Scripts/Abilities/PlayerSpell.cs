using System;
using UnityEngine;

public abstract class PlayerSpell : ITurnable
{
    public string SpriteReference, Name, Text;
    public int Cost, Cooldown, Ammo, CooldownCounter;
    public bool HasCost, HasCooldown, HasAmmo;
    public Type TargetType;
    public Player Player;
    public abstract void TurnEnd();
    public void TurnStart(){}
    public abstract void Cast();

    public bool IsDisabled(){
        if(HasCost){
            if(Player == null) return true;
            if(Player.Mana < Cost) return true;
        }
        if(HasCooldown){
            if(CooldownCounter != 0) return true;
        }
        if(HasAmmo){
            if(Ammo <= 0) return true;
        }

        return false;
    }

    public static PlayerSpell ConstructSpell(string input){
        switch(input){
            case "Heal":
            return new SpellHeal();
            default:
            return null;
        }
    }
}

public class SpellHeal : PlayerSpell{

    public SpellHeal(){
        SpriteReference = "PS_Heal";
        Name = "Heal";
        Text = "Heal any unit for 2 HP.";
        Ammo = 5;
        Cooldown = 1;
        CooldownCounter = 0;
        HasAmmo = HasCooldown = true;
        TargetType = Type.GetType("Unit");
    }

    public override void TurnEnd(){
        CooldownCounter --;
        if(CooldownCounter < 0) CooldownCounter = 0;
    }

    public override void Cast(){
        if(IsDisabled()) return;
        Debug.Log("Lol, no clue how I'm gonna implement selecting and stuff yet.");
        Ammo --;
        CooldownCounter = Cooldown;
    }
}