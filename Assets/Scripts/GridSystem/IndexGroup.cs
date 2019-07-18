using UnityEngine;
using System.Collections.Generic;

public class IndexGroup 
{
	public List<int> Values;

	#region Initializers
	public IndexGroup (){
		Values = new List<int> ();
	}

	public IndexGroup (IndexGroup _group){
		Values = new List<int> (_group.Values);
	}

	public IndexGroup (int _groupElement){
		Values = new List<int> ();
		Values.Add (_groupElement);
	}
	#endregion
}
