<!-- markdown -->
# Creating Sample Bink Videos

This guide explains how to create Bink Video 1 (.bik) and Bink Video 2 (.bk2) files for testing.

## 📦 Requirements

### Windows
- **Bink Video Tools** (from RAD Game Tools)
  - Available with Bink license
  - Includes encoder and player

### Alternative: FFmpeg + Online Tools
- **FFmpeg**: Video conversion tool
- **Online Bink Encoder**: Web-based alternatives

## 🎬 Creating Test Videos

### Quick Test: Using Built-in Video

For quick testing without encoding:

1. **Create a simple video**
   ```bash
   ffmpeg -f lavfi -i color=c=black:s=1280x720:d=5 -f lavfi -i sine=frequency=440:duration=5 test.mp4
   ```
   Creates 5-second black video with audio

2. **Or use a royalty-free video**
   - Download from Pexels, Pixabay, or Unsplash
   - Format as MP4 or similar

### Encoding to Bink Video 1 (.bik)

Using RAD Bink Video Tools:

```bash
binkconv.exe input.mp4 output.bik
```

**Options:**
```bash
binkconv.exe input.mp4 output.bik -b 1500k -q 90
```

- `-b`: Bitrate (e.g., 1500k for 1500 kbps)
- `-q`: Quality 0-100

### Encoding to Bink Video 2 (.bk2)

```bash
bink2conv.exe input.mp4 output.bk2
```

**Advanced options:**
```bash
bink2conv.exe input.mp4 output.bk2 -b 1000k -q 85 -alpha
```

- `-alpha`: Include alpha channel for transparency

## 📝 FFmpeg Method (No Bink Tools)

### Convert Video Format
```bash
# MP4 to AVI (intermediate format)
ffmpeg -i input.mp4 -codec:v mpeg4 -q:v 5 -codec:a libmp3lame -q:a 4 output.avi

# Then use online encoder or Bink tools on the AVI
```

### Create Test Video
```bash
# Black screen (5 seconds)
ffmpeg -f lavfi -i color=c=black:s=1280x720:d=5 -f lavfi -i sine=1000:d=5 output.mp4

# Blue gradient (10 seconds)
ffmpeg -f lavfi -i color=c=blue:s=1280x720:d=10 output.mp4

# With fade effect
ffmpeg -i input.mp4 -vf fade=t=in:st=0:d=1,fade=t=out:st=9:d=1 output.mp4
```

## 🎨 Creating Sample Videos for Examples

### Video 1: Opening Cutscene (3-5 seconds)
```bash
# Create black intro fade to logo
ffmpeg -f lavfi -i color=c=black:s=1920x1080:d=3 \
  -f lavfi -i sine=1000:d=3 \
  -pix_fmt yuv420p output.mp4

# Encode to Bink Video 2
bink2conv.exe output.mp4 opening.bk2 -b 2000k
```

### Video 2: Boss Introduction (2-3 seconds)
```bash
# Create red dramatic intro
ffmpeg -f lavfi -i color=c=red:s=1920x1080:d=2 \
  -f lavfi -i sine=800:d=2 \
  output.mp4

bink2conv.exe output.mp4 boss_intro.bk2 -b 1500k
```

### Video 3: Menu Loop (10 seconds looping)
```bash
# Create blue looping background
ffmpeg -f lavfi -i color=c=blue:s=1280x720:d=10 \
  -f lavfi -i sine=200:d=10 \
  output.mp4

bink2conv.exe output.mp4 menu_loop.bk2 -b 1000k
```

## 📂 Organization

Create a folder structure:

```
Assets/
└── Videos/
    ├── Cutscenes/
    │   ├── opening.bk2          (5 sec, Video 2)
    │   ├── boss_intro.bk2       (3 sec, Video 2)
    │   └── ending.bk2           (10 sec, Video 2)
    ├── Legacy/
    │   ├── example.bik          (Video 1 format)
    │   └── intro.bik
    ├── Menus/
    │   └── menu_loop.bk2        (Looping background)
    └── Raw/
        ├── video1.mp4           (Source files)
        └── video2.mp4
```

