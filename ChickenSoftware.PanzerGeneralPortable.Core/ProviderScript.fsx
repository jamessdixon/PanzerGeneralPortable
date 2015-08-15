module ProviderScript

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