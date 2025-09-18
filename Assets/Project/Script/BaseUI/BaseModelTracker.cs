using NSJ_MVVM;
using UnityEngine;

public class BaseModelTracker : MonoBehaviour
{
    BaseModel _model;

    private void Start()
    {
        _model.InitStart();
    }
    private void OnDestroy()
    {
        _model.DestroyModel();
    }

    public void SetModel(BaseModel model)
    {
        _model = model;
    }
}
