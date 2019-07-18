using UnityEngine;
using System.Collections.Generic;

public class GridSystem : MonoBehaviour 
{
	#region Variables
	internal static List<GridPiece> GridMap;

	internal static List<IndexGroup> EverySelectableTriangleInGridSystem;

	int gridXLength, gridYLength, bombPieceInstantiateEveryXPoint;
	Color[] colors;
	#endregion
	#region Initialize
	void Awake(){
		HexagonalPiecePrefab = Resources.Load ("Prefabs/Hexagonal2") as GameObject;
		BombPiecePrefab = Resources.Load ("Prefabs/Bomb") as GameObject;
	}

	public void StartGame(int _gridXLength, int _gridYLength, int _bombPieceInstantiateEveryXPoint, Color[] _colors){
		gridXLength = _gridXLength;
		gridYLength = _gridYLength;
		bombPieceInstantiateEveryXPoint = _bombPieceInstantiateEveryXPoint;
		colors = _colors;
		EverySelectableTriangleInGridSystem = new List<IndexGroup> ();
		createdBombPieceCount = 0;
		gridCreation ();
	}
	#endregion
	#region Grid Piece Creations
	internal float yPlusPos, OffsetX, xPlusPos, OffsetY, oneSideScale;

	#region Create Grid Map
	void gridCreation(){
		oneSideScale = gridYLength < gridXLength + 2 ? findOneSideScaleOfPieceObjects (gridXLength) : findOneSideScaleOfPieceObjects (gridYLength);
		giveScalesOfPieces ();
		xPlusPos = oneSideScale / 2f + oneSideScale / 4f;
		yPlusPos = 2f * oneSideScale / 4f * Mathf.Sqrt (3f);
		OffsetY = (gridYLength * yPlusPos + (yPlusPos / 2f)) * oneSideScale / 2f;
		OffsetX = ((gridXLength * (3f * oneSideScale / 4f) + oneSideScale / 4f) - oneSideScale) / 2f;
		Vector3 position = Vector3.zero, rotation = HexagonalPiecePrefab.transform.rotation.eulerAngles;
		GridMap = new List<GridPiece> ();
		for (int i = 0; i < gridXLength; i++) {
			for (int b = 0; b < gridYLength; b++) {
				position = GiveThePositionOfGridIndex ((gridYLength * i) + b);
				GridPiece gridPiece = InstantiateGridPiece ();
				GridMap.Add (gridPiece);
				gridPiece.transform.localPosition = position;
				gridPiece.transform.rotation = Quaternion.Euler (rotation);
				GiveAColorToTheCreatedHexagonalPiece (gridPiece, false);
			} 
		}
	}

	float findOneSideScaleOfPieceObjects(float xOryLength){
		return 4.8f / (Mathf.Clamp (xOryLength, 7, 100) * 3f * 1f / 4f + 1f / 4f);
	}

	void giveScalesOfPieces(){
		HexagonalPiecePrefab.transform.localScale = new Vector3 (oneSideScale, oneSideScale, 1f);
		BombPiecePrefab.transform.localScale = new Vector3 (oneSideScale * 5f / 6f, oneSideScale * 5f / 6f, 1f);
	}
	#endregion
	#region Grid Piece Manage
	static GameObject HexagonalPiecePrefab, BombPiecePrefab;

	static int createdBombPieceCount;

	public GridPiece InstantiateGridPiece(){
		if ((float)(createdBombPieceCount + 1) <= ((float)Stats.scoreSystem.score / bombPieceInstantiateEveryXPoint)) {
			createdBombPieceCount++;
			return Instantiate (BombPiecePrefab, transform).GetComponent<GridPiece> ();
		}
		return Instantiate (HexagonalPiecePrefab, transform).GetComponent<GridPiece> ();
	}

	public void destroyEveryPieceInMap(bool showParticle){
		for (int x = 0; x < GridMap.Count; x++) {
			if (showParticle)
				GameSkeleton.particleManager.ShowParticle(GridMap [x]);
			Destroy (GridMap [x].gameObject);
		}
	}
	#endregion
	#region Give Color To a Piece
	public void GiveAColorToTheCreatedHexagonalPiece (GridPiece gridPiece, bool random){
		IColored cPiece = gridPiece.GetComponent<IColored> ();
		if (cPiece == null)
			return;
		int stunIndex = (GridMap.Count - 1).GetColumnOfIndex (gridYLength);
		int satırIndex = (GridMap.Count - 1).GetRowOfIndex (gridYLength);
		if (random || (stunIndex == 0) || ((stunIndex % 2 != 0) && (satırIndex == 0))) {
			int cI = Random.Range (0, colors.Length);
			cPiece.ChangeColor (cI, colors [cI]);
			return;
		}
		List<IndexGroup> groupHolder = SelectableTrianglesOfaGridPiece (gridPiece);
		int randomColorIndex = Random.Range(0,colors.Length);
		cPiece.ColorIndexOfThisPiece = randomColorIndex;
		if (groupHolder.CheckForGroupElementsHaveAGivenCountOfDifferentColor (1) == true) {
			for (int i = 0; i < colors.Length - 1; i++) {
				randomColorIndex++;
				if (randomColorIndex >= colors.Length) {	randomColorIndex -= colors.Length;	}
				cPiece.ColorIndexOfThisPiece = randomColorIndex;
				if (groupHolder.CheckForGroupElementsHaveAGivenCountOfDifferentColor (1) == false)
					break;
			}
		}
		cPiece.ChangeColor (randomColorIndex, colors [randomColorIndex]);
		return;
	}
	#endregion
		
