using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

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

        }

        
    }
}
