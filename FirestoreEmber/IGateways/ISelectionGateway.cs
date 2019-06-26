using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FirestoreEmber.IGateways
{
    interface ISelectionGateway
    {
        Task<List<Dictionary<string, object>>> ReadDocuments(string collectionPath);
    }
}
