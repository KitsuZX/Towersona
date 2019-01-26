using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    public int levelWidth = 10;
    public int levelHeigth = 10;
    public Tile[,] tiles;

    [Header("References")]
    [SerializeField]
    private GameObject tilePrefab;
    [SerializeField]
    private Transform worldTransform;
    [SerializeField]
    private Texture2D[] grassTextures;
    [SerializeField]
    private Texture2D[] pathTextures;

    private List<PathDirection> path;
    private Tile currentTile;
    private List<GameObject> controlPoints;

    private void Awake() {
        tiles = new Tile[levelWidth, levelHeigth];
        path = new List<PathDirection>();
        controlPoints = new List<GameObject>();
    }

    public void GenerateWorld() {
        for (int i = 0, num = 0; i < levelWidth; i++)
        {
            for (int j = 0; j < levelHeigth; j++, num++)
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
                tiles[i, j] = t;
                ChangeToRandomTexture(t);
            }
        }
    }

    public void GeneratePath() {
        //TODO: esto pero de manera procedural

        //Elige una celda de inicio aleatoria
        int beginTile = Random.Range(0, levelHeigth);

        currentTile = tiles[0, beginTile];
        currentTile.isPath = true;
        ChangeToRandomTexture(currentTile);

        int iteration = 0;
  

        //Mientras no hayamos llegado a la última columna
        while (true)
        {         
            if(currentTile.position.x == levelWidth - 1)
            {
                Debug.Log("Has llegado al final del camino!");
                break;
            }

            //Elegir una dirección aleatoria
            PathDirection nextDirection = ChooseRandomDirection(currentTile, iteration);
            MoveDirection(nextDirection);      

            iteration++;        
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
            path.Add(nextDirection);
            return nextDirection;
        }

        prevDirection = path[path.Count - 1];

        //Comprobamos si estamos en un borde
        //Borde de abajo
        if (tile.position.y == 0)
        {
            posibleDirections.Remove(PathDirection.Down);
        }

        //Borde de arriba
        if(tile.position.y == levelHeigth - 1)
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
        if (path.Count > 2)
        {            
            PathDirection prev2Direction = path[path.Count - 2];
            if (prev2Direction == PathDirection.Up)
            {
                posibleDirections.Remove(PathDirection.Down);
            }

            if (prev2Direction == PathDirection.Down)
            {
                posibleDirections.Remove(PathDirection.Up);
            }
        }

        posibleDirections.Add(prevDirection);
        posibleDirections.Add(prevDirection);
        int num = Random.Range(0, posibleDirections.Count);

        nextDirection = posibleDirections[num];                 
        path.Add(nextDirection);

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

        Tile t = tiles[(int)currentTile.position.x, (int)currentTile.position.y];
        currentTile = t;
        t.isPath = true;
        ChangeToRandomTexture(t);
    }

    private void ChangeToRandomTexture(Tile tile)
    {
        Texture2D randomTexture;

        if (tile.isPath)
        {
            int num = Random.Range(0, pathTextures.Length);
            randomTexture = pathTextures[num];
        }
        else
        {
            int num = Random.Range(0, grassTextures.Length);
            randomTexture = grassTextures[num];
        }

        tile.ChangeTexture(randomTexture);
    }

    private void SetControlPoint(Tile tile)
    {
        GameObject controlPoint = new GameObject("ControlPoint " + controlPoints.Count);
        controlPoint.transform.position = tile.transform.position;
        controlPoint.transform.SetParent(tile.transform);
        controlPoints.Add(controlPoint);
    }

    private void OnDrawGizmos()
    {
        if (Application.IsPlaying(this))
        {
            for (int i = 0; i < controlPoints.Count; i++)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawSphere(controlPoints[i].transform.position, 0.2f);
            }
        }
    }
    public enum PathDirection{
       Up = 0,
       Right = 1,
       Down = 2,
       
    }
}
