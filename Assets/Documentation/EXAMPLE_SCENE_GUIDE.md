<!-- markdown -->
# Bink Video Example Scene Guide

This guide explains how to use the included example scenes and scripts to understand Bink video integration with the ORK Framework.

## 📋 Overview

The example system includes:
- **Example Scene**: Complete scene with playback controls
- **Scene Controller**: UI interaction and playback management
- **ORK Cutscene Example**: Integration with ORK Framework
- **Visual Scripting Example**: Helper methods for visual scripts

## 🎬 Example Scene Setup

### Opening the Scene
1. Open `Assets/Scenes/BinkVideoExample.unity`
2. Make sure you have video files in `Assets/Videos/`
3. Hit Play to start

### Scene Hierarchy

```
BinkVideoPlayer (Main GameObject)
├── BinkVideo1Player (Component)
├── BinkVideo2Player (Component)
├── BinkVideoORKIntegration (Component)
├── ExampleSceneController (Script)
├── VideoCanvas (UI)
│   └── VideoDisplay (RawImage)
└── Directional Light
```

### Components

#### BinkVideo1Player
- **Video Type**: Video 1 (legacy .bik format)
- **Video Path**: Path to your .bik file
- **Render Texture**: Target texture for rendering

#### BinkVideo2Player
- **Video Type**: Video 2 (.bk2 format)
- **Video Path**: Path to your .bk2 file
- **More features**: Alpha, looping, seeking

#### BinkVideoORKIntegration
- **Pause Gameplay**: Auto-pause ORK during playback
- **Show Skip Prompt**: Display skip indicator
- **Skip Delay**: Seconds before skip is allowed

## 🎮 Controls

### Keyboard Shortcuts
| Key | Action |
|-----|--------|
| **Space** | Play/Pause toggle |
| **ESC** | Stop video |
| **Tab** | Switch between Video 1 & 2 |

### UI Buttons
- **Play**: Start video playback
- **Pause**: Pause video
- **Resume**: Resume paused video
- **Stop**: Stop and close video
- **Switch Format**: Toggle between Video 1 and 2

### GUI Buttons
Quick access buttons in-game for testing:
- Play (Space)
- Pause
- Resume
- Stop (ESC)
- Switch Format (Tab)

## 📝 ExampleSceneController Script

This script manages basic video playback control.

### Key Methods

```csharp
// Play current video format
PlayVideo();

// Pause playback
PauseVideo();

// Resume paused video
ResumeVideo();

// Stop and clean up
StopVideo();

// Switch between Video 1 and 2
SwitchVideoFormat();
```

### Progress Tracking

The scene includes a progress slider that shows:
- Current playback time
- Total video duration
- Real-time position tracking

## 🎭 ORK Cutscene Example

### Playing Cutscenes

```csharp
// Opening cutscene
PlayOpeningCutscene();

// Boss introduction
PlayBossCutscene();

// Background loop (doesn't pause gameplay)
PlayBackgroundLoop();
```

### Cutscene Configuration

```csharp
var videoEvent = new BinkVideoORKIntegration.BinkVideoEvent
{
    eventName = "MyCutscene",
    videoPath = "Assets/Videos/cutscene.bk2",
    videoType = BinkVideoORKIntegration.VideoType.Video2,
    pauseGameplay = true,        // Pause ORK
    showSkipPrompt = true,       // Show skip indicator
    skipDelay = 2f               // Can skip after 2 seconds
};

orkIntegration.PlayVideoEvent(videoEvent);
```

### Event Callbacks

Subscribe to cutscene events:

```csharp
orkIntegration.OnCutsceneStart += (eventName) => 
{
    Debug.Log($"Cutscene started: {eventName}");
};

orkIntegration.OnCutsceneComplete += (eventName) => 
{
    Debug.Log($"Cutscene completed: {eventName}");
};

orkIntegration.OnCutsceneSkipped += (eventName) => 
{
    Debug.Log($"Cutscene skipped: {eventName}");
};
```

## 🔗 Visual Scripting Integration

### Using in Visual Scripts

