#! /bin/sh

# Example install script for Unity3D project. See the entire example: https://github.com/JonathanPorta/ci-build

# This link changes from time to time. I haven't found a reliable hosted installer package for doing regular
# installs like this. You will probably need to grab a current link from: http://unity3d.com/get-unity/download/archive
echo 'Downloading from https://download.unity3d.com/download_unity/cc85bf6a8a04/MacEditorInstaller/Unity-2017.1.2f1.pkg?_ga=2.157252923.1270817159.1511310980-1180231831.1510707011: '
curl -o Unity.pkg 'https://download.unity3d.com/download_unity/cc85bf6a8a04/MacEditorInstaller/Unity-2017.1.2f1.pkg?_ga=2.157252923.1270817159.1511310980-1180231831.1510707011'

echo 'Installing Unity.pkg'
sudo installer -dumplog -package Unity.pkg -target /
