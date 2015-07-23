module TypesScript

#r "..\packages\FSharp.Data.2.2.5\lib\portable-net40+sl5+wp8+win8\FSharp.Data.dll"

open System.IO
open FSharp.Data

let sourceDirectory = "../Source"
let targetDirectory = "../Destination"

let baseDirectory = __SOURCE_DIRECTORY__
let baseDirectory' = Directory.GetParent(baseDirectory)

let getJsonData(className:string) =
    let filePath = @"Data\"+className+".json"
    let fullPath = Path.Combine(baseDirectory'.FullName, filePath)
    File.ReadAllText(fullPath)

type Campaign = JsonProvider<"../Data/campaign.Json">
let campaigns = Campaign.Parse(getJsonData("campaign"))
    
type CampaignBriefing = JsonProvider<"../Data/campaignBriefing.Json">
let campaignBriefings = CampaignBriefing.Parse(getJsonData("campaignBriefing"))

type CampaignStep = JsonProvider<"../Data/campaignStep.Json">
let campaignSteps = CampaignStep.Parse(getJsonData("campaignStep"))

type CampaignStepType = JsonProvider<"../Data/campaignStepType.Json">
let campaignStepTypes = Campaign.Parse(getJsonData("campaignStepType"))

type CampaignTree = JsonProvider<"../Data/campaignTree.Json">
let campaignTrees = CampaignTree.Parse(getJsonData("campaignTree"))

type Equipment = JsonProvider<"../Data/equipment.Json">
let equipments = Equipment.Parse(getJsonData("equipment"))

type EquipmentClass = JsonProvider<"../Data/EquipmentClass.Json">
let equipmentClasses = EquipmentClass.Parse(getJsonData("equipmentClass"))

type EquipmentGroup = JsonProvider<"../Data/EquipmentGroup.Json">
let equipmentGroups = EquipmentGroup.Parse(getJsonData("equipmentGroup"))

type EquipmentLossCalculationGroup = JsonProvider<"../Data/EquipmentLossCalculationGroup.Json">
let equipmentLossCalculationGroups = EquipmentLossCalculationGroup.Parse(getJsonData("equipmentLossCalculationGroup"))

type EquipmentSubClass = JsonProvider<"../Data/EquipmentSubClass.Json">
let equipmentSubClasses = EquipmentSubClass.Parse(getJsonData("equipmentSubClass"))

type MovementCost = JsonProvider<"../Data/MovementCost.Json">
let movementCosts = MovementCost.Parse(getJsonData("movementCost"))

type MovementCostModifier = JsonProvider<"../Data/MovementCostModifier.Json">
let movementCostModifiers = MovementCostModifier.Parse(getJsonData("movementCostModifier"))

type MovementType = JsonProvider<"../Data/MovementType.Json">
let movementTypes = MovementType.Parse(getJsonData("movementType"))

type Nation = JsonProvider<"../Data/Nation.Json">
let nations = Nation.Parse(getJsonData("nation"))

type ObjectiveType = JsonProvider<"../Data/ObjectiveType.Json">
let objectiveTypes = ObjectiveType.Parse(getJsonData("objectiveType"))

type Scenario = JsonProvider<"../Data/Scenario.Json">
let scenarios = Scenario.Parse(getJsonData("scenario"))

type ScenarioClassPurchase = JsonProvider<"../Data/scenarioClassPurchase.Json">
let scenariosClassPurchases = ScenarioClassPurchase.Parse(getJsonData("scenarioClassPurchase"))

type ScenarioNation = JsonProvider<"../Data/scenarioNation.Json">
let scenariosNations = ScenarioNation.Parse(getJsonData("scenarioNation"))

type ScenarioPrestigeAllotment = JsonProvider<"../Data/scenarioPrestigeAllotment.Json">
let scenariosPrestigeAllotment = ScenarioPrestigeAllotment.Parse(getJsonData("scenarioPrestigeAllotment"))

type ScenarioSide = JsonProvider<"../Data/scenarioSide.Json">
let scenariosSide = ScenarioPrestigeAllotment.Parse(getJsonData("scenarioSide"))

type ScenarioTile = JsonProvider<"../Data/scenarioTile.Json">
let scenarioTiles = ScenarioTile.Parse(getJsonData("scenarioTile"))

type ScenarioUnit = JsonProvider<"../Data/scenarioUnit.Json">
let scenarioUnits = ScenarioUnit.Parse(getJsonData("scenarioUnit"))

type Side = JsonProvider<"../Data/side.Json">
let sides = Side.Parse(getJsonData("side"))

type TargetType = JsonProvider<"../Data/targetType.Json">
let targetTypes = TargetType.Parse(getJsonData("targetType"))

type Terrain = JsonProvider<"../Data/terrain.Json">
let terrains = Terrain.Parse(getJsonData("terrain"))

type TerrainCondition = JsonProvider<"../Data/terrainCondition.Json">
let terrainConditions = TerrainCondition.Parse(getJsonData("terrainCondition"))

type TerrainGroup = JsonProvider<"../Data/terrainGroup.Json">
let terrainGroups = TerrainGroup.Parse(getJsonData("terrainGroup"))

type TerrainType = JsonProvider<"../Data/terrainType.Json">
let terrainTypes = TerrainType.Parse(getJsonData("terrainType"))

type TileName = JsonProvider<"../Data/tileName.Json">
let tileNames = TileName.Parse(getJsonData("tileName"))

type VictoryConditionCampaign = JsonProvider<"../Data/victoryConditionCampaign.Json">
let victoryConditionCampaign = VictoryConditionCampaign.Parse(getJsonData("victoryConditionCampaign"))

type VictoryConditionStandAlone = JsonProvider<"../Data/victoryConditionStandAlone.Json">
let victoryConditionStandAlone = VictoryConditionStandAlone.Parse(getJsonData("victoryConditionStandAlone"))

type WeatherCondition = JsonProvider<"../Data/weatherCondition.Json">
let weatherConditions = WeatherCondition.Parse(getJsonData("weatherCondition"))

type WeatherProbability = JsonProvider<"../Data/weatherProbability.Json">
let weatherProbabilities = WeatherProbability.Parse(getJsonData("weatherProbability"))

type WeatherZone = JsonProvider<"../Data/weatherzone.Json">
let weatherZones = WeatherZone.Parse(getJsonData("weatherZone"))

type GroundType =
| SoftGround
| HardGround
| Air
| Sea

type Tile = {
    TileId:int; tileName: string;
    ColumnNumber: int; rowNumber: int; 
    Terrain: Terrain; GroundType: GroundType;
    VictoryInd: bool; SupplyInd: bool;
    DeployInd: bool; MovementCost: int}

type Initative =
| AttackerStrikesFirst
| DefenderStrikesFirst
| Simultanous

type BattleInput = {
    AggressorTile: Tile; 
    ProtectorTile: Tile; 
    AggressorUnit: Unit; 
    ProtectorUnit: Unit; 
    TerrainCondition: TerrainCondition}

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

