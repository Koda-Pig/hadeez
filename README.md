# Unity Game

[CM Tut](https://youtu.be/AmGSEH7QcDg?si=1jsnQWUKKulV43mJ&t=16635)

## Notes

1. Good practice to keep visuals and logic separate. In interest of this, the Player object was created for the logic, and an object inside that was created for the visuals.
2. You can only animate child objects. Another good reason to keep the visuals and logic separate.
3. Always remember to zero the position of new objects and things you add to the game (set position coordinates to 0, 0, 0)
4. I've set the 'Active Input Handling' to 'Both', as per the recommendation of the tutorial, but against the recommendation of the Unity program itself. Unity recommends using the newer input manager.
5. The tutorial instructs to create a new InputActions thing in the Project tab (named `PlayerInputActions`). I've not done so because there is an existing one that seems strongly tied to the new version of Unity. So I'm going to use the existing one (named `InputSystem_Actions`) instead. This is NB because the project often refers to `PlayerInputActions`, but in my case it will be `InputSystem_Actions`
6. Jeez like object collisions are insanely simple in Unity. Basically just this code in the Player.cs class:

   ```C#
   float playerSize = .7f;
   bool canMove = !Physics.Raycast(transform.position, moveDirection, playerSize);

   if (canMove)
   {
   	transform.position += moveDirection * moveSpeed * Time.deltaTime;
   }
   ```

   Coming from a game development background in vanilla JavaScript, where signifigant calculations are involved in detecting collisions, this is fantastic. See what I mean in [this game I made with just JS](https://github.com/Koda-Pig/playground/blob/main/knight-of-cups/script.js#L97). OK just progressed further in the tut and it was explained that the `Raycast` method detects collisions from the center of the player, not taking into account any player width or height. This is unsuitable for obvious reasons. Instead, a `CapsuleCast` is used, which is generally accepted as a good substitute for an actual player shape.

7. DON'T use Unity tags. Tags use strings, and those are shit identifiers.
8. I'm noticing some collision detection with the floor object, which is not happening in the tutorial. Not presenting an issue yet, but if it does a possible solution is to use a layer mask and then customize the Raycast check to filter out collisions with objects in that layer.
9. Use event listeners instead of doing constant `if` checks where appropriate. A good example of this is a player interaction. Not something you want to be checking every single frame, better to have a listener.
10. When creating multiple visuals inside of an object, ensure that one of the visuals is slightly bigger than the other (1% is enough). If they are left the same size, it can sometimes cause visual bugs.
11. `static` keyword means that the variable belongs only to the class itself! Not instances of the class.
12. Gotta remember to attach scripts, and references to things in the scripts in Unity UI as well.
