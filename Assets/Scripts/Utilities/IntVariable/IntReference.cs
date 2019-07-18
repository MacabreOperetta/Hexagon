using UnityEngine;

[System.Serializable]
public class IntReference 
{
	[SerializeField]
	private bool UseConstant = false;
	[SerializeField]
	private int ConstantValue = 0;
	[SerializeField]
	private IntVariable Variable = null;

	public int Value { get{ return UseConstant ? ConstantValue : Variable.Value; } }
}
