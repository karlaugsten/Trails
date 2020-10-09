public class RequestError {
  public string Message { get; private set; }
  public string FieldName { get; private set; }

  public RequestError(string message) : this(message, null) {
  }

  public RequestError(string message, string fieldName) {
    this.Message = message;
    this.FieldName = fieldName;
  }
}