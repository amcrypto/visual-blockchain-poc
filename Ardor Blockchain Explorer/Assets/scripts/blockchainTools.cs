using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


namespace Ardor
{

    [Serializable]
    public class Request
    {
        public string key;
        public string value;
        public Request(string k, string v) { key = k; value = v; }
    }

    public static class Tools
    {
        public static string apiUrl;


        public static IEnumerator getData(List<Request> rData, System.Action<string> dataJson)
        {
            WWWForm form = new WWWForm();
            foreach (Request r in rData)
            {
                if (r.value != "")
                {
                    form.AddField(r.key, r.value);
                    //Debug.Log(r.key + " : " + r.value);
                }
            }
            
            UnityWebRequest www = UnityWebRequest.Post(apiUrl, form);  // Local host "http://localhost:27876/nxt"
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                dataJson(www.downloadHandler.text);
            }
            yield return null;
            Debug.Log("test");
        }
    }

    namespace Data
    {
        #region Transaction Api data
        [Serializable]
        public class Transaction
        {
            public string sender;//(S) is the account ID of the sender
            public string senderRS;//(S) is the Reed-Solomon address of the sender
            public string feeNQT;//(S) is the fee(in NQT) of the transaction
            public string amountNQT;//(S) is the amount(in NQT) of the transaction
            public int transactionIndex;//(N) is a zero-based index giving the order of the transaction in its block
            public int timestamp;//(N) is the time(in seconds since the genesis block) of the transaction
            public string referencedTransactionFullHash;//(S) is the full hash of a transaction referenced by this one, omitted if no previous transaction is referenced
            public int confirmations;//(N) is the number of transaction confirmations
            public int subtype;//(N) is the transaction subtype(refer to Get Constants for a current list of subtypes)
            public string block;//(S) is the ID of the block containing the transaction
            public int blockTimestamp;//(N) is the timestamp(in seconds since the genesis block) of the block
            public int height;//(N) is the height of the block in the blockchain
            public string senderPublicKey;//(S) is the public key of the sending account for the transaction
            public int type;//(N) is the transaction type(refer to Get Constants for a current list of types)
            public int chain;//(N) is the chain related to the transaction
            public int fxtTransaction;//(N) is the child chain block transaction
            public int deadline;//(N) is the deadline(in minutes) for the transaction to be confirmed
            public string signature;//(S) is the digital signature of the transaction
            public string recipient;//(S) is the account number of the recipient, if applicable
            public string recipientRS;//(S) is the Reed-Solomon address of the recipient, if applicable
            public string fullHash;//(S) is the full hash of the signed transaction
            public string signatureHash;//(S) is a SHA-256 hash of the transaction signature
            public bool approved;//(B) is a boolean indicating if the transaction is approved(only included when includePhasingResult is true and the transaction is phased)
            public string result;//(S) is a string containing the result of the transaction(only included when includePhasingResult is true and the transaction is phased)
            public int executionHeight;//(N) is the height the transaction was executed(only included when includePhasingResult is true and the transaction is phased)
            public string transaction;//(S) is the transaction ID
            public int version;//(N) is the transaction version number
            public bool phased;//(B) is true if the transaction is phased, false otherwise
            public int ecBlockId;//(N) is the economic clustering block ID
            public int ecBlockHeight;//(N) is the economic clustering block height
            public Attachment attachment;//(O) is an object containing any additional data needed for the transaction, if applicable
            public string lastBlock;//(S) is the last block ID on the blockchain(applies if requireBlock is provided but not requireLastBlock)
            public int requestProcessingTime;//(N) is the API request processing time(in millisec)
        }



        #endregion



        #region Blocks Api data
        [Serializable]
        public class Block
        {
            public string previousBlockHash;//(S) is the 32-byte hash of the previous block
            public int payloadLength;//(N) is the length(in bytes) of all transactions included in the block
            public string generationSignature;//(S) is the 32-byte generation signature of the generating account
            public string generator;//(S) is the generating account number
            public string generatorPublicKey;//(S) is the 32-byte public key of the generating account
            public string baseTarget;//(S) is the base target for the next block generation
            public string payloadHash;//(S) is the 32-byte hash of the payload(all transactions)
            public string generatorRS;//(S) is the Reed-Solomon address of the generating account
            public string nextBlock;//(S) is the next block ID
            public int numberOfTransactions;//(N) is the number of transactions in the block
            public string blockSignature;//(S) is the 64-byte block signature
            public Transaction[] transactions;//(A) is an array of transaction IDs or transaction objects(if includeTransactions provided, refer to Get Transaction for details)
            public Transaction[] executedPhasedTransactions;//(A) is an array of transaction IDs or transaction objects(if includeExecutedPhased provided, refer to Get Transaction for details)
            public int version;//(N) is the block version
            public string totalFeeFQT;//(S) is the total fee(in FQT) of the transactions in the block
            public string previousBlock;//(S) is the previous block ID
            public string cumulativeDifficulty;//(S) is the cumulative difficulty for the next block generation
            public string block;//(S) is the block ID
            public int height;//(N) is the zero-based block height
            public int timestamp;//(N) is the timestamp(in seconds since the genesis block) of the block
            public string lastBlock;//(S) is the last block ID on the blockchain(applies if requireBlock is provided but not requireLastBlock)
            public int requestProcessingTime;//(N) is the API request processing time(in millisec)
        }

        [Serializable]
        public class BlockId
        {
            public string block; // (S) is the block ID
            public string lastBlock; // (S) is the last block ID on the blockchain(applies if requireBlock is provided but not requireLastBlock)
            public int requestProcessingTime; // (N) is the API request processing time(in millisec)
        }

        [Serializable]
        public class BlockchainStatus
        {
            public int currentMinRollbackHeight; // (N) is the current minimum rollback height
            public int numberOfBlocks; // (N) is the number of blocks in the blockchain(height + 1)
            public bool isTestnet; // (B) is true if the node is connected to testnet, false otherwise
            public bool includeExpiredPrunable; // (B) is the value of the nxt.includeExpiredPrunable property
            public int requestProcessingTime; // (N) is the API request processing time(in millisec)
            public string version; // (S) is the application version
            public int maxRollback; // (N) is the value of the nxt.maxRollback property
            public string lastBlock; // (S) is the last block ID on the blockchain
            public string application; // (S) is application name, typically NRS
            public bool isScanning; // (B) is true if the blockchain is being scanned by the application, false otherwise
            public bool isDownloading; // (B) is true if a download is in progress, false otherwise; true when a batch of more than 10 blocks at once has been downloaded from a peer, reset to false when an attempt to download more blocks from a peer does not result in any new blocks
            public string cumulativeDifficulty; // (S) is the cumulative difficulty
            public int lastBlockchainFeederHeight; // (N) is the height of the last blockchain of greatest cumulative difficulty obtained from a peer
            public int maxPrunableLifetime; // (N) is the maximum prunable lifetime(in seconds)
            public int time; // (N) is the current timestamp(in seconds since the genesis block)
            public string lastBlockchainFeeder; // (S) is the address or announced address of the peer providing the last blockchain of greatest cumulative difficulty
            public string blockchainState; // (S) Current state of this node's blockchain (UP_TO_DATE or DOWNLOADING)
        }

        [Serializable]
        public class Blocks
        {
            public Block[] blocks; // (A) is an array of blocks retrieved(refer to Get Block for details)
            public string lastBlock; // (S) is the last block ID on the blockchain(applies if requireBlock is provided but not requireLastBlock)
            public int requestProcessingTime; // (N) is the API request processing time(in millisec)
        }

        [Serializable]
        public class ECBlock
        {
            public int ecBlockHeight; //  (N) is the EC block height
            public string ecBlockId; // (S) is the EC block ID
            public int timestamp; // (N) is the timestamp(in seconds since the genesis block) of the EC block
            public string lastBlock; // (S) is the last block ID on the blockchain(applies if requireBlock is provided but not requireLastBlock)
            public int requestProcessingTime; // (N) is the API request processing time(in millisec)
        }
        #endregion



        #region Experimental
        [Serializable]
        public class Attachment
        {
            public string ChildChainBlock;
            public int chain;
            public string[] childTransactionFullHashes;
            public string orderHash;
            public string FxtCoinExchangeOrderCancel;
            public string hash;

        }
        [Serializable]
        public class testA
        {
            public Attachment attachment;
        }
        #endregion
    }

}