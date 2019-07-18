using UnityEngine;
using System.Collections;

public class PieceParticle : MonoBehaviour 
{
	public void ShowParticle(Vector3 pos, Color c){
		transform.position = pos;
		var main = GetComponent<ParticleSystem>().main;
		Color forAlphaChange = c;
		forAlphaChange.a = 0.75f;
		main.startColor = forAlphaChange;
		StartCoroutine (waitForStop ());
	}

	IEnumerator waitForStop(){
		yield return new WaitForSeconds (1f);
		Destroy (gameObject);
	}
}
