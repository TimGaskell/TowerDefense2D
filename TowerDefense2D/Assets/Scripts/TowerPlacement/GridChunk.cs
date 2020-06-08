using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridChunk : MonoBehaviour
{
    Cell[] cells;

	private void Awake() {
		cells = new Cell[GlobalDataBase.chunckSizeX * GlobalDataBase.chunckSizeY];
	}

	/// <summary>
	/// Adds a cell into this chunks list of cells it contains.
	/// </summary>
	/// <param name="index"> int index of cell based on its overall position in chunk </param>
	/// <param name="cell"> Cell cell being added to chunk </param>
	public void AddCell(int index, Cell cell) {
		cells[index] = cell;
		cell.chunk = this;
		cell.transform.SetParent(transform, false);

	}
}
