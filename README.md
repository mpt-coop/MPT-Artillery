# SSH MPT Artillery Strikes

## Description
Artillery Artillery Strikes is a rewritten mod for MPT. This mod enhances the gameplay experience by introducing the functionality to deploy smoke grenades, which in turn can call in artillery strikes. Whether you're looking to create strategic cover or rain destruction upon your enemies, Artillery Mod provides you with the tools to do so effectively.

## Features
- **Smoke Grenades:** Equip your character with smoke grenades to create instant cover or diversionary tactics on the battlefield. Additionally, throwing a smoke grenade can call in artillery strikes to the designated location, providing both offensive and defensive capabilities.
- **Artillery Strikes:** Utilize the power of artillery strikes to unleash devastating attacks on your foes. Choose your target wisely and rain destruction from above.

## Usage
- **Smoke Grenades:** Equip and throw a smoke grenade. Use them to obscure vision, create cover, or distract enemies, and mark a position for an artillery barrage.

## Build Instructions

To build and install the SSH MPT Artillery Strikes mod, follow these steps:

1. **Prepare References Folder:**
   - Create a folder named "References" inside the Artillery folder, alongside the .csproj file.

2. **Copy Necessary Files:**
   - Navigate to your MPT (My Private Tarkov) installation directory.
   - Go into the "EscapeFromTarkov_Data" folder.
   - Copy the "Managed" folder from the MPT installation directory into the "References" folder you created in step 1.
   - Similarly, copy the "BepInEx" folder from the MPT installation directory into the "References" folder.

3. **Build Mod:**
   - Open the solution file (.sln) for the Artillery Strikes mod in Visual Studio.
   - Build the project in Visual Studio. Ensure that the build output is successful.

4. **Install Mod:**
   - Locate the output of the build process, usually found in the "bin" or "bin/Debug" folder of your project directory.
   - Copy the built files from the output directory.
   - Paste the copied files into the "BepInEx/Plugins" directory within your MPT installation directory.

By following these steps, you should successfully build and install the SSH MPT Artillery Strikes mod for your MPT game. Enjoy the enhanced gameplay experience with artillery strikes!

## Notes
This mod serves as a practical example of utilizing the modding framework supplied in MPT. Explore the code and implementation to understand how to leverage these tools to create engaging and immersive experiences in MPT.
