using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;

namespace SearchPowerPort
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            pSerial = new SerialPort();        
        }

        private bool SearchPortAndShowDataToList()
        {
            //리스트 박스 초기화 
            listBox_SearchPort.Items.Clear();
            listBox_Message.Items.Clear();

            m_bSearchStop = false;


            // 포트 얻기 
            if (!LoadPortName())
            {
                MessageBox.Show("Do not search port");
                return false;
            }

            // 하나씩 연결하면서 연결되는지 확인          
            int nPortCnt = PortName.Length;
            int nBaudRateCnt = System.Enum.GetValues(typeof(BaudRate)).Length;

         
            foreach(String Searchport in PortName)
            {
                foreach (int nBaudRate in Enum.GetValues(typeof(BaudRate)))
                {

                    if (m_bSearchStop)
                        return false;

                    //연결 시작
                    if (pSerial.IsOpen)
                    {
                        pSerial.Close();
                    }

                    //세팅값은 2개만 하지. 추가할것 있으면 더 해도됨
                    pSerial.PortName = Searchport;
                    pSerial.BaudRate = nBaudRate;

                    // 연결
                    pSerial.Open();

                    if(pSerial.IsOpen)
                    {
                        string strTemp;
                        strTemp = string.Format("{0} BaudRate {1}", Searchport, nBaudRate);
                        listBox_SearchPort.Items.Add(strTemp);
                        listBox_Message.EndUpdate();

                        pSerial.Close();
                    }
                    Thread.Sleep(100);              
                }
 
            }

            return true;
        }

        bool LoadPortName()
        {
            PortName = SerialPort.GetPortNames();

            int nNum = PortName.Length;

            if (nNum <= 0)
                return false;

            return true;
        }
        bool ConnectDirectPort()
        {
            int nCurrentPort = listBox_SearchPort.SelectedIndex;

            String strUsingPort = "";
            int nUsingBaudRate = -1;
            int nTempCnt = -1;

            if (PortName.Length <= 0)
                return false;

            bool bIsStop = false;
            foreach (String Searchport in PortName)
            {
                foreach (int nBaudRate in Enum.GetValues(typeof(BaudRate)))
                {
                    strUsingPort = Searchport;
                    nUsingBaudRate = nBaudRate;

                    if (nTempCnt == nCurrentPort)
                        bIsStop = true;

                    if (bIsStop)
                        break;
                }
                if (bIsStop)
                    break;
            }

            if (nUsingBaudRate == -1 || nTempCnt == -1)
            {
                MessageBox.Show("Port Search Fail");
            }

            //통신 연결 
            if (pSerial.IsOpen)
                pSerial.Close();

            pSerial.PortName = strUsingPort;
            pSerial.BaudRate = nUsingBaudRate;
            pSerial.DataReceived += new SerialDataReceivedEventHandler(ReceiveData); // 리시브 받을려고 생성 
            pSerial.Open();

            if (pSerial.IsOpen)
            {
                MessageBox.Show("Success Connect");
            }
            else
            {
                MessageBox.Show("Fail Connect");
                return false;
            }

            return true;
        }


        bool UnConnectDirectPort()
        {
            if (pSerial.IsOpen)
                pSerial.Close();

            if (!pSerial.IsOpen)
            {
                MessageBox.Show("Fail Unconnect");
                return false;
            }
            return true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (pSerial.IsOpen)
            {
                pSerial.Write(textBox1.Text);
            }
            else
            {
                MessageBox.Show("Do not open the port");
            }
        }

        private void ReceiveData(object sender, SerialDataReceivedEventArgs e)
        {
            this.Invoke(new EventHandler(MyReceived));
        }

        private void MyReceived(object s, EventArgs e)
        {
            String strData = pSerial.ReadExisting();
            strData = string.Format("{0:X2}", strData);

            listBox_Message.Items.Add(strData);

        }
    }
}
