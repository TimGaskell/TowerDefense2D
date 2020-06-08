using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapEditor : MonoBehaviour
{

    public MapGenerater mapGenerater;
    public Material testingMaterial;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            HandleInput();
            return;
        }
    }

    /// <summary>
    /// Main function which determines what action needs to be taken upon user input.
    /// </summary>
   void HandleInput() {

        Cell currentCell = GetCellUnderCursor();
        Debug.Log(currentCell.gameObject.name);
        EditCell(currentCell);
   }

    /// <summary>
    /// Determines which cell is currently being clicked on
    /// </summary>
    /// <returns> Cell that is being clicked on </returns>
    Cell GetCellUnderCursor() {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit)) {
            if(hit.transform.gameObject.tag == ("Cell")){
                return hit.transform.GetComponent<Cell>();
            }
        }
        return null;
    }

    /// <summary>
    /// Main function for editing the properties of a cell
    /// </summary>
    /// <param name="cell"> Cell cell being edited </param>
    void EditCell(Cell cell) {

        cell.GetComponent<MeshRenderer>().material = testingMaterial;
    }

  
}
