using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferma
{
    public class Cell
    {

        const int prPlanted = 20;
        const int prGreen = 60;
        const int prImmature = 80;
        const int prMature = 100;
        public CellState state = CellState.Empty;
        private int progress;


        internal void Plant()
        {
            state = CellState.Planted;
        }

        internal void Harvest(ref int money)
        {
            if (state == CellState.Immature)
            {
                money += 2;
                EmptyState();
            }
            else if (state == CellState.Mature)
            {
                money += 5;
                EmptyState();
            }
            else if (state == CellState.Overgrow && money >= 1)
            {
                money--;
                EmptyState();
            }
            else if (state==CellState.Green)
            {
                EmptyState();
            }
        }
        private void EmptyState()
        {
            state = CellState.Empty;
            progress = 0;
        }
        public void NextStep()
        {
            if ((state != CellState.Overgrow) && (state != CellState.Empty))
            {
                progress++;
                if (progress < prPlanted) state = CellState.Planted;
                else if (progress < prGreen) state = CellState.Green;
                else if (progress < prImmature) state = CellState.Immature;
                else if (progress < prMature) state = CellState.Mature;
                else state = CellState.Overgrow;
            }
        }

    }
}
