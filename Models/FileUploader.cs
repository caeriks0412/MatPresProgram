using Amazon.S3;
using Amazon.S3.Transfer;
using Amazon;
using System;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Amazon.S3.Model;
using System.Net;
using System.Runtime.CompilerServices;

namespace MVPMatpres.Models
{/// <summary>
/// This class is made to upload the JSON input file created by the program to the AWS s3 server.
/// </summary>
    public class FileUploader
    {
        //Currently, the server is hardcoded. It can be updated however in this class.
        //For the S3 server, we need these keys, the name of the s3 bucket and a filepath to upload
        string accessKey = "";
        string secretKey = "";
        string bucketName = "";
        string filepath = ".\\plantsmith data.json"; 

        public FileUploader()
        {

        }
        //This method uploads the file to the s3 bucket.
        //we use a using statement here since it will only be used once then discarded. Saves some memory.
        public void UploadToBucket()
        {
            var credentials = new Amazon.Runtime.BasicAWSCredentials(accessKey, secretKey);
            var client = new AmazonS3Client(credentials, RegionEndpoint.EUNorth1);

            var transferUtility = new TransferUtility(client);

            using (FileStream fileStream = new FileStream(filepath, FileMode.Open))
            {
                var fileName = Path.GetFileName(filepath);
                transferUtility.Upload(fileStream, bucketName, fileName);
            }
        }
        /// <summary>
        /// This method is used to download the file from the s3 bucket.
        /// 
        /// </summary>
        public async Task<bool> waitForFile(string filename)
        {
            bool fileDownloaded = false;
            string lastETag = null; //This should be the fix for the file not being fully uploaded.
            DateTime currentDateTime = DateTime.Now;

            var credentials = new Amazon.Runtime.BasicAWSCredentials(accessKey, secretKey);
            var client = new AmazonS3Client(credentials, RegionEndpoint.EUNorth1);

            var request = new ListObjectsV2Request
            
            {
                BucketName = bucketName,
                Prefix = filename
            };

            while(!fileDownloaded)
            {
                //MessageBox.Show("I am online!");
                ListObjectsV2Response response = null;
                try
                {
                    response = await client.ListObjectsV2Async(request);
                }
                catch(AmazonS3Exception e) 
                {
                    MessageBox.Show("An error occured: " + e);
                }

                if(response != null)
                {
                    foreach(var obj in response.S3Objects)
                    {
                        if(obj.LastModified > currentDateTime)
                        {
                            var downloadRequest = new GetObjectRequest
                            {
                                BucketName = bucketName,
                                Key = obj.Key,
                            };

                            using (var responseFromClient = await client.GetObjectAsync(downloadRequest))
                            using (var responseStream = responseFromClient.ResponseStream)
                            {
                                using (var fileStream =  new FileStream(filename, FileMode.Create))
                                {
                                    responseStream.CopyTo(fileStream);

                                    MessageBox.Show("File downloaded!");
                                }
                            }
                            fileDownloaded = true;
                            //currentDateTime = obj.LastModified;
                        }
                    }
                }

                await Task.Delay(1000);

            }
            return true;
        }


    }
}
