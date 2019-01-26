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
    private GameObject tilePrefab = null;
    [SerializeField]
    private Transform worldTransform = null;
    [SerializeField]
    private Material pathMaterial = null;

    private List<PathDirection> path;
    private Tile currentTile;

    private void Awake() {
        tiles = new Tile[levelWidth, levelHeigth];
        path = new List<PathDirection>();        
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
                GameObject tile = Instantiate(tilePrefab, position, Quaternion.identity);

                tile.name = "Tile " + num;
                tile.transform.SetParent(worldTransform);
                
                Tile t = tile.GetComponent<Tile>();
                t.position = new Vector2(i, j);
                tiles[i, j] = t;
            }
        }
    }

    public IEnumerator GeneratePath() {
        //TODO: esto pero de manera procedural

        //Elige una celda de inicio aleatoria
        int beginTile = Random.Range(0, levelHeigth);

        currentTile = tiles[0, beginTile];
        currentTile.MakePath();

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
            yield return new WaitForSeconds(0.1f);
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

        if (path.Count > 2)
        {
            //Evitamos "cuadrados"
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


        int num = Random.Range(0, posibleDirections.Count);

        nextDirection = posibleDirections[num];                 
        path.Add(nextDirection);

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
        t.MakePath();
    }

    public enum PathDirection{
       Up = 0,
       Right = 1,
       Down = 2,
       
    }
}
