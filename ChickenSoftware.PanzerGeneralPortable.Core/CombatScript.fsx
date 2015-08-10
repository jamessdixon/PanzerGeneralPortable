
#load "TypesScript.fsx"

open TypesScript

let isSurprised() =
    false

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


let determineAttackGrade(attackingUnit: Unit, defendingUnit: Unit) =
    let attackGrade = 
        match defendingUnit.Equipment.EquipmentClass with
        | Ground Soft -> attackingUnit.Equipment.SoftAttack
        | Ground Hard -> attackingUnit.Equipment.HardAttack
        | Air -> attackingUnit.Equipment.AirAttack
        | Naval -> attackingUnit.Equipment.NavalAttack

    let attackGrade' = attackGrade + attackingUnit.BattleStars

    let attackGrade'' =
        match isMotorized(attackingUnit.Equipment.MovementType), attackingUnit.Fuel = 0 with
        | true, true -> attackGrade'/2
        | _,_ -> attackGrade'
    attackGrade''

let isRuggedDefense(attackingUnit:Unit, defendingUnit:Unit, random: System.Random) =
    match  isGround defendingUnit.Equipment.EquipmentClass, isCombat defendingUnit.Equipment.EquipmentClass with
    | true,true -> 
        let experienceRatio = (defendingUnit.Experience + 2) / (attackingUnit.Experience + 2);
        let entrenchmentRatio = (defendingUnit.Equipment.EntrenchmentRate + 1) / (attackingUnit.Equipment.EntrenchmentRate + 1)
        let ruggedDefenseChance = float defendingUnit.Entrenchment * float experienceRatio * float entrenchmentRatio * 0.05
        let roll = float (random.Next(100))
        ruggedDefenseChance > roll
    | _ -> false

let determineEntrenchmentAmount(attackingUnit:Unit, defendingUnit:Unit) =
    match attackingUnit.Equipment.IgnoreEntrenchment, isGroundCombat(defendingUnit.Equipment.EquipmentClass), isInfantry(attackingUnit.Equipment.EquipmentClass) with
    | true,_,_ -> 0
    | false, false, _ -> 0
    | false, true, true -> int(float defendingUnit.Entrenchment * 0.5)
    | false, true, false -> defendingUnit.Entrenchment

let determineGroundVNavalAdjustment(attackingUnit:Unit, defendingUnit:Unit) =
    match isGroundCombat(attackingUnit.Equipment.EquipmentClass), isNaval(getTargetType(defendingUnit.Equipment.EquipmentClass)) with
    | true,true -> 8
    | _,_ -> 0

let determineArtilleryVWetGroundAdjustment(attackingUnit:Unit, tile: Tile) =
    match isArtillery(attackingUnit.Equipment.EquipmentClass), isNotDry(tile.Condition) with
    | true, true -> 3
    | _ , _ -> 0
       
let determineDefenseGrade(attackingUnit:Unit, defendingUnit:Unit, tile: Tile) =
    let defenseGrade =
        match attackingUnit.Equipment.EquipmentClass with
        | Ground _ -> defendingUnit.Equipment.GroundDefense
        | Air -> defendingUnit.Equipment.AirDefense
        | Naval -> defendingUnit.Equipment.GroundDefense

    let defenseGrade' = defenseGrade + defendingUnit.BattleStars

    let defenseGrade'' = 
        match tile.Terrain.RiverInd with
        | true -> defenseGrade' + 4
        | false -> defenseGrade'

    defenseGrade''

let determineInitativeCap(equipmentInitative, terrainInitiative) =
    match equipmentInitative < terrainInitiative with
    | true -> equipmentInitative
    | false -> terrainInitiative

let determineExperienceBonusForInitiative(numberOfStars) =
    match numberOfStars with
    | 0 -> 0
    | 1 | 2 -> 1
    | 3 | 4 -> 2
    | _ -> 3

let determineSubmarineAttackAdjustment(attackingUnit:Unit, defendingUnit:Unit) =
    match(isSubmarine(attackingUnit.Equipment.EquipmentClass), isNaval(defendingUnit.Equipment.EquipmentClass)) with
    | true, true -> 99
    | _, _ -> 0

