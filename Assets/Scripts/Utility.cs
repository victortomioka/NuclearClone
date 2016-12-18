using UnityEngine;
using System.Collections;

public static class Utility{

	public static void randomizeArray(Object[] arr)
	{
		for (var i = arr.Length - 1; i > 0; i--) {
			var r = Random.Range(0,i);
			var tmp = arr[i];
			arr[i] = arr[r];
			arr[r] = tmp;
		}
	}
}
