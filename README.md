# Unity Game

[CM Tut](https://youtu.be/AmGSEH7QcDg?si=1jsnQWUKKulV43mJ&t=29197)

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
13. There are no 'folders' for organising objects in the Hierachy, but you can use empty game objects in their stead. Just make sure to do the usual zeroing.
14. Remember that in C# when you divide an int by another int, the result will be an int (eg: 1 / 10 = 0 {!= 0.1}). Careful with this when trying to get fractional values.
15. At around [this timestamp](https://youtu.be/AmGSEH7QcDg?si=Eg-qQesHX0yGwHPt&t=23532), the instructor adds a UI image and it shows up on the scene view, despite it having its dimensions all set to 0. Not the case for me, so I've given it the same dimensions as its parent element (0.3). Everything still seems to work the same way, just noting it incase it causes issues for some reason down the line.
16. I'm making a habit of inverting the logic for the scripting from the tutorial. For example using early returns instead of nested `if` statements. This is helping me to conceptualize the logic better, and if I make a mistake in my version of the logic, I have an opportunity to practice some debugging.
17. You can have multiple methods with the same name and it works, as long as they have different params. The compiler automatically picks the right one based on the args passed. Good stuff.

---

## Additional Features

1. Add a chair for the player to sit down on. Poor cunt is surrounded by counters.
