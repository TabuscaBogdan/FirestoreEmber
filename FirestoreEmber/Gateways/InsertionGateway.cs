using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
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


    }
}
