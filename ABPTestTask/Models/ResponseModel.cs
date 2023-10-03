namespace ABPTestTask.Models;

public class ResponseModel<T>
{
    public string Key { get; set; }
    public T Value { get; set; }

    public ResponseModel(string key, T value)
    {
        Key = key;
        Value = value;
    }

    public ResponseModel()
    { }
}