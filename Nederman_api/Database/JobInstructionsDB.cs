using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Nederman_api.Database
{
    public class JobInstructionsDB
    {
        private GridFSBucket fs;
        public JobInstructionsDB(string nameOfDatabase)
        {
            var client = new MongoClient();
            var database = client.GetDatabase(nameOfDatabase);
            fs = new GridFSBucket(database);
        }

        public ObjectId UploadFile(string filePath, string nameOfFile)
        {
            using (var s = File.OpenRead(@"" + filePath))
            {
                var t = Task.Run<ObjectId>(() => { return
                        fs.UploadFromStreamAsync(nameOfFile, s);
                });

                return t.Result;
            }
        }

        public byte[] DownloadFile(string nameOfFile)
        {
            var t = fs.DownloadAsBytesByNameAsync(nameOfFile);
            Task.WaitAll(t);
            return t.Result;
        }
    }
}
