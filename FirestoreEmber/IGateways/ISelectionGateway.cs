using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;

namespace FirestoreEmber.IGateways
{
    interface ISelectionGateway
    {
        Task<Dictionary<string, object>> GetDocument(string collectionPath, string documentName);
        Task<List<Dictionary<string, object>>> GetDocuments(string collectionPath);
        Task<IList<CollectionReference>> GetDocumentSubcollections(string collectionPath, string documentName);

    }
}
