using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;


namespace Ex05.WinFormUI
{
    public class FormLogin : Form
    {
        private readonly int r_MinNumberOfChances;
        private readonly int r_MaxNumberOfChances;
        private int m_SelectedNumberOfChances;
        Button m_ButtonStart = new Button();
        Button m_ButtonNumberOfChances = new Button();

        public FormLogin()
        {
            this.Size = new Size(250, 150);
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Bool Pgia";
            r_MinNumberOfChances = 4;
            r_MaxNumberOfChances = 10;
            m_SelectedNumberOfChances = 4;
        }

        public int SelectedNumberOfChances
        {
            get { return m_SelectedNumberOfChances; }
            set { m_SelectedNumberOfChances = value; }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            InitControls();
        }

        private void InitControls()
        {
            InitButtonNumberOfChances();
            InitButtonStart();
        }

        private void InitButtonNumberOfChances()
        {
            m_ButtonNumberOfChances.Text = string.Format("Number of chances: {0}", m_SelectedNumberOfChances);
            m_ButtonStart.Width = this.Width - 10;
            this.m_ButtonNumberOfChances.Click += new EventHandler(m_ButtonNumberOfChances_Click);
        }

        private void InitButtonStart()
        {
            m_ButtonStart.Text = "Start";
            this.m_ButtonStart.Click += new EventHandler(m_ButtonStart_Click);
        }

        void m_ButtonNumberOfChances_Click(object sender, EventArgs e)
        {
            m_SelectedNumberOfChances++;
            m_ButtonNumberOfChances.Text = string.Format("Number of chances: {0}", m_SelectedNumberOfChances);
        }

        void m_ButtonStart_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}