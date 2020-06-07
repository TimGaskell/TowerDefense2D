using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerater : MonoBehaviour {

    int cellCountX, cellCountY;
    public Cell cellPrefab;
    public GameObject Map;
    Cell[] cells;

    public void Start() {
        CreateMap(10, 10);

    }
    public void CreateMap(int x, int y) {

        cellCountX = x;
        cellCountY = y;

        CreateCells();
    }

    void CreateCells() {

        cells = new Cell[cellCountX * cellCountY];

        for (int y = 0, i = 0; y < cellCountY; y++) {
            for (int x = 0; x < cellCountX; x++) {
                CreateCell(x, y, i++);
            }
        }

    }

    void CreateCell(int x, int y, int i) {
        Vector3 position;
        position.x = x;
        position.y = y;
        position.z = 0f;

        Cell cell = cells[i] = Instantiate<Cell>(cellPrefab);
        cell.gameObject.transform.parent = Map.transform;
        cell.transform.localPosition = position;
        cell.name = x + " " + y;

        Triangulate(cells[i]);

    }

    void Triangulate(Cell cell) {

        MeshRenderer renderer = cell.GetComponent<MeshRenderer>();
        renderer.sharedMaterial = new Material(Shader.Find("Standard"));
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
    }
}
