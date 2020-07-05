namespace Example.NullObject
{
    public interface IMyObject
    {
        public int Id { get; }
        public string Value { get; }
    }

  public class MyObject : IMyObject
  {
    public MyObject(int id, string value)
    {
        Id = id;
        Value = value;
    }

    public int Id { get; }
    public string Value { get; }
  }

  public class MyNullObject : IMyObject
  {
      public MyNullObject()
      {
          Id = -1;
          Value = "N/A";    
      }

      public int Id { get; }
      public string Value { get; }
  }
}