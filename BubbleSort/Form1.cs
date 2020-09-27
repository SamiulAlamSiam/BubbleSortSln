using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BubbleSort
{
    public enum StepsEnum
    {
        Start = 0,
        Selection = 1,
        Comparison = 2,
        Success = 3,
        Failure = 4
    }
    public partial class Form1 : Form
    {
        List<Button> allBtns =new List<Button>();
        private StepsEnum currentStep;
        private Button firstButton, secondButton;
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            allBtns.Add(button1);
            allBtns.Add(button2);
            allBtns.Add(button3);
            allBtns.Add(button4);
            allBtns.Add(button5);
            allBtns.Add(button6);

            currentStep = StepsEnum.Start;
        }

        private int i = -1, j = -1, totalIteration = 1, prevX, count = 0;

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            for (int k = 0; k < allBtns.Count; k++)
            {
                allBtns[k].BackColor = Color.DarkCyan;
            }

            if (currentStep == StepsEnum.Start)
            {
                Start();
            }
            else if (currentStep == StepsEnum.Selection)
            {
                Selection();
            }

            else if (currentStep == StepsEnum.Comparison)
            {
                CompareValue();
            }
            else if (currentStep == StepsEnum.Failure)
            {
                currentStep = StepsEnum.Selection;
                Selection();
            }
            else if (currentStep == StepsEnum.Success)
            {
                var temp = allBtns[i];
                allBtns[i] = allBtns[j];
                allBtns[j] = temp;

                prevX = firstButton.Location.X;
                count= 1;

                btnNext.Enabled = false;
                timer1.Start();

            }
            label1.Text = totalIteration.ToString();
        }

        private void CompareValue()
        {
            int f, s;
            f = Int32.Parse(firstButton.Text);
            s = Int32.Parse(secondButton.Text);

            if (f > s)
            {
                firstButton.BackColor = secondButton.BackColor = Color.DarkGreen;
                currentStep = StepsEnum.Success;
            }
            else
            {
                firstButton.BackColor = secondButton.BackColor = Color.DarkRed;
                currentStep = StepsEnum.Failure;
            }
        }

        private void Selection()
        {
            if (i == -1 && j == -1)
            {
                i = 0;
                j = 1;
            }

            else if (totalIteration > (allBtns.Count * allBtns.Count)-1)
            {
                btnNext.Enabled = false;
            }

            else if (totalIteration % (allBtns.Count - 1) == 0)
            {
                i = 0;
                j = 1;
                totalIteration++;
            }

            else
            {
                i++;
                j++;
                totalIteration++;
            }

            firstButton = allBtns[i];
            secondButton = allBtns[j];

            firstButton.BackColor = secondButton.BackColor = Color.DarkBlue;

            currentStep = StepsEnum.Comparison;
        }

        private void Start()
        {
            currentStep = StepsEnum.Selection;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (firstButton.Location.X == prevX && count <= 5)
            {
                firstButton.Location = new Point(firstButton.Location.X, firstButton.Location.Y - 5);
                secondButton.Location = new Point(secondButton.Location.X, secondButton.Location.Y + 5);
                count++;
            }
            else if (secondButton.Location.X > prevX)
            {
                firstButton.Location = new Point(firstButton.Location.X + 5, firstButton.Location.Y);
                secondButton.Location = new Point(secondButton.Location.X - 5, secondButton.Location.Y);
            }
            else if (firstButton.Location.X != prevX && count > 1)
            {
                firstButton.Location = new Point(firstButton.Location.X, firstButton.Location.Y + 5);
                secondButton.Location = new Point(secondButton.Location.X, secondButton.Location.Y - 5);
                count--;
            }
            else
            {
                timer1.Stop();
                btnNext.Enabled = true;
                currentStep = StepsEnum.Selection;
            }
        }

        
    }
}
