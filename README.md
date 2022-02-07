# FlexibleUI
Unity UI skinning system using Scriptable Objects.
Project is using Unity 2020.3.25 but scripts should be useable in older versions of Unity (oldest version checked 2017).

## How to use
1. In the Assets/Scripts/FlexibleUI folder there are FlexibleUI components that can be attached to game objects with corresponding UI component (i.e. Attach FlexibleUI Image to game objects with an Image component).
2. Once attached, the 'FlexibleUI' component will need to point to a 'FlexibleUI Data' scriptable object. You can create them by right-clicking in the Project folder and selecting them under the Create > FlexibleUI Data > section. For a 'FlexibleUI Image', you'll need to select an 'Image Data'.
3. In the 'Image Data', define the properties for an Image (Image Sprite, Color, Image Material, etc.).
4. Then assign that Data to FlexibleUI component.
5. Now any Image that points to that Data will have it's settings updated by the Scriptable Object Data.

* See Sample Scene in the project for examples of Image, Button and Text Mesh Pro (TMP) Data being used.
