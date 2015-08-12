module UnitScript

#load "TypesScript.fsx"
open TypeScript

let getEquipment(id: int) =
    {EquipmentId=id; Description="TEST";
    EquipmentClass = Infantry Basic;
    MovementType = Walk; HardAttack = 5;
    SoftAttack = 6; AirAttack = 2;
    NavalAttack = 3; EntrenchmentRate=1;
    GroundDefense=6; AirDefense=5;
    IgnoreEntrenchment=false; Initative=4}

let getUnit(id: int) =
    {UnitId=id;
    Nation=Bulgaria;
    Equipment = getEquipment 1;
    Experience=100;
    BattleStars=1;
    Ammo=10;
    Fuel=0;
    Entrenchment=1;
    HitPoints=60;
    AttackPoints=10;
    IsSupressed=false}
