using System;
namespace Game_Of_Life
{
	public class Life
	{
		public int row;
		public int col;

		public Life(int row, int col)
		{
			this.row = row;
			this.col = col; 
		}

		public bool isNeighbor(Life life)
		{
			int delta_row = this.row - life.row;
			int delta_col = this.col - life.col;

			if (Math.Abs(delta_row) == 1 || Math.Abs(delta_col) == 1)
			{				
				return true;			
			}

		}

		public int num_neighbors(Life[] lives)
		{
			int count = 0; 

			foreach (Life life in lives)
			{
				if (isNeighbor(life))
				{
					count += 1; 
				}
			}

			return count; 
		}

		public bool should_die(Life[] lives)
		{
			int count = num_neighbors(lives);

			if( count >= 4 || count <= 2) 
			{
				return true;
			}
			return false;
		}
	}
}
