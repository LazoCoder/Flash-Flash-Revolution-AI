# Flash-Flash-Revolution-AI
Artificial Intelligence that plays Flash Flash Revolution. This AI is undefeatable but not omnipotent. It will never lose a game, no matter the difficulty of the level. However, it does not achieve perfect scores on harder levels.

# Details

The levels in the game range from 0 to 120. The following is a sample of the AI playing "Chaotic Sound", a level 39 song:

![alt-tag] (Results/level_chaotic_sound.gif)

The results at the end of the game show that the AI did not miss a single incoming arrow.

![alt-tag] (Results/result_chaotic_sound.png)

The most difficult level in the campaign is titled "The Disappearance of Hatsune Miku", at level 103. Although the AI did make some mistakes it was able to get through the level with 87% accuracy. Despite not achieving a 100% accuracy I still find the results astounding as I personally cannot survive this level for even 2 seconds.

![alt-tag] (Results/result_the_disappearance.png)

# Usage

It is a requirement to have a stand-alone Adobe Flash Player. It has only been test on version 16 and it can be found in the Flash Player folder. The link to the game is (this is for linking the stand-alone player to the game):
~~~
http://www.flashflashrevolution.com/~velocity/R%5E3.swf
~~~
The AI needs to be able to detect the window before starting and it also needs to be run at the beginning of the game. Therefore:

1. Open the stand-alone Flash Player.
2. Open the URL specified above.
3. Pick a song and start the game.
4. Run the AI.
5. Press enter in the AI console window to start it.
6. When the game is complete, close the AI window.

Each time a song is played afterwards, begin from step 4.

## Notes

The External folder contains two essential .dll files that must be referenced for the code to work. These two files are WindowsAPI.dll and and ImageProcessing.dll and the code for these can be seen in my Task-Automation and Image-Processing-Library repositories respectively. 
