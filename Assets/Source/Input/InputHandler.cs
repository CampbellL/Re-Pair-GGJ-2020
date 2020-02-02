using System.Collections.Generic;
using Source;
using Source.Animals;
using UniRx;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private bool _dragging;
    private Stack<LineRenderer> _renderers;
    private Collider2D _startHit;
    private GameObject _objectToClone;
    private Camera _mainCamera;
    private Collider2D _endHit;

    // Start is called before the first frame update
    void Start()
    {
        _mainCamera = Camera.main;
        _objectToClone = new GameObject("Line");
        _renderers = new Stack<LineRenderer>();
        var mouseUpObservable = Observable.EveryUpdate()
            .Where(_ => Input.touchCount == 1)
            .Where(_ => Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetTouch(0).phase == TouchPhase.Canceled)
            .Where(_ => _startHit != null && _startHit.GetComponent<Animal>().connectedObject == null);

        var clickObservable = Observable.EveryUpdate()
            .Where(_ => Input.touchCount == 1)
            .Where(_ => Input.GetTouch(0).phase == TouchPhase.Began)
            .Where(_ =>
            {
                Vector2 mousePos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
                _startHit = Physics2D.OverlapCircle(mousePos, 1, LayerMask.GetMask("Animal"));
                return _startHit != null;
            })
            .Where(_ => _startHit.GetComponent<Animal>().connectedObject == null);
     

        clickObservable.Subscribe(element =>
        {
            _dragging = true;
            GameObject tempObject = Instantiate(_objectToClone, this.transform);
            _renderers.Push(tempObject.AddComponent<LineRenderer>());
            _renderers.Peek().enabled = false;
            Vector3 hitPosition = _startHit.transform.position;
            _renderers.Peek().SetPosition(0, new Vector3(hitPosition.x, hitPosition.y, 0));
        }).AddTo(this);

        mouseUpObservable.Subscribe(_ =>
            {
                this._dragging = false;
                Vector3 pos = _mainCamera.ScreenToWorldPoint(Input.GetTouch(0).position);
                _endHit = Physics2D.OverlapCircle(new Vector2(pos.x,pos.y),1,LayerMask.GetMask("Animal"));
                if (_endHit == null || _endHit.gameObject == _startHit.gameObject || _endHit.GetComponent<Animal>().connectedObject != null)
                {
                    Destroy(this._renderers.Peek().gameObject);
                    this._renderers.Pop();
                    return;
                }
                Vector3 hitPosition = _endHit.gameObject.transform.position;
                this._renderers.Peek().SetPosition(1, new Vector3(hitPosition.x, hitPosition.y, 1));
                Animal hitAnimal = _endHit.GetComponent<Animal>();
                hitAnimal.connectedObject = _startHit.gameObject;
                hitAnimal.isStatic = true;
                _startHit.gameObject.GetComponent<Animal>().connectedObject = _endHit.gameObject;
                GameMode.instance.Play();
            })
            .AddTo(this);
    }

    private void Update()
    {
        if (!_dragging) return;
        if (this._renderers.Count == 0) return;
        if (_startHit.transform == null) return;
        if (Camera.main == null) return;
        if (Input.touchCount < 1) return;
        
        Vector2 mousePos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Collider2D obstacle = Physics2D.OverlapCircle(mousePos, 1, LayerMask.GetMask("Obstacle"));

        if (obstacle != null)
        {
            Destroy(this._renderers.Peek().gameObject);
            this._renderers.Pop();
            return;
        }
        
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
        this._renderers.Peek().enabled = true;
        this._renderers.Peek().SetPosition(1, new Vector3(pos.x, pos.y, 1));
    }
}