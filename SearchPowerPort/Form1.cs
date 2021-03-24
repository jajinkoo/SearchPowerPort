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
                    }
                    Thread.Sleep(100);
                    break;
                }
                break;
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
    }
}
