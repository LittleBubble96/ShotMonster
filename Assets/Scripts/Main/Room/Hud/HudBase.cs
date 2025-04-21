using UnityEngine;
using UnityEngine.UI;

public class HudBase : RecycleObject
{
    [SerializeField] private float _duration = 0.5f;
    private Slider slider;
    private float _startValue;
    private float _endValue;
    private float _durationCount;
    private int maxValue;
    
    
    public void Init(int max)
    {
        slider = GetComponentInChildren<Slider>();
        if (slider == null)
        {
            Debug.LogError("Slider component not found on HudBase.");
        }
        maxValue = max;
        slider.maxValue = maxValue;
        slider.value = maxValue;
    }
    
    public void DoUpdate(float dt)
    {
        if (slider == null)
        {
            return;
        }
        
        if (_durationCount > 0)
        {
            _durationCount -= dt;
            float t = Mathf.Clamp01(1 - _durationCount / _duration);
            slider.value = Mathf.Lerp(_startValue, _endValue, t);
            if (_durationCount <= 0)
            {
                slider.value = _endValue;
                _startValue = 0;
                _endValue = 0;
            }
        }
        //看向摄像机 并保持平行
        Vector3 right = Camera.main.transform.right;
        Vector3 forward = Camera.main.transform.forward;
        Vector3 up = Vector3.Cross(right, forward);
        transform.rotation = Quaternion.LookRotation(forward, up);
    }
    
    public void SetValue(int value)
    {
        if (slider == null)
        {
            return;
        }
        
        if (value > maxValue)
        {
            value = maxValue;
        }
        
        _startValue = slider.value;
        _endValue = value;
        _durationCount = _duration;
    }
}