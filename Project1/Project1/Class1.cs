using System;
using System.Collections.Generic;
using System.Text;

namespace Project1
{
    public abstract class A
    {
        public delegate void OnClick(int i);
        internal event OnClick m_click;
        int i;
        public static int lapid;

        public A(int i_i)
        {
            i = i_i;
        }

        public void onPush()
        {
            m_click.Invoke(7);

        }

    }
        public class B 
        {

            public B(int i)
            
            {
                i = 3;
            }

            public void RunFAST()
            {

            }

            private  void privateRun()
            {

            }

            protected virtual void onPush()
            {
            Object B = 8;

            }
        }


    }
