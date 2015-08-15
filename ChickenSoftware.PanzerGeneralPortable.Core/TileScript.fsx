module TileScript

#load "GameScript.fsx"
open GameScript

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