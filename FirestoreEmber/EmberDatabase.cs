using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FirestoreEmber.Gateways;
using FirestoreEmber.IGateways;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using Grpc.Auth;
using Grpc.Core;

namespace FirestoreEmber
{
    public class EmberDatabase

    {
        private FirestoreClient firestoreClient;
        private FirestoreDb database;

        private ISelectionGateway selectionGateway;
        private IInsertionGateway insertionGateway;

        public EmberDatabase(string projectId, string credentialsFilePath)
        {
            GoogleCredential cred = GoogleCredential.FromFile(credentialsFilePath);
            Channel channel = new Channel(FirestoreClient.DefaultEndpoint.Host,
                FirestoreClient.DefaultEndpoint.Port,
                cred.ToChannelCredentials());

            firestoreClient = FirestoreClient.Create(channel);

            database = FirestoreDb.Create(projectId, client: firestoreClient);

            selectionGateway = new SelectionGateway(database);
            insertionGateway = new InsertionGateway(database);
        }

        public async Task CreateDocument(string collectionPath, string documentName, Dictionary<string, object> data)
        {
            await insertionGateway.CreateDocument(collectionPath, documentName, data);
        }

        public async Task<List<Dictionary<string, object>>> ReadDocuments(string collectionPath)
        {
            var result = await selectionGateway.ReadDocuments(collectionPath);
            return result;
        }
    }
}

