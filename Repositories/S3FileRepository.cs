

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Amazon.Runtime.Internal;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Logging;

public class S3FileRepository : IFileRepository
{
  private readonly string bucketName;
  private readonly IAmazonS3 client;
  
  private readonly ILogger logger;

  private static string FILE_PREFIX = "images/";

  public S3FileRepository(ILoggerFactory loggerFactory, String bucketName, IAmazonS3 client) {
    this.bucketName = bucketName;
    this.client = client;
    this.logger = loggerFactory.CreateLogger<S3FileRepository>();
  }

  public Stream Get(string fileName)
  {
    try {
      using (GetObjectResponse response = client.GetObjectAsync(bucketName, fileName).Result)
      using (Stream responseStream = response.ResponseStream)
      {
        MemoryStream memoryStream = new MemoryStream(10000);
        responseStream.CopyTo(memoryStream);


        if(response.HttpStatusCode == HttpStatusCode.OK) {
          memoryStream.Position = 0;
          return memoryStream;
        } else {
          throw new KeyNotFoundException("No file.");
        } 
      }
    } catch(AggregateException e) {
      if(e.InnerException is AmazonS3Exception) {
        AmazonS3Exception InnerException = e.InnerException as AmazonS3Exception;
        logger.LogError("Error retrieving s3 file: " + fileName, InnerException);
        if(InnerException.ErrorCode == "NoSuchKey") throw new KeyNotFoundException("File not found");
        else throw InnerException;
      }
      else throw e;
    }
  }

  public string Save(string fileType, Stream fileStream) =>
    SaveAsync(fileType, fileStream).Result;

  public async Task<string> SaveAsync(string fileType, Stream fileStream)
  {
    try
    {
        // The path in S3 to the object
        var name = Guid.NewGuid().ToString();
        var fileName = name + fileType;
        String objectKey = fileName;
        await client.UploadObjectFromStreamAsync(bucketName, objectKey, fileStream, null);
        return objectKey;
    }
    catch (AmazonS3Exception e)
    {
        logger.LogError(
                "Error encountered ***. Message:'{0}' when writing an object"
                , e.Message);
        throw e;
    }
    catch (Exception e)
    {
        logger.LogError(
            "Unknown encountered on server. Message:'{0}' when writing an object"
            , e.Message);
            throw e;
    }
  }
}