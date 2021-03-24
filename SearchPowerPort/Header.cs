using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;




/// <summary>
/// 프로그램 내용 
/// 시스템에 사용중이 port를 차례대로 돌면서 사용할 포트를 찾고 리스트 업한다. 
/// 개별 포트로 연결 후 command전달하고 받을수 있다. 
/// 
/// </summary>

namespace SearchPowerPort
{

    public partial class Form1 : Form
    {
        enum BaudRate {
            RATE2400 = 2400,
            RATE4800 = 4800, 
            RATE9600 = 9600, 
            RATE19200 = 19200, 
            RATE38400 = 38400, 
            RATE57600 = 57600,
            RATE115200 = 115200
        }

        SerialPort pSerial;
        bool m_bSearchStop;
        string[] PortName;


        private void button1_Click(object sender, EventArgs e)
        {
            if (!SearchPortAndShowDataToList())
            {
                return;
            }
 
        }
        private void button2_Click(object sender, EventArgs e)
        {
            m_bSearchStop = true;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            ConnectDirectPort();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            UnConnectDirectPort();
        }


    }
}
