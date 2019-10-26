using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript9 : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	private Vector3 m_screenPos;
	/// <summary>
	/// WolrdPostionToRectTransfromToWorldPos(m_Target.position, m_Trans, UI_Camera222.Instance.camera);
	/// </summary>
	/// <param name="worldPos"> ui要跟着的世界坐标空间  </param>
	/// <param name="rect"> ui 对象 </param>
	/// <param name="uiCamera">ui 相机 </param>
	public void WolrdPostionToRectTransfromToWorldPos(Vector3 worldPos, RectTransform rect, Camera uiCamera)
	{
		Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
		if (m_screenPos == screenPos) return;
		m_screenPos = screenPos;
		rect.gameObject.transform.position = uiCamera.ScreenToWorldPoint(screenPos);
		Vector3 localPos = rect.gameObject.transform.localPosition;
		localPos.z = 0;
		rect.gameObject.transform.localPosition = localPos;
	}


	void Update()
	{
//

	}

	// Update is called once per frame

}
