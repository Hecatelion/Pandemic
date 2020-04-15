using System.Collections;
using System.Collections.Generic;
using UnityEngine;

	public static class CustomMaths
	{
		static double epsilon = 0.001f;

		public static bool Approximately(Vector3 v, Vector3 _vec)
		{
			return Approximately(v.x, _vec.x) &&
				Approximately(v.y, _vec.y) &&
				Approximately(v.z, _vec.z);
		}

		public static bool Approximately(float _a, float _b)
		{
			double delta = _a - _b;

			return epsilon > delta && delta > -epsilon;
		}
	}
