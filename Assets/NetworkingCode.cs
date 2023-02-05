using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class NetworkingCode : MonoBehaviour
{
    string data = null;
    string enemyIp;
    bool start = true;
    float timer = 1;
    GameObject main;
    public string enemyPos;
    public Vector3 enemyPosition;
    bool newData = false;
    GameObject myShip;
    GameObject main2;
    Vector3 myPosition;
    bool gotPosition = false;
    bool needPosition = false;
    bool needFire = false;
    bool needHit = false;
    bool gotFire = false;
    bool gotHit = false;
    int transferNum = 1;
    bool fire = false;
    public bool playerFired = false;
    public bool enemyFire = false;
    public bool enemyFiring = false;
    bool newData2 = false;
    bool newData3 = false;
    public bool playerHit = false;
    bool hit = false;
    public bool hitEnemy = false;
    public bool gameOver = false;
    public bool readGameState = false;
    bool needGameState = false;
    bool readHitValue = false;
    bool gotGameState = false;
    bool apparentlyFinished = false;
    public int pingTime = 0;
    GameObject lostConnectionPanel;
    bool connectionLost = false;
    // Start is called before the first frame update
    void Start()
    {
        
        enemyIp = null;
        main = GameObject.Find("Main Camera");
        main2 = GameObject.Find("Canvas");
        lostConnectionPanel = GameObject.Find("LostConnectionPanel");
        lostConnectionPanel.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(connectionLost == true)
        {
            gameOver = main2.GetComponent<OnlineGameCode>().gameOver;
            lostConnectionPanel.SetActive(true);
            connectionLost = false;
        }
        if(apparentlyFinished == true)
        {
            UnityEngine.Debug.LogError("Reached apparently finished");
            string enemy = null;
            try
            {
                 enemy = main2.GetComponent<OnlineGameCode>().ActiveShipEnemy();
            }
            catch (MissingReferenceException)
            {
                UnityEngine.Debug.LogError("MISSING REFRENCE EXCEPTION CALLED");
            }
                
            
            GameObject temp = GameObject.Find(enemy);
            if(temp != null)
            {
                temp.GetComponent<HealthOnlineEnemy>().health = 0;
            }
        }
        gameOver = main2.GetComponent<OnlineGameCode>().gameOver;
        if(start == true)
        {
            if(timer<= 0)
            {
                start = false;
                main.GetComponent<LoadData2>().LoadPosition();
                enemyIp = main.GetComponent<LoadData2>().position;
                myShip = GameObject.Find(main2.GetComponent<OnlineGameCode>().ActiveShip());
                int length2 = enemyIp.Length;
                transferNum = 1;
                if (enemyIp[length2-1] == 'H')
                {
                    enemyIp = enemyIp.Remove(length2-1);
                    UnityEngine.Debug.Log(enemyIp);
                    Thread serverThread = new Thread(Server);
                    serverThread.IsBackground = true;
                    serverThread.Start();
                }
                else
                {
                    enemyIp = enemyIp.Remove(length2 - 1);
                    UnityEngine.Debug.Log(enemyIp);
                    Thread clientThread = new Thread(Client);
                    clientThread.IsBackground = true;
                    clientThread.Start();
                }
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }
        if(newData2 == true)
        {
            enemyFiring = enemyFire;
            newData2 = false;
        }
        if(newData3 == true)
        {
            hitEnemy = readHitValue;
            newData3 = false;
        }
        if (newData == true)
        {
            int length = 0;
            int i = 0;
            foreach (char c in enemyPos)
            {
                if (c == ' ')
                {
                    length = i;
                    break;
                }
                i++;
            }
            
            List<char> posXTemp = new List<char>();
            for (int a = 0; a < i; a++)
            {
                posXTemp.Add(enemyPos[a]);
            }
            string posX = string.Concat(posXTemp);
            int length2 = enemyPos.Length;
            List<char> posYTemp = new List<char>();
            for (int a = i + 1; a < length2; a++)
            {
                posYTemp.Add(enemyPos[a]);
            }
            string posY = string.Concat(posYTemp);
            UnityEngine.Debug.LogError("posx = " + posX);
            enemyPosition.x = float.Parse(posX);
            enemyPosition.y = float.Parse(posY);
            enemyPosition.z = 90;
            newData = false;

        }
        if(needFire == true)
        {
            fire = playerFired;
            if (playerFired == true)
            {
                playerFired = false;
            }
            needFire = false;
            gotFire = true;
        }
        if(needHit == true)
        {
            hit = playerHit;
            if (playerHit == true)
            {
                playerHit = false;
            }
            needHit = false;
            gotHit = true;
        }
        if(needPosition == true)
        {
            try
            {
                myPosition = myShip.transform.position;
                needPosition = false;
                gotPosition = true;
            }
            catch (Exception)
            {

            }
        }
        if(needGameState == true)
        {
            needGameState = false;
            readGameState = gameOver;
            gotGameState = true;
        }
    }
    public void Client()
    {
        
        IPAddress ip = IPAddress.Parse(enemyIp);
        IPEndPoint end = new IPEndPoint(ip, 3333);
        Socket client = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        int attempts = 0;
        while (true)
        {
            if (attempts < 5)
            {
                try
                {
                    client.Connect(end);
                    break;
                }
                catch (Exception)
                {
                    attempts += 1;
                }
            }
            else
            {
                connectionLost = true;
            }
        }
        Byte[] bytes;
        bool doneFetching = false;
        bool gameFinish = false;
        while (true)
        {
            if(gotGameState == false)
            {
                needGameState = true;
            }
            else
            {
                gotGameState = false;
                if(readGameState == true)
                {
                    gameFinish = true;
                    
                }
            }
            Byte[] data3 = null;
            
            while (true)
            {
               if(gameFinish == true)
                {
                    data3 = Encoding.ASCII.GetBytes("END!!!");
                   
                    client.Send(data3);
                    client.Close();
                    break;
                }
                if (transferNum == 1)
                {
                    if (gotPosition == false)
                    {
                        needPosition = true;
                    }
                    else
                    {
                        string myPos = myPosition.x + " " + myPosition.y;
                        data3 = Encoding.ASCII.GetBytes(myPos + "!!!");
                        doneFetching = true;
                    }
                }
                else if (transferNum == 2)
                {
                   
                    if (gotFire == false)
                    {
                        needFire = true;
                    }
                    else
                    {
                        string fireState = fire.ToString();
                      
                        data3 = Encoding.ASCII.GetBytes(fireState + "!!!");
                        doneFetching = true;
                    }
                }
                else if (transferNum == 3)
                {
                    if (gotHit == false)
                    {
                        needHit = true;
                    }
                    else
                    {
                        string imHit = hit.ToString();
                        data3 = Encoding.ASCII.GetBytes(imHit + "!!!");
                        doneFetching = true;
                    }
                }


                if (doneFetching == true)
                {
                  
                    client.Send(data3);
                    gotPosition = false;
                    gotHit = false;
                    gotFire = false;
                    doneFetching = false;
                    break;
                }

            }
            if(gameFinish == true)
            {
                break;
            }
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            while (true)
            {
                try
                {
                    bytes = new Byte[1024];
                    int bytesRec = client.Receive(bytes);
                
                    if (bytesRec <= 0)
                    {


                        break;
                    }
                    string tempData = Encoding.ASCII.GetString(bytes, 0, bytesRec);

                    data = Encoding.ASCII.GetString(bytes, 0, bytesRec);
                    if (data.Contains("!!!"))
                    {
                        if (data.Contains("END"))
                        {
                            UnityEngine.Debug.LogError("Recieved END");
                            client.Close();
                            gameFinish = true;
                            break;
                        }
                        data = data.Remove(data.Length - 3);
                        break;
                    }

                    if (data.IndexOf("<EOF>") > -1)
                    {
                        break;
                    }
                  
                }
                catch (Exception e)
                {
                    UnityEngine.Debug.LogError(e);
                    connectionLost = true;
                }
            }
            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;
            try
            {
                pingTime = int.Parse(ts.Milliseconds.ToString());
            }
            catch (FormatException)
            {
                pingTime = 0;
            }
            if(gameFinish == true)
            {
                apparentlyFinished = true;
                break;
            }
            if (transferNum == 1)
            {
                enemyPos = data;
                newData = true;
                transferNum = 2;
            }
            else if(transferNum == 2)
            {
                enemyFire = bool.Parse(data);
                newData2 = true;
                transferNum = 3;
            }
            else
            {
                readHitValue = bool.Parse(data);
                newData3 = true;
                transferNum = 1;
            }
           

        }
        client.Close();
    }
    public void Server()
    {
        string externalip = new WebClient().DownloadString("http://icanhazip.com");

        string ip = externalip.TrimEnd();


        IPAddress ip2 = IPAddress.Parse(ip.ToString());
        IPEndPoint endPoint = new IPEndPoint(ip2, 3333);
        Socket listener = new Socket(ip2.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        bool keepReading = true;

        Socket handler = null;
        Byte[] bytes;
        try
        {
            listener.Bind(endPoint);
            listener.Listen(10);
        }
        catch (Exception e)
        {
            connectionLost = true;
            UnityEngine.Debug.LogError(e);
        }
        bool end = false;
        while (end == false)
        {

            keepReading = true;
            UnityEngine.Debug.Log("Waiting");

            handler = listener.Accept();

            UnityEngine.Debug.Log("Connected In Game");
            data = null;
            break;
        }
        bool gameFinish = false;
        bool recievedGameOver = false;
        while (true)
        {
            while (true)
            {
                if (gotGameState == false)
                {
                    needGameState = true;
                }
                else
                {
                    gotGameState = false;
                    if (readGameState == true)
                    {
                        gameFinish = true;

                    }
                    break;
                }
            }
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            while (keepReading == true)
            {
                try
                {
                    bytes = new Byte[1024];
                    int bytesRec = handler.Receive(bytes);
                 
                    if (bytesRec <= 0)
                    {
                        keepReading = false;
                        handler.Disconnect(true);
                        break;
                    }
                    string tempData = Encoding.ASCII.GetString(bytes, 0, bytesRec);

                    data = Encoding.ASCII.GetString(bytes, 0, bytesRec);
                    if (data.Contains("!!!"))
                    {
                        if (data.Contains("END"))
                        {
                            handler.Close();
                            listener.Close();
                            apparentlyFinished = true;
                            recievedGameOver = true;
                            break;

                        }
                        data = data.Remove(data.Length - 3);
                        break;
                    }
                    if (data.IndexOf("<EOF>") > -1)
                    {
                        break;
                    }

                }
                catch (Exception e)
                {
                    UnityEngine.Debug.LogError(e);
                    connectionLost = true;
                }

            }
            stopwatch.Stop();
            Stopwatch stopwatch2 = new Stopwatch();
            stopwatch2.Start();
            if(recievedGameOver == true)
            {
                break;
            }
            if (gameFinish == true)
            {

            }
            else
            {
                if (transferNum == 1)
                {
                    enemyPos = data;
                    newData = true;

                }
                else if (transferNum == 2)
                {
                    enemyFire = bool.Parse(data);
                    newData2 = true;

                }
                else
                {
                    readHitValue = bool.Parse(data);
                    newData3 = true;

                }
            }
            bool doneFetching = false;
            Byte[] data3 = null;
            while (true)
            {
                if (gotGameState == false)
                {
                    needGameState = true;
                }
                else
                {
                    gotGameState = false;
                    if (readGameState == true)
                    {
                        gameFinish = true;
                        
                    }
                    break;
                }
            }
            stopwatch2.Stop();

            while (true)
            {
                
                if (gameFinish == true)
                {
                    UnityEngine.Debug.LogWarning("SENT GAME OVER");
                    data3 = Encoding.ASCII.GetBytes("END!!!");
                    
                    handler.Send(data3);
                    handler.Close();
                    listener.Close();
                    break;
                }
                if (transferNum == 1)
                {
                    if (gotPosition == false)
                    {
                        needPosition = true;
                    }
                    else
                    {
                        string myPos = myPosition.x + " " + myPosition.y;
                        data3 = Encoding.ASCII.GetBytes(myPos + "!!!");
                        doneFetching = true;
                        transferNum = 2;
                    }
                }
                else if (transferNum == 2)
                {

                    if (gotFire == false)
                    {
                        needFire = true;
                    }
                    else
                    {
                        string fireState = fire.ToString();
                        data3 = Encoding.ASCII.GetBytes(fireState + "!!!");
                        doneFetching = true;
                        transferNum = 3;
                    }
                }
                else if (transferNum == 3)
                {
                    if (gotHit == false)
                    {
                        needHit = true;
                    }
                    else
                    {
                        string imHit = hit.ToString();
                        data3 = Encoding.ASCII.GetBytes(imHit + "!!!");
                        doneFetching = true;
                        transferNum = 1;
                    }
                }


                if (doneFetching == true)
                {
                  
                    handler.Send(data3);
                    gotPosition = false;
                    gotHit = false;
                    gotFire = false;
                    doneFetching = false;
                    break;
                }

            }
            
            TimeSpan ts = stopwatch.Elapsed;
            TimeSpan ts2 = stopwatch2.Elapsed;
            int timeOther = int.Parse(ts2.Milliseconds.ToString());
            try
            {
                pingTime = int.Parse(ts.Milliseconds.ToString());
            }
            catch (FormatException)
            {
                pingTime = 0;
            }
            if(gameFinish == true)
            {
                break;
            }


        }
        handler.Close();
        listener.Close();



    }
}
