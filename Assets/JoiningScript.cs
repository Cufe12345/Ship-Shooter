using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class JoiningScript : MonoBehaviour
{
    GameObject hostPanel;
    GameObject clientPanel;
    GameObject mainPanel;
    GameObject inputField;
    GameObject connectButton;
    GameObject ipText;
    GameObject inputText;
    GameObject main;
    GameObject hostText;
    GameObject clientText;
    string data;
    string ip;
    bool change = false;
    string path10 = "Assets/GameData/Position.txt";
    string path4 = "Assets/GameData/HealthLevelOnline.txt";
       string path3 = "Assets/GameData/BulletLevelOnline.txt";
        string path2 = "Assets/GameData/GunLevelOnline.txt";
        string path1 = "Assets/GameData/ShipTypeOnline.txt";
       string  path5 = "Assets/GameData/MoneyValue.txt";
    Thread clientThread;
    Thread serverThread;
    LoadData2 dataLoad;
    int start = 0;
    bool DoneFully = false;
    string hostTextValue;
    string clientTextValue;

    // Start is called before the first frame update
    void Start()
    {
        hostTextValue = "Waiting for connection....";
        clientTextValue = "Waiting for connection....";
        main = GameObject.Find("Canvas");
        dataLoad = main.GetComponent<LoadData2>();
        mainPanel = GameObject.Find("PanelMain");
        clientPanel = GameObject.Find("PanelConnectingClient");
        hostPanel = GameObject.Find("PanelConnectingHost");
        inputField = GameObject.Find("InputField");
        connectButton = GameObject.Find("ConnectButton");
        ipText = GameObject.Find("IpText");
        hostText = GameObject.Find("HostText");
        clientText = GameObject.Find("ClientText");
        inputText = GameObject.Find("TextMain");
        clientPanel.SetActive(false);
        hostPanel.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hostText.activeSelf == true)
        {
            hostText.GetComponent<Text>().text = hostTextValue;
        }
        if (clientText.activeSelf == true)
        {
            clientText.GetComponent<Text>().text = clientTextValue;
        }

        if (change == true)
        {
            change = false;
            ipText.GetComponent<Text>().text = "Give the ip below to your friend as they will need it to join                          " +
                "                                                                                 Your Ip is: " + ip;
        }
        if(DoneFully == true)
        {
            SceneManager.LoadScene("OnlineVsScene");
        }
    }
    public void Back(bool pressed = false)
    {
        if (pressed == true)
        {
            clientPanel.SetActive(false);
            hostPanel.SetActive(false);
            mainPanel.SetActive(true);
        }
    }
    public void Host(bool pressed = false)
    {
        if (pressed == true)
        {
            clientPanel.SetActive(false);
            hostPanel.SetActive(true);
            mainPanel.SetActive(false);
            hostTextValue = "Waiting for connection....";
            start = 1;
            serverThread = new Thread(Server);
            serverThread.IsBackground = true;
            serverThread.Start();
        }
    }
    public void ClientPanel(bool pressed = false)
    {
        if (pressed == true)
        {
            clientTextValue = "Waiting for connection....";
            clientPanel.SetActive(true);
            hostPanel.SetActive(false);
            mainPanel.SetActive(false);
            inputField.SetActive(true);
            connectButton.SetActive(true);
        }
    }
    public void Connect(bool pressed = false)
    {
        if (pressed == true)
        {
            start = 2;
            ip = inputText.GetComponent<Text>().text;
            inputField.SetActive(false);
            connectButton.SetActive(false);
            clientThread = new Thread(Client);
            clientThread.IsBackground = true;
            clientThread.Start();
        }
    }
    public void Client()
    {
        
        Debug.Log(ip);
        IPAddress ip2 = IPAddress.Parse(ip);
        IPEndPoint end = new IPEndPoint(ip2, 3333);
        Socket client = new Socket(ip2.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        try
        {
            client.Connect(end);
        }catch(Exception e)
        {
            clientTextValue = e + "                                                                                " +
                "                                                 " + "Please return back to main menu and try again";
        }
        clientTextValue = "Connected to device on Ip "+ip + "                                                     " +
            "                                                              Setting up game........";
        Byte[] data1;
        int readNum = 1;
        while (true)
        {
            Debug.LogError("Should send : " + readNum);
            //data sending
            if (readNum == 1)
            {
                string externalip = new WebClient().DownloadString("http://icanhazip.com");
                string tempIp = externalip.TrimEnd();
                data1 = Encoding.ASCII.GetBytes(tempIp + "!!!");
                client.Send(data1);
            }
            
            else if (readNum == 2)
            {
                string temp1 = dataLoad.shipType.ToString();
                data1 = Encoding.ASCII.GetBytes(temp1 + "!!!");
                client.Send(data1);
                Debug.LogError("Sent");
            }
            else if (readNum == 3)
            {
                string temp2 = dataLoad.gunLevel.ToString();
                data1 = Encoding.ASCII.GetBytes(temp2 + "!!!");
                client.Send(data1);
            }
            else if (readNum == 4)
            {
                string temp3 = dataLoad.bulletLevel.ToString();
                data1 = Encoding.ASCII.GetBytes(temp3 + "!!!");
                client.Send(data1);
            }
            else if (readNum == 5)
            {
                string temp4 = dataLoad.healthLevel.ToString();
                data1 = Encoding.ASCII.GetBytes(temp4 + "!!!");
                client.Send(data1);
            }

            Byte[] bytes;

            while (true)
            {
                try
                {
                    bytes = new Byte[1024];
                    int bytesRec = client.Receive(bytes);
                    Debug.LogError("Received");
                    if (bytesRec <= 0)
                    {

                        client.Close();
                        break;
                    }
                    string tempData = Encoding.ASCII.GetString(bytes, 0, bytesRec);

                    data = Encoding.ASCII.GetString(bytes, 0, bytesRec);

                    Debug.Log("readNum= " + readNum.ToString());
                    if (data.Contains("!!!"))
                    {
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
                    Debug.LogError(e);
                    clientTextValue = e + "                                                                                " +
                "                                                 " + "Please return back to main menu and try again";
                }

            }
            if (readNum == 1)
            {
                StreamWriter w = new StreamWriter(path10, false);
                w.WriteLine(data + "C");
                w.Close();
                ip = data;
            }
            else if (readNum == 2)
            {
                StreamWriter w = new StreamWriter(path1, false);
                w.WriteLine(data);
                w.Close();
            }
            else if (readNum == 3)
            {
                StreamWriter w = new StreamWriter(path2, false);
                w.WriteLine(data);
                w.Close();
            }
            else if (readNum == 4)
            {
                StreamWriter w = new StreamWriter(path3, false);
                w.WriteLine(data);
                w.Close();
            }
            else if (readNum == 5)
            {
                StreamWriter w = new StreamWriter(path4, false);
                w.WriteLine(data);
                w.Close();
            }
            readNum++;
            if (readNum == 6)
            {
                break;
            }
        }
        clientTextValue = "Done setup loading you into the game now....";
        client.Close();
        DoneFully = true;
        clientThread.Abort();



    }
    public void Server()
    {
        int readNum = 1;
        string externalip = new WebClient().DownloadString("http://icanhazip.com");

        ip = externalip.TrimEnd();
        change = true;

        IPAddress ip2 = IPAddress.Parse(ip.ToString());
        IPEndPoint endPoint = new IPEndPoint(ip2, 3333);
        Socket listener = new Socket(ip2.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        bool keepReading = true;

        Socket handler = null;
        Byte[] bytes;
        Byte[] data2;
        try
        {
            listener.Bind(endPoint);
            listener.Listen(10);
        }
        catch (Exception e)
        {
            hostTextValue = e + "                                                                                " +
                "                                                 " + "Please return back to main menu and try again";
            Debug.LogError(e);
        }
        bool end = false;
        while (true)
        {
            
            keepReading = true;
            Debug.Log("Waiting");

            handler = listener.Accept();

            Debug.Log("Connected");
            hostTextValue = "Connected setting up game........";
            break;
        }
        while (end == false)
        {

            
            data = null;


            while (keepReading == true)
            {
                try
                {
                    bytes = new Byte[1024];
                    int bytesRec = handler.Receive(bytes);
                    Debug.Log("Received");
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
                        data = data.Remove(data.Length - 3);
                        break;
                    }
                    Debug.Log("readNum= " + readNum.ToString());
                   if(readNum == 6)
                    {
                        
                        break;
                    }
                    if (data.IndexOf("<EOF>") > -1)
                    {
                        break;
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                    hostTextValue = e + "                                                              " +
                        "                  " + "Please return back to main menu and try again";
                }

            }
            if (readNum == 1)
            {
                StreamWriter w = new StreamWriter(path10, false);
                w.WriteLine(data + "H");
                w.Close();
                ip = data;
                string tempIp = externalip.TrimEnd();
                data2 = Encoding.ASCII.GetBytes(tempIp + "!!!");
                handler.Send(data2);
                Debug.Log("Sent");
            }
            else if (readNum == 2)
            {
                StreamWriter w = new StreamWriter(path1, false);
                w.WriteLine(data);
                w.Close();
                string temp1 = dataLoad.shipType.ToString();
                data2 = Encoding.ASCII.GetBytes(temp1 + "!!!");
                handler.Send(data2);
            }
            else if (readNum == 3)
            {
                StreamWriter w = new StreamWriter(path2, false);
                w.WriteLine(data);
                w.Close();
                string temp2 = dataLoad.gunLevel.ToString();
                data2 = Encoding.ASCII.GetBytes(temp2 + "!!!");
                handler.Send(data2);
            }
            else if (readNum == 4)
            {
                StreamWriter w = new StreamWriter(path3, false);
                w.WriteLine(data);
                w.Close();
                string temp3 = dataLoad.bulletLevel.ToString();
                data2 = Encoding.ASCII.GetBytes(temp3 + "!!!");
                handler.Send(data2);
            }
            else if (readNum == 5)
            {
                StreamWriter w = new StreamWriter(path4, false);
                w.WriteLine(data);
                w.Close();
                string temp4 = dataLoad.healthLevel.ToString();
                data2 = Encoding.ASCII.GetBytes(temp4 + "!!!");
                handler.Send(data2);
            }
            readNum++;

            if (readNum == 6)
            {
                Debug.Log("Done");
                hostTextValue = " Completed setup joining game.........";
               
                
                
                
                
                
                
                
              



                handler.Close();
                listener.Close();
                DoneFully = true;
                
                serverThread.Abort();
            }
            
            
            
            
        }
    }
}
