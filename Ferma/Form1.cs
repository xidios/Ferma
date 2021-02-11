using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ferma
{
    public partial class Form1 : Form
    {
        int day = 0;
        int money = 100;
        Dictionary<CheckBox, Cell> field = new Dictionary<CheckBox, Cell>();

        public Form1()
        {
            InitializeComponent();
            foreach (CheckBox cb in tableLayoutPanel1.Controls)
                field.Add(cb, new Cell());
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (sender as CheckBox);
            if (cb.Checked) Plant(cb);
            else Hervest(cb);
        }
        private void Plant(CheckBox cb)
        {
            if (money >= 2)
            {
                field[cb].Plant();
                money -= 2;
            }
            UpdateBox(cb);
        }
        private void Hervest(CheckBox cb)
        {
            field[cb].Harvest(ref money);           
            UpdateBox(cb);
        }

        

        private void timer1_Tick(object sender, EventArgs e)
        {
            day++;
            foreach (CheckBox cb in tableLayoutPanel1.Controls)
            {
                field[cb].NextStep();
                UpdateBox(cb);
            }
            CountOfPlantes();
            Text = $"Day: {day}";
            money_label.Text = $"Money: {money}";
            label1.Text = $"Timer: {trackBar1.Value} ms";
        }
        private void CountOfPlantes()
        {
            int count_of_empty = 0;
            int count_of_planted = 0;
            int count_of_green = 0;
            int count_of_immature = 0;
            int count_of_mature = 0;
            int count_of_overgrow = 0;

            foreach (CheckBox cb in tableLayoutPanel1.Controls)
            {
                switch (field[cb].state)
                {
                    case CellState.Empty:
                        count_of_empty++;
                        break;
                    case CellState.Planted:
                        count_of_planted++;
                        break;
                    case CellState.Green:
                        count_of_green++;
                        break;
                    case CellState.Immature:
                        count_of_immature++;
                        break;
                    case CellState.Mature:
                        count_of_mature++;
                        break;
                    case CellState.Overgrow:
                        count_of_overgrow++;
                        break;
                }
            }
            empty_label.Text = $"Count of empty cells: {count_of_empty}";
            planted_label.Text = $"Count of planted cells: {count_of_planted}";
            green_label.Text = $"Count of green cells: {count_of_green}";
            immature_label.Text = $"Count of immature cells: {count_of_immature}";
            mature_label.Text = $"Count of mature cells: {count_of_mature}";
            overgrow_label.Text = $"Count of overgrow cells: {count_of_overgrow}";
        }
        
        private void UpdateBox(CheckBox cb)
        {
            Color c = Color.White;
            switch (field[cb].state)
            {
                case CellState.Planted:
                    c = Color.Black;
                    break;
                case CellState.Green:
                    c = Color.Green;
                    break;
                case CellState.Immature:
                    c = Color.Yellow;
                    break;
                case CellState.Mature:
                    c = Color.Red;
                    break;
                case CellState.Overgrow:
                    c = Color.Brown;
                    break;
            }

            cb.BackColor = c;

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            timer1.Interval = trackBar1.Value;
            label1.Text = $"Timer: {trackBar1.Value} ms";
        }
    }
}
