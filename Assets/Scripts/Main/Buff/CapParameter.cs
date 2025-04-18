
public class CapParameter
{
    protected CapParameter()
    {
            
    }
}

public class CapParameter<T> : CapParameter
{
    public T Value { get; set; }
        
    public CapParameter(T value)
    {
        this.Value = value;
    }

} 
    
public class CapParameter<T,T1> : CapParameter<T>
{
    public T1 Value1 { get; set; }

    public CapParameter(T value,T1 value1) : base(value)
    {
        this.Value1 = value1;
    }
}
    
public class CapParameter<T,T1,T2> : CapParameter<T,T1>
{
    public T2 Value2 { get; set; }

    public CapParameter(T value,T1 value1,T2 value2) : base(value,value1)
    {
        this.Value2 = value2;
    }
}