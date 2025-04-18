public class TimeBonusListener : IBonusListener
{
    private float duration;
    private float loopTimeCount;
    public TimeBonusListener(float dur)
    {
        duration = dur;
        loopTimeCount = dur;
    }
    public void OnInit()
    {
    }

    public void OnDestroy()
    {
    }

    public bool OnSuc(float dt)
    {
        loopTimeCount -= dt;
        if (loopTimeCount <= 0)
        {
            return true;
        }
        return false;
    }

    public void Reset()
    {
        loopTimeCount = duration;
    }
}