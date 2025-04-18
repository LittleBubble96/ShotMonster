public interface IBonusListener
{
    void OnInit();
    
    void OnDestroy();

    bool OnSuc(float dt);
    
    void Reset();
}