using UnityEngine;
using WebSocketSharp;
using System;
using System.Collections;


public class WebSocketWorker : MonoBehaviour
{
    [System.Serializable]
    public class DataHolder
    {
        public string type;
        public string data;
    }

    /*
     * this line of code is not needed for this project
    public class DataHolderList
    {
        public ArrayList listOfData;

        public DataHolderList() { 
            this.listOfData = new ArrayList();
        }

        public void Add(DataHolder dataHolder)
        {
            this.listOfData.Add(dataHolder);
        }

        public DataHolder Get(int index) { 
            return (DataHolder)this.listOfData[index];
        }

        public void Remove(int index)
        {
            this.listOfData.RemoveAt(index);
        }

        public void Clear()
        {
            this.listOfData.Clear();
        }

    }
    */

    public string serverUrl = "ws://localhost:8080";

    private WebSocket ws;

    public DataHolder dataHolder;

    void Start()
    {
        ws = new WebSocket(serverUrl);
        ws.Connect();
        ws.OnOpen += (sender, e) =>
        {
            Debug.Log("WebSocket connected.");
        };


        ws.OnMessage += (sender, e) =>
        {
            string data = e.Data;
            dataHolder = JsonUtility.FromJson<DataHolder>(data);
        };

    }

    public string ReturnWhatHasBeenRecived()
    {
        return dataHolder.data;
    }

    public void Send(string data)
    {
        if (ws.IsAlive) { 
            try {
                ws.Send(data);
            } catch (Exception ex) // display error when it cant send the data
            {
                Debug.LogException(ex);
            }
        }
    }


    void OnDestroy()
    {
        // Cleanup When Closed (killing the game by pressing the close button)
        if (ws != null && ws.IsAlive)
        {
            //dataHolderList.Clear(); not needed for this project
            ws.Close();
        }
    }
}
