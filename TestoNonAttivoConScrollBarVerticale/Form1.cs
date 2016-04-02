using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace provaTestoCon_Scorrimento_non_attivo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private int topIndex;

        private int bottomIndex;

        private int topLine;

        private int bottomLine;

        private int numrows = 0;

        private int numLinesDisplayed = 0;

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Text = "Questo e' un testo di esempio, fatto molto lungo affinché venga visualizzato automaticamente la scrollbar verticale, il problema però consiste nel mostrare la scrollbar verticale solo all'occorrenza e non quando non ce nè bisogno.";

            textBox1_Resize(null, null);
                
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            int pagPrec = textBox1.GetCharIndexFromPosition(new Point(1, 1));
            int pageNext = textBox1.GetCharIndexFromPosition(new Point(1, textBox1.Height - 1));

            var objVertScroll = sender as VScrollBar;
            if (e.OldValue > e.NewValue)
            {
                if (pagPrec > 0)
                    textBox1.SelectionStart = pagPrec;
                else
                    textBox1.SelectionStart = 0;
            }
            else if (e.OldValue < e.NewValue)
            {
                textBox1.SelectionStart = pageNext;
            }

            textBox1.SelectionLength = 0;
            textBox1.ScrollToCaret();
        }

        private int GetTextHeight(TextBox tBox)
        {
            return TextRenderer.MeasureText(tBox.Text, tBox.Font, tBox.ClientSize,
                     TextFormatFlags.WordBreak | TextFormatFlags.TextBoxControl).Height;
        }

        private void textBox1_Resize(object sender, EventArgs e)
        {
            int lineHeightPixel = TextRenderer.MeasureText(textBox1.Text, textBox1.Font).Height + 2;

            int pointTop = (int)(lineHeightPixel / 2);
            int pointBottom = (textBox1.ClientSize.Height - lineHeightPixel);

            topIndex = textBox1.GetCharIndexFromPosition(new Point(1, pointTop));
            bottomIndex = textBox1.GetCharIndexFromPosition(new Point(1, pointBottom));

            topLine = textBox1.GetLineFromCharIndex(topIndex);
            bottomLine = textBox1.GetLineFromCharIndex(bottomIndex) + 1;

            int numLinesDisplayed = bottomLine - topLine;

            int numrows = textBox1.GetLineFromCharIndex(textBox1.Text.Length) + 1;

            vScrollBar1.LargeChange = 1;
            if ((numrows - numLinesDisplayed) >= 1)
            {
                vScrollBar1.Visible = true;
                vScrollBar1.Maximum = numrows - numLinesDisplayed;    
            }
            else
            {
                vScrollBar1.Visible = false;
                vScrollBar1.Value = 0;
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            //MessageBox.Show("x,y: " + e.X + ", " + e.Y);
            bottomIndex = textBox1.GetCharIndexFromPosition(new Point(e.X, e.Y));
            //MessageBox.Show("y bottom: " + bottomIndex);
            bottomLine = textBox1.GetLineFromCharIndex(bottomIndex);

            MessageBox.Show("bottom Line: " + bottomLine);
        }

        private void textBox1_MouseMove(object sender, MouseEventArgs e)
        {
            label2.Text = e.X + ", " + e.Y;
        }

        private void scegliFontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fontDialog1.Font = textBox1.Font;
            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Font = fontDialog1.Font;
                textBox1_Resize(null, null);
            }
        }

        private void cambiaTestoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 modificaTesto = new Form2(textBox1.Text);
            if (modificaTesto.ShowDialog() == DialogResult.OK)
            {
                if (textBox1.Text != modificaTesto._strTesto)
                {
                    textBox1.Text = modificaTesto._strTesto;
                    vScrollBar1.Value = 0;
                }
                textBox1_Resize(null, null);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            var objCheck = sender as CheckBox;
            if (objCheck.Checked)
            {
                textBox1.Enabled = true;
                checkBox1.Text = "Text Box Abilitato"; 
            }
            else
            {
                textBox1.Enabled = false;
                checkBox1.Text = "Text Box Disabilitato";
            }

        }


    }
}