## ✅ Recommended Settings

### High Quality (Cutscenes)
```bash
bink2conv.exe input.mp4 output.bk2 -b 3000k -q 95
```
- **Bitrate**: 3000 kbps
- **Quality**: 95
- **Use case**: Cinematics, plot-critical scenes

### Medium Quality (In-game Videos)
```bash
bink2conv.exe input.mp4 output.bk2 -b 1500k -q 85
```
- **Bitrate**: 1500 kbps
- **Quality**: 85
- **Use case**: Boss intros, exposition

### Low Bandwidth (Web/Streaming)
```bash
bink2conv.exe input.mp4 output.bk2 -b 800k -q 75
```
- **Bitrate**: 800 kbps
- **Quality**: 75
- **Use case**: Menus, background loops

## 🎯 Resolution Guidelines

| Resolution | Bitrate | Quality | Use Case |
|-----------|---------|---------|----------|
| 640x480   | 500k    | 70      | Mobile   |
| 1280x720  | 1000k   | 80      | Web      |
| 1920x1080 | 2000k   | 85      | PC/Console |
| 3840x2160 | 5000k   | 90      | 4K       |

## 🔧 Batch Encoding Script

### Windows Batch File (convert_all.bat)

```batch
@echo off
REM Convert all MP4 files to Bink Video 2

setlocal enabledelayedexpansion

for %%f in (*.mp4) do (
    echo Converting %%f...
    bink2conv.exe "%%f" "%%~nf.bk2" -b 1500k -q 85
)

echo All videos converted!
pause
```

### Bash Script (Linux/Mac)

```bash
#!/bin/bash
# Convert all MP4 files to Bink Video 2

for file in *.mp4; do
    output="${file%.mp4}.bk2"
    echo "Converting $file to $output..."
    bink2conv.exe "$file" "$output" -b 1500k -q 85
done

echo "All videos converted!"
```

## 🎬 Test Videos to Create

For the example scene, create these videos:

### 1. example.bik (Video 1 Test)
```bash
ffmpeg -f lavfi -i color=c=gray:s=1280x720:d=3 -f lavfi -i sine:d=3 test.mp4
binkconv.exe test.mp4 example.bik -b 1000k
```

### 2. example.bk2 (Video 2 Test)
```bash
ffmpeg -f lavfi -i color=c=cyan:s=1280x720:d=3 -f lavfi -i sine:d=3 test.mp4
bink2conv.exe test.mp4 example.bk2 -b 1000k
```

### 3. opening.bk2 (Cutscene)
```bash
ffmpeg -f lavfi -i color=c=black:s=1920x1080:d=5 -f lavfi -i sine:d=5 test.mp4
bink2conv.exe test.mp4 opening.bk2 -b 2000k -q 90
```

## 📊 File Size Reference

| Format | Bitrate | Duration | File Size |
|--------|---------|----------|-----------|
| Video 1 | 1000k   | 5 sec    | ~625 KB   |
| Video 2 | 1000k   | 5 sec    | ~625 KB   |
| Video 2 | 2000k   | 10 sec   | ~2.5 MB   |

## 🚀 Next Steps

1. **Encode your videos** using above settings
2. **Place in Assets/Videos/** folder
3. **Update paths** in Example scripts
4. **Test in Unity** with BinkVideoExample scene
5. **Optimize** based on performance

## 📚 Resources

- **RAD Game Tools**: https://www.radgametools.com/bink.htm
- **FFmpeg**: https://ffmpeg.org/
- **Bink Video Format**: https://www.radgametools.com/bink2support.htm
- **Online Converters**: Search for "Bink Video Encoder Online"

---

**Ready to encode!** 🎬
