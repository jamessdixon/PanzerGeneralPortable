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
let campaignProvider = CampaignContext.Parse(getXmlData("campaign"))

type CampaignBriefingContext = XmlProvider<"../Data/campaignBriefing.xml">
let campaignBriefingProvider = CampaignBriefingContext.Parse(getXmlData("campaignBriefing"))

type CampaignStepContext = XmlProvider<"../Data/campaignStep.xml">
let campaignStepProvider = CampaignStepContext.Parse(getXmlData("campaignStep"))

type CampaignTreeContext = XmlProvider<"../Data/campaignTree.xml">
let campaignTreeProvider = CampaignTreeContext.Parse(getXmlData("campaignTree"))

type EquipmentContext = XmlProvider<"../Data/equipment.xml">
let equipmentProvider = EquipmentContext.Parse(getXmlData("equipment"))

type MovementCostContext = XmlProvider<"../Data/MovementCost.xml">
let movementCostProvider = MovementCostContext.Parse(getXmlData("movementCost"))

type MovementCostModifierContext = XmlProvider<"../Data/MovementCostModifier.xml">
let movementCostModifierProvider = MovementCostModifierContext.Parse(getXmlData("movementCostModifier"))

type ScenarioContext = XmlProvider<"../Data/Scenario.xml">
let scenarioProvider = ScenarioContext.Parse(getXmlData("scenario"))

type ScenarioClassPurchaseContext = XmlProvider<"../Data/scenarioClassPurchase.xml">
let scenariosClassPurchaseProvider = ScenarioClassPurchaseContext.Parse(getXmlData("scenarioClassPurchase"))

type ScenarioNationContext = XmlProvider<"../Data/scenarioNation.xml">
let scenariosNationProvider = ScenarioNationContext.Parse(getXmlData("scenarioNation"))

type ScenarioPrestigeAllotmentContext = XmlProvider<"../Data/scenarioPrestigeAllotment.xml">
let scenariosPrestigeAllotmentProvider = ScenarioPrestigeAllotmentContext.Parse(getXmlData("scenarioPrestigeAllotment"))

type ScenarioSideContext = XmlProvider<"../Data/scenarioSide.xml">
let scenariosSideProvider = ScenarioPrestigeAllotmentContext.Parse(getXmlData("scenarioSide"))

type ScenarioTileContext = XmlProvider<"../Data/scenarioTile.xml">
let scenarioTileProvider = ScenarioTileContext.Parse(getXmlData("scenarioTile"))

type ScenarioUnitContext = XmlProvider<"../Data/scenarioUnit.xml">
let scenarioUnitProvider = ScenarioUnitContext.Parse(getXmlData("scenarioUnit"))

type TerrainContext = XmlProvider<"../Data/terrain.xml">
let terrainProvider = TerrainContext.Parse(getXmlData("terrain"))

type TileNameContext = XmlProvider<"../Data/tileName.xml">
let tileNameProvider = TileNameContext.Parse(getXmlData("tileName"))

type VictoryConditionCampaignContext = XmlProvider<"../Data/victoryConditionCampaign.xml">
let victoryConditionCampaignProvider = VictoryConditionCampaignContext.Parse(getXmlData("victoryConditionCampaign"))

type VictoryConditionStandAloneContext = XmlProvider<"../Data/victoryConditionStandAlone.xml">
let victoryConditionStandAloneProvider = VictoryConditionStandAloneContext.Parse(getXmlData("victoryConditionStandAlone"))

type WeatherProbabilityContext = XmlProvider<"../Data/weatherProbability.xml">
let weatherProbabilitieProvider = WeatherProbabilityContext.Parse(getXmlData("weatherProbability"))

//type Campaign = CampaignContext.Campaign
//type CampaignBriefing = CampaignBriefingContext.Briefing
//type CampaignStep = CampaignStepContext.CampaignStep
//type CampaginTree = CampaignTreeContext.CampaignTree
//type MovementCost = MovementCostContext.MovementCost
//type MovementCostModifier = MovementCostModifierContext.MovementCostModifier
//type Scenario = ScenarioContext.Scenario
//type ScenarioClassPurchase = ScenarioClassPurchaseContext.ScenarioClassPurchase
//type ScenarioNation = ScenarioNationContext.ScenarioNation
//type ScenarioPrestigeAllotment = ScenarioPrestigeAllotmentContext.ScenarioPrestigeAllotment
//type ScenarioSide = ScenarioSideContext.ScenarioSide
//type ScenarioTile = ScenarioTileContext.ScenarioTile
//type ScenarioUnit = ScenarioUnitContext.ScenarioUnit
//type TileName = TileNameContext.TileName
//type VictoryConditionCampaign = VictoryConditionCampaignContext.VictoryConditionCampaign
//type VictoryConditionStandAlone = VictoryConditionStandAloneContext.VictoryConditionStandAlone
//type WeatherProbability = WeatherProbabilityContext.WeatherProbability
