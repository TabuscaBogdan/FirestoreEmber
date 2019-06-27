using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FirestoreEmber.Exceptions;
using Google.Cloud.Firestore;

namespace FirestoreEmber.Gateways
{
    public class ActualizationGateway
    {
        private readonly FirestoreDb database;

        public ActualizationGateway(FirestoreDb database)
        {
            this.database = database;
        }

        public async Task UpdateDocument(string collectionPath, string documentName, Dictionary<string, object> data)
        {
            var documentReference = database.Collection(collectionPath).Document(documentName);
            await documentReference.UpdateAsync(data);
        }

        public async Task UpdateDocumentWithTimestamp(string collectionPath, string documentName,
            Dictionary<string, object> data)
        {
            
            data["Timestamp"] = Timestamp.GetCurrentTimestamp();
            var documentReference = database.Collection(collectionPath).Document(documentName);
            await documentReference.UpdateAsync(data);
        }

        public async Task UpdateDocumentBatch(Dictionary<string, Dictionary<string, Dictionary<string,object>>> collectionDocumentData)
        {
            WriteBatch batch = database.StartBatch();
            short operations = 0;

            foreach (var collectionKey in collectionDocumentData.Keys)
            {
                foreach (var documentKey in collectionDocumentData[collectionKey].Keys)
                {
                    var docRef = database.Collection(collectionKey).Document(documentKey);
                    batch.Update(docRef, collectionDocumentData[collectionKey][documentKey]);
                    operations++;

                    if (operations > 500)
                    {
                        throw new EmberBatchException("Number of update operation exceeded " +
                                                      "the batch's 500 operation limit.");
                    }
                }
            }

            await batch.CommitAsync();
        }
    }
}