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
    }
}
