using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using Eyon.Utilities.Extensions;
namespace Eyon.Utilities.API
{
    public class AmazonWebService : IDisposable
    {
        string _encryptedAccessKey;
        string _encryptedSecretKey;
        AmazonS3Client _client;
        public AmazonWebService(string encryptedAccessKey, string encryptedSecretKey)
        {
            this._encryptedAccessKey = encryptedAccessKey;
            this._encryptedSecretKey = encryptedSecretKey;
        }
        public void Initialize()
        {

            using ( Eyon.Utilities.API.AwsCsvHelper helper = new Utilities.API.AwsCsvHelper() )
            {       
                var key = helper.GetKey();
                var iv = helper.GetIV();
                _client = new AmazonS3Client(_encryptedAccessKey.Decrypt(key, iv), _encryptedSecretKey.Decrypt(key, iv), Amazon.RegionEndpoint.USEast2);
            }
        }
        public async Task<string> GetAsync(string bucketName, string fileKeyName)
        {
            string responseBody = "";
            // source: https://docs.aws.amazon.com/AmazonS3/latest/dev/RetrievingObjectUsingNetSDK.html
            // TODO read: https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/S3/MS3GetObjectGetObjectRequest.html
            try
            {
                GetObjectRequest request = new GetObjectRequest()
                {
                    BucketName = bucketName,
                    Key = fileKeyName
                };
                using ( GetObjectResponse response = await _client.GetObjectAsync(request) )
                {
                    //response.WriteResponseStreamToFileAsync()
                    using ( Stream responseStream = response.ResponseStream )
                    using ( StreamReader reader = new StreamReader(responseStream) )
                    {
                        string title = response.Metadata["x-amz-meta-title"]; // Assume you have "title" as medata added to the object.
                        string contentType = response.Headers["Content-Type"];
                        responseBody = reader.ReadToEnd(); // Now you process the response body.

                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return responseBody;
        }

        public async Task DeleteAsync(string bucketName, string fileKeyName)
        {
            try
            {
                var deleteObjectRequest = new DeleteObjectRequest
                {
                    BucketName = bucketName,
                    Key = fileKeyName
                };

                Console.WriteLine("Deleting an object");
                await _client.DeleteObjectAsync(deleteObjectRequest);
            }
            catch ( AmazonS3Exception e )
            {
                throw e;
            }
            catch ( Exception e )
            {
                throw e;
            }
        }

        public async Task<bool> PutAsync(MemoryStream memoryStreamFile, string bucketName, string fileKeyName )
        {
            try
            {
                using(Amazon.S3.Transfer.TransferUtility transUtility = new Amazon.S3.Transfer.TransferUtility(_client) )
                {
                    await transUtility.UploadAsync(memoryStreamFile, bucketName, fileKeyName);
                }
            }
            catch(Exception ex)
            {
                throw ex; 
            }
            return true;
        }

        public void Dispose()
        {
            _encryptedAccessKey = null;
            _encryptedSecretKey = null;
            _client.Dispose();
        }
    }
}
