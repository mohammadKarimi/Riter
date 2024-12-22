<!-- PROJECT SHIELDS -->
<!--
![image 138](https://github.com/mohammadKarimi/Riter/assets/5300102/9720e942-4853-4f7f-a426-f0f7a9fefeca)
*** I'm using markdown "reference style" links for readability.
*** Reference links are enclosed in brackets [ ] instead of parentheses ( ).
*** See the bottom of this document for the declaration of the reference variables
*** for contributors-url, forks-url, etc. This is an optional, concise syntax you may use.
*** https://www.markdownguide.org/basic-syntax/#reference-style-links
-->

<a name="readme-top"></a>


## 📐 Riter - Modern screen drawing

Riter is a modern, versatile screen drawing application built with WPF (Windows Presentation Foundation) that enables users to draw directly on their screens. Whether for visual communication, presentations, or tutorials, Riter offers a streamlined and intuitive interface, making it an ideal tool for professionals and educators alike.

[![Contributors][contributors-shield]][contributors-url]
[![Forks][forks-shield]][forks-url]
[![Stargazers][stars-shield]][stars-url]
[![Issues][issues-shield]][issues-url]
[![MIT License][license-shield]][license-url]
[![LinkedIn][linkedin-shield]][linkedin-url]
![dotnet-version]



![image](https://github.com/user-attachments/assets/cc2038fd-a2df-4eee-9a74-c5b4d0f09e64)

![image](https://github.com/user-attachments/assets/65429695-dc19-4576-b05a-4ca516dc7a49)

---

## Features

### 🖌️ Drawing Tools
- **Ink Canvas**: Draw freely on your screen using the InkCanvas control.
- **Modes**: Switch between drawing, erasing, and highlighting modes.
- **Brush Size Selection**: Choose brush sizes (1x, 2x, 3x) for different stroke thicknesses.

### 🎨 Color Management
- **Color Palette**: Select from a pre-configured set of colors.
- **Custom Color Selection**: Choose personalized colors for added flexibility.

### 🧹 Line Management
- **Undo/Redo**: Reverse or reapply recent drawing actions.
- **Clear All**: Quickly remove all strokes from the canvas.
- **Fading Ink**: With this new Ink you can draw an Ink which removed automatically.

### 🖥️ Display Modes
- **Hide All**: Temporarily hide drawings.
- **Background Modes**: Choose between transparent, whiteboard, or blackboard backgrounds.

### 🔧 Toolbar and User Interface
- **Floating Toolbar**: Easy access to tools and settings with a stylish, floating toolbox.
- **Visual Feedback**: Hover and selection effects enhance user interaction.

### ⚙️ Settings and Customization
- **Settings Panel**: Customize colors, brush sizes, application behavior, and more.
- **Global Hotkeys**: Access tools quickly with keyboard shortcuts, even when the app is out of focus.
- **Startup Location**: You can set your fav Start up location (center, BottomCenter, BottomLeft, BottomRight).
- **AppSettings.json**: Now you can change the InkDefaultColor and BrushSize manually.
- 
### 🚀 Performance
- **Efficient Stroke Handling**: Optimized stroke management ensures smooth performance during extended usage.

---

## Hotkeys

### Drawing & Erasing
- **D** - Switch to Drawing mode
- **E** - Switch to Erasing mode
- **H** - Highlighter
- **R** - Release tool
- **CTRL + F** - Enable Fade Ink
- **Shift(On Drawing Mode)** - Draw a line

### Stroke History
- **Z** - Undo
- **X** - Redo

### View Modes
- **Ctrl + Shift + H** - Hide all drawings
- **Ctrl + T** - Clear canvas
- **Ctrl + T** - Transparent board
- **Ctrl + B** - Blackboard
- **Ctrl + W** - Whiteboard

### Brush Size
- **Ctrl + 1** - 0.7x Brush size
- **Ctrl + 2** - 1x Brush size
- **Ctrl + 3** - 2x Brush size
- **Ctrl + 4** - 3x Brush size

### Color Shortcuts
- **1** - Yellow
- **2** - Purple
- **3** - Mint
- **4** - Coral
- **5** - Red
- **6** - Cyan
- **7** - Pink
- **8** - Gray
- **9** - Black
- **0** - Rainbow Color (Shapes Only)

### Draw Shape
- **Ctrl + L** -> Line
- **Ctrl + A** -> Arrow
- **Ctrl + R** -> Rectangle
- **Ctrl + C** -> Circle
- **Ctrl + D** -> Database

### Move Shape
- **Press ALT** -> with press alt you can move objects.

### Screenshot
- **Ctrl + Shift + P** -> Take a screen shot and save it is appDirectory/Screenshots/
---

## Getting Started

### Requirements
- **OS**: Windows
- **Framework**: .NET 8 (x86/x64)

### Installation

Follow these steps to set up and use Riter:

1. Clone the repository: 
   ```bash
   git clone https://github.com/username/Riter.git
   ```
2. Navigate to the project directory:
   ```bash
   cd Riter
   ```
3. Build and run the project in Visual Studio or via .NET CLI:
   ```bash
   dotnet run
   ```

---

## License
This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

--- 

Feel free to contribute, report issues, or fork the repository to add your own features. Happy drawing!

<!-- MARKDOWN LINKS & IMAGES -->
<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links -->
[contributors-url]: https://github.com/mohammadKarimi/Riter/graphs/contributors
[stars-url]: https://github.com/mohammadKarimi/Riter/stargazers
[forks-url]: https://github.com/mohammadKarimi/Riter/network/members
[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=for-the-badge&logo=linkedin&colorB=555
[linkedin-url]: https://www.linkedin.com/in/mha-karimi/
[contributors-shield]: https://img.shields.io/github/contributors/mohammadKarimi/Riter.svg?style=for-the-badge
[forks-shield]: https://img.shields.io/github/forks/mohammadKarimi/Riter.svg?style=for-the-badge
[stars-shield]: https://img.shields.io/github/stars/mohammadKarimi/Riter.svg?style=for-the-badge
[issues-shield]: https://img.shields.io/github/issues/mohammadKarimi/Riter.svg?style=for-the-badge
[issues-url]: https://github.com/mohammadKarimi/Riter/issues
[license-shield]: https://img.shields.io/github/license/mohammadKarimi/Riter.svg?style=for-the-badge
[license-url]: https://github.com/mohammadKarimi/Riter/blob/main/LICENSE.txt
[dotnet-version]: https://img.shields.io/badge/dotnet%20version-net8.0-blue
