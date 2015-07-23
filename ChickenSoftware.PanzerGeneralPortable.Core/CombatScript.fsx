
#load "TypesScript.fsx"

open TypesScript

weatherZones |> Seq.length

//CalculateBattle
//CalculateSurprise
//DetermineSupportingVolleyUnit

//CalculateSupportBattle
//DetermineSupportingVolleyTile
//DetermineAttackPoints
//CalculateVolly
//CalculateInitative
//DetermineSupportingBattleResult

//CalculateMainBattle
//CalculateInitative
//DetermineAttackPoints
//CalculateVolly

//DetermineMainBattleResult


//supportingUnitVolly
//Vollies

//Volly
//AttackerGrade
//DefenderGrade
//NumberOfVollies: Attack - Defense

//DetemineVollyResults
//DetermineStandardEquipmentVolleyEffect (add experience and ddjustHitPoints
//DetermineSpecialEquipmentVolleyEffect

//A volly is done for each of the attacker's volly points


let defenderRetreats(protectorBaseStrength, protectorStrength) =
    if (protectorStrength * 2) > protectorBaseStrength then
        false
    else
        true

defenderRetreats(10,6)
defenderRetreats(10,5)


let determineBattleResult(aggressorStrength, protectorBaseStrength, protectorStrength) =
    match aggressorStrength <= 0, protectorStrength <= 0 with
    | true, true ->  AggressorDestroyed_ProtectorDestroyed
    | true, false -> AggressorDestroyed_ProtectorHolds
    | false, true -> AggressorAdvances_ProtectorDestroyed
    | false, false -> 
        match defenderRetreats(protectorBaseStrength,protectorStrength) with
        | true -> AggressorAdvances_ProtectorRetreats
        | false -> AggressorHolds_ProtectorHolds


determineBattleResult(0, 10, 0)
determineBattleResult(1, 10, 0)
determineBattleResult(0, 10, 1)
determineBattleResult(1, 10, 1)
determineBattleResult(1, 10, 9)



