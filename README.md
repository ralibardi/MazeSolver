# MazeSolver

/***************************************
        HOW TO USE
***************************************/
- Run MazeSolver.exe

- Type the route of the file (with the 
file
name and file extension (only *.txt))

- Press ENTER and wait for the solution

/***************************************
        HOW IT WORKS
***************************************/
The program uses a variation from the 
Wavefront algorithm to find a solution 
assigning a value to each cell according 
to how far they are from the end of the 
maze and a fixed value to the walls or 
obstacles that we might find. The initial
value is selected and, in this case, 
corresponds to the number of cells in the 
grid. After this, every iteration serves 
to assign a value to nearby cells using 
the movement guidelines until we get to 
the starting point. 

/***************************************
        JUSTIFICATION
***************************************/
I decided to use this approach because 
it was one of the algorithms I had to use
on my graduate thesis (frankly the easiest
one and the one with the best results).
