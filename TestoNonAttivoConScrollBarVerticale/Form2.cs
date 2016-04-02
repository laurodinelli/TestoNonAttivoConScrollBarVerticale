using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace provaTestoCon_Scorrimento_non_attivo
{
    public partial class Form2 : Form
    {
        public string _strTesto = "";
        public Form2(string testo)
        {
            InitializeComponent();
            _strTesto = testo;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            richTextBox1.Text = _strTesto;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _strTesto = richTextBox1.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
