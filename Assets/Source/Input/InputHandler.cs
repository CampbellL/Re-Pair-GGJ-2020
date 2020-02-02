using System;
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
    public GameObject objectToClone;
    private Camera _mainCamera;
    private Collider2D _endHit;

    // Start is called before the first frame update
    void Start()
    {
        _mainCamera = Camera.main;
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
                _startHit = Physics2D.OverlapCircle(mousePos, 0.2f, LayerMask.GetMask("Animal"));
                return _startHit != null;
            })
            .Where(_ => _startHit.GetComponent<Animal>().connectedObject == null);


        clickObservable.Subscribe(element =>
        {
            _dragging = true;
            GameObject tempObject = Instantiate(objectToClone, this.transform);
            _renderers.Push(tempObject.GetComponent<LineRenderer>());
            _renderers.Peek().enabled = false;
            Vector3 hitPosition = _startHit.transform.position;
            _renderers.Peek().SetPosition(0, new Vector3(hitPosition.x, hitPosition.y, 0));
        }).AddTo(this);

        mouseUpObservable.Subscribe(_ =>
            {
                this._dragging = false;
                Vector3 pos = _mainCamera.ScreenToWorldPoint(Input.GetTouch(0).position);
                _endHit = Physics2D.OverlapCircle(new Vector2(pos.x, pos.y), 0.2f, LayerMask.GetMask("Animal"));
                if (_endHit == null || _endHit.gameObject == _startHit.gameObject ||
                    _endHit.GetComponent<Animal>().connectedObject != null)
                {
                    Destroy(this._renderers.Peek().gameObject);
                    this._renderers.Pop();
                    return;
                }

                Vector3 hitPosition = _endHit.gameObject.transform.position;
                Animal hitAnimal = _endHit.GetComponent<Animal>();
                this._renderers.Peek().SetPosition(1, new Vector3(hitPosition.x, hitPosition.y, 1));
                hitAnimal.connectedObject = _startHit.gameObject;
                hitAnimal.GetComponent<Animal>().isStatic = true;
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
        if (_startHit.GetComponent<Animal>().animalType == "Kevin")
        {
            if (this._renderers.Peek().positionCount < 3)
                this._renderers.Peek().positionCount++;
            Vector3 position = _startHit.transform.position;
            Vector2 middlePos = new Vector2(GetMiddlePositionX(position.x, pos.x),
                GetMiddlePositionY(position.y, pos.y));

            var xLength = GetXLength(position.x,pos.x);
            var yLength = GetYLength(position.y,pos.y);

            Vector2 finalMidPos;
            
            if (position.x <= pos.x && position.y <= pos.y)        //ok
            {
                finalMidPos = new Vector2(middlePos.x - yLength/2,middlePos.y + xLength/2);
            }
            else if (position.x <= pos.x && position.y > pos.y)
            {
                finalMidPos = new Vector2(middlePos.x + yLength/2,middlePos.y + xLength/2);
            }
            else if (position.x > pos.x && position.y <= pos.y)        //ok
            {
                finalMidPos = new Vector2(middlePos.x - yLength/2,middlePos.y - xLength/2);
            }
            else
            {
                finalMidPos = new Vector2(middlePos.x + yLength/2,middlePos.y - xLength/2);
            }
            

            this._renderers.Peek().SetPosition(1, new Vector3(finalMidPos.x,finalMidPos.y,1));
            this._renderers.Peek().SetPosition(2, new Vector3(pos.x, pos.y, 1));
        }
        else
        {
            this._renderers.Peek().SetPosition(1, new Vector3(pos.x, pos.y, 1));
        }
    }

    private float GetXLength(float startX, float endX)
    {
        return Math.Abs(startX - endX);
    }
    
    private float GetYLength(float startY, float endY)
    {
        return Math.Abs(startY- endY);
    }
    
    private float GetLength(Vector2 startPos, Vector2 endPos)
    {
        var length = Math.Abs(startPos.x - endPos.x);
        var height = Math.Abs(startPos.y - endPos.y);
        var result = Math.Pow(length,2) + Math.Pow(height,2);
        return (float) Math.Sqrt(result);
    }
    
    private float GetMiddlePositionX(float startPosX, float endPosX)
    {
        return startPosX + (endPosX - startPosX) / 2;
    }

    private float GetMiddlePositionY(float startPosY, float endPosY)
    {
        return startPosY + (endPosY - startPosY) / 2;
    }
}

