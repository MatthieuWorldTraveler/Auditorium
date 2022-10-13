using UnityEngine;

public class RaycastMouse : MonoBehaviour
{
    [SerializeField] private LayerMask _layer;
    [SerializeField] private Texture2D _cursorMove, _cursorResize, _cursorNormal;
    [SerializeField] GameObject _activeEffector;

    CircleShape _circleShape;
    Camera _camera;
    bool _isMoving;
    Transform effector;
    Vector3 worldPosition;
    Vector3 dragOffest;

    private void Start()
    {
        _camera = GetComponent<Camera>();
    }

    private void Update()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 20, Color.red);

        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, 20f, _layer);

        if (hit.collider != null)
        {
            effector = hit.collider.GetComponent<Transform>();
            if (effector.CompareTag("MoveEffector"))
            {
                Cursor.SetCursor(_cursorMove, Vector2.zero, CursorMode.Auto);
                //If mouse click
                if (Input.GetButtonDown("Fire1"))
                {
                    _activeEffector = effector.parent.gameObject;
                    dragOffest = _activeEffector.transform.position - DragEffector();
                }
                if (Input.GetButton("Fire1") && _activeEffector != null)
                {
                    _activeEffector.transform.position = DragEffector() + dragOffest;
                }
                if (Input.GetButtonUp("Fire1"))
                    _activeEffector = null;
            }
            else if (effector.CompareTag("ResizeEffector"))
            {
                Cursor.SetCursor(_cursorResize, Vector2.zero, CursorMode.Auto);
                if (Input.GetButtonDown("Fire1"))
                {
                    _activeEffector = effector.gameObject;
                    dragOffest = _activeEffector.transform.position - DragEffector();
                    _circleShape = effector.GetComponent<CircleShape>();
                }
                if (Input.GetButton("Fire1") && _activeEffector != null)
                {
                    ScaleEffector();
                }
                if (Input.GetButtonUp("Fire1"))
                    _activeEffector = null;
            }
        }
        else
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    Vector3 DragEffector()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = _activeEffector.transform.position.z;
        return mousePos;
    }

    void ScaleEffector()
    {
        float scale = Vector2.Distance(_activeEffector.transform.position, DragEffector());
        //Debug.Log(_activeEffector.transform.position + " " + DragEffector() + " " + dragOffest);
        _circleShape.Radius = scale;
        _activeEffector.GetComponent<AreaEffector2D>().forceMagnitude = _circleShape.Radius * 4;
    }
}
