using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FirestoreEmber.IGateways
{
    interface IInsertionGateway
    {

        Task CreateDocument(string collectionPath, string documentName,
            Dictionary<string, object> data);

        Task CreateDocumentBatch(Dictionary<string, Dictionary<string, Dictionary<string, object>>> collectionDocumentData);

        Task<string> CreateAnonymousDocument(string collectionPath, Dictionary<string, object> data);
    }
}
