module CombatScript

#load "ProviderScript.fsx"
#load "GameScript.fsx"
#load "UnitScript.fsx"
#load "TileScript.fsx"

open ProviderScript
open GameScript
open UnitScript
open TileScript

type Initative =
| AttackerStrikesFirst
| DefenderStrikesFirst
| Simultanous

type BattleInput = {
    Tile: Tile; 
    AggressorUnit: Unit; 
    ProtectorUnit: Unit}

type BattleOutcome =
| AggressorHolds_ProtectorHolds
| AggressorAdvances_ProtectorRetreats
| AggressorAdvances_ProtectorDestroyed
| AggressorDestroyed_ProtectorHolds
| AggressorDestroyed_ProtectorDestroyed

type VolleyOutcome =
| AttackerHurt_DefenderUnhurt
| AttackerHurt_DefenderHurt
| AttackerUnhurt_DefenderUnhurt
| AttackerUnhurt_DefenderHurt

let isSurprised() =
    false

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

let attackingUnit00 = getUnit(45).Value
let defendingUnit00 = getUnit(402).Value
determineAttackGrade(attackingUnit00, defendingUnit00)

let attackingUnit01 = {attackingUnit00 with Fuel=0}
determineAttackGrade(attackingUnit01, defendingUnit00)

let attackingUnit02 = {attackingUnit00 with BattleStars=5}
determineAttackGrade(attackingUnit02, defendingUnit00)

let isRuggedDefense(attackingUnit:Unit, defendingUnit:Unit, random: System.Random) =
    let isGround = isGround defendingUnit.Equipment.EquipmentClass
    let isCombat = isCombat defendingUnit.Equipment.EquipmentClass
    match  isGround, isCombat with
    | true,true -> 
        let experienceRatio = (defendingUnit.Experience + 2) / (attackingUnit.Experience + 2);
        let entrenchmentRatio = (defendingUnit.Equipment.EntrenchmentRate + 1) / (attackingUnit.Equipment.EntrenchmentRate + 1)
        let ruggedDefenseChance = float defendingUnit.Entrenchment * float experienceRatio * float entrenchmentRatio * 0.05
        let roll = float (random.Next(100))
        ruggedDefenseChance > roll
    | _ -> false

let random = new System.Random(42) //next = 66
let defendingUnit01 = {defendingUnit00 with Experience=500; Entrenchment=10}
isRuggedDefense(attackingUnit00, defendingUnit01, random)

let defendingUnit02 = {defendingUnit00 with Experience=0; Entrenchment=0}
isRuggedDefense(attackingUnit00, defendingUnit02, random)

let determineEntrenchmentAmount(attackingUnit:Unit, defendingUnit:Unit) =
    match attackingUnit.Equipment.IgnoreEntrenchment, 
        isGroundCombat(defendingUnit.Equipment.EquipmentClass), 
        isInfantry(attackingUnit.Equipment.EquipmentClass) with
    | true,_,_ -> 0
    | false, false, _ -> 0
    | false, true, true -> int(float defendingUnit.Entrenchment * 0.5)
    | false, true, false -> defendingUnit.Entrenchment


determineEntrenchmentAmount(attackingUnit00, defendingUnit01)
determineEntrenchmentAmount(defendingUnit01, defendingUnit01)

let determineGroundVNavalAdjustment(attackingUnit:Unit, defendingUnit:Unit) =
    match isGroundCombat(attackingUnit.Equipment.EquipmentClass), isNaval(defendingUnit.Equipment.EquipmentClass) with
    | true,true -> 8
    | _,_ -> 0

let defendingUnit03 = getUnit(288).Value
determineGroundVNavalAdjustment(attackingUnit00,defendingUnit03)

let determineArtilleryVWetGroundAdjustment(attackingUnit:Unit, tile: Tile) =
    match isArtillery(attackingUnit.Equipment.EquipmentClass), isNotDry(tile.Condition) with
    | true, true -> 3
    | _ , _ -> 0

