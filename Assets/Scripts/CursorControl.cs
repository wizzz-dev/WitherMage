using UnityEngine;

public class CursorControl : MonoBehaviour
{
    public Transform cursorImage;
    public Vector3 displacement= new Vector3(23,-37,0);
    private void Start()
    {
        Cursor.visible = false;
    }
    private void Update()
    {
        cursorImage.position = Input.mousePosition + displacement;
    }

}
