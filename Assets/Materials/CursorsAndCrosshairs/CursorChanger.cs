using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorChanger : MonoBehaviour {

    public Texture2D cursor;

	void Start () 
	{
        Cursor.SetCursor(cursor, new Vector2(0, 0), CursorMode.Auto);
	}
}
