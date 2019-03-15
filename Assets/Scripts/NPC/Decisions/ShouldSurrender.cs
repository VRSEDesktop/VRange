using Assets.Scripts.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShouldSurrender : Decision
{
    /// <summary>
    /// Has the player drawn their weapon and has the NPC made a decision.
    /// </summary>
    private DecisionStatus WeaponAimed;
    /// <summary>
    /// Has the player used their weapon and has the NPC made a decision.
    /// </summary>
    private DecisionStatus WeaponUsed;
    /// <summary>
    /// Is the NPC injured and have they made a decision.
    /// </summary>
    private DecisionStatus NPCInjured;

    public override bool Decide(NPCController owner)
    {
        if (IsIntimidatedByWeapon(owner))
            return true;
        if (IsIntimidatedByWeaponUsage(owner))
            return true;
        if (IsIntimidatedByInjury(owner))
            return true;

        return false;

    }

    /// <summary>
    /// Is the NPC intimidated by the weapon being aimed at them.
    /// </summary>
    private bool IsIntimidatedByWeapon(NPCController owner)
    {
        switch(WeaponAimed)
        {
            case DecisionStatus.DidNotDecide:
                CheckWeaponDrawn(owner);
                return false;
            case DecisionStatus.ShouldDecide:
                WeaponAimed = DecisionStatus.DidDecide;
                return MakeRandomDecision(100 - owner.LevelOfAgression);
            case DecisionStatus.DidDecide:
                return false;
            default:
                return false;
        }
    }

    /// <summary>
    /// Is the NPC intimidated by the weapon being used.
    /// </summary>
    private bool IsIntimidatedByWeaponUsage(NPCController owner)
    {
        switch (WeaponUsed)
        {
            case DecisionStatus.DidNotDecide:
                CheckWeaponUsed(owner);
                return false;
            case DecisionStatus.ShouldDecide:
                WeaponUsed = DecisionStatus.DidDecide;
                return MakeRandomDecision(125 - owner.LevelOfAgression);
            case DecisionStatus.DidDecide:
                return false;
            default:
                return false;
        }
    }

    /// <summary>
    /// Is the NPC intimidated after being injured.
    /// </summary>
    private bool IsIntimidatedByInjury(NPCController owner)
    {
        switch (NPCInjured)
        {
            case DecisionStatus.DidNotDecide:
                CheckNPCInjured(owner);
                return false;
            case DecisionStatus.ShouldDecide:
                NPCInjured = DecisionStatus.DidDecide;
                return MakeRandomDecision(150 - owner.LevelOfAgression);
            case DecisionStatus.DidDecide:
                return false;
            default:
                return false;
        }
    }

    /// <summary>
    /// Checks whether the NPC is being aimed at and changes the decision status.
    /// </summary>
    private void CheckWeaponDrawn(NPCController owner)
    {
        if (true)
            WeaponAimed = DecisionStatus.ShouldDecide;
    }

    /// <summary>
    /// Checks whether the player has threatened with agression and changes the decision status.
    /// </summary>
    private void CheckWeaponUsed(NPCController owner)
    {
        if (true)
            WeaponUsed = DecisionStatus.ShouldDecide;
    }

    /// <summary>
    /// Checks whether the NPC is injured changes the decision status.
    /// </summary>
    private void CheckNPCInjured(NPCController owner)
    {
        if (true)
            NPCInjured = DecisionStatus.ShouldDecide;
    }

    /// <summary>
    /// Calculates a random chance based on a given percentage.
    /// </summary>
    private bool MakeRandomDecision(float chancePercentage)
    {
        if (chancePercentage > 100)
            chancePercentage = 100;
        if (chancePercentage <= 0)
            return false;
        return Random.Range(1, 100 / chancePercentage) == 1;
    }
}
