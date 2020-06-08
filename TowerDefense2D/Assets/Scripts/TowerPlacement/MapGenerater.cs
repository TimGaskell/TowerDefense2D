using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script responsible for the spawning of the tile map. Creates individual cells through primitive meshes and assigns them into chunks.
/// </summary>


public class MapGenerater : MonoBehaviour {

    int cellCountX, cellCountY;
    int chunkCountX, chunkCountY;
    Transform[] columns;
    GridChunk[] chunks;
    Cell[] cells;

    public Cell cellPrefab;
    public GridChunk chunkPrefab;
    public GameObject Map;
   

    public void Start() {
        CreateMap(10, 10);

    }

    /// <summary>
    /// Main method used for generating the tile map into the scene. Creates it in the dimensions of the inputed X and Y.
    /// </summary>
    /// <param name="x"> int amount of tiles in the X </param>
    /// <param name="y"> int amount of tiles in the Y </param>
    public void CreateMap(int x, int y) {

        cellCountX = x;
        cellCountY = y;

        chunkCountX = cellCountX / GlobalDataBase.chunckSizeX;
        chunkCountY = cellCountY / GlobalDataBase.chunckSizeY;

        CreateChunks();
        CreateCells();
    }

    /// <summary>
    /// Creates empty game objects for Columns and Chunks for the cells to be assigned to.
    /// Creates as many chunks as needed in the X and Y direction to fit all cells in.
    /// </summary>
    void CreateChunks() {

        columns = new Transform[chunkCountX];

        for (int x = 0; x < chunkCountX; x++) {
            columns[x] = new GameObject("Column").transform;
            columns[x].SetParent(transform, false);
        }

        chunks = new GridChunk[chunkCountX * chunkCountY];
        for (int z = 0, i = 0; z < chunkCountY; z++) {
            for (int x = 0; x < chunkCountX; x++) {
                GridChunk chunk = chunks[i++] = Instantiate(chunkPrefab);
                chunk.transform.SetParent(columns[x], false);
            }
        }

    }

    /// <summary>
    /// Creates the array size of how many cells will be needed. Loops through and creates a new cell with its own index.
    /// </summary>
    void CreateCells() {

        cells = new Cell[cellCountX * cellCountY];

        for (int y = 0, i = 0; y < cellCountY; y++) {
            for (int x = 0; x < cellCountX; x++) {
                CreateCell(x, y, i++);
            }
        }
    }

    /// <summary>
    /// Function used for instantiating the cell into the scene and assigning its coordinates, index and name.
    /// </summary>
    /// <param name="x"> int x position of cell </param>
    /// <param name="y"> int y position of cell </param>
    /// <param name="i"> int index of cell in cells array </param>
    void CreateCell(int x, int y, int i) {
        Vector3 position;
        position.x = x;
        position.y = y;
        position.z = 0f;

        Cell cell = cells[i] = Instantiate<Cell>(cellPrefab);
        cell.transform.localPosition = position;
        cell.name = x + " " + y;
        cell.index = i;

        Triangulate(cells[i]);

        AddCellToChunk(x, y, cell);
    }

    /// <summary>
    /// Function responsible for creating the Cell mesh by creating two triangles to form a square. 
    /// </summary>
    /// <param name="cell"></param>
    void Triangulate(Cell cell) {

        MeshRenderer renderer = cell.GetComponent<MeshRenderer>();
        renderer.sharedMaterial = new Material(Shader.Find("Standard")); //Assign a default material on start
        MeshFilter filter= cell.GetComponent<MeshFilter>();
        
        Mesh mesh = new Mesh();

        Vector3[] vertices = new Vector3[4] {
            new Vector3(0 , 0,0),
            new Vector3(GlobalDataBase.CellWidth ,0,0),
            new Vector3(0, GlobalDataBase.CellHeight,0),
            new Vector3( GlobalDataBase.CellWidth, GlobalDataBase.CellHeight,0),
        };
        mesh.vertices = vertices;

        int[] tris = new int[6] {
            0,2,1, //Lower left triangle
            2,3,1  //Upper Right Triangle
        };
        mesh.triangles = tris;

        Vector3[] normals = new Vector3[4] {
            -Vector3.forward,
            -Vector3.forward,
            -Vector3.forward,
            -Vector3.forward,
        };
        mesh.normals = normals;

        Vector2[] uv = new Vector2[4] {
            new Vector2(0,0),
            new Vector2(1,0),
            new Vector2(0,1),
            new Vector2(1,1)
        };
        mesh.uv = uv;

        filter.mesh = mesh;

        cell.AssignCollider(mesh);
    }

    /// <summary>
    /// Assigns a cell to a chunk Dependant on its location and the chunk size.
    /// </summary>
    /// <param name="x"> int x location of cell </param>
    /// <param name="y"> int y location of cell </param>
    /// <param name="cell"> Cell cell being assigned</param>
    void AddCellToChunk(int x, int y, Cell cell) {

        int chunkX = x / GlobalDataBase.chunckSizeX;
        int chunkZ = y / GlobalDataBase.chunckSizeY;
        GridChunk chunk = chunks[chunkX + chunkZ * chunkCountX];

        int localX = x - chunkX * GlobalDataBase.chunckSizeX; //localX coordinate based on chunk size
        int localY = y - chunkZ * GlobalDataBase.chunckSizeY; //localY coordinate based on chunk size
        chunk.AddCell(localX + localY * GlobalDataBase.chunckSizeX, cell);

    }


}
