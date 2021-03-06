﻿using System.Collections.Generic;
using UnityEngine;

public class GameSettings
{
	public readonly int InitialPlayerCount = 8;
	public readonly float CircleRange = 15;
	public readonly float MinScore = 30;
	public readonly int BallCount = 1;

	public readonly List<Color> ColorList = new List<Color>()
	{
		new Color32(0x58, 0x56, 0xd6, 0xff),
		
		new Color32(0xff, 0x3b, 0x30, 0xff),
		
		new Color32(0x00, 0x7a, 0xff, 0xff),
		
		new Color32(0x4c, 0xd9, 0x64, 0xff),
				
		new Color32(0x34, 0xaa, 0xdc, 0xff),
		
		new Color32(0xff, 0x2d, 0x55, 0xff),
		
		new Color32(0x5a, 0xc8, 0xfa, 0xff),
		
		new Color32(0xff, 0x95, 0x00, 0xff)
	};
}
