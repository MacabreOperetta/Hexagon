using UnityEngine;

public class ParticleManager : MonoBehaviour, IParticleManager 
{
	internal GameObject PieceParticlePrefab;

	public Color[] Colors{ get; private set;}

	void Awake(){
		PieceParticlePrefab = Resources.Load ("Prefabs/pieceExplosion") as GameObject;
	}

	public void StartGame(Color[] _colors){
		Colors = _colors;
	}

	public void ShowParticle(GridPiece explodedPiece){
		if (explodedPiece.GetComponent<IColored> () != null) {
			Instantiate (PieceParticlePrefab).GetComponent<PieceParticle> ().ShowParticle (explodedPiece.transform.position + Vector3.back, Colors [explodedPiece.GetComponent<IColored> ().ColorIndexOfThisPiece]);
		}
	}
}
