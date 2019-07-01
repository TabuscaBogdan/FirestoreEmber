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
        private IActualtizationGateway actualtizationGateway;
        private IDeletionGateway deletionGateway;

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
            actualtizationGateway = new ActualizationGateway(database);
            deletionGateway = new DeletionGateway(database);
        }

        /// <summary>
        /// Get a reference to the FirestoreDb.
        /// </summary>
        /// <returns></returns>
        public FirestoreDb GetDatabaseReference()
        {
            return database;
        }

        /// <summary>
        ///  Creates a document in the specified collection with the given data.
        /// </summary>
        /// <param name="collectionPath">A string containing the path and collection Name</param>
        /// <param name="documentName">A string representing the document's name</param>
        /// <param name="data">Data to be placed in the created document (cannot be null)</param>
        /// <returns></returns>
        public async Task CreateDocument(string collectionPath, string documentName, Dictionary<string, object> data)
        {
            await insertionGateway.CreateDocument(collectionPath, documentName, data);
        }
        /// <summary>
        /// Allows the creation of multiple documents in multiple collections at once.
        /// Batch["Collection"]["DocumentName"]["DataField"]
        /// </summary>
        /// <param name="collectionDocumentData">Object that specifies the collection, the document name and the data it should contain.</param>
        /// <returns></returns>
        public async Task CreateDocumentBatch(Dictionary<string, Dictionary<string, Dictionary<string, object>>> collectionDocumentData)
        {
            await insertionGateway.CreateDocumentBatch(collectionDocumentData);
        }

        /// <summary>
        /// Creates a document in the specified collection with the given data.
        /// Returns the generated document's name.
        /// </summary>
        /// <param name="collectionPath">A string containing the path and collection Name</param>
        /// <param name="data">Data to be placed in the created document (cannot be null)</param>
        /// <returns>The generated document's name.</returns>
        public Task<string> CreateAnonymousDocument(string collectionPath, Dictionary<string, object> data)
        {
            var documentId = insertionGateway.CreateAnonymousDocument(collectionPath, data);
            return documentId;
        }

        /// <summary>
        /// Attempt to read the specified document. Returns an empty dictionary if inexistent.
        /// </summary>
        /// <param name="collectionPath">A string containing the path and collection Name</param>
        /// <param name="documentName">A string representing the document's name</param>
        /// <returns></returns>
        public async Task<Dictionary<string, object>> GetDocument(string collectionPath, string documentName)
        {
            var result = await selectionGateway.GetDocument(collectionPath, documentName);
            return result;
        }
        /// <summary>
        /// Gets all the documents in the specified collection.
        /// Possible out of memory exception for large collections.
        /// </summary>
        /// <param name="collectionPath">A string containing the path and collection name.</param>
        /// <returns></returns>
        public async Task<List<Dictionary<string, object>>> GetDocuments(string collectionPath)
        {
            var result = await selectionGateway.GetDocuments(collectionPath);
            return result;
        }

        /// <summary>
        /// Gets the references of all the subcollections in the specified document.
        /// </summary>
        /// <param name="collectionPath">A string containing the path and collection name.</param>
        /// <param name="documentName">A string representing the document's name.</param>
        /// <returns></returns>
        public async Task<IList<CollectionReference>> GetDocumentSubcollections(string collectionPath,
            string documentName)
        {
            var result = await selectionGateway.GetDocumentSubcollections(collectionPath, documentName);
            return result;
        }

        /// <summary>
        /// Updates the document in the given path. Merges fields.
        /// </summary>
        /// <param name="collectionPath">A string containing the path and collection name.</param>
        /// <param name="documentName">A string representing the document's name.</param>
        /// <param name="data">Update data.</param>
        /// <returns></returns>
        public async Task UpdateDocument(string collectionPath, string documentName, Dictionary<string, object> data)
        {
            await actualtizationGateway.UpdateDocument(collectionPath, documentName, data);
        }

        /// <summary>
        /// Updates the document in the given path and adds an additional "Timestamp" field with the server
        /// update time.
        /// </summary>
        /// <param name="collectionPath">A string containing the path and collection name.</param>
        /// <param name="documentName">A string representing the document's name.</param>
        /// <param name="data">Update data.</param>
        /// <returns></returns>
        public async Task UpdateDocumentWithTimestamp(string collectionPath, string documentName,
            Dictionary<string, object> data)
        {
            await actualtizationGateway.UpdateDocumentWithTimestamp(collectionPath, documentName, data);
        }

        /// <summary>
        /// Allows the actualization of multiple documents in multiple collections at once.
        /// Batch["Collection"]["DocumentName"]["DataField"]
        /// </summary>
        /// <param name="collectionDocumentData"></param>
        /// <returns></returns>

        public async Task UpdateDocumentBatch(
            Dictionary<string, Dictionary<string, Dictionary<string, object>>> collectionDocumentData)
        {
            await actualtizationGateway.UpdateDocumentBatch(collectionDocumentData);
        }

        /// <summary>
        /// Deletes the specified document.
        /// </summary>
        /// <param name="collectionPath">A string containing the path and collection name.</param>
        /// <param name="documentName">A string representing the document's name.</param>
        /// <returns></returns>
        public async Task DeleteDocument(string collectionPath, string documentName)
        {
            await deletionGateway.DeleteDocument(collectionPath, documentName);
        }

        /// <summary>
        /// Deletes the specified fields inside a document.
        /// </summary>
        /// <param name="collectionPath">A string containing the path and collection name.</param>
        /// <param name="documentName">A string representing the document's name.</param>
        /// <param name="fields">A list of fields to be deleted.</param>
        /// <returns></returns>
        public async Task DeleteDocumentFields(string collectionPath, string documentName, List<string> fields)
        {
            await deletionGateway.DeleteDocumentFields(collectionPath, documentName, fields);
        }

        /// <summary>
        /// Deletes all the documents inside that collections, but it will not delete subcollections.
        /// </summary>
        /// <param name="collectionPath"></param>
        /// <param name="batchSize"></param>
        /// <returns></returns>
        public async Task DeleteCollection(string collectionPath, int batchSize = 1000)
        {
            await deletionGateway.DeleteCollection(collectionPath, batchSize);
        }

        /// <summary>
        /// Allows the deletion of multiple documents in multiple collections at once.
        /// Collection with List of documents to be deleted.
        /// </summary>
        /// <param name="collectionDocuments"></param>
        /// <returns></returns>
        public async Task DeleteDocumentBatch(Dictionary<string, List<string>> collectionDocuments)
        {
            await deletionGateway.DeleteDocumentBatch(collectionDocuments);
        }
    }
}