let attackingUnit03 = getUnit(58).Value
//let tile = {TileId=1; }
       
let determineDefenseGrade(attackingUnit:Unit, defendingUnit:Unit, tile: Tile) =
    let defenseGrade =
        match attackingUnit.Equipment.EquipmentClass with
        | Ground _ -> defendingUnit.Equipment.GroundDefense
        | Air -> defendingUnit.Equipment.AirDefense
        | Naval -> defendingUnit.Equipment.GroundDefense

    let defenseGrade' = defenseGrade + defendingUnit.BattleStars

    let defenseGrade'' = 
        match isRiver(tile.Terrain) with
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
    match isTank(attackingUnit.Equipment.EquipmentClass), isAntiTank(defendingUnit.Equipment.EquipmentClass) with
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

let calculateInitiative(attackingUnit:Unit, defendingUnit:Unit, terrainType: TerrainType, surprised:bool, random: System.Random) =
    let ai = calculateAttackingInitiative(attackingUnit, defendingUnit, terrainType, random)
    let di = calculateDefendingInitiative(attackingUnit, defendingUnit, terrainType, random)

    match surprised with
    | true -> DefenderStrikesFirst
    | false -> 
        match (ai,di) with
        | (ai, di) when ai > di -> AttackerStrikesFirst
        | (ai, di) when ai < di -> DefenderStrikesFirst
        | (_,_) -> Simultanous


let determineVolleyEffect(attackingUnit:Unit, defendingUnit:Unit, rollResult: int) =
    let attackingUnit' = {attackingUnit with Experience =  attackingUnit.Experience + 1; Ammo = attackingUnit.Ammo - 1; AttackPoints = attackingUnit.AttackPoints - 1}
    let defendingUnit' = match rollResult with
                                | 13 | 14 | 15 | 16 | 17 | 18 | 19 | 20 -> {defendingUnit with Experience =  defendingUnit.Experience + 1; Ammo = defendingUnit.Ammo - 1;Entrenchment = defendingUnit.Entrenchment - 1; HitPoints = defendingUnit.HitPoints - 1}
                                | 11 | 12 -> {defendingUnit with Experience =  defendingUnit.Experience + 1; Ammo = defendingUnit.Ammo - 1; Entrenchment = defendingUnit.Entrenchment - 1}
                                | _ -> {defendingUnit with Experience =  defendingUnit.Experience + 1; Ammo = defendingUnit.Ammo - 1}
    attackingUnit', defendingUnit'

let determineVollyResults(attackingUnit:Unit, defendingUnit:Unit, netGrade: int, roll: int) =
    let roll' = 
        match netGrade with
        | 0| 1 | 2 | 3 | 4 -> roll + netGrade
        | _ -> roll + 4 + ((2/5) * (netGrade-4))
    determineVolleyEffect(attackingUnit, defendingUnit,roll')

let rec determineVolly(vollyCount:int, attackingUnit:Unit, defendingUnit:Unit, random: System.Random) =
    match vollyCount, defendingUnit.HitPoints <=0 with
    | 0, _ -> attackingUnit, defendingUnit
    | _, true -> attackingUnit, defendingUnit
    | _, false -> determineVolly(vollyCount, attackingUnit, defendingUnit, random)

let determineAttackPointsForVolley(unit: Unit) =
    match isCombat(unit.Equipment.EquipmentClass), unit.IsSupressed  with
    | true, false -> unit.HitPoints
    | true, true -> 0
    | false, _ -> 0

let calculateVolly (attackingUnit:Unit, defendingUnit:Unit, tile:Tile, random:System.Random) =
    let attackerAttackGrade = determineAttackGrade(attackingUnit, defendingUnit)
    let defenderAttackGrade = determineDefenseGrade(attackingUnit, defendingUnit, tile)
    let netAttackGrade = attackerAttackGrade - defenderAttackGrade
    determineVolly(attackingUnit.AttackPoints,attackingUnit,defendingUnit,random)


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



