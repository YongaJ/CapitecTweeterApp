using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APIcall
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            API.Service1 twiterApp = new API.Service1();
            var abc = twiterApp.ViewUsersFollowers();
            foreach (var item in abc)
            {
                listBox1.Items.Add(item.User + "=>" + item.Follower);
            }
        }
    }
}
