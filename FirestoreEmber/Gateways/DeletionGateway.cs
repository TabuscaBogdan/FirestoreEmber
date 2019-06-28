using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FirestoreEmber.Exceptions;
using FirestoreEmber.IGateways;
using Google.Cloud.Firestore;

namespace FirestoreEmber.Gateways
{
    public class DeletionGateway : IDeletionGateway
    {
        private FirestoreDb database;

        public DeletionGateway(FirestoreDb database)
        {
            this.database = database;
        }

        public async Task DeleteDocument(string collectionPath, string documentName)
        {
            DocumentReference documentReference = database.Collection(collectionPath).Document(documentName);
            await documentReference.DeleteAsync();
        }

        public async Task DeleteDocumentFields(string collectionPath, string documentName, List<string> fields)
        {
            var updates = new Dictionary<string, object>();
            foreach (var field in fields)
            {
                updates[field] = FieldValue.Delete;
            }

            DocumentReference documentReference = database.Collection(collectionPath).Document(documentName);
            await documentReference.UpdateAsync(updates);
        }

        public async Task DeleteCollection(string collectionPath, int batchSize=1000)
        {
            CollectionReference collectionReference = database.Collection(collectionPath);
            QuerySnapshot snapshot = await collectionReference.Limit(batchSize).GetSnapshotAsync();
            IReadOnlyList<DocumentSnapshot> documents = snapshot.Documents;

            while (documents.Count>0)
            {
                foreach (DocumentSnapshot document in documents)
                {
                    await document.Reference.DeleteAsync();
                }
                snapshot = await collectionReference.Limit(batchSize).GetSnapshotAsync();
                documents = snapshot.Documents;
            }
        }

        public async Task DeleteDocumentBatch(Dictionary<string, List<string>> collectionDocuments)
        {
            WriteBatch batch = database.StartBatch();
            short operations = 0;

            foreach (var collectionKey in collectionDocuments.Keys)
            {
                foreach (var document in collectionDocuments[collectionKey])
                {
                    var docRef = database.Collection(collectionKey).Document(document);
                    batch.Delete(docRef);
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
