using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{  
    [Header("References")]
    [SerializeField]
    private GameObject tilePrefab;
    [SerializeField]
    private GameObject pathTypes;
    [SerializeField]
    private Transform worldTransform;
    [SerializeField]
    private Texture2D[] grassTextures;    

    private List<PathDirection> directionsPath;
    private List<Tile> tilesPath;
    private Tile currentTile;

    private WavesController wavesController;

    private void Awake() {
        tilesPath = new List<Tile>();
        directionsPath = new List<PathDirection>();    
        wavesController = GetComponent<WavesController>();               
    }

    public void GenerateWorld() {
        for (int i = 0, num = 0; i < World.Instance.levelWidth; i++)
        {
            for (int j = 0; j < World.Instance.levelHeigth; j++, num++)
            {
                Vector3 position;
                position.x = i + 0.5f;
                position.y = 0;
                position.z = j + 0.5f;
                GameObject tile = Instantiate(tilePrefab, position, Quaternion.Euler(0f, 180f, 0f));

                tile.name = "Tile " + num;
                tile.transform.SetParent(worldTransform);         
                
                Tile t = tile.GetComponent<Tile>();
                t.position = new Vector2(i, j);
                ChangeToRandomTexture(t);
                World.Instance.tiles[i, j] = t;              
            }
        }
    }

    public void GeneratePath() {
        //TODO: esto pero de manera procedural

        //Elige una celda de inicio aleatoria
        int beginTile = Random.Range(0, World.Instance.levelHeigth);

        currentTile = World.Instance.tiles[0, beginTile];
        currentTile.isPath = true;

        tilesPath.Add(currentTile);

        wavesController.SetSpawnPoint(currentTile.transform);

        int iteration = 0;
  

        //Mientras no hayamos llegado a la última columna
        while (true)
        {         
            //Hemos llegado al final
            if(currentTile.position.x == World.Instance.levelWidth - 1)
            {
                SetControlPoint(currentTile);
                break;
            }

            //Elegir una dirección aleatoria
            PathDirection nextDirection = ChooseRandomDirection(currentTile, iteration);
            MoveDirection(nextDirection);      

            iteration++;        
        }

        //Una vez creado el camino, asginamos sus texturasç
        for(int i = 0; i < tilesPath.Count; i++)
        {
            ChangeToRandomPathTexture(i);
        }
    }

    private PathDirection ChooseRandomDirection(Tile tile, int iteration)
    {
        PathDirection nextDirection;
        PathDirection prevDirection;

        List<PathDirection> posibleDirections = new List<PathDirection>();
        posibleDirections.Add(PathDirection.Up);
        posibleDirections.Add(PathDirection.Right);
        posibleDirections.Add(PathDirection.Down);
        posibleDirections.Add(PathDirection.Right);

        //Primera iteración
        if (iteration == 0)
        {
            nextDirection = PathDirection.Right;
            directionsPath.Add(nextDirection);           
            return nextDirection;
        }

        prevDirection = directionsPath[directionsPath.Count - 1];

        //Comprobamos si estamos en un borde
        //Borde de abajo
        if (tile.position.y == 0)
        {
            posibleDirections.Remove(PathDirection.Down);
        }

        //Borde de arriba
        if(tile.position.y == World.Instance.levelHeigth - 1)
        {
            posibleDirections.Remove(PathDirection.Up);
        }

        //Comprobamos que no se hagan movimientos opuestos
        if(prevDirection == PathDirection.Up)
        {
            posibleDirections.Remove(PathDirection.Down);
        }

        if (prevDirection == PathDirection.Down)
        {
            posibleDirections.Remove(PathDirection.Up);
        }

        //Evitamos "cuadrados"
        if (directionsPath.Count > 2)
        {            
            PathDirection prev2Direction = directionsPath[directionsPath.Count - 2];
            if (prev2Direction == PathDirection.Up)
            {
                posibleDirections.Remove(PathDirection.Down);
            }

            if (prev2Direction == PathDirection.Down)
            {
                posibleDirections.Remove(PathDirection.Up);
            }
        }
        
        int num = Random.Range(0, posibleDirections.Count);

        nextDirection = posibleDirections[num];                 
        directionsPath.Add(nextDirection);

        //Si vamos a girar, establecemos un control point
        if(prevDirection != nextDirection)
        {
            SetControlPoint(tile);
        }

        return nextDirection;
    }

    private void MoveDirection(PathDirection direction)
    {
        switch (direction)
        {
            case PathDirection.Up:
                currentTile.position.y += 1;
                break;
            case PathDirection.Right:
                currentTile.position.x += 1;
                break;
            case PathDirection.Down:
                currentTile.position.y -= 1;
                break;
        }

        Tile t = World.Instance.tiles[(int)currentTile.position.x, (int)currentTile.position.y];
        currentTile = t;
        t.isPath = true;
        tilesPath.Add(t);
    }

    private void ChangeToRandomTexture(Tile tile)
    {
        Texture2D randomTexture;
    
        int num = Random.Range(0, grassTextures.Length);
        randomTexture = grassTextures[num];     

        tile.ChangeTexture(randomTexture);
    }


    private void ChangeToRandomPathTexture(int tileIndex)
    {
        PathTypes type;
        Texture2D texture;

        PathDirection prevDirection, nextDirection;

        if (tileIndex == 0)
        {
            type = PathTypes.LeftRight;
            texture = ChooseRandomPathType(type);
            tilesPath[tileIndex].ChangeTexture(texture);
            return;
        }

        prevDirection = directionsPath[tileIndex - 1];

        if (tileIndex == tilesPath.Count - 1)
        {          
            nextDirection = PathDirection.Right;
        }
        else
        {         
            nextDirection = directionsPath[tileIndex];
        }

        if (prevDirection == PathDirection.Down && nextDirection == PathDirection.Down) {
            type = PathTypes.UpDown;
        }
        else if(prevDirection == PathDirection.Right && nextDirection == PathDirection.Right)
        {
            type = PathTypes.LeftRight;
        }
        else if (prevDirection == PathDirection.Down && nextDirection == PathDirection.Right)
        {
            type = PathTypes.UpRight;
        }
        else if (prevDirection == PathDirection.Up && nextDirection == PathDirection.Right)
        {
            type = PathTypes.DownRight;
        }
        else if (prevDirection == PathDirection.Right && nextDirection == PathDirection.Up)
        {
            type = PathTypes.LeftUp;
        }
        else if (prevDirection == PathDirection.Up && nextDirection == PathDirection.Up)
        {
            type = PathTypes.UpDown;
        }
        else
        {
            type = PathTypes.LeftDown;
        }

        texture = ChooseRandomPathType(type);
        tilesPath[tileIndex].ChangeTexture(texture);
    }

    private Texture2D ChooseRandomPathType(PathTypes type)
    {
        Transform child = pathTypes.transform.GetChild((int)type);
        PathTileType pathType = child.GetComponent<PathTileType>();
        int num = Random.Range(0, pathType.possibleTextures.Length);

        return pathType.possibleTextures[num];
    }

    private void SetControlPoint(Tile tile)
    {
        GameObject controlPoint = new GameObject("ControlPoint " + World.Instance.controlPoints.Count);
        controlPoint.transform.position = tile.transform.position;
        controlPoint.transform.SetParent(tile.transform);
        World.Instance.controlPoints.Add(controlPoint.transform);
    }

    public enum PathDirection{
       Up = 0,
       Right = 1,
       Down = 2,       
    }

    public enum PathTypes
    {
        UpDown, LeftRight, DownRight, UpRight, LeftUp, LeftDown
    }
}
