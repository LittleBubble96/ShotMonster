
public class BuffParameter
{
    protected BuffParameter()
    {
            
    }
}

public class BuffParameter<T> : EventType
{
    public T Value { get; set; }
        
    public BuffParameter(T value)
    {
        this.Value = value;
    }

} 
    
public class BuffParameter<T,T1> : BuffParameter<T>
{
    public T1 Value1 { get; set; }

    public BuffParameter(T value,T1 value1) : base(value)
    {
        this.Value1 = value1;
    }
}
    
public class BuffParameter<T,T1,T2> : BuffParameter<T,T1>
{
    public T2 Value2 { get; set; }

    public BuffParameter(T value,T1 value1,T2 value2) : base(value,value1)
    {
        this.Value2 = value2;
    }
}