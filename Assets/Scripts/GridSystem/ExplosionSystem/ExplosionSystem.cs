using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class ExplosionSystem 
{
	static List<ExplodeType> explodeTypesToUse;
	public static bool IsAnyExplosionInTheSystem{ get { return explodeTypesToUse.Exists (x => x.IsAnyExplosionInTheSystem == true); } }

	static int gridYLength;

	#region Initialize
	public static void StartGame(List<ExplosionTypes> tS, int _gridYLength){
		gridYLength = _gridYLength;
		explodeTypesToUse = new List<ExplodeType> ();
		for (int i = 0; i < tS.Count; i++) {
			switch (tS[i]) {
			case ExplosionTypes.TriangleExplosion:
				explodeTypesToUse.Add (new TriangleExplode ());
				break;
			default:
				break;
			}
		}
	}
	#endregion

	public static IEnumerator Explode(){
		//patlayacak tüm grupları al
		List<IndexGroup> explodingGroups = giveEveryGroupThatGoingToExplodeInExplosionTypes();
		//patlayacak gruplarda
		visualProcessInTheExplodingGroups (explodingGroups);
		yield return GameSkeleton.Instance.StartCoroutine (spawnNewPiecesInTheMapAndRecorrectGridIndexes (divideExplodingGroupsInToColumns (explodingGroups)));
		yield return new WaitForSeconds (.1f);
		if (IsAnyExplosionInTheSystem) {
			GameSkeleton.Instance.StartCoroutine (ExplosionSystem.Explode ());
			yield break;
		}
		//Game over checks
		GameSkeleton.Instance.GameOverChecks ();
	}

	/// <summary>
	/// Spawns the new pieces ın the map and recorrectteds grid ındexes.
	/// </summary>
	/// <returns>The new pieces ın the map and recorrect grid ındexes.</returns>
	/// <param name="stunVeOStundakiPatlayanIndexler">Stun ve O stundaki patlayan ındexler.</param>
	static IEnumerator spawnNewPiecesInTheMapAndRecorrectGridIndexes (Dictionary<int, IndexGroup> stunVeOStundakiPatlayanIndexler) {
		List<List<GridPiece>> tumStunlarIcinNeedToMove = new List<List<GridPiece>> ();

		foreach (var item in stunVeOStundakiPatlayanIndexler) {
			item.Value.Values.Sort ();
			int insertIndex = ((item.Key + 1) * gridYLength);
			Vector2 enTepePos = GridSystem.GridMap [insertIndex - 1].transform.position;

			insertInTheTopOfTheStun (item.Value.Values.Count, insertIndex, enTepePos);
			destroyExplodedGridPieces (item.Value);
			tumStunlarIcinNeedToMove.Add (giveNeedsToMovesInTheStun (item.Value.Values [0], insertIndex));
		}
		yield return new WaitForSeconds (0.2f);
		yield return GameSkeleton.Instance.StartCoroutine (startMoveOnTheRows (tumStunlarIcinNeedToMove));
	}

	#region Column Row Processes
	/// <summary>
	/// Starts the move on the columns.
	/// </summary>
	/// <returns>The move on the columns.</returns>
	/// <param name="needToMove">Need to move.</param>
	static IEnumerator startMoveOnTheColumns(List<GridPiece> needToMove){
		List<Coroutine> lastMovedPieces = new List<Coroutine> ();
		for (int i = 0; i < needToMove.Count; i++) {	
			lastMovedPieces.Add (needToMove [i].StartCoroutine (needToMove [i].MoveToPosRoutine (GameSkeleton.gridSystem.GiveThePositionOfGridIndex (GameSkeleton.gridSystem.GridIndexOfaPiece (needToMove [i])))));
			yield return new WaitForSeconds (0.08f);
		}
		for (int i = 0; i < lastMovedPieces.Count; i++) {
			yield return lastMovedPieces [i];
		}
	}

	/// <summary>
	/// Starts the move on the rows.
	/// </summary>
	/// <returns>The move on the rows.</returns>
	/// <param name="allNeedsToMovesInTheRows">All needs to moves ın the rows.</param>
	static IEnumerator startMoveOnTheRows(List<List<GridPiece>> allNeedsToMovesInTheRows){
		List<Coroutine> lastMovedStuns = new List<Coroutine> ();
		for (int i = 0; i < allNeedsToMovesInTheRows.Count; i++) {
			lastMovedStuns.Add (GameSkeleton.Instance.StartCoroutine (startMoveOnTheColumns (allNeedsToMovesInTheRows [i])));
		}
		for (int i = 0; i < lastMovedStuns.Count; i++) {
			yield return lastMovedStuns [i];
		}
	}

	/// <summary>
	/// İnserts the ın the top of the stun.
	/// </summary>
	/// <param name="count">Count.</param>
	/// <param name="insertIndex">İnsert ındex.</param>
	/// <param name="enTepePos">En tepe position.</param>
	static void insertInTheTopOfTheStun (int count, int insertIndex, Vector2 enTepePos)
	{
		for (int i = 0; i < count; i++) {
			GridPiece temp = GameSkeleton.gridSystem.InstantiateGridPiece ();
			temp.transform.position = new Vector2 (enTepePos.x, enTepePos.y + (GameSkeleton.gridSystem.yPlusPos * i) + 5f);
			GridSystem.GridMap.Insert (insertIndex + i, temp);
			GameSkeleton.gridSystem.GiveAColorToTheCreatedHexagonalPiece (temp, true);
		}
	}

	/// <summary>
	/// Divides the exploding groups ın to columns.
	/// </summary>
	/// <returns>The exploding groups ın to columns.</returns>
	/// <param name="explodingGroups">Exploding groups.</param>
	static Dictionary<int, IndexGroup> divideExplodingGroupsInToColumns(List<IndexGroup> explodingGroups){
		//Tüm hepsinin stün ve satır larını, yeni parçalar oluşturup, üstünde kalan parçalarla beraber doğru index ve pozisyonlara koymak adına tut.
		Dictionary<int, IndexGroup> stunVeOStundakiPatlayanIndexler = new Dictionary<int, IndexGroup> ();
		for (int i = 0; i < explodingGroups.Count; i++) {
			for (int b = 0; b < explodingGroups[i].Values.Count; b++) {
				int stunIndex = (explodingGroups [i].Values [b]).GetColumnOfIndex (gridYLength);
				if (stunVeOStundakiPatlayanIndexler.ContainsKey (stunIndex)) {
					stunVeOStundakiPatlayanIndexler [stunIndex].Values.Add (explodingGroups [i].Values [b]);
				} else {
					stunVeOStundakiPatlayanIndexler.Add (stunIndex, new IndexGroup (explodingGroups [i].Values [b] ));
				}
			}
		}
		return stunVeOStundakiPatlayanIndexler;
	}

	/// <summary>
	/// Gives the needs to moves ın the stun.
	/// </summary>
	/// <param name="lowestIndexExplodedInThatStun">Lowest ındex exploded ın that stun.</param>
	/// <param name="insertIndex">İnsert ındex.</param>
	static List<GridPiece> giveNeedsToMovesInTheStun(int lowestIndexExplodedInThatStun, int insertIndex){
		List<GridPiece> needToMove = new List<GridPiece> ();
		for (int b = lowestIndexExplodedInThatStun; b < insertIndex; b++) {
			needToMove.Add (GridSystem.GridMap [b]);
		}
		return needToMove;
	}
	#endregion

	/// <summary>
	/// Visuals the process ın the exploding groups.
	/// </summary>
	/// <param name="explodingGroups">Exploding groups.</param>
	static void visualProcessInTheExplodingGroups (List<IndexGroup> explodingGroups)
	{
		for (int i = 0; i < explodingGroups.Count; i++) {
			for (int b = 0; b < explodingGroups [i].Values.Count; b++) {
				GridPiece tempPieceRef = GridSystem.GridMap [explodingGroups [i].Values [b]];
				//Tüm hepsini kaldırmadan önce kapatıp, görsele başla ve hesaplamaları çalıştır
				GameSkeleton.particleManager.ShowParticle (tempPieceRef);
				tempPieceRef.gameObject.SetActive (false);
			}
			Stats.scoreSystem.GroupExplodedAddScoreAndShowPlayer (explodingGroups [i].Values.Count, (Vector3)(explodingGroups [i].GiveMiddlePointOfIndexGroup ()) + Vector3.back);
		}
	}

	/// <summary>
	/// Destroies the exploded grid pieces.
	/// </summary>
	/// <param name="item">İtem.</param>
	static void destroyExplodedGridPieces (IndexGroup item)
	{
		for (int i = item.Values.Count - 1; i >= 0; i--) {
			GridPiece temp = GridSystem.GridMap [item.Values [i]];
			GridSystem.GridMap.RemoveAt (item.Values [i]);
			GameObject.Destroy (temp.gameObject);
		}
	}

	/// <summary>
	/// Gives the every group that going to explode ın explosion types.
	/// </summary>
	/// <returns>The every group that going to explode ın explosion types.</returns>
	static List<IndexGroup> giveEveryGroupThatGoingToExplodeInExplosionTypes(){
		List<IndexGroup> explodingGroups = new List<IndexGroup> ();
		if (explodeTypesToUse.Count > 0)
			explodingGroups.AddRange (explodeTypesToUse [0].GiveEveryGroupThatGoingToExplode ());
		for (int i = 1; i < explodeTypesToUse.Count; i++) {
			List<IndexGroup> temp = explodeTypesToUse [i].GiveEveryGroupThatGoingToExplode ();
			for (int x = 0; x < temp.Count; x++)
				if (explodingGroups.Exists (y => y.IsListTrianglesHaveAOneOrMoreSameElement (temp [x]))) {
					IndexGroup rf = explodingGroups [explodingGroups.FindIndex (y => y.IsListTrianglesHaveAOneOrMoreSameElement (temp [x]))];
					rf = rf.MergeGroupsToOneBiggerWithNoRepeatignElement (temp [x]);
				} else
					explodingGroups.Add (temp [x]);
		}
		return explodingGroups;
	}
}