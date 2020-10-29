using Trails.FileProcessing.Models;

public class FileProcessingTask {
  public string Status { get; set; }
  public string ErrorMessage { get; set; }
  public string CallbackUrl { get; set; }
  public string FinishedUrl { get; set; }
}