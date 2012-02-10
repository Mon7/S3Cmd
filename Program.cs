using System;
using System.Configuration;
using Amazon.S3.Model;
using System.IO;
using Amazon.S3;

namespace S3Cmd
{
    class Program
    {
        static int Main(string[] args)
        {
            if (args.Length > 0)
                switch (args[0])
                {
                    case "put":
                        if (args.Length != 4) break;
                        var filepath = args[1];
                        var bucket = args[2];
                        var key = args[3];
                        return Upload(filepath, bucket, key);
                }
            return Usage();
        }

        static int Upload(string filepath, string bucket, string key)
        {
            try
            {
                var conf = ConfigurationManager.AppSettings;
                using (var s3 = Amazon.AWSClientFactory.CreateAmazonS3Client(conf["AWSAccessKey"], conf["AWSSecretKey"]))
                using (var tu = new Amazon.S3.Transfer.TransferUtility(s3))
                    tu.Upload(filepath, bucket, key);
                return 0;
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine(string.Format("File not found: {0}", filepath));
                return 1;
            }
            catch (AmazonS3Exception e)
            {
                Console.WriteLine(string.Format("S3 Error: {0}", e.Message));
                return 2;
            }
            catch (Exception e)
            {
                Console.WriteLine(string.Format("Unknown error: {0}", e));
                return 3;
            }
        }

        static int Usage()
        {
            Console.WriteLine("Usage: s3cmd put <file> <bucket> <key>");
            return -1;
        }
    }
}
