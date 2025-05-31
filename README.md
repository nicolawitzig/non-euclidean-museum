# Non-Euclidean VR Museum: Making arbatrarily big museums when physical space is limited

![](https://img.shields.io/badge/Unity-6000.0.32f-blue.svg)

![](https://img.shields.io/badge/Render%20Pipeline-URP-green.svg)

![](https://img.shields.io/badge/VR-SteamVR-orange.svg)

**In VR applications we often havt the problem that the players phyiscal space is limited. By ditching the Euclidean axioms we can overcome this constraint and provide a much bigger space to explore than would actually be possible**

---

## Quick Look: Try to keep count of how many different exhibition rooms you see


https://github.com/user-attachments/assets/8b5a83c8-77c9-4a29-a4b6-98af580155bc


---

## Project Overview

The main trick to achieve thsi is using **stencil buffer portal techniques**, to make this feel like a much larger environment. Imagine in this demo **8 distinct rooms seamlessly connect within the physical space of just 4 rooms!**.

This project is only a **proof-of-concept**, primarily demonstrating the possibilities of non-euclidean level design in VR. Many differnet level designs would be possible this one is rather simple just doubling the amount of rooms. However, this technique of stencil portals is not at all limited to this. It allows for an arbitrary number of rooms. This means we can both extend a space by adding rooms but also make a space feel much smaller down to having only one room. Since I'm more interested in the concept and I'm terrible at modelling the current artworks are just geometric shapes to illustrate the concept. In the video demonstration free models from the asset store were used to make it feel less stale.

Using stencil buffers also allows for cool effects like the **Impossible Cube**: Depending on which face you peer into, you'll see entirely different "pockets" of space:

https://github.com/user-attachments/assets/5ac31ca1-9e88-413c-bc7a-ec726d1c12bd

This project was very much inspited by the YouTube videos by CodeParade (check out their [Non-Euclidean World Engine video](https://www.youtube.com/watch?v=kEB11PQ9Eo8) or their Hyperbolica DevLog series).

---

## Technology Stack

- **Unity Engine:** Developed with version `6000.0.32f` (newer versions likely also compatible).
- **Universal Render Pipeline (URP):** Worked well for me.
- **SteamVR Plugin:** Allows to easily bring Unity project to different VR devices.
- **Stencil Buffer Manipulation:** Custom shader and materials to change stencil values in the GPUs stencil buffer.

---

## Getting Started & Controls

### Prerequisites:

1. **Unity Hub** and **Unity Editor 6000.0.32f** (or a newer compatible version).
2. **SteamVR** installed on your system.
3. A **SteamVR-compatible VR Headset** (e.g., HTC Vive, Valve Index, Oculus Rift/Quest with Link).

### Running the Project:

1. Clone this repository: `git clone [your-repo-url]`
2. Open the project in Unity Hub.
3. Ensure your VR headset is connected and SteamVR is running.
4. Open the main scene
5. Press the **Play** button in the Unity Editor.

### Controls:

- **VR (Recommended):**
    - **Movement:**
        - Physically walk around your play space (room-scale).
        - VR joystick movement has been configured for HTC Vive Pro, this should not be too hard to change, check the PlayerMovement script
    - **Teleport to Origin:**
        - Highly recommended. This action needs to be mapped to a button on your hand controller. Again current configuration is for HTC Vive Pro
        - Open the `PlayerMovement.cs` script (usually attached to your VR Player/CameraRig GameObject).
        - Modify the script to assign a specific SteamVR action (e.g., Grip, Trigger, Menu button) to trigger the `TeleportToOrigin()` function.
    - **Interaction:**
        - Currently, users can pick up and throw some physics-enabled balls using the trigger/grip buttons (standard SteamVR interaction).
- **Desktop (For quick viewing, portals NOT functional):**
    - **WASD Keys:** Move forward/backward and strafe left/right.
    - **Mouse:** Look around.

---

## Important Notes & Limitations

- **VR IS ESSENTIAL:** To experience the core non-euclidean portal effects, a VR headset is **required**. The desktop mode is primarily for development and will not showcase the core features.
- **Proof of Concept Assets:** The 3D models and textures are from the Unity Asset Store used in the video were removed and replaced by simple geometric objects. The focus is on the spatial mechanics.
---

## Some future ideas I had

This project just outlines the basics. Maybe I will revisit it in the future. Feel free to use any ideas/code from this repo however you like.

- **Enhanced Interactivity:**
    - Develop mini-games or puzzles within each "room" that leverage the non-euclidean nature of the space.
    - Refine the physics interactions for more engaging experiences.
- **Different Non-Euclidean Geometries:**
    - Explore implementing different types of non-euclidean spaces in dedicated rooms:
        - **Hyperbolic Rooms:** Where parallel lines diverge.
        - **Spherical Rooms:** Where parallel lines eventually meet.
        - **Nil Geometry Rooms:** Pretty mind-bending manifolds: [Nil-Geometry Explained](https://www.youtube.com/watch?v=FNX1rZotjjI)
- **Artworks:**
    - Replace placeholder assets with custom, high-quality models and textures.
    - Some nice inspration for art that might work well in this concept is TeamLabs and Artechouse.
