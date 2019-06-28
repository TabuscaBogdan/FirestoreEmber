using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<Dictionary<string, object>> GetDocument(string collectionPath, string documentName)
        {
            DocumentReference documentReference = database.Collection(collectionPath).Document(documentName);
            DocumentSnapshot snapshot = await documentReference.GetSnapshotAsync();

            if (snapshot.Exists)
            {
                Dictionary<string, object> document = snapshot.ToDictionary();
                return document;
            }
            
            return new Dictionary<string, object>();
        }

        public async Task<List<Dictionary<string, object>>> GetDocuments(string collectionPath)
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

        public async Task<IList<CollectionReference>> GetDocumentSubcollections(string collectionPath, string documentName)
        {
            DocumentReference documentReference = database.Collection(collectionPath).Document(documentName);
            IList<CollectionReference> subcollections = await documentReference.ListCollectionsAsync().ToList();

            return subcollections;
        }

    }
}
