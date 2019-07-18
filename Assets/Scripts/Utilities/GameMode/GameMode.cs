using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "GameMode01", menuName = "VERTIGO demo/Game Mode")]
public class GameMode : ScriptableObject 
{
	public int GridXLength, GridYLength;
	public List<Color> Colors;
	public IntReference BombPieceInstantiateEveryXPoint;

	[Header("For The Future elements")]
	public List<MapElementInfo> GridElements;
	public List<ExplosionTypes> ExplosionTypes;
}
	
[System.Serializable]
public class MapElementInfo{
	public List<GridPiece> GridPieces;
	public GameObject SelectorPrefab;
}

public enum ExplosionTypes {TriangleExplosion}