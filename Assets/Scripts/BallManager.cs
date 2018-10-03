using System.Collections.Generic;
using UnityEditor.Experimental.UIElements;
using UnityEditor.Experimental.UIElements.GraphView;
using UnityEngine;

public class BallManager : MonoBehaviour {

	[SerializeField] private Transform _ballRoot;
	[SerializeField] private GameObject _ballPrefab;
	
	private List<Transform> _ballTransforms;
	private List<Ball> _balls;
	
	public void Init(int ballCount)
	{
		_balls = new List<Ball>();
		_ballTransforms = new List<Transform>();
		
		for (int i = 0; i < ballCount; i++)
		{
			_ballTransforms.Add(Instantiate(_ballPrefab, _ballRoot).transform);
			_ballTransforms[i].name = "Ball" + (i + 1).ToString();
			_balls.Add(_ballTransforms[i].GetComponent<Ball>());
		}

		for (int i = 0; i < ballCount; i++)
		{
			_balls[i].Init();
		}
	}


	public Ball GetClosestBall(Transform t)
	{
		float minValue = float.PositiveInfinity;
		int minInd = -1;
		
		for (int i = 0; i < _ballTransforms.Count; i++)
		{
			float newValue = (_ballTransforms[i].position - t.position).magnitude;
			if (newValue < minValue)
			{
				minInd = i;
				minValue = newValue;
			}	
		}

		return _balls[minInd];
	}
	
}
