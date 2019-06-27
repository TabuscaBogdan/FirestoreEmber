using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FirestoreEmber.Exceptions;
using FirestoreEmber.IGateways;
using Google.Cloud.Firestore;

namespace FirestoreEmber.Gateways
{
    public class InsertionGateway : IInsertionGateway
    {
        private FirestoreDb database;

        public InsertionGateway(FirestoreDb database)
        {
            this.database = database;
        }

        public async Task CreateDocument(string collectionPath, string documentName, Dictionary<string, object> data)
        {
            DocumentReference documentReference = database.Collection(collectionPath).Document(documentName);
            await documentReference.SetAsync(data);
        }

        public async Task<string> CreateAnonymousDocument(string collectionPath, Dictionary<string, object> data)
        {
            DocumentReference documentReference = await database.Collection(collectionPath).AddAsync(data);
            return documentReference.Id;
        }

        public async Task CreateDocumentBatch(Dictionary<string, Dictionary<string, Dictionary<string, object>>> collectionDocumentData)
        {
            WriteBatch batch = database.StartBatch();
            short operations = 0;

            foreach (var collectionKey in collectionDocumentData.Keys)
            {
                foreach (var documentKey in collectionDocumentData[collectionKey].Keys)
                {
                    var docRef = database.Collection(collectionKey).Document(documentKey);
                    batch.Set(docRef, collectionDocumentData[collectionKey][documentKey]);
                    operations++;

                    if (operations > 500)
                    {
                        throw new EmberBatchException("Number of set operation exceeded " +
                                                      "the batch's 500 operation limit.");
                    }
                }
            }

            await batch.CommitAsync();
        }

    }
}
