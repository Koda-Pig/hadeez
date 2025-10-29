# Unity Game

[CM Tut](https://youtu.be/AmGSEH7QcDg?si=258Soi2uLDatAPtj&t=7407)

## Notes

1. Good practice to keep visuals and logic separate. In interest of this, the Player object was created for the logic, and an object inside that was created for the visuals.
2. You can only animate child objects. Another good reason to keep the visuals and logic separate.
3. Always remember to zero the position of new objects and things you add to the game (set position coordinates to 0, 0, 0)
4. I've set the 'Active Input Handling' to 'Both', as per the recommendation of the tutorial, but against the recommendation of the Unity program itself. Unity recommends using the newer input manager.
5. The tutorial instructs to create a new InputActions thing in the Project tab (named `PlayerInputActions`). I've not done so because there is an existing one that seems strongly tied to the new version of Unity. So I'm going to use the existing one (named `InputSystem_Actions`) instead. This is NB because the project often refers to `PlayerInputActions`, but in my case it will be `InputSystem_Actions`
