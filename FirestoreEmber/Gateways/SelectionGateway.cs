using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FirestoreEmber.IGateways;
using Google.Cloud.Firestore;

namespace FirestoreEmber.Gateways
{

    public class SelectionGateway : ISelectionGateway
    {
        FirestoreDb database;

        public SelectionGateway(FirestoreDb database)
        {
            this.database = database;
        }

        public async Task<List<Dictionary<string, object>>> ReadDocuments(string collectionPath)
        {
            CollectionReference collectionReference = database.Collection(collectionPath);
            QuerySnapshot snapshot = await collectionReference.GetSnapshotAsync();

            var documentsList = new List<Dictionary<string, object>>();

            foreach (DocumentSnapshot document in snapshot.Documents)
            {
                documentsList.Add(document.ToDictionary());
            }

            return documentsList;
        }
    }
}
