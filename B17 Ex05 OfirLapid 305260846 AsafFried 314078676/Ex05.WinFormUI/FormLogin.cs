﻿using System;
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
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.Text = "Bool Pgia";
            r_MinNumberOfChances = 4;
            r_MaxNumberOfChances = 10;
            m_SelectedNumberOfChances = r_MinNumberOfChances;
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
            this.Controls.AddRange(new Control[] { m_ButtonStart, m_ButtonNumberOfChances });
        }

        private void InitButtonNumberOfChances()
        {
            m_ButtonNumberOfChances.Text = string.Format("Number of chances: {0}", m_SelectedNumberOfChances);
            m_ButtonNumberOfChances.Width = this.Width - 20;
            m_ButtonNumberOfChances.Location = new Point(7, 20);
            this.m_ButtonNumberOfChances.Click += new EventHandler(ButtonNumberOfChances_Click);
        }

        private void InitButtonStart()
        {
            m_ButtonStart.Text = "Start";
            m_ButtonStart.Location = new Point(m_ButtonNumberOfChances.Right - m_ButtonStart.Width,
                                               m_ButtonNumberOfChances.Top + 70);
            this.m_ButtonStart.Click += new EventHandler(ButtonStart_Click);
        }

        void ButtonNumberOfChances_Click(object sender, EventArgs e)
        {
            if(m_SelectedNumberOfChances < r_MaxNumberOfChances)
            {
				m_SelectedNumberOfChances++;
				m_ButtonNumberOfChances.Text = string.Format("Number of chances: {0}", m_SelectedNumberOfChances);
            }
        }

        void ButtonStart_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}