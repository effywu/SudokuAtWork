using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SudokuAtWork
{
    public partial class MainForm : Form
    {
        int[,] grids = new int[9, 9];
        HashSet<int>[,] tried_grid = new HashSet<int>[9, 9];
        public MainForm()
        {
            InitializeComponent();
            generate();
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    string id = "label" + (i * 9 + j + 1).ToString();
                    tableLayoutPanel1.GetControlFromPosition(j, i).Text = grids[i, j].ToString();
                };
            };
        }


        private bool safeNum(int row, int column, int num)
        {
            //check current row
            for (int i = 0; i < column; i++)
            {
                if (grids[row, i] == num) return false;
            };
            //check current column
            for (int i = 0; i < row; i++)
            {
                if (grids[i, column] == num) return false;
            };
            //check 3 x 3
            int whichrow = row % 3;
            int whichcolumn = column % 3;
            for (int i = row - whichrow; i < row; i++)
            {
                for (int j = column - whichcolumn; j < column - whichcolumn + 3; j++)
                {
                    if (grids[i, j] == num) return false;
                };
            };
            return true;
        }
        private bool generate()
        {
            int row = 0;
            int column = 0;
            if (!findEmpty(ref row, ref column)) return true;

            tried_grid[row, column] = new HashSet<int>();
            Random rand = new Random();
            while (tried_grid[row, column].Count < 9)
            {
                var range = Enumerable.Range(1, 9).Where(i => !tried_grid[row, column].Contains(i));
                int randomIndex = rand.Next(0, 8 - (tried_grid[row, column].Count));
                int num = range.ElementAt(randomIndex);
                tried_grid[row, column].Add(num);
                if (safeNum(row, column, num))
                {
                    grids[row, column] = num;
                    if (generate()) return true;
                    grids[row, column] = 0;
                };
            };
            return false;
        }

        private bool findEmpty(ref int row, ref int column)
        {
            for (row = 0; row < 9; row++)
            {
                for (column = 0; column < 9; column++)
                {
                    if (grids[row, column] == 0) return true;
                };
            };
            return false;
        }
    }
}
