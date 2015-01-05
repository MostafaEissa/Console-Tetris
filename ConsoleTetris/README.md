# Console Tetris

This is the famous Tetris game implemented as a console application using C# and the .NET framework.

## What is Tetris

Tetris is a tile-matching puzzle video game. Tetriminos are game pieces, geometric shapes composed of four square blocks each. see [Tetris](http://en.wikipedia.org/wiki/Tetris) for a detailed history on [Wikipedia](http://www.wikipedia.org).


## How to Play?

A random sequence of Tetriminos fall down the playing field. The objective of the game is to manipulate these Tetriminos, by moving each one sideways and rotating it by 90 degree units, with the aim of creating a horizontal line of ten blocks without gaps. When such a line is created, it disappears, and any block above the deleted line will fall. The more lines you achieve the higher your score will get. As the game progresses, each level causes the Tetriminos to fall faster, and the game ends when the stack of Tetriminos reaches the top of the playing field.

###Controls

The game has five controls.

| Control   |             Action                 |
|-------|----------------------------------------|
|:arrow_right:| Move the current block to the right.   |
|:arrow_left:| Move the current block to the left.    |
|:arrow_up: | Rotate the current block.              |
|:arrow_down: |Speed up the fall of the current block. |
|ESC   |Exit the game.                          |
