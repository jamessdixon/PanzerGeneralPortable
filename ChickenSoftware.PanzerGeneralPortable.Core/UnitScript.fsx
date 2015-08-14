module UnitScript

#load "TypesScript.fsx"
open TypeScript

let getEquipmentClass(id: int) =
    match id with
    | 0 -> Some (Infantry Basic)
    | 1 -> Some Tank
    | 2 -> Some Recon 
    | 3 -> Some (AntiTank TowedLight)
    | 4 -> Some (Artillery TowedLight)
    | 5 -> Some (AirAttack AntiAir )
    | 6 -> Some (AirAttack (AirDefense TowedLight))
    | 7 -> Some (Emplacement Fort)
    | 8 -> Some (Fighter Prop)
    | 9 -> Some (Bomber Tactical)
    | 10 -> Some (Bomber Strategic)
    | 11 -> Some (CombatShip Submarine)
    | 12 -> Some (CombatShip Destroyer)
    | 13 -> Some (CombatShip CapitalShip)
    | 14 -> Some (Transport AircraftCarrier)
    | 15 -> Some (Transport GroundTransport)
    | 16 -> Some (Transport AirTransport)
    | 17 -> Some (Transport SeaTransport)
    | 18 -> Some (AntiTank SelfPropelled)
    | 19 -> Some (Artillery SelfPropelled)
    | 20 -> Some (AirAttack (AirDefense SelfPropelled))
    | 21 -> Some (Fighter Jet)
    | 22 -> Some (Infantry Engineer)
    | 23 -> Some (Infantry Ranger)
    | 24 -> Some (Infantry Bridging)
    | 25 -> Some (Emplacement Strongpoint)
    | 26 -> Some (AntiTank TowedHeavy)
    | 27 -> Some (Artillery TowedHeavy)
    | _ -> None

let getMovementType(id: int) =
    match id with
    | 0 -> Some Tracked
    | 1 -> Some HalfTracked
    | 2 -> Some Wheeled
    | 3 -> Some Walk
    | 4 -> Some Immovable
    | 5 -> Some Airborne
    | 6 -> Some Water
    | 7 -> Some AllTerrain
    | _ -> None

let getEquipment(id: int) =
    let equipment = equipmentProvider.Equipments |> Seq.find(fun e -> e.EquipmentId = id)
    let equipmentClass = getEquipmentClass(equipment.EquipmentSubClassId);

    match equipmentClass.IsSome with
    | true -> 
        let nation = getNation(equipment.NationId)
        let movementType = getMovementType(equipment.MovementTypeId)
        let entrenchmentRate = getEntrenchmentRate(equipmentClass.Value)
        Some {EquipmentId=id; Description=equipment.EquipmentDescription;
        EquipmentClass = equipmentClass.Value; Nation = nation.Value;
        MovementType = movementType.Value ; HardAttack = equipment.HardAttack;
        SoftAttack = equipment.SoftAttack; AirAttack = equipment.AirAttack;
        NavalAttack = equipment.NavalAttack; EntrenchmentRate=entrenchmentRate;
        GroundDefense=equipment.GroundDefense; AirDefense=equipment.AirDefense;
        IgnoreEntrenchment=equipment.IgnoresEntrenchment; Initative=equipment.Initiative}
    | false -> None

let getUnit(id: int) =
    let equipment = getEquipment id;
    match equipment.IsSome with
    | true -> 
        Some {UnitId=id;
        Nation=equipment.Value.Nation;
        Equipment = equipment.Value;
        Experience=100;
        BattleStars=1;
        Ammo=10;
        Fuel=0;
        Entrenchment=1;
        HitPoints=60;
        AttackPoints=10;
        IsSupressed=false}
    | false -> None
