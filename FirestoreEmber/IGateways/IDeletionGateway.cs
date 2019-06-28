using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;

namespace FirestoreEmber.IGateways
{
    interface IDeletionGateway
    {
        Task DeleteDocument(string collectionPath, string documentName);

        Task DeleteDocumentFields(string collectionPath, string documentName, List<string> fields);

        Task DeleteCollection(string collectionPath, int batchSize = 1000);

        Task DeleteDocumentBatch(Dictionary<string, List<string>> collectionDocuments);
    }
}
