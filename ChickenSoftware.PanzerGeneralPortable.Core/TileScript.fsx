module TileScript

#load "ProviderScript.fsx"
#load "GameScript.fsx"
open ProviderScript
open GameScript

type WeatherCondition =
| Fair
| Cloudy
| Rain
| Snow

let getWeatherCondition(id: int) =
    match id with
    | 0 -> Some Fair
    | 1 -> Some Cloudy
    | 2 -> Some Rain
    | 3 -> Some Snow
    | _ -> None

type WeatherZone =
| Desert
| Mediterrian
| WesternEurope
| EasternEurope

let getWeatherZone(id: int) =
    match id with
    | 0 -> Some Desert
    | 1 -> Some Mediterrian
    | 2 -> Some WesternEurope
    | 3 -> Some EasternEurope
    | _ -> None

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

let getTerrainType(id: int) =
    match id with
    | 0 -> Some Ocean
    | 1 -> Some Port
    | 2 -> Some Rough
    | 3 -> Some Mountain
    | 4 -> Some City
    | 5 -> Some Clear
    | 6 -> Some Forest
    | 7 -> Some Swamp
    | 8 -> Some Airfield
    | 9 -> Some Fortification
    | 10 -> Some Bocage
    | 11 -> Some Desert
    | 12 -> Some RoughDesert
    | 13 -> Some Escarpment
    | _ -> None

type Terrain = {TerrainId:int; TerrainType: TerrainType; ImageCoordinate: ImageCoordinate}

let isRiver(terrain:Terrain) =
    let terrain = terrainProvider.Terrains  |> Seq.find(fun t -> t.TerrainId = terrain.TerrainId)
    terrain.RiverInd

let isRoad(terrain:Terrain) =
    let terrain = terrainProvider.Terrains  |> Seq.find(fun t -> t.TerrainId = terrain.TerrainId)
    terrain.RoadInd

let getTerrainTypeInititaiveCap = function
| City -> 1
| Forest | Fortification | Bocage-> 3
| Swamp -> 4
| Port | Rough | RoughDesert -> 5
| Mountain | Escarpment -> 8
| _ -> 99

type TerrainCondition =
| Dry
| Muddy
| Frozen

let getTerrainCondition(id:int) =
    match id with
    | 0 -> Some Dry
    | 1 -> Some Muddy
    | 2 -> Some Frozen
    | _ -> None

let isDry = function 
| Dry -> true 
| _ -> false

let isNotDry = function 
| Dry -> false 
| _ -> true

type Tile = {
    TileId:int; Description: string;
    Nation: Nation; 
    GameBoardCoordinate: GameBoardCoordinate; 
    Terrain: Terrain; TerrainCondition: TerrainCondition}

let getTerrain(id: int) = 
    let terrain = terrainProvider.Terrains |> Seq.find(fun t -> t.TerrainId = id)
    let terrainType = getTerrainType(terrain.TerrainTypeId)

    match terrainType.IsSome with
    | true -> Some {TerrainId=id; TerrainType=terrainType.Value;ImageCoordinate = terrain.ImageXCoordinate, terrain.ImageYCoordinate}
    | false -> None

type TileName = {TileNameId: int; TileDescription: string}

let getTileName(id:int) =
    let tileName = tileNameProvider.TileNames |> Seq.find(fun tn -> tn.TileNameId = id)
    match tileName.TileDescription.IsSome with
    | true -> Some {TileNameId = tileName.TileNameId; TileDescription = tileName.TileDescription.Value}
    | false -> None

let isVictoryTile(tile: Tile) =
    let tile = scenarioTileProvider.ScenarioTiles  |> Seq.find(fun t -> t.ScenarioTileId = tile.TileId)
    tile.VictoryTileInd

let isSupplyTile(tile: Tile) =
    let tile = scenarioTileProvider.ScenarioTiles  |> Seq.find(fun t -> t.ScenarioTileId = tile.TileId)
    tile.SupplyTileInd

let isDeployTile(tile: Tile) =
    let tile = scenarioTileProvider.ScenarioTiles  |> Seq.find(fun t -> t.ScenarioTileId = tile.TileId)
    tile.DeployTileInd

let getTile(id: int) =
    let tile = scenarioTileProvider.ScenarioTiles  |> Seq.find(fun t -> t.ScenarioTileId = id)
    let terrain = getTerrain(tile.TerrainId)
    match terrain.IsSome with
    | true ->
        let nation = getNation(tile.NationId)
        let tileName = getTileName(tile.TileNameId)
        Some {TileId=id; Description=tileName.Value.TileDescription; 
        Nation=nation.Value; GameBoardCoordinate=tile.ColumnNumber, tile.RowNumber;
        Terrain=terrain.Value;TerrainCondition=TerrainCondition.Dry}
    | false -> None

getTile(1)
