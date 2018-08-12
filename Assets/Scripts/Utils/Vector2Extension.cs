﻿using UnityEngine;

namespace Utils
{
	public static class Vector2Extension {
		 
		public static Vector2 Rotate(Vector2 v, float degrees) 
		{
			float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
			float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);
			 
			float tx = v.x;
			float ty = v.y;
			v.x = (cos * tx) - (sin * ty);
			v.y = (sin * tx) + (cos * ty);
			return v;
		}

		public static float GetAngle(Vector2 v)
		{
			float dot = v.normalized.x * 1 + 0;  
			float det = 0 - v.normalized.y * 1;
			float angle = Mathf.Rad2Deg * Mathf.Atan2(det, dot);
			return (angle + 360) % 360;
		}
	}
}
