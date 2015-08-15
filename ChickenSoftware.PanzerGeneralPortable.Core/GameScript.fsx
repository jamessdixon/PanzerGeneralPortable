module GameScript

#load "ProviderScript.fsx"
open ProviderScript

type CampaignStepType =
| MajorVictory
| MinorVictory
| Loss

type ObjectiveType =
| AxisAttack
| AxisDefend

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