The `VisualScriptingExample` class provides helper methods:

```csharp
// Get video path by name
GetVideoPath("scene1");        // Returns video1Path

// Handle completion
OnVideoComplete("scene1");

// Handle skip
OnVideoSkipped("scene1");

// Control all videos
PauseAllVideos();
ResumeAllVideos();
StopAllVideos();

// Play sequence
PlaySequence(new[] { "path1", "path2", "path3" });
```

### Visual Script Graph Example

```
[Trigger Event]
    ↓
[Play Bink Video]
    ↓
[Wait for Event: On Video Complete]
    ↓
[Resume Gameplay]
    ↓
[Start Dialogue]
```

### Node Categories

All nodes appear under:
- `Bink/Play` - Playback control
- `Bink/Query` - Video information
- `Bink/Events` - Event responses

## 📁 File Structure

```
Assets/
├── Scenes/
│   └── BinkVideoExample.unity
├── Scripts/
│   ├── Examples/
│   │   ├── ExampleSceneController.cs
│   │   ├── ORKCutsceneExample.cs
│   │   └── VisualScriptingExample.cs
│   ├── BinkVideo1/
│   ├── BinkVideo2/
│   ├── Common/
│   └── ORK/
├── Videos/
│   ├── example.bik           (Video 1 format)
│   └── example.bk2           (Video 2 format)
└── Documentation/
    └── EXAMPLE_SCENE_GUIDE.md
```

## 🎬 Common Workflows

### Workflow 1: Play Introduction Video

```csharp
// On game start
exampleController.PlayVideo();

// Wait for completion
orkIntegration.OnCutsceneComplete += (name) => 
{
    // Start main menu
    StartMainMenu();
};
```

### Workflow 2: Cutscene with Dialogue

```csharp
var cutscene = new BinkVideoORKIntegration.BinkVideoEvent
{
    eventName = "IntroDialogue",
    videoPath = "Assets/Videos/intro.bk2",
    videoType = BinkVideoORKIntegration.VideoType.Video2,
    pauseGameplay = true,
    showSkipPrompt = true,
    skipDelay = 3f
};

orkIntegration.PlayVideoEvent(cutscene);

orkIntegration.OnCutsceneComplete += (name) =>
{
    // Start dialogue system
    dialogueManager.StartDialogue("intro_dialogue");
};
```

### Workflow 3: Multiple Videos in Sequence

```csharp
string[] sequence = new[]
{
    "Assets/Videos/intro.bk2",
    "Assets/Videos/exposition.bk2",
    "Assets/Videos/climax.bk2"
};

vsHelper.PlaySequence(sequence);
```

## 🔧 Troubleshooting

### Video Not Playing
- Check file path in inspector
- Verify file format (.bik for Video 1, .bk2 for Video 2)
- Ensure file exists at specified path
- Check console for error messages

### No Audio
- Audio must be handled separately
- Use Audio Source component alongside video
- See audio documentation

### Choppy Playback
- Reduce video resolution
- Check CPU usage
- Adjust Bink2 flags for performance

### Skip Not Working
- Check `skipDelay` is set > 0
- Verify `showSkipPrompt` is enabled
- Check skip timeout hasn't expired

## 📊 Performance Tips

1. **Texture Size**: Match video resolution to display size
2. **Bitrate**: Use appropriate bitrate for target platform
3. **Threading**: Bink can use async loading with BINK2_FILEIO flag
4. **Memory**: Close videos when done to free resources

## 🎓 Next Steps

1. **Customize**: Modify example scripts for your game
2. **Add Audio**: Integrate with audio system
3. **Create Assets**: Encode your videos to Bink format
4. **Expand**: Create more cutscene variations
5. **Optimize**: Profile and optimize for target platform

## 📚 Related Documentation

- [ORK Integration Guide](ORK_INTEGRATION_GUIDE.md)
- [Bink Video 2 Guide](BINK_VIDEO_2_GUIDE.md)
- [Visual Scripting Guide](VISUAL_SCRIPTING_GUIDE.md)
- [Main README](../README.md)

---

**Happy video scripting!** 🎬
