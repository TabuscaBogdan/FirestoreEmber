using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FirestoreEmber.IGateways
{
    interface IActualtizationGateway
    {
        Task UpdateDocument(string collectionPath, string documentName, Dictionary<string, object> data);

        Task UpdateDocumentWithTimestamp(string collectionPath, string documentName,
            Dictionary<string, object> data);

        Task UpdateDocumentBatch(
            Dictionary<string, Dictionary<string, Dictionary<string, object>>> collectionDocumentData);
    }
}
