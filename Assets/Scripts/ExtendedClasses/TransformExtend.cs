using UnityEngine;

public static class TransformExtend 
{
	/// <summary>
	/// The the angle of position relative to this transform.
	/// </summary>
	/// <returns>The angle of position relative to this transform.</returns>
	/// <param name="piece">Piece.</param>
	/// <param name="pos">Position.</param>
	public static float TheAngleOfPositionRelativeToThisTransform(this Transform piece, Vector2 pos){
		//objectin merkezinin position dan farkını al
		float xDiff = pos.x - piece.position.x;
		float yDiff = pos.y - piece.position.y;

		float angle = Mathf.Atan2(yDiff, xDiff) * 180.0f / Mathf.PI;
		//correction with 2PI, if its neccessary
		if (angle < 0){angle += 360f;}
		return angle;
	}
}
