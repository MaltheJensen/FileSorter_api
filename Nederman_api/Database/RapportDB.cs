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
    public class RapportDB
    {
        private GridFSBucket fs;
        private IMongoDatabase database;
        public RapportDB(string nameOfDatabase)
        {
            var client = new MongoClient();
            database = client.GetDatabase(nameOfDatabase);
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

        public void UploadFile(string fileName, byte[] record)
        {
            fs.UploadFromBytesAsync(fileName, record);
        }

        public byte[] DownloadFile(string nameOfFile)
        {
            var t = fs.DownloadAsBytesByNameAsync(nameOfFile);
            Task.WaitAll(t);
            return t.Result;
        }
    }
}
