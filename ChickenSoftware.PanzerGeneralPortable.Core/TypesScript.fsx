module TypeScript

#r "..\packages\FSharp.Data.2.2.5\lib\portable-net40+sl5+wp8+win8\FSharp.Data.dll"
#r "System.Xml.Linq"

open System.IO
open FSharp.Data

let sourceDirectory = "../Source"
let targetDirectory = "../Destination"

let baseDirectory = __SOURCE_DIRECTORY__
let baseDirectory' = Directory.GetParent(baseDirectory)

let getXmlData(className:string) =
    let filePath = @"Data\"+className+".xml"
    let fullPath = Path.Combine(baseDirectory'.FullName, filePath)
    File.ReadAllText(fullPath)

type CampaignContext = XmlProvider<"../Data/campaign.xml">
type Campaign = CampaignContext.Campaign
let campaigns = CampaignContext.Parse(getXmlData("campaign"))
    
type CampaignBriefingContext = XmlProvider<"../Data/campaignBriefing.xml">
type CampaignBriefing = CampaignBriefingContext.Briefing
let campaignBriefings = CampaignBriefingContext.Parse(getXmlData("campaignBriefing"))

type CampaignStepContext = XmlProvider<"../Data/campaignStep.xml">
type CampaignStep = CampaignStepContext.CampaignStep
let campaignSteps = CampaignStepContext.Parse(getXmlData("campaignStep"))

type CampaignTreeContext = XmlProvider<"../Data/campaignTree.xml">
type CampaginTree = CampaignTreeContext.CampaignTree
let campaignTrees = CampaignTreeContext.Parse(getXmlData("campaignTree"))

type EquipmentContext = XmlProvider<"../Data/equipment.xml">
let equipmentProvider = EquipmentContext.Parse(getXmlData("equipment"))

type MovementCostContext = XmlProvider<"../Data/MovementCost.xml">
type MovementCost = MovementCostContext.MovementCost
let movementCosts = MovementCostContext.Parse(getXmlData("movementCost"))

type MovementCostModifierContext = XmlProvider<"../Data/MovementCostModifier.xml">
type MovementCostModifier = MovementCostModifierContext.MovementCostModifier
let movementCostModifiers = MovementCostModifierContext.Parse(getXmlData("movementCostModifier"))

type ScenarioContext = XmlProvider<"../Data/Scenario.xml">
type Scenario = ScenarioContext.Scenario
let scenarios = ScenarioContext.Parse(getXmlData("scenario"))

type ScenarioClassPurchaseContext = XmlProvider<"../Data/scenarioClassPurchase.xml">
type ScenarioClassPurchase = ScenarioClassPurchaseContext.ScenarioClassPurchase
let scenariosClassPurchases = ScenarioClassPurchaseContext.Parse(getXmlData("scenarioClassPurchase"))

type ScenarioNationContext = XmlProvider<"../Data/scenarioNation.xml">
type ScenarioNation = ScenarioNationContext.ScenarioNation
let scenariosNations = ScenarioNationContext.Parse(getXmlData("scenarioNation"))

type ScenarioPrestigeAllotmentContext = XmlProvider<"../Data/scenarioPrestigeAllotment.xml">
type ScenarioPrestigeAllotment = ScenarioPrestigeAllotmentContext.ScenarioPrestigeAllotment
let scenariosPrestigeAllotment = ScenarioPrestigeAllotmentContext.Parse(getXmlData("scenarioPrestigeAllotment"))

type ScenarioSideContext = XmlProvider<"../Data/scenarioSide.xml">
type ScenarioSide = ScenarioSideContext.ScenarioSide
let scenariosSide = ScenarioPrestigeAllotmentContext.Parse(getXmlData("scenarioSide"))

type ScenarioTileContext = XmlProvider<"../Data/scenarioTile.xml">
type ScenarioTile = ScenarioTileContext.ScenarioTile
let scenarioTiles = ScenarioTileContext.Parse(getXmlData("scenarioTile"))

type ScenarioUnitContext = XmlProvider<"../Data/scenarioUnit.xml">
type ScenarioUnit = ScenarioUnitContext.ScenarioUnit
let scenarioUnits = ScenarioUnitContext.Parse(getXmlData("scenarioUnit"))

type TerrainContext = XmlProvider<"../Data/terrain.xml">
type Terrain = TerrainContext.Terrain
let terrains = TerrainContext.Parse(getXmlData("terrain"))

type TileNameContext = XmlProvider<"../Data/tileName.xml">
type TileName = TileNameContext.TileName
let tileNames = TileNameContext.Parse(getXmlData("tileName"))

type VictoryConditionCampaignContext = XmlProvider<"../Data/victoryConditionCampaign.xml">
type VictoryConditionCampaign = VictoryConditionCampaignContext.VictoryConditionCampaign
let victoryConditionCampaign = VictoryConditionCampaignContext.Parse(getXmlData("victoryConditionCampaign"))

type VictoryConditionStandAloneContext = XmlProvider<"../Data/victoryConditionStandAlone.xml">
type VictoryConditionStandAlone = VictoryConditionStandAloneContext.VictoryConditionStandAlone
let victoryConditionStandAlone = VictoryConditionStandAloneContext.Parse(getXmlData("victoryConditionStandAlone"))

type WeatherProbabilityContext = XmlProvider<"../Data/weatherProbability.xml">
type WeatherProbability = WeatherProbabilityContext.WeatherProbability
let weatherProbabilities = WeatherProbabilityContext.Parse(getXmlData("weatherProbability"))

type CampaignStepType =
| MajorVictory
| MinorVictory
| Loss

type ObjectiveType =
| AxisAttack
| AxisDefend

type WeatherCondition =
| Fair
| Cloudy
| Rain
| Snow

type WeatherZone =
| Desert
| Mediterrian
| WesternEurope
| EasternEurope

type Side =
| Axis
| Allied
| Neutral

type Nation =
| Neutral
| AlliedForces
| Bulgaria
| France 
| German 
| Greece 
| UnitedStates 
| Hungray 
| Italy
| Norway 
| Poland
| Romania
| SovietUnion 
| GreatBritain
| Yugoslavia 

let (|Neutral|Axis|Allied|) =
    function
    | Neutral -> Neutral
    | AlliedForces | France | Greece | UnitedStates | Norway | Poland | SovietUnion | GreatBritain -> Allied
    | Bulgaria | German | Hungray | Italy | Romania | Yugoslavia -> Axis

let getNation(id:int) =
    match id with
    | -1 -> Some Neutral
    | 2 -> Some AlliedForces
    | 3 -> Some Bulgaria
    | 7 -> Some France
    | 8 -> Some German
    | 9 -> Some Greece
    | 10 -> Some UnitedStates
    | 11 -> Some Hungray
    | 13 -> Some Italy
    | 15 -> Some Norway
    | 16 -> Some Poland
    | 18 -> Some Romania
    | 20 -> Some SovietUnion
    | 23 -> Some GreatBritain
    | 24 -> Some Yugoslavia
    | _ -> None

let getNationImageCoordinates(nation:Nation) =
    match nation with
    | Neutral -> 0,0
    | AlliedForces -> 60,0
    | Bulgaria -> 120,0
    | France -> 360,0
    | German -> 420,0
    | Greece -> 480,0
    | UnitedStates -> 540,0
    | Hungray -> 600,0
    | Italy -> 720,0
    | Norway -> 840,0
    | Poland -> 900,0
    | Romania -> 1020,0
    | SovietUnion -> 1140,0
    | GreatBritain -> 1320,0
    | Yugoslavia -> 1380,0
    | _ -> 0,0

type TerrainType =
| Ocean
| Port
| Rough
| Mountain
| City
| Clear
| Forest
| Swamp
| Airfield
| Fortification
| Bocage
| Desert
| RoughDesert
| Escarpment

let getTerrainTypeInititaiveCap = function
| City -> 1
| Forest | Fortification | Bocage-> 3
| Swamp -> 4
| Port | Rough | RoughDesert -> 5
| Mountain | Escarpment -> 8
| _ -> 99

type Condition =
| Dry
| Muddy
| Frozen

let isDry = function 
| Dry -> true 
| _ -> false

let isNotDry = function 
| Dry -> false 
| _ -> true

type Tile = {
    TileId:int; TileName: string;
    Nation: Nation;
    ColumnNumber: int; RowNumber: int; 
    Terrain: Terrain; Condition: Condition;
    VictoryInd: bool;  SupplyInd: bool; 
    DeployInd: bool; MovementCost: int}

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

let isParadroppable = function 
| Airborne  -> true 
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
| _ -> false

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
    IgnoreEntrenchment: bool;
    Initative: int}

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