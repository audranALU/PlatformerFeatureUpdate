# Assignment 4 — 2D Platformer Feature Update

**Student:** Audran Ndayisenga  
**Programme:** Software Engineering, African Leadership University  
**Project:** PlatformerFeatureUpdate  
**Engine:** Unity 6.6 (6000.6.0f1), C#  
**Date:** 2 October 2026

## Submission links

Replace these placeholders before submission.

| Deliverable | Link |
|---|---|
| Gameplay demonstration video | ADD GAMEPLAY VIDEO LINK |
| Script walkthrough video | ADD SCRIPT WALKTHROUGH LINK |
| GitHub repository | ADD GITHUB REPOSITORY LINK |
| Published documentation page | ADD DOCUMENTATION PAGE LINK |

## Project overview

This assignment involved completing an inherited 2D platformer rather than creating a game from scratch. The supplied project contained artwork, a level, enemy prefabs, animations, and scripts, but some gameplay logic was incomplete. My task was to identify the gaps, fix movement and camera behaviour, manage player lives, and add the menu flow needed to make the game playable.

The updated game lets the player move across platforms, jump, collect coins, and encounter enemies. Falling into water removes a life and returns the player to a safe point near that water. When no lives remain, the game opens an end scene with Replay and Quit buttons.

## Controls

| Input | Action |
|---|---|
| A/D or left/right arrows | Move horizontally |
| Space | Jump when grounded |
| Mouse | Use menu buttons and settings controls |

## Bugs and fixes

### Player movement and jumping

The original `Awake()` method did not assign the Rigidbody2D component to `myBody`. I corrected it to `myBody = GetComponent<Rigidbody2D>();`. This gave the script a valid physics component to control.

The horizontal input used an incorrect axis name. I changed it to `Input.GetAxis("Horizontal")`, allowing A/D and the left/right arrows to move the player. The project was also configured for the new Input System while these scripts used `UnityEngine.Input`, so Active Input Handling was changed to Both.

I added `CheckIfGrounded()` and `PlayerJump()` to `Update()` so ground detection and jump input are checked every frame. Jumping uses `Input.GetKeyDown(KeyCode.Space)`, which detects a fresh press rather than continuously jumping while Space is held. The jump is permitted only when the ground check succeeds. The player's Ground Layer initially contained Nothing; selecting the platform layer fixed the remaining jump problem. The updated movement script uses Unity 6's Rigidbody2D `linearVelocity` property.

### Camera follow

The camera's private `target` variable had not been assigned, which caused a NullReferenceException. In `Start()`, I assigned it using `GameObject.FindGameObjectWithTag("Player").transform`. I kept the variable private and did not add `[SerializeField]`, as required by the rubric.

The camera keeps its depth offset and uses `Vector3.SmoothDamp` to follow horizontally. I removed the condition that allowed following only to the right. Following in both directions lets the camera return with the player after a respawn. The camera's vertical position remains fixed.

## Lives, water detection, and respawn

`GameManager.cs` manages the shared life counter, which starts at three, and updates LifeText. Enemy damage and water falls use this same counter. At zero lives, it loads `EndScene`.

`WaterRespawn.cs` is attached to individual water objects with trigger colliders. It detects the player through `OnTriggerEnter2D()` and asks GameManager to process the fall. With lives remaining, GameManager clears the player's linear and angular velocity and moves the player to the assigned respawn point.

I placed empty Transform objects on safe ground near the water pools. Water pieces in the same pool use the same nearby respawn point. Separate pools use their own points, so falling into water does not return the player to the beginning of the level.

The existing `PlayerDamage.cs` was updated to call GameManager rather than maintain a separate life counter. It includes a two-second cooldown between enemy damage calls.

## User interface and scene flow

The gameplay UI places the life and coin counts beside their icons using top-left anchors. A TextMeshPro timer placeholder displays `00:00` at the top-right. The timer is a visual placeholder; it does not count down.

Canvas Scaler uses Scale With Screen Size, a 1920 × 1080 reference resolution, and a Match value of 0.5. Anchors keep the HUD elements attached to their intended screen corners.

The project contains three scenes:

| Scene | Purpose |
|---|---|
| StartScene | Title, Play, Settings, and Quit buttons |
| GameScene-ALU | Platformer gameplay |
| EndScene | Game Over screen with Replay and Quit |

StartScene is first in the enabled build scene list. Play loads the gameplay scene, zero lives loads EndScene, and Replay reloads gameplay from the beginning. Quit stops Play mode in the Editor and exits a standalone application build.

## Settings and additional features

The settings page contains a volume slider, a Mute checkbox, and a Back button. `MainMenuController.cs` switches between the main menu and settings panels and handles Play and Quit.

Although functioning settings were optional, I implemented them with `AudioSettings.cs`. The slider adjusts global audio volume. Checking Mute silences all audio; unchecking it restores the selected volume. The settings are saved using PlayerPrefs and loaded when the start menu runs again.

My additional features are nearby water respawn and functioning audio settings with saved preferences. I also populated the level with different supplied enemy prefabs and tested their interactions with the player.

## Script responsibilities

| Script | Main responsibility |
|---|---|
| PlayerMovement.cs | Horizontal movement, facing direction, ground detection, and jumping |
| CameraFollow.cs | Find the player and smoothly follow horizontal movement |
| GameManager.cs | Track lives, update the life display, respawn, and load the end scene |
| WaterRespawn.cs | Detect player entry into a water trigger and supply a respawn point |
| PlayerDamage.cs | Forward enemy damage with a cooldown |
| MainMenuController.cs | Start gameplay, open/close Settings, and quit |
| EndMenu.cs | Replay gameplay and quit from the end screen |
| AudioSettings.cs | Set volume, mute audio, and save preferences |
| ScoreManager.cs | Supplied coin collection and coin count logic |

## Challenges and key takeaways

Working through this project taught me that a gameplay problem can come from either code or Inspector configuration. The jump code was in place, but it still failed while Ground Layer was set to Nothing. I learned to check component references, layers, tags, and collider settings alongside the script.

I also encountered duplicate `Start()` methods and duplicate PlayerDamage classes while editing. These errors helped me understand that replacing a method means removing the original block, and that an existing script should be edited rather than recreated with the same class name. Keeping the original script asset also preserves its scene connections.

Another takeaway was the difference between Editor testing and build startup. Pressing Play runs the currently open scene, while a built game starts from the first enabled scene in the build list. Opening EndScene in the Editor therefore showed the end menu instead of gameplay.

Overall, I gained practice reading inherited code, tracing null references, using 2D trigger events, arranging UI with anchors, and organising scene transitions. Testing each change separately made it easier to identify the cause when something failed.

## Testing

The features were manually tested in the Unity Editor during development. I tested movement, grounded jumping, camera following, water life loss and nearby respawn, enemy interactions, and the start/end menu buttons. I also checked volume and mute behaviour through gameplay sounds. After these checks, I confirmed that everything was working in the Editor.

Before final submission, I will repeat the complete flow from StartScene through gameplay, Game Over, and Replay, and show the relevant features in the gameplay recording. A standalone build has not been verified in this development record.

## Asset acknowledgement

The base level, artwork, animations, enemy prefabs, sounds, and original scripts came from the starter package supplied with the assignment. My contribution was completing and updating the scripts, configuring the gameplay objects, arranging the HUD, creating the menu scenes, and implementing nearby respawn and audio settings. The uploaded reference video was supplied as an assignment reference.