let determineTankAttackAntiTankDefenseAdjustment(attackingUnit:Unit, defendingUnit:Unit) = 
    match isTank(attackingUnit.Equipment.EquipmentClass), isAntitank(defendingUnit.Equipment.EquipmentClass) with
    | true, true -> 99
    | _, _ -> 0

let determineAirAttackAirDefenseDefenseAdjustment(attackingUnit:Unit, defendingUnit:Unit) = 
    match isAir(attackingUnit.Equipment.EquipmentClass), isAirAttack(defendingUnit.Equipment.EquipmentClass) with
    | true, true -> 99
    | _, _ -> 0

let calculateAttackingInitiative(attackingUnit:Unit, defendingUnit:Unit, terrainType: TerrainType, random: System.Random) =
    let attackingInitiativeCap = determineInitativeCap(attackingUnit.Equipment.Initative, getTerrainTypeInititaiveCap(terrainType))
    let attackingExperienceBonus = determineExperienceBonusForInitiative(attackingUnit.BattleStars)
    attackingInitiativeCap + attackingExperienceBonus
                            + determineSubmarineAttackAdjustment(attackingUnit, defendingUnit)
                            + random.Next(3)

let calculateDefendingInitiative(attackingUnit:Unit, defendingUnit:Unit, terrainType: TerrainType, random: System.Random) = 
    let defendingInitiativeCap = determineInitativeCap(defendingUnit.Equipment.Initative, getTerrainTypeInititaiveCap(terrainType))
    let defendingExperienceBonus = determineExperienceBonusForInitiative(defendingUnit.BattleStars)
    defendingInitiativeCap + defendingExperienceBonus
                                    + determineTankAttackAntiTankDefenseAdjustment(attackingUnit, defendingUnit)
                                    + determineAirAttackAirDefenseDefenseAdjustment(attackingUnit, defendingUnit)
                                    + random.Next(3)

let CalculateInitiative(attackingUnit:Unit, defendingUnit:Unit, terrainType: TerrainType, surprised:bool, random: System.Random) =
    let ai = calculateAttackingInitiative(attackingUnit, defendingUnit, terrainType, random)
    let di = calculateDefendingInitiative(attackingUnit, defendingUnit, terrainType, random)

    match surprised with
    | true -> DefenderStrikesFirst
    | false -> 
        match (ai,di) with
        | (ai, di) when ai > di -> AttackerStrikesFirst
        | (ai, di) when ai < di -> DefenderStrikesFirst
        | (_,_) -> Simultanous


//AttackerGrade
//DefenderGrade
//Rugged Defense
//NumberOfVollies: Attack - Defense

//DetemineVollyResults
//DetermineStandardEquipmentVolleyEffect (add experience and adjustHitPoints
//DetermineSpecialEquipmentVolleyEffect

//A volly is done for each of the attacker's volly points


let doesDefenderRetreat(protectorBaseStrength, protectorStrength) =
    if (protectorStrength * 2) > protectorBaseStrength then
        false
    else
        true

doesDefenderRetreat(10,6)
doesDefenderRetreat(10,5)


let determineBattleResult(aggressorStrength, protectorBaseStrength, protectorStrength) =
    match aggressorStrength <= 0, protectorStrength <= 0 with
    | true, true ->  AggressorDestroyed_ProtectorDestroyed
    | true, false -> AggressorDestroyed_ProtectorHolds
    | false, true -> AggressorAdvances_ProtectorDestroyed
    | false, false -> 
        match doesDefenderRetreat(protectorBaseStrength,protectorStrength) with
        | true -> AggressorAdvances_ProtectorRetreats
        | false -> AggressorHolds_ProtectorHolds


determineBattleResult(0, 10, 0)
determineBattleResult(1, 10, 0)
determineBattleResult(0, 10, 1)
determineBattleResult(1, 10, 1)
determineBattleResult(1, 10, 9)



