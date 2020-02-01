using UniRx;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Ray ray;
        RaycastHit hit = new RaycastHit();
        var clickObservable = Observable.EveryUpdate()
            .Where(_ => Input.GetMouseButtonDown(0))
            .Where(_ =>
            {
                if (Camera.main == null) return false;
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Physics.Raycast(ray, out hit, 10000f, LayerMask.GetMask("Animal"));
                return hit.collider != null;
            });
        clickObservable.Subscribe(element =>
        {
            Debug.Log(hit.collider.tag);
        }).AddTo(this);
    }
}