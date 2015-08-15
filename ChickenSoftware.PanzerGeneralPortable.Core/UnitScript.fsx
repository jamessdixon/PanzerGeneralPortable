module UnitScript

#load "GameScript.fsx"
open GameScript

type GroundTarget =
| Soft
| Hard

type TargetType =
| Ground of GroundTarget
| Air
| Naval

type MovementType =
| Tracked
| HalfTracked
| Wheeled
| Walk
| Immovable
| Airborne
| Water
| AllTerrain

let (|Motorized|UnMotorized|) =
    function
    | Tracked | HalfTracked | Wheeled | Airborne | Water | AllTerrain -> Motorized
    | Walk | Immovable -> UnMotorized

let isMotorized = function 
| Motorized -> true 
| UnMotorized -> false

type Infantry =
| Basic
| Engineer
| Ranger
| Bridging

let canParadrop = function 
| Airborne  -> true 
| _ -> false

let canBridgeRivers = function
| Bridging  -> true 
| _ -> false

type Emplacement = 
| Fort
| Strongpoint

type Bomber =
| Tactical
| Strategic

type Fighter =
| Prop
| Jet 

type CombatShip =
| Submarine
| Destroyer
| CapitalShip

type Cannon =
| TowedLight
| TowedHeavy
| SelfPropelled

type AirAttack =
| AirDefense of Cannon
| AntiAir

type Transport =
| GroundTransport
| AirTransport
| SeaTransport
| AircraftCarrier

type EquipmentClass =
| Infantry of Infantry
| Tank
| Recon
| Artillery of Cannon
| AntiTank of Cannon
| AirAttack of AirAttack
| Emplacement of Emplacement
| Fighter of Fighter
| Bomber of Bomber
| CombatShip of CombatShip
| Transport of Transport

let (|Towed|SelfPropelled|Static|) = function
    | Artillery TowedLight | Artillery TowedHeavy | AntiTank TowedLight | AntiTank TowedHeavy | AirAttack(AirDefense TowedHeavy) | AirAttack(AirDefense TowedLight) -> Towed
    | Emplacement _ -> Static
    | _ -> SelfPropelled

let isTowed = function 
| Towed -> true 
| _ -> false

let isSelfPropelled = function 
| SelfPropelled -> true 
| _ -> false

let isStatic = function 
| Static -> true 
| _ -> false

let (|Ground|Air|Naval|) = function
    | Infantry _  -> Ground Soft
    | Tank -> Ground Hard
    | Recon  -> Ground Soft
    | Artillery _ -> Ground Soft
    | AntiTank TowedLight -> Ground Soft 
    | AntiTank TowedHeavy -> Ground Soft 
    | AntiTank _ -> Ground Hard 
    | AirAttack _ -> Ground Soft
    | Emplacement _ -> Ground Soft
    | Transport GroundTransport -> Ground Soft
    | Fighter _ -> Air
    | Bomber _ -> Air
    | Transport AirTransport -> Air
    | CombatShip _ -> Naval
    | Transport SeaTransport -> Naval
    | Transport AircraftCarrier -> Naval

let isInfantry = function
| Infantry _  -> true
| _ -> false

let isArtillery = function
| Artillery _ -> true
| _ -> false

let isTank = function
| Tank _ -> true
| _ -> false

let isAntiTank = function
| AntiTank _ -> true
| _ -> false

let isAirAttack = function
| AirAttack _ -> true
| _ -> false

let isSubmarine = function
| CombatShip Submarine -> true
| _ -> false

let isNaval = function
| Naval -> true
| _ -> false

let isGround = function 
| Ground _ -> true 
| _ -> false

let isAir = function 
| Air -> true 
| _ -> false

let isTransport = function
| Transport _  -> true
| _ -> false

let isCombat = function
| Transport _  -> false
| _ -> true

let canCaptureHexes = function
| Infantry _ | Tank | Recon | AntiTank _ -> true
| _ -> false
     
let isGroundCombat(equipmentClass: EquipmentClass) =
    isGround(equipmentClass) && isCombat(equipmentClass) 

let getEntrenchmentRate(equipmentClass) = 
    match equipmentClass with
    | Infantry _  -> 3
    | Tank -> 1
    | Recon  -> 2
    | Artillery _ -> 2
    | AntiTank _ -> 2 
    | AirAttack _ -> 2
    | Emplacement _ -> 1
    | Transport GroundTransport -> 2
    | Fighter _ -> 0
    | Bomber _ -> 0
    | Transport AirTransport -> 0
    | CombatShip _ -> 0
    | Transport SeaTransport -> 0
    | Transport AircraftCarrier -> 0

type ImageCoordinate = int * int

type Equipment = {
    EquipmentId: int;
    Nation: Nation;
    Description: string;
    EquipmentClass: EquipmentClass;
    MovementType: MovementType;
    HardAttack: int;
    SoftAttack: int;
    AirAttack: int;
    NavalAttack: int;
    EntrenchmentRate: int;
    GroundDefense: int;
    AirDefense: int;
    CloseDefense: int;
    IgnoreEntrenchment: bool;
    Initative: int;
    Range:int;
    Spotting: int;
    Movement: int;
    MaxFuel:int;
    MaxAmmo:int;
    Cost:int;
    StartService:System.DateTime;
    EndService: System.DateTime;
    FullImageCoordinate: ImageCoordinate;
    StackedImageCoordinate: ImageCoordinate}

type Unit = {
    UnitId:int;
    Nation: Nation;
    Equipment: Equipment;
    Experience: int;
    BattleStars: int;
    Ammo: int;
    Fuel: int;
    Entrenchment: int;
    HitPoints: int;
    AttackPoints: int;
    IsSupressed: bool}

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

let getStartDate(month:int, year:int) =
    new System.DateTime(1900+year,month,1)    

let getEndDate(month:int, year:int) =
    new System.DateTime(1900+year,month,28)    

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
        CloseDefense=equipment.CloseDefense; IgnoreEntrenchment=equipment.IgnoresEntrenchment; 
        Initative=equipment.Initiative; Range=equipment.Range;
        Spotting=equipment.Spotting; Movement=equipment.Movement;
        MaxFuel=equipment.MaxFuel; MaxAmmo=equipment.MaxAmmo;
        Cost=equipment.Cost; StartService=getStartDate(equipment.Month,equipment.Year);
        EndService = getEndDate(equipment.Month, equipment.LastYear);
        FullImageCoordinate = equipment.ImageXCoordinate, equipment.ImageYCoordinate;
        StackedImageCoordinate = equipment.StackedImageXCoordinate, equipment.StackedImageYCoordinate;}
    | false -> None

let getUnit(id: int) =
    let equipment = getEquipment id;
    match equipment.IsSome with
    | true -> 
        Some {UnitId=id;
        Nation=equipment.Value.Nation;
        Equipment = equipment.Value;
        Experience=0;
        BattleStars=0;
        Ammo=equipment.Value.MaxAmmo;
        Fuel=equipment.Value.MaxFuel;
        Entrenchment=0;
        HitPoints=10;
        AttackPoints=10;
        IsSupressed=false}
    | false -> None

        