	#endregion
	#region Funcs

	#region Is Any Other Move Exist In Grid System
	public bool IsAnyOtherMoveExist(){
		if (GridSystem.EverySelectableTriangleInGridSystem.CheckForGroupElementsHaveAGivenCountOfDifferentColor (2)) {
			List<IndexGroup> every_TripleGroup_That_Have_A_2_Same_Colored = GridSystem.EverySelectableTriangleInGridSystem.FindAndGiveEveryGroupThatHaveAGivenCountOfColor (2, false);

			for (int i = 0; i < every_TripleGroup_That_Have_A_2_Same_Colored.Count; i++) {
				int grid_Index_of_a_Different_Colored = every_TripleGroup_That_Have_A_2_Same_Colored [i].GiveGridIndexOfADifferentColoredInTheGroup ();
				int color_Index_Of_a_same_Coloreds = every_TripleGroup_That_Have_A_2_Same_Colored [i].GiveColorIndexOfAMultipleSameColoredsInTheGroup ();

				IndexGroup every_Surrounding_Piece_of_Different_Colored = SelectableTrianglesOfaGridPiece (GridMap [grid_Index_of_a_Different_Colored]).MergeGroupsToOneBiggerWithNoRepeatignElement ();
				int wanted_Color_Count_in_Surroundings = 0;

				for (int b = 0; b < every_Surrounding_Piece_of_Different_Colored.Values.Count; b++) {
					if (color_Index_Of_a_same_Coloreds == GridMap [every_Surrounding_Piece_of_Different_Colored.Values [b]].GetComponent<IColored> ().ColorIndexOfThisPiece) {
						wanted_Color_Count_in_Surroundings++;
					}
				}

				if (wanted_Color_Count_in_Surroundings > 2) {
					return true;
				}
			}
		}
		GameSkeleton.gridSystem.destroyEveryPieceInMap (false);
		GameSkeleton.Instance.GameOver ();
		return false;
	}

	#endregion

	public Vector2 GiveThePositionOfGridIndex(int index){
		Vector2 position = Vector2.zero;
		int stunIndex = index.GetColumnOfIndex (gridYLength), satırIndex = index.GetRowOfIndex(gridYLength);
		position.x = stunIndex * xPlusPos - OffsetX;
		position.y = stunIndex % 2 == 0 ? satırIndex * yPlusPos : (satırIndex * yPlusPos - yPlusPos / 2f);
		position.y -= OffsetY;
		return position;
	}

	public int GridIndexOfaPiece(GridPiece piece){
		return GridMap.FindIndex (x => x == piece);
	}

	public List<IndexGroup> SelectableTrianglesOfaGridPiece(GridPiece tempMapPiece){
		List<IndexGroup> groupHolder = new List<IndexGroup> ();
		for (int i = 0; i < 6; i++) {
			float angle = (45f + 60f * i);
			int xMultiplierForSecondAndThirdAreas = ((angle / 90f > 1) && (angle / 90f < 4)) ? -1 : +1;
			int yMultiplierForThirdAndFourthAreas = (angle / 90f > 2) ? -1 : +1;
			float xOfVector = Mathf.Sin (angle) * xMultiplierForSecondAndThirdAreas / 2f;
			//The y-axis of this direction is the sinus of this angle
			float yOfVector = Mathf.Cos (angle) * yMultiplierForThirdAndFourthAreas / 2f;

			IndexGroup group = GameSkeleton.gridSystem.GiveClosestIndexesOfAGridPieceInGridSystem (tempMapPiece, tempMapPiece.transform.position + new Vector3 (xOfVector, yOfVector, 0f), 2);
			if (!group.IsHolderContainsThisList(groupHolder)) {
				groupHolder.Add (group);
				if (!group.IsHolderContainsThisList(EverySelectableTriangleInGridSystem)) {
					EverySelectableTriangleInGridSystem.Add (group);
				}
			}
		}
		return groupHolder;
	}

	public IndexGroup GiveClosestIndexesOfAGridPieceInGridSystem (GridPiece ClickedPiece, Vector3 Pos, int countToGive){
		IndexGroup selectedIndexex = new IndexGroup ();

		List<GridPiece> excludedPieces = new List<GridPiece> ();
		excludedPieces.Add (ClickedPiece);
		Vector2 mPoint = (new List<Vector2>(	new Vector2[]{ Pos, ClickedPiece.transform.position }	)).giveMiddePointOfVectors();

		for (int i = 0; i < countToGive; i++) {
			int closestIndex = 0;
			float closestPoint = 100f;
			for (int b = 0; b < GridMap.Count; b++) {
				if (!excludedPieces.Contains (GridMap [b])) {
					if (Vector2.Distance (GridMap [b].transform.position, mPoint) < closestPoint) {
						closestIndex = b;
						closestPoint = Vector2.Distance (GridMap [b].transform.position, mPoint);
					}
				}
			}
			selectedIndexex.Values.Add (closestIndex);
			excludedPieces.Add (GridMap [closestIndex]);
			List<Vector2> tempArray = new List<Vector2> (new Vector2[]{ Pos, ClickedPiece.transform.position });
			for (int c = 0; c < excludedPieces.Count; c++) {
				tempArray.Add (excludedPieces [c].transform.position);
			}
			mPoint = tempArray.giveMiddePointOfVectors ();
		}

		selectedIndexex.Values.Add (GridIndexOfaPiece (ClickedPiece));
		return selectedIndexex;
	}
	#endregion
}