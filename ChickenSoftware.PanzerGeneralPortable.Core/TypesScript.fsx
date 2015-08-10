module TypesScript

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

type CampaignStepTypeContext = XmlProvider<"../Data/campaignStepType.xml">
type CampaignStepType = CampaignStepTypeContext.CampaignStepType
let campaignStepTypes = CampaignContext.Parse(getXmlData("campaignStepType"))

type CampaignTreeContext = XmlProvider<"../Data/campaignTree.xml">
type CampaginTree = CampaignTreeContext.CampaignTree
let campaignTrees = CampaignTreeContext.Parse(getXmlData("campaignTree"))

type EquipmentLossCalculationGroupContext = XmlProvider<"../Data/EquipmentLossCalculationGroup.xml">
type EquipmentLossCalculationGroup = EquipmentLossCalculationGroupContext.EquipmentLossCalculationGroup
let equipmentLossCalculationGroups = EquipmentLossCalculationGroupContext.Parse(getXmlData("equipmentLossCalculationGroup"))

type MovementCostContext = XmlProvider<"../Data/MovementCost.xml">
type MovementCost = MovementCostContext.MovementCost
let movementCosts = MovementCostContext.Parse(getXmlData("movementCost"))

type MovementCostModifierContext = XmlProvider<"../Data/MovementCostModifier.xml">
type MovementCostModifier = MovementCostModifierContext.MovementCostModifier
let movementCostModifiers = MovementCostModifierContext.Parse(getXmlData("movementCostModifier"))

type NationContext = XmlProvider<"../Data/Nation.xml">
type Nation = NationContext.Nation
let nations = NationContext.Parse(getXmlData("nation"))

type ObjectiveTypeContext = XmlProvider<"../Data/ObjectiveType.xml">
type ObjectiveType = ObjectiveTypeContext.ObjectiveType
let objectiveTypes = ObjectiveTypeContext.Parse(getXmlData("objectiveType"))

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

type SideContext = XmlProvider<"../Data/side.xml">
type Side = SideContext.Side
let sides = SideContext.Parse(getXmlData("side"))

type TerrainContext = XmlProvider<"../Data/terrain.xml">
type Terrain = TerrainContext.Terrain
let terrains = TerrainContext.Parse(getXmlData("terrain"))

type TerrainGroupContext = XmlProvider<"../Data/terrainGroup.xml">
type TerrainGroup = TerrainGroupContext.TerrainGroup
let terrainGroups = TerrainGroupContext.Parse(getXmlData("terrainGroup"))

type TileNameContext = XmlProvider<"../Data/tileName.xml">
type TileName = TileNameContext.TileName
let tileNames = TileNameContext.Parse(getXmlData("tileName"))

type VictoryConditionCampaignContext = XmlProvider<"../Data/victoryConditionCampaign.xml">
type VictoryConditionCampaign = VictoryConditionCampaignContext.VictoryConditionCampaign
let victoryConditionCampaign = VictoryConditionCampaignContext.Parse(getXmlData("victoryConditionCampaign"))

type VictoryConditionStandAloneContext = XmlProvider<"../Data/victoryConditionStandAlone.xml">
type VictoryConditionStandAlone = VictoryConditionStandAloneContext.VictoryConditionStandAlone
let victoryConditionStandAlone = VictoryConditionStandAloneContext.Parse(getXmlData("victoryConditionStandAlone"))

type WeatherContext = XmlProvider<"../Data/weather.xml">
type Weather = WeatherContext.Weather
let weatherConditions = WeatherContext.Parse(getXmlData("weather"))

type WeatherProbabilityContext = XmlProvider<"../Data/weatherProbability.xml">
type WeatherProbability = WeatherProbabilityContext.WeatherProbability
let weatherProbabilities = WeatherProbabilityContext.Parse(getXmlData("weatherProbability"))

type WeatherZoneContext = XmlProvider<"../Data/weatherzone.xml">
type WeatherZone = WeatherZoneContext.WeatherZone
let weatherZones = WeatherZoneContext.Parse(getXmlData("weatherZone"))

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
| None
| Airborne
| Water
| AllTerrain

let (|Motorized|UnMotorized|) =
    function
    | Tracked | HalfTracked | Wheeled | Airborne | Water | AllTerrain -> Motorized
    | Walk | None -> UnMotorized

let isMotorized = function 
| Motorized -> true 
| UnMotorized -> false

type Infantry =
| Basic
| Engineer
| Pioniere
| Airborne
| Ranger
| Bridging

let isParadroppable = function 
| Airborne | Ranger -> true 
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
| Antitank of Cannon
| AirAttack of AirAttack
| Emplacement of Emplacement
| Fighter of Fighter
| Bomber of Bomber
| CombatShip of CombatShip
| Transport of Transport

let (|Towed|SelfPropelled|Static|) = function
    | Artillery TowedLight | Artillery TowedHeavy | Antitank TowedLight | Antitank TowedHeavy | AirAttack(AirDefense TowedHeavy) | AirAttack(AirDefense TowedLight) -> Towed
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

let getTargetType = function
    | Infantry _  -> Ground Soft
    | Tank -> Ground Hard
    | Recon  -> Ground Soft
    | Artillery _ -> Ground Soft
    | Antitank TowedLight -> Ground Soft 
    | Antitank TowedHeavy -> Ground Soft 
    | Antitank _ -> Ground Hard 
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

let isAntitank = function
| Antitank _ -> true
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
| Infantry _ | Tank | Recon | Antitank _ -> true
| _ -> false
     
let isGroundCombat(equipmentClass: EquipmentClass) =
    let targetType = getTargetType equipmentClass
    isGround(targetType) && isCombat(equipmentClass) 

type Equipment = {
    Id: int;
    Description: string;
    EquipmentClass: EquipmentClass;
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
    TargetType: TargetType;
    Equipment: Equipment;
    Experience: int;
    BattleStars: int;
    MovementType: MovementType;
    Ammo: int;
    Fuel: int;
    Entrenchment: int}

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