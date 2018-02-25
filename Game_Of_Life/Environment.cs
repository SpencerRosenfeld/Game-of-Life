using System;


namespace Game_Of_Life
{
	public class Environment
	{
		public const int MAX_POPULATION = 200;
		int current_population = 0; 
		public const int ENVIRONMENT_SIZE = 30;
		public Boolean[,] Lives = new Boolean[ENVIRONMENT_SIZE,ENVIRONMENT_SIZE];
		public Boolean[,] NextGeneration = new Boolean[ENVIRONMENT_SIZE, ENVIRONMENT_SIZE];

		int[] row_mesh = { 0,1,1,1,0,-1,-1,-1};
		int[] col_mesh = { 1, 1, 0, -1, -1, -1, 0, 1 };

		public Environment()
		{
			for (int row = 0; row < ENVIRONMENT_SIZE; row++)
			{
				for (int col = 0; col < ENVIRONMENT_SIZE; col++)
				{
					Lives[row, col] = false;
					NextGeneration[row, col] = false;
				}
			}
		}

		public void make_glider(int r, int c)
		{
			make_alive(c+0, r+2, Lives);
			make_alive(c+1, r+2, Lives);
			make_alive(c+2, r+2, Lives);
			make_alive(c+2, r+1, Lives);
			make_alive(c+1, r+0, Lives);

		}

		public void normalize_rows_cols(ref int row, ref int col)
		{
			if (row < 0)
			{
				row = row + ENVIRONMENT_SIZE;
			}
			if (col < 0)
			{
				col = col + ENVIRONMENT_SIZE;
			}
			row = row % ENVIRONMENT_SIZE;
			col = col % ENVIRONMENT_SIZE;
		}
		public void make_alive(int row, int col, Boolean[,] Lives)
		{
			if (current_population < MAX_POPULATION)
			{
				normalize_rows_cols(ref row, ref col);
				Lives[row, col] = true;
			}
		}

		public void make_dead(int row, int col, Boolean[,] Lives)
		{
			normalize_rows_cols(ref row, ref col); 
			Lives[row, col] = false;
		}

		public void Prepare_Next_Gen()
		{
			for (int row = 0; row < ENVIRONMENT_SIZE; row++)
			{
				for (int col = 0; col < ENVIRONMENT_SIZE; col++)
				{
					NextGeneration[row, col] = Lives[row,col];
				}
			}
		}

		public void Copy_Next_Gen_To_Present()
		{
			for (int row = 0; row < ENVIRONMENT_SIZE; row++)
			{
				for (int col = 0; col < ENVIRONMENT_SIZE; col++)
				{
					Lives[row, col] = NextGeneration[row, col];
				}
			}
		}

		public int num_neighbors(int row, int col)
		{
			int count = 0;

			for (int i = 0; i < row_mesh.Length; i++)
			{
				if (isAlive( row + row_mesh[i], col + col_mesh[i] ))
				{
					count += 1;
				}
			}

			return count;
		}

		public bool isAlive(int row, int col)
		{
			normalize_rows_cols(ref row, ref col);

			if (Lives[row, col] == true)
			{
				return true;
			}

			return false; 
		}

		public void Update()
		{
			this.Prepare_Next_Gen();
			for (int row = 0; row < ENVIRONMENT_SIZE; row++)
			{
				for (int col = 0; col < ENVIRONMENT_SIZE; col++)
				{
					int count = num_neighbors(row, col);

					if (isAlive(row, col))
					{
						if (count > 3 )
						{
							make_dead(row, col, NextGeneration);
							current_population -= 1;
						}
						else if (count < 2)
						{
							make_dead(row, col, NextGeneration);
							current_population -= 1; 
						}
					}
					else
					{
						if (count == 3)
						{
							make_alive(row, col, NextGeneration);
							current_population += 1;
						}
					}
				}
			}
			this.Copy_Next_Gen_To_Present();
		}
	}
}